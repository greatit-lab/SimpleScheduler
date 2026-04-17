// Job.cs
using System;
using System.Xml.Serialization;

namespace SimpleScheduler
{
    public enum ScheduleType
    {
        Interval = 0,
        Daily = 1,
        Weekly = 2,
        Monthly = 3
    }

    public class Job
    {
        [XmlAttribute]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
        public string TargetPath { get; set; }
        public string Arguments { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime? EndTime { get; set; }
        public bool IsEnabled { get; set; } = true;

        // ▼ 추가된 스케줄 세부 옵션들
        public ScheduleType RunType { get; set; } = ScheduleType.Interval;
        public int IntervalMinutes { get; set; } = 1;
        public DateTime DailyRunTime { get; set; } = DateTime.Today;
        public DayOfWeek WeeklyRunDay { get; set; } = DayOfWeek.Monday;
        public int MonthlyRunDay { get; set; } = 1;

        [XmlIgnore]
        public DateTime NextRunTime { get; set; }
    }
}
