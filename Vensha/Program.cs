using System;
using System.Threading.Tasks;

namespace Vensha
{
    public class Program
    {
        public static VenshaClient Instance;
        public static CommandHandler.Handler CommandHandler;
        public static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();

        public async Task MainAsync(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;

            Instance = new VenshaClient();

            await Task.Delay(-1);
        }
    }
}