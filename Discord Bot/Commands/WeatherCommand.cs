using System.IO;
using System.Net;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;

namespace DiscordBot.Commands
{
    class WeatherCommand : BaseCommandModule
    {
        [Command("Väder")]
        internal async Task GetWeatherData(CommandContext commandContext, string city)
        {
            WeatherHelper.WeatherData weatherData = GetWeather(city);
            string outPut = weatherData != null ? $"Det är {weatherData.Main.Temp}C i {city}, men det känns som {weatherData.Main.FeelsLike}C" : $"Nu vart det något fel, är {city} verkligen en plats?";
            await commandContext.Channel.SendMessageAsync(outPut).ConfigureAwait(false);
        }

        private WeatherHelper.WeatherData GetWeather(string city)
        {
            try
            {
                string apiKey = "ec0d1a9998b7d02596189ce825c86cdb";
                HttpWebRequest apiRequest = WebRequest.Create($"http://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;

                string apiResponse = "";
                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponse = reader.ReadToEnd();
                }

                WeatherHelper.WeatherData weather = JsonConvert.DeserializeObject<WeatherHelper.WeatherData>(apiResponse);
                return weather;
            }
            catch
            {
                return null;
            }
        }
    }
}
