using System.Threading.Tasks;
using Vensha.CommandHandler;

namespace Vensha.Modules
{
    public class TestCommand : VenshaCommand
    {
        [Command("test")]
        [Aliases("t", "lol")]
        [Usage("Very epico!")]
        [OwnerOnly]
        public Task Test()
        {
            return Ctx.Channel.SendMessageAsync(Ctx.Guild != null ? "This is a guild" : "This is a dm");
        }
    }
}