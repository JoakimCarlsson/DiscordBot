using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;

namespace Discord_Bot.Commands
{
    internal class FunCommands : BaseCommandModule
    {
        [Command("Hello")]
        internal async Task Hello(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("World").ConfigureAwait(false);
        }

        //[Command("Rövarspråket")]
        //internal async Task Response(CommandContext ctx)
        //{
        //    var interactivty = ctx.Client.GetInteractivity();
        //    var message = await interactivty.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

        //    await ctx.Channel.SendMessageAsync(message.Result.Content);
        //}

        [Command("Source")]
        internal async Task Source(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("https://github.com/JoakimCarlsson/DiscordBot").ConfigureAwait(false);
        }
    }
}
