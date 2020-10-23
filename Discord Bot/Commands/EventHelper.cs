using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;

namespace DiscordBot.Commands
{
    public class Event
    {
        [JsonProperty("dtstart")] private string _startDate;
        [JsonProperty("dtend")] private string _endDate;
        [JsonProperty("last-modified")] private string _lastModified;
        [JsonProperty("summary")] private string _summary;
        [JsonProperty("description")] private string _description;

        public DateTime? StartDate => ParseDate(_startDate);
        public DateTime? EndDate => ParseDate(_endDate);
        public DateTime? LastModified => ParseDate(_lastModified);

        public string Teacher => ParseTeacher(_summary);
        public string Course => ParseCourse(_summary);

        private string ParseCourse(string summary)
        {
            string[] inputs = summary.Split(',');
            string tmpString = inputs[1].Replace("\\", "").Remove(0, 1);
            return tmpString;
        }

        private string ParseTeacher(string summary)
        {
            string[] inputs = summary.Split(',');
            string tmpString = inputs[1].Replace("\\", "").Remove(0, 1);
            return tmpString;
        }

        [JsonProperty("location")]
        public string Location { get; private set; }

        private DateTime? ParseDate(string value)
        {
            if (DateTime.TryParseExact(value.ToLower().Replace("t", " ").Replace("z", ""), "yyyyMMdd HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime date))
                return date;

            return null;
        }

        public override string ToString()
        {
            return $"Nästa lektion är {StartDate:f} till {EndDate:t} med Lärare: {Teacher} i sal: {Location}";
        }
    }

    public class Calendar
    {
        [JsonProperty("vevent")]
        public List<Event> Event { get; set; }
    }

    public class EventHelper
    {
        [JsonProperty("vcalendar")]
        public List<Calendar> Calendar { get; set; }
    }
}
