using CommanderHelper;
using System;

namespace QuickTest
{


    public class Program
    {

        static void Main(string[] args)
        {
            CommandExcute commandExcute = new CommandExcute("sh","","test.sh test.txt out.txt");
            commandExcute.Excute();

            Console.ReadLine();
            //Console.ReadLine();
        }
    }
}
