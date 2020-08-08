using System.Collections.Generic;
using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace Vensha
{
    public class VenshaClient : DiscordSocketClient
    {
        public Config config;

        public VenshaClient() : base()
        {
            this.Log += this.LogInfo;
            this.config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Directory.GetCurrentDirectory() + "/config.json"));

            Program.commandHandler = new CommandHandler.Handler(this);
            Program.commandHandler.InitCommands();

            this.Init();
        }

        public async void Init()
        {
            await this.LoginAsync(TokenType.Bot, config.token);
            await this.StartAsync();
        }

        public Task LogInfo(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }

    public class Config
    {
        public string token { get; set; }
        public string prefix { get; set; }
        public HashSet<ulong> owners { get; set; }
    }
}