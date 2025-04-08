using System;

namespace DailyReportApp.Models
{
    public class AppSettings
    {
        public int Id { get; set; }
        public string ChatworkApiKey { get; set; }
        public string ChatworkRoomId { get; set; }
        public string ReportTemplate { get; set; }
        public bool AutoSendToChatwork { get; set; }
        public TimeSpan DailyReminderTime { get; set; }
    }
}
