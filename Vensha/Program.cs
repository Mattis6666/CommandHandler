/*
          __                       ___.                                
  _______/  |_____________ __  _  _\_ |__   __________________ ___.__. 
 /  ___/\   __\_  __ \__  \\ \/ \/ /| __ \_/ __ \_  __ \_  __ <   |  | 
 \___ \  |  |  |  | \// __ \\     / | \_\ \  ___/|  | \/|  | \/\___  | 
/____  > |__|  |__|  (____  /\/\_/  |___  /\___  >__|   |__|   / ____| 
     \/                   \/            \/     \/              \/      
     
                 /~~\ /~~~\--\         _,/---\.   __
               /~|  _|--~~"-\ |     /  _-|-----\<   \
           /~~-> /~          |  \/-\/~~           ~\./\\.
          |   /               | / /                 |  | ~~--->->
          /~~/                 ///                   |/,_,-=~~/
         |  |                  |/                    |~~|    /
         '\ |                  V                     L_/    '
        ./~~|                                       /  |
        |   |                                      L.  /
         \--|        .                            / \
         |  \         \                         _/  /
      ,  '--~\     ,_--~\                     // <
  ,    \      \ /-~      |                 ./~   /
;   \    \._/~~|         '             ._/~/  ~~
  \_.__>=|~  /~~\                  .=/~~ _/
  -~/  / |  /   >,             ./-~ |
   /  /  |  '~~| '\>\       ,/\.   /~
               '\_/| \,   ./   |~-~
                   |__|/\/ ~-~'
                      |   |
                      \ _ /
*/
using System;
using System.Threading.Tasks;

namespace Vensha {
    public class Program {
        public static VenshaClient Instance;
        public static CommandHandler.Handler CommandHandler;
        public static void Main (string[] args) => new Program ().MainAsync (args).GetAwaiter ().GetResult ();

        public async Task MainAsync (string[] args) {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;

            Instance = new VenshaClient ();

            await Task.Delay (-1);
        }
    }
}