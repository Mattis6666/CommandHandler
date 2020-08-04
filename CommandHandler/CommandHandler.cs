using System.Threading.Tasks;
using System;
using System.Reflection;
using Discord.WebSocket;
using System.Collections.Generic;
using System.Linq;

namespace CommandHandler
{
    public class CommandHandler
    {
        private IEnumerable<CommandConstructor> commands = new List<CommandConstructor>();
        private DiscordSocketClient client;
        private Config config;
        public CommandHandler(DiscordSocketClient cl, Config conf)
        {
            client = cl;
            config = conf;
        }

        public void initCommands()
        {
            var types = Assembly.GetEntryAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(VenshaCommand)));
            foreach (var t in types)
            {
                var methods = t.GetMethods().Where(m => m.GetCustomAttributes(typeof(Command)).Count() > 0);
                foreach (var method in methods)
                {
                    var command = new CommandConstructor(method, t);
                    commands = commands.Append(command);
                }
            }

            this.client.MessageReceived += OnMessage;
        }

        private async Task OnMessage(SocketMessage msgRaw)
        {
            var msg = msgRaw as SocketUserMessage;
            if (msg == null) return;

            if (!msg.Content.StartsWith(this.config.prefix)) return;
            string[] args = msg.Content.Trim().Substring(this.config.prefix.Length).Split(' ', int.MaxValue, StringSplitOptions.RemoveEmptyEntries);

            string commandName = args.FirstOrDefault()?.ToLower();
            if (commandName == null) return;

            var command = this.commands.FirstOrDefault(c => c.name == commandName || c.aliases.Contains(commandName));
            if (command == null) return;

            args = args.Skip(1).ToArray();

            await command.callback(client, msgRaw, args);
        }
    }
}