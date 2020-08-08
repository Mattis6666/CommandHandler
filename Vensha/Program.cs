using System.Threading.Tasks;

namespace Vensha
{
    public class Program
    {
        public static VenshaClient instance;
        public static CommandHandler.Handler commandHandler;
        public static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();

        public async Task MainAsync(string[] args)
        {
            instance = new VenshaClient();
            await Task.Delay(-1);
        }
    }
}