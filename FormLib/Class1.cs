using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormLib
{
    public class OpenDocument
    {
        public static int Main(string[] args)
        {
            
            Process process = new Process();
            try
            {
                MessageBox.Show(args[1]);
                process = Process.Start(new ProcessStartInfo($"{args[1]}") { UseShellExecute = true });
                process.WaitForExit();
            }
            catch {
                return 1; 
            }

            return 0;
        }
    }

    public class Message
    {
        public static void ShowSuccess()
        {
            MessageBox.Show("Document successfully downloaded");
        }
        public static void ShowError(Exception ex)
        {
            MessageBox.Show($"Exception : {ex.Message}");
        }
    }
}
