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
                            FileMonitor fileMonitor = new FileMonitor(folder, @"C:\Backup\", "*.txt", true);
                            break;
                        }

                    case "r":
                        {
                            Console.WriteLine("Input restore date");
                            FileMonitor fileMonitor = new FileMonitor(folder, @"C:\Backup\", "*.txt", false);
                            DateTime recoveryDate = new DateTime();
                            Console.WriteLine("Input recovery data (format dd.MM.yyyy hh:mm):");
                            recoveryDate = DateTime.Parse(Console.ReadLine());
                            fileMonitor.Recovery(recoveryDate);
                            break;
                        }
                }
            }
            while (key != "q");
        }
    }
}