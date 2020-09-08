using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;

namespace Discord_Bot.Commands
{
    internal class TestCommands : BaseCommandModule
    {
        char[] Vowels = { 'a', 'o', 'u', 'å', 'e', 'i', 'å', 'ä', 'ö' };


        [Command("Hello")]
        internal async Task Hello(CommandContext commandContext)
        {
            await commandContext.Channel.SendMessageAsync("World").ConfigureAwait(false);
        }

        [Command("Rövarspråket")]
        internal async Task Response(CommandContext commandContext)
        {
            var interactivty = commandContext.Client.GetInteractivity();
            var message = await interactivty.WaitForMessageAsync(x => x.Channel == commandContext.Channel).ConfigureAwait(false);

            await commandContext.Channel.SendMessageAsync(Translate(message.Result.Content));
        }

        [Command("Source")]
        internal async Task Source(CommandContext commandContext)
        {
            await commandContext.Channel.SendMessageAsync("https://github.com/JoakimCarlsson/DiscordBot").ConfigureAwait(false);
        }

        private string Translate(string text)
        {
            var builder = new StringBuilder();
            foreach (char ch in text)
            {
                if (IsConsonant(ch))
                {
                    builder.Append(ch);
                    builder.Append('o');
                    builder.Append(ch);
                }
                else builder.Append(ch);

            }
            return builder.ToString();
        }

        private bool IsVowel(char ch)
        {
            char low = char.ToLower(ch);
            return Vowels.Contains(low);
        }

        private bool IsConsonant(char character)
        {
            if (char.IsLetter(character) && !IsVowel(character)) return true;
            return false;
        }
    }
}
