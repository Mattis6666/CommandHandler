using System.Threading.Tasks;
using Vensha.CommandHandler;

namespace Vensha.Modules {
    public class Yandere : VenshaCommand {
        [Command ("yandere")]
        [Aliases ("yan")]
        [Usage ("Very epico!")]
        [OwnerOnly]
        public Task Test () {
            return Ctx.Channel.SendMessageAsync (Ctx.Guild != null ? "This is a guild" : "This is a dm");
        }
    }
}