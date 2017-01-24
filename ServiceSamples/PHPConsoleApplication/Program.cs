using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHPConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string command = @"PHP.exe";
            string phpArgs = "Program.php";

            try
            {
                Process process = new Process();
                process.StartInfo.FileName = command;
                process.StartInfo.Arguments = phpArgs;
                process.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
