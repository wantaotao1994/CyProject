using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CommanderHelper
{
    public static  class CommandMap
    {

        public static string FastqToFasta(string inPath,string  outPath)
        {

            string  shPath = @"sh"; 

            var  command = new   CommandExcute(shPath,outPath, $"wantaotao.sh {inPath} {outPath}");

            string guiId = Guid.NewGuid().ToString();

            CommandTaskQueue.CommandExcuteSet.Add(guiId,command);
            Task.Factory.StartNew(()=> {
                command.Excute();
            });

            return guiId;
        }
    }
}
