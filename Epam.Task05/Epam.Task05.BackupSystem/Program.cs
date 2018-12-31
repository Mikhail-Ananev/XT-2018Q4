using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task05.BackupSystem
{
    public class Program
    {
        protected static void Main(string[] args)
        {
            string folder = Environment.CurrentDirectory;
            string key;
            FileMonitor fileMonitor = new FileMonitor(folder, @"C:\Backup\", "*.txt");
            do
            {
                Console.WriteLine("Press 'm' to monitoring on the folder " + Environment.CurrentDirectory);
                Console.WriteLine("Press 'r' to restore files");
                Console.WriteLine("Press 'q' to quit program");
                key = Console.ReadLine();
                switch (key)
                {
                    case "m":
                        {
                            Console.WriteLine("Monitoring ON");
                            fileMonitor.MonitorOn = true;
                            fileMonitor.Start();

                            break;
                        }

                    case "r":
                        {
                            if (fileMonitor.MonitorOn == true)
                            {
                                Console.WriteLine("Wile watcher in work you cann`t have recovery!");
                                Console.WriteLine("To stop monoring folder press 's':");
                                if (Console.ReadLine() == "s")
                                {
                                    fileMonitor.MonitorOn = false;
                                }
                                else
                                {
                                    return;
                                }
                            }

                            DateTime recoveryDate = new DateTime();
                            Console.WriteLine("Input recovery data (format dd.MM.yyyy hh:mm):");
                            recoveryDate = DateTime.Parse(Console.ReadLine());
                            fileMonitor.Recovery(recoveryDate);
                            break;
                        }

                    default:
                        {
                            // fileMonitor.ShowQueue();
                            break;
                        }
                }
            }
            while (key != "q");
        }
    }
}