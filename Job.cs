using System;
using System.Xml.Serialization;

namespace SimpleScheduler
{
    public class Job
    {
        [XmlAttribute]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
        public string TargetPath { get; set; }
        public string Arguments { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime? EndTime { get; set; }
        public int IntervalMinutes { get; set; } = 1;
        public bool IsEnabled { get; set; } = true;

        [XmlIgnore]
        public DateTime NextRunTime { get; set; }
    }
}
