using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CommanderHelper
{
   public static class CommandTaskQueue
    {
        static CommandTaskQueue() {
            CommandExcuteSet = new Dictionary<string, CommandExcute>();
        }

        public static Dictionary<string, CommandExcute> CommandExcuteSet; 
    }
}
