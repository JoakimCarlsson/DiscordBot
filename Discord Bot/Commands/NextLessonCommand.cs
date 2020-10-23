using System.IO;
using System.Net;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;

namespace DiscordBot.Commands
{
    class NextLessonCommand : BaseCommandModule
    {
        [Command("Nästa Lektion")]
        internal async Task GetClasses(CommandContext commandContext)
        {
            //string callUrl = "https://cloud.timeedit.net/nackademin/web/1/ri10560y6Z6855Q067QQY050Z567018Q561W95.asmx";
            string callUrl = "https://cloud.timeedit.net/nackademin/web/1/ri105607g76857Q0g9QY6160Z76YX1Q5Q68Y550yZ5.asmx"; //https://cloud.timeedit.net/nackademin/web/1/ri10Z607g76857Q0g9QY61Z0ZQ6YX1Q5b6cY550yZ55Q67x6xbQ8aW5ZbnlQc.asmx
            string url = $"http://ical-to-json.herokuapp.com/convert.json?url={callUrl}";

            HttpWebRequest apiRequest = WebRequest.Create(url) as HttpWebRequest;
            string apiResponse;
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }

            EventHelper lesson = JsonConvert.DeserializeObject<EventHelper>(apiResponse);

            foreach (Calendar calendar in lesson.Calendar)
            {
                for (int i = 0; i < 2; i++)
                {
                    await commandContext.Channel.SendMessageAsync(calendar.Event[i].ToString()).ConfigureAwait(false);
                }
            }

        }
    }
}
