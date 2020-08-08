using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Vensha
{
    public class TestCommand : VenshaCommand
    {
        [Command("test")]
        [Aliases("t", "lol")]
        [Usage("Very epico!")]
        public Task Test(DiscordSocketClient client, SocketUserMessage msg, string[] args)
        {
            return msg.Channel.SendMessageAsync(args.FirstOrDefault() ?? "Hello!");
        }
    }
}