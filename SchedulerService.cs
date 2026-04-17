// SchedulerService.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

namespace SimpleScheduler
{
    public class SchedulerService
    {
        private System.Threading.Timer _timer;
        private List<Job> _jobs = new List<Job>();
        private readonly string _configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jobs.xml");
        public event Action<string> Logged;

        public bool IsRunning { get; private set; } = false;

        public SchedulerService()
        {
            LoadJobs();
            CalculateNextRunTimes();
        }

        public List<Job> GetJobs() => _jobs.ToList();

        public void Start()
        {
            if (IsRunning) return;
            IsRunning = true;
            Log("스케줄러 서비스를 시작합니다.");
            CalculateNextRunTimes();
            _timer = new Timer(OnTimerTick, null, 1000 * 10, 1000 * 30);
        }

        public void Stop()
        {
            if (!IsRunning) return;
            IsRunning = false;
            Log("스케줄러 서비스를 중지합니다.");
            _timer?.Dispose();
            _timer = null;
        }

        private void OnTimerTick(object state)
        {
            var now = DateTime.Now;
            foreach (var job in _jobs.Where(j => j.IsEnabled).ToList())
            {
                if (now >= job.NextRunTime)
                {
                    if (job.EndTime.HasValue && now > job.EndTime.Value)
                    {
                        job.IsEnabled = false;
                        Log($"작업 '{job.Name}'의 실행 기간이 만료되어 비활성화합니다.");
                        continue;
                    }
                    ExecuteJob(job);

                    job.NextRunTime = GetNextRunTime(job, DateTime.Now);
                    Log($"작업 '{job.Name}'의 다음 실행 시간: {job.NextRunTime:yyyy-MM-dd HH:mm:ss}");
                }
            }
            SaveJobs();
        }

        private void ExecuteJob(Job job)
        {
            try
            {
                if (!File.Exists(job.TargetPath))
                {
                    Log($"오류: 실행 파일을 찾을 수 없습니다 - {job.TargetPath}");
                    return;
                }
                Log($"작업 실행: '{job.Name}' (대상: {job.TargetPath})");
                Process.Start(job.TargetPath, job.Arguments);
            }
            catch (Exception ex)
            {
                Log($"오류: 작업 '{job.Name}' 실행 중 예외 발생 - {ex.Message}");
            }
        }

        private DateTime GetNextRunTime(Job job, DateTime now, bool isImmediateUpdate = false)
        {
            DateTime nextRun;
            switch (job.RunType)
            {
                case ScheduleType.Daily:
                    nextRun = now.Date.Add(job.DailyRunTime.TimeOfDay);
                    if (nextRun <= now) nextRun = nextRun.AddDays(1);
                    break;
                case ScheduleType.Weekly:
                    nextRun = now.Date.Add(job.DailyRunTime.TimeOfDay);
                    int daysToAdd = ((int)job.WeeklyRunDay - (int)now.DayOfWeek + 7) % 7;
                    if (daysToAdd == 0 && nextRun <= now) daysToAdd = 7;
                    nextRun = nextRun.AddDays(daysToAdd);
                    break;
                case ScheduleType.Monthly:
                    int targetDay = Math.Min(job.MonthlyRunDay, DateTime.DaysInMonth(now.Year, now.Month));
                    nextRun = new DateTime(now.Year, now.Month, targetDay).Add(job.DailyRunTime.TimeOfDay);
                    if (nextRun <= now)
                    {
                        DateTime nextMonth = now.AddMonths(1);
                        targetDay = Math.Min(job.MonthlyRunDay, DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month));
                        nextRun = new DateTime(nextMonth.Year, nextMonth.Month, targetDay).Add(job.DailyRunTime.TimeOfDay);
                    }
                    break;
                case ScheduleType.Interval:
                default:
                    if (job.StartTime > now) return job.StartTime;
                    return isImmediateUpdate ? now : now.AddMinutes(job.IntervalMinutes);
            }

            if (job.StartTime > nextRun)
            {
                while (nextRun < job.StartTime)
                {
                    if (job.RunType == ScheduleType.Daily) nextRun = nextRun.AddDays(1);
                    else if (job.RunType == ScheduleType.Weekly) nextRun = nextRun.AddDays(7);
                    else if (job.RunType == ScheduleType.Monthly)
                    {
                        DateTime nextMonth = nextRun.AddMonths(1);
                        int targetDay = Math.Min(job.MonthlyRunDay, DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month));
                        nextRun = new DateTime(nextMonth.Year, nextMonth.Month, targetDay).Add(job.DailyRunTime.TimeOfDay);
                    }
                }
            }
            return nextRun;
        }

        private void CalculateNextRunTimes()
        {
            var now = DateTime.Now;
            foreach (var job in _jobs)
            {
                if (job.NextRunTime == DateTime.MinValue && job.IsEnabled)
                {
                    job.NextRunTime = GetNextRunTime(job, now);
                }
            }
        }

        public void AddOrUpdateJob(Job job)
        {
            var existingJob = _jobs.FirstOrDefault(j => j.Id == job.Id);
            if (existingJob != null)
            {
                _jobs.Remove(existingJob);
            }

            job.NextRunTime = GetNextRunTime(job, DateTime.Now, isImmediateUpdate: true);
            _jobs.Add(job);
            SaveJobs();
        }

        public void DeleteJob(Guid id)
        {
            var job = _jobs.FirstOrDefault(j => j.Id == id);
            if (job != null)
            {
                _jobs.Remove(job);
                SaveJobs();
            }
        }

        public void UpdateJobEnabledStatus(Guid jobId, bool isEnabled)
        {
            var job = _jobs.FirstOrDefault(j => j.Id == jobId);
            if (job != null && job.IsEnabled != isEnabled)
            {
                job.IsEnabled = isEnabled;
                if (job.IsEnabled)
                {
                    job.NextRunTime = GetNextRunTime(job, DateTime.Now);
                    Log($"작업 '{job.Name}'이(가) 활성화되었습니다.");
                }
                else
                {
                    Log($"작업 '{job.Name}'이(가) 비활성화되었습니다.");
                }
                SaveJobs();
            }
        }

        // ▼ [추가] 사용자 지정 경로로 전체 작업 목록 내보내기
        public void ExportJobs(string filePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<Job>));
                using (var writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, _jobs);
                }
                Log($"작업 목록을 내보냈습니다: {filePath}");
            }
            catch (Exception ex)
            {
                Log($"오류: 작업 목록 내보내기 실패 - {ex.Message}");
                throw;
            }
        }

        // ▼ [추가] 사용자 지정 경로에서 전체 작업 목록 불러오기 (덮어쓰기)
        public void ImportJobs(string filePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<Job>));
                using (var reader = new StreamReader(filePath))
                {
                    var importedJobs = (List<Job>)serializer.Deserialize(reader);
                    _jobs = importedJobs;
                }
                CalculateNextRunTimes();
                SaveJobs(); // 로드한 데이터를 기본 설정 파일(jobs.xml)에도 동기화
                Log($"작업 목록을 불러왔습니다: {filePath}");
            }
            catch (Exception ex)
            {
                Log($"오류: 작업 목록 불러오기 실패 - {ex.Message}");
                throw;
            }
        }

        private void SaveJobs()
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<Job>));
                using (var writer = new StreamWriter(_configFilePath))
                {
                    serializer.Serialize(writer, _jobs);
                }
            }
            catch (Exception ex)
            {
                Log($"오류: 작업 목록 저장 실패 - {ex.Message}");
            }
        }

        private void LoadJobs()
        {
            if (!File.Exists(_configFilePath))
            {
                _jobs = new List<Job>();
                return;
            }
            try
            {
                var serializer = new XmlSerializer(typeof(List<Job>));
                using (var reader = new StreamReader(_configFilePath))
                {
                    _jobs = (List<Job>)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Log($"오류: 작업 목록 불러오기 실패 - {ex.Message}");
                _jobs = new List<Job>();
            }
        }

        private void Log(string message)
        {
            Logged?.Invoke($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
        }
    }
}
