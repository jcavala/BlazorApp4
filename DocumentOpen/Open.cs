using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentOpen
{
     class Open
    {
        public static int Main(string[] args)
        {
            try
            {
                Process process = new Process();
                process = Process.Start(new ProcessStartInfo(args[0]) { UseShellExecute = true });
                process.WaitForExit();
            }catch(Exception ex)
            {
                return 1;
            }

            return 0;
        }
    }
}
