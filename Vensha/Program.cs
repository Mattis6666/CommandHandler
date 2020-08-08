using System.Threading.Tasks;

namespace Vensha
{
    public class Program
    {
        public static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();

        public async Task MainAsync(string[] args)
        {
            new VenshaClient();
            await Task.Delay(-1);
        }
    }
}