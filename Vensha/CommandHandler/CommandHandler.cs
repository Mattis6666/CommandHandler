using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord.WebSocket;
using msg = Discord.LogMessage;

namespace Vensha.CommandHandler
{
    public class Handler
    {
        private IEnumerable<CommandConstructor> commands = new List<CommandConstructor>();
        private VenshaClient client;

        public Handler(VenshaClient client)
        {
            this.client = client;
        }

        public void InitCommands()
        {
            var types = from t in Assembly.GetEntryAssembly().GetTypes()
                        where t.IsSubclassOf(typeof(VenshaCommand))
                        select t;

            foreach (var t in types)
            {
                var methods = from m in t.GetMethods()
                              where m.GetCustomAttributes(typeof(Command)).Count() > 0
                              select m;

                foreach (var method in methods)
                {
                    var command = new CommandConstructor(method, t);
                    commands = commands.Append(command);
                }
            }

            this.client.LogInfo(new msg(Discord.LogSeverity.Info, "CommandHandler", "Commands successfully initialised!"));
            this.client.MessageReceived += OnMessage;
        }

        private async Task OnMessage(SocketMessage msgRaw)
        {
            var msg = msgRaw as SocketUserMessage;
            if (msg == null) return;

            if (!msg.Content.StartsWith(this.client.config.prefix)) return;
            string[] args = msg.Content
                .Trim()
                .Substring(this.client.config.prefix.Length)
                .Split(' ', int.MaxValue, StringSplitOptions.RemoveEmptyEntries);

            string commandName = args.FirstOrDefault()?.ToLower();
            if (commandName == null) return;

            var command = this.GetCommand(commandName);
            if (command == null) return;

            foreach (var attr in command.attributes.Where(a => a is BaseAttribute))
            {
                if (!(await this.CheckCondition(msg, attr as BaseAttribute))) return;
            }

            await command.callback(new CommandContext(client, msg, args));
        }

        public CommandConstructor? GetCommand(string name) => this.commands.FirstOrDefault(c => c.name == name || c.aliases.Contains(name));

        private async Task<bool> CheckCondition(SocketUserMessage msg, BaseAttribute attr)
        {
            if (attr.test(msg)) return true;

            if (attr.error != null) await msg.Channel.SendMessageAsync(attr.error);
            return false;
        }
    }
}
