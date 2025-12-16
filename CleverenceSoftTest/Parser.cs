using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CleverenceSoftTest
{
    public class Parser
    {
        private string baseDirectory;
        private string filePathLog;
        private string filePathLogProblems;
        private List<Regex> formatList;

        public Parser()
        {
            baseDirectory = AppContext.BaseDirectory;
            filePathLog = Path.Combine(baseDirectory, "log.txt");
            filePathLogProblems = Path.Combine(baseDirectory, "problems.txt");
            formatList = new List<Regex>();
            formatList.Add(new Regex(@"^(?<date1>\d{2}\.\d{2}\.\d{4}) (?<time>\d{2}:\d{2}:\d{2}\.\d{3}) (?<level>\w+)\s+(?<message>.+)$"));
            formatList.Add(new Regex(@"^(?<date2>\d{4}-\d{2}-\d{2}) (?<time>\d{2}:\d{2}:\d{2}\.\d{4})\|\s+(?<level>\w+)\|(?<thread>\d+)\|(?<method>[^\|]+)\| (?<message>.+)$"));

        }
        public void AddLog(string log)
        {
            LogEntry val = ParseLine(log);
            if (val == null || val.Level == null)
                using (StreamWriter wr = new StreamWriter(filePathLogProblems, true))
                {
                    wr.WriteLine(log);
                }
            else
                using (StreamWriter wr = new StreamWriter(filePathLog, true))
                {
                    wr.Write(val.Date + " ");
                    wr.Write(val.Time + " ");
                    wr.Write(val.Level + " ");
                    wr.Write(val.Method + " ");
                    wr.Write(val.Message + " ");
                    wr.WriteLine();
                    wr.WriteLine();
                }
        }
        private LogEntry ParseLine(string log)
        {
            LogEntry res;
            foreach (var item in formatList)
            {
                res = ParserOneFormat(item, log);
                if (res != null)
                    return res;
            }
            return null;
        }
        private LogEntry ParserOneFormat(Regex format, string log)
        {
            Match match = format.Match(log);
            if (match.Success)
            {

                return new LogEntry
                {
                    Date = ConvertData(match),
                    Time = ConvertTime(match),
                    Level = ConvertLevel(match),
                    Method = ConvertMethod(match),
                    Message = ConvertMessage(match)
                };
            }
            return null;
        }

        private string ConvertData(Match match)
        {
            string res = "";
            if (match.Groups["date1"].Success)
            {
                string val = match.Groups["date1"].Value;
                res = val.Substring(6, 4) + "-" + val.Substring(3, 2) + "-" + val.Substring(0, 2);
            }
            else
            {
                res = match.Groups["date2"].Value;
            }
            return res;
        }
        private string ConvertTime(Match match)
        {
            return match.Groups["time"].Value;
        }
        private string ConvertLevel(Match match)
        {
            switch (match.Groups["level"].Value)
            {
                case "INFORMATION":
                    return "INFO";
                case "WARNING":
                    return "WARN";
                case "INFO":
                    return match.Groups["level"].Value;
                case "WARN":
                    return match.Groups["level"].Value;
                case "ERROR":
                    return match.Groups["level"].Value;
                case "DEBUG":
                    return match.Groups["level"].Value;
                default:
                    return null;
            }
            
        }
        private string ConvertMethod(Match match)
        {

            if (match.Groups["method"].Value != "")
                return match.Groups["method"].Value;
            else
                return "DEFAULT";
        }
        private string ConvertMessage(Match match)
        {
            return match.Groups["message"].Value;
        }
        public string GetLog()
        {
            try
            {
                using (StreamReader rd = new StreamReader(filePathLog))
                {
                    return rd.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return "";
            }

        }
        public string GetProblems()
        {
            try
            {
                using (StreamReader rd = new StreamReader(filePathLogProblems))
                {
                    return rd.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
    }

}
