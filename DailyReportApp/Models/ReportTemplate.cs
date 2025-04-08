using System;

namespace DailyReportApp.Models
{
    public class ReportTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string TasksContent { get; set; }
        public string ResultsContent { get; set; }
        public string TomorrowPlanContent { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UsageCount { get; set; }
    }
}
