using System;

namespace DailyReportApp.Models
{
    public class DailyReport
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string Tasks { get; set; }
        public string Results { get; set; }
        public string TomorrowPlan { get; set; }
        public string MonthlyReview { get; set; }
        public bool IsSentToChatwork { get; set; }
        public string SentToChatworkAt { get; set; }
    }
}
