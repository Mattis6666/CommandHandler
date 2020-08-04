using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.IO;

namespace CommandHandler
{
    public class Program
    {
        private DiscordSocketClient client;
        private Config config;
        public static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();

        public async Task MainAsync(string[] args)
        {
            client = new DiscordSocketClient();

            client.Log += Log;

            config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Directory.GetCurrentDirectory() + "/config.json"));


            await client.LoginAsync(TokenType.Bot, config.token);
            await client.StartAsync();

            var commands = new CommandHandler(client, config);
            commands.initCommands();

            await Task.Delay(-1);
        }
        public Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }

    public class Config
    {
        public string token { get; set; }
        public string prefix { get; set; }
    }
}
