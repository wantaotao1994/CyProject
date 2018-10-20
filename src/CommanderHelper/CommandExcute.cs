using System;
using System.Diagnostics;

namespace CommanderHelper
{
    public class CommandExcute
    {
        private string _path;
        string [] _arg;

        public bool IsCommplete { get; private set; } = false;


        public string OutFilePath { get; private set; }

        public CommandExcute(string outPath ,string shPath="sh",params string [] arg )
        {
            _path = shPath;
            _arg = arg;
            OutFilePath = outPath;
        }

        public  void Excute() {
            //创建一个ProcessStartInfo对象 使用系统shell 指定命令和参数 设置标准输出
            ExcuteCommand();
        }

        private void ExcuteCommand()
        {
            string par = "";
            foreach (var item in _arg)
            {
                par += $"{item} ";
            }
            var psi = new ProcessStartInfo(_path, par) { RedirectStandardOutput = true };
            Console.WriteLine($"Begin Start Commond:{par}");
            try
            {
                //启动
                var proc = Process.Start(psi);
                if (proc == null)
                {
                    Console.WriteLine("Can not exec.");
                }
                else
                {
                    Console.WriteLine("-------------Start read standard output--------------");
                    //开始读取
                    using (var sr = proc.StandardOutput)
                    {
                        while (!sr.EndOfStream)
                        {
                            Console.WriteLine(sr.ReadLine());
                        }

                        if (!proc.HasExited)
                        {
                            proc.Kill();
                        }
                    }

                    IsCommplete = true;
                    Console.WriteLine("---------------Read end------------------");
                    Console.WriteLine($"Total execute time :{(proc.ExitTime - proc.StartTime).TotalMilliseconds} ms");
                    Console.WriteLine($"Exited Code ： {proc.ExitCode}");
                }
            }
            catch (Exception e)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error:{e.Message}:\n {e.StackTrace}");
                Console.ResetColor();
            }
          
        }
    }
}
