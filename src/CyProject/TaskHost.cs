using System;
using System.Threading;
using System.Threading.Tasks;
using CommanderHelper;
using CyProject.Schedule;
using Microsoft.AspNetCore.Builder;

namespace CyProject
{
    public class Task2Host
    {

     

        public  static Task  Run()
        {
            while (true)
            {
                var status = TaskSchedule.Prop();
                if (status.Value!=null)
                {
                    CommandExcute commandExcute  = new CommandExcute("task1.sh","sh",status.Value.InFastqPath,status.Value.InRefFaPath,"0",status.Key);
                    Console.WriteLine("开始执行任务:"+status.Key);
                    commandExcute.Excute();
                }
                else
                {
                    Console.WriteLine("没有任务 ");
                }
                
                Thread.Sleep(10000);

            }
        }
    }
}