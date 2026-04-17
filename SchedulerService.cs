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
                    job.NextRunTime = DateTime.Now.AddMinutes(job.IntervalMinutes);
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

        private void CalculateNextRunTimes()
        {
            var now = DateTime.Now;
            foreach (var job in _jobs)
            {
                if (job.NextRunTime == DateTime.MinValue && job.IsEnabled)
                {
                    if (job.StartTime > now)
                    {
                        job.NextRunTime = job.StartTime;
                    }
                    else
                    {
                        job.NextRunTime = now.AddMinutes(job.IntervalMinutes);
                    }
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
            var now = DateTime.Now;
            if (job.StartTime > now)
            {
                job.NextRunTime = job.StartTime;
            }
            else
            {
                job.NextRunTime = now;
            }
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

        // IsEnabled 상태를 업데이트하는 메서드는 그대로 유지
        public void UpdateJobEnabledStatus(Guid jobId, bool isEnabled)
        {
            var job = _jobs.FirstOrDefault(j => j.Id == jobId);
            if (job != null && job.IsEnabled != isEnabled)
            {
                job.IsEnabled = isEnabled;
                if (job.IsEnabled)
                {
                    job.NextRunTime = DateTime.Now.AddMinutes(job.IntervalMinutes);
                    Log($"작업 '{job.Name}'이(가) 활성화되었습니다.");
                }
                else
                {
                    Log($"작업 '{job.Name}'이(가) 비활성화되었습니다.");
                }
                SaveJobs();
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
