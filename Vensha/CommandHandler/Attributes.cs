using System;
using Discord.WebSocket;

namespace Vensha.CommandHandler
{
    public class Command : Attribute
    {
        public string val;
        public Command(string val) => this.val = val;
    }

    public class Usage : Attribute
    {
        public string val;
        public Usage(string val) => this.val = val;
    }

    public class Aliases : Attribute
    {
        public string[] val;
        public Aliases(params string[] val) => this.val = val;
    }

    public class GuildOnly : BaseAttribute
    {
        public GuildOnly() : base((SocketUserMessage msg) => msg.Channel as SocketGuildChannel != null, $"This command can only be used on a server") { }
    }
    public class DmOnly : BaseAttribute
    {
        public DmOnly() : base((SocketUserMessage msg) => msg.Channel as SocketDMChannel != null, $"This command can only be used in a private message") { }
    }

    public class OwnerOnly : BaseAttribute
    {
        public OwnerOnly() : base((SocketUserMessage msg) => Program.instance.config.owners.Contains(msg.Author.Id), null) { }
    }

    public class BaseAttribute : Attribute
    {
        public Func<SocketUserMessage, bool> test;
        public string error;
        public BaseAttribute(Func<SocketUserMessage, bool> test, string err)
        {
            this.test = test;
            this.error = err;
        }
    }
}