using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Vensha.CommandHandler
{
    public class CommandConstructor
    {
        public string name;
        public string usage;
        public int argCount = 0;
        public IEnumerable<string> aliases;
        public bool guildOnly = false;
        public bool dmOnly = false;
        public bool ownerOnly = false;
        public IEnumerable<Attribute> attributes;
        private MethodInfo method;
        private Type type;

        public CommandConstructor(MethodInfo method, Type type)
        {
            this.method = method;
            this.type = type;

            this.attributes = method.GetCustomAttributes();
            if (this.attributes.FirstOrDefault(x => x is Command) as Command == null) throw new Exception("How did this happen?");

            foreach (var attr in this.attributes)
            {
                switch (attr)
                {
                    case Command c:
                        if (Program.commandHandler.GetCommand(this.name) != null)
                        {
                            System.Console.WriteLine($"Duplicate command {this.name}");
                            Environment.Exit(1);
                        }
                        this.name = c.val;
                        break;
                    case Usage u:
                        this.usage = u.val;
                        break;
                    case Aliases a:
                        this.aliases = a.val;
                        break;
                    case GuildOnly _:
                        this.guildOnly = true;
                        break;
                    case DmOnly _:
                        this.dmOnly = true;
                        break;
                    case OwnerOnly _:
                        this.ownerOnly = true;
                        break;
                }
            }
        }
        public Task callback(CommandContext ctx)
        {
            var cmd = this.type.GetConstructor(new Type[0]).Invoke(null);

            (cmd as VenshaCommand).Inject(ctx);

            return this.method.Invoke(cmd, new object[] { }) as Task;
        }

    }

    public class VenshaCommand
    {
        public CommandContext Ctx;
        public void Inject(CommandContext ctx)
        {
            this.Ctx = ctx;
        }
    }

    public class CommandContext
    {
        public VenshaClient Client;
        public SocketUserMessage Message;
        public ISocketMessageChannel Channel;
        public List<string> Args;
        public SocketGuild? Guild = null;
        public CommandContext(VenshaClient client, SocketUserMessage msg, string[] args)
        {
            this.Channel = msg.Channel;
            this.Message = msg;
            this.Client = client;
            this.Args = args.Skip(1).ToList();

            this.Guild = (msg.Channel as SocketGuildChannel)?.Guild;
        }
    }
}