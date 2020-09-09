using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Discord_Bot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Discord_Bot
{
    internal class Bot
    {
        public DiscordClient DiscordClient { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync()
        {
            string json = string.Empty;

            using (FileStream fileStream = File.OpenRead(""))
            {
                using (StreamReader streamReader = new StreamReader(fileStream, new UTF8Encoding(false)))
                {
                    json = await streamReader.ReadToEndAsync().ConfigureAwait(false);
                }
            }

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            DiscordConfiguration configuration = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
                UseRelativeRatelimit = true,
            };

            DiscordClient = new DiscordClient(configuration);
            DiscordClient.Ready += OnClientReady;

            DiscordClient.UseInteractivity(new InteractivityConfiguration());

            CommandsNextConfiguration commandsConfiguration = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] {configJson.CommandPrefix},
                EnableDms = true,
                EnableMentionPrefix = true,

            };

            Commands = DiscordClient.UseCommandsNext(commandsConfiguration);

            Commands.RegisterCommands<TestCommands>();
            Commands.RegisterCommands<Weather>();

            await DiscordClient.ConnectAsync();

            await Task.Delay(-1);
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
