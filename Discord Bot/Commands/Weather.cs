using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;

namespace Discord_Bot.Commands
{
    class Weather : BaseCommandModule
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
                string apiKey = "";
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
            catch (WebException ex)
            {
                return null;
            }
        }
    }
}
