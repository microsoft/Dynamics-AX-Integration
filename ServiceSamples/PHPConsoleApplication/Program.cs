using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PHPConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            /* When making service requets to Sandbox or Prod AX environemnts it must be ensured that TLS version is 1.2
             * .NET 4.5 supports TLS 1.2 but it is not the default protocol. The below statement can set TLS version explicity.
             * Note that this statement may not work in .NET 4.0, 3.0 or below.
             * Also note that in .NET 4.6 and above TLS 1.2 is the default protocol.
             */

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

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
