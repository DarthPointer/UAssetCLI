using System.Collections.Generic;

namespace UAssetCLI
{
    class Report
    {
        static readonly Dictionary<ReportLevel, string> messagePrefixes =
            new Dictionary<ReportLevel, string>()
            {
                {ReportLevel.Notification, "NOT: " },
                {ReportLevel.Warning, "WRN: " },
                {ReportLevel.Error, "ERR: " }
            };

        string message;

        public enum ReportLevel
        {
            Notification,
            Warning,
            Error
        }

        public override string ToString()
        {
            return message;
        }

        public Report(string customMessage, ReportLevel reportLevel)
        {
            message = messagePrefixes[reportLevel] + customMessage;
        }

        public static Report Notification(string message)
        {
            return new Report(message, ReportLevel.Notification);
        }

        public static Report Warning(string message)
        {
            return new Report(message, ReportLevel.Warning);
        }

        public static Report Error(string message)
        {
            return new Report(message, ReportLevel.Error);
        }
    }
}
