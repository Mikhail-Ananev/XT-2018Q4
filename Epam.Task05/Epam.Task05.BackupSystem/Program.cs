using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    public class FileMonitor
    {
        private readonly FileSystemWatcher fileSystemWatcher;
        private readonly string currentFolder;
        private readonly string backupFolder;
        private readonly string logFile;
        private readonly string filter;
        private StreamWriter log = null;
        private int fileQuantity = 0;
        private void SaveFile(string fileEvent, params string[] storedFilesName)
        {
            using (log = new StreamWriter(logFile, true))
            {
                string data = DateTime.Now.ToString();
                foreach (var fileName in storedFilesName)
                {
                    string backupFileName = Path.Combine(backupFolder, "Backup" + fileQuantity.ToString() + ".txt");
                    File.Copy(fileName, Path.Combine(backupFolder, backupFileName));
                    log.WriteLine(data);
                    log.WriteLine(fileEvent);
                    log.WriteLine(fileName);
                    log.WriteLine(backupFileName);
                    fileQuantity++;
                }
            }

            log.Close();
        }

        private void FileWatcherOnRenamed(object sender, RenamedEventArgs systemEvent)
        {
            if (!File.Exists(systemEvent.FullPath))
            {
                return;
            }

            var fileInfo = new FileInfo(systemEvent.OldFullPath);
            if (fileInfo.Extension != filter)
            {
                return;
            }

            Console.WriteLine("File: {0} renamed to {1}", systemEvent.OldFullPath, systemEvent.FullPath);
            string newFileName = systemEvent.FullPath;
            string oldFileName = systemEvent.OldFullPath;
            using (log = new StreamWriter(logFile, true))
            {
                log.WriteLine(DateTime.Now.ToString());
                log.WriteLine("Renamed");
                log.WriteLine(newFileName);
                log.WriteLine(oldFileName);
            }

            log.Close();
        }

        private void FileWatcherOnDeleted(object sender, FileSystemEventArgs systemEvent)
        {
            var fileInfo = new FileInfo(systemEvent.FullPath);
            if (fileInfo.Extension != filter)
            {
                return;
            }

            Console.WriteLine("File: {0} deleted", systemEvent.FullPath);
            string deletedFileName = systemEvent.FullPath;
            using (log = new StreamWriter(logFile, true))
            {
                log.WriteLine(DateTime.Now.ToString());
                log.WriteLine("Deleted");
                log.WriteLine(deletedFileName);
            }

            log.Close();
        }

        private void FileWatcherOnCreated(object sender, FileSystemEventArgs systemEvent)
        {
            if (!File.Exists(systemEvent.FullPath))
            {
                return;
            }

            var fileInfo = new FileInfo(systemEvent.FullPath);
            if (fileInfo.Extension != filter)
            {
                return;
            }

            Console.WriteLine("File: {0} created", systemEvent.FullPath);
            string createdFileName = systemEvent.FullPath;
            SaveFile("Created", createdFileName);
        }

        private void FileWatcherOnChanged(object sender, FileSystemEventArgs systemEvent)
        {
            try
            {
                fileSystemWatcher.EnableRaisingEvents = false;
                if (!File.Exists(systemEvent.FullPath))
                {
                    return;
                }

                var fileInfo = new FileInfo(systemEvent.FullPath);
                if (fileInfo.Extension != filter)
                {
                    return;
                }

                Console.WriteLine("File: {0} changed", systemEvent.FullPath);
                string storedFileName = systemEvent.FullPath;
                SaveFile("Changed", storedFileName);

            }
            finally
            {
                fileSystemWatcher.EnableRaisingEvents = true;
            }
        }

        private void MonitorOn()
        {
            fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void MonitorOff()
        {
            fileSystemWatcher.EnableRaisingEvents = false;
        }

        private void FileRecovery(StreamReader log, string action)
        {
            string destFile;
            string movedFile;
            string backupFile;
            switch (action)
            {
                case "Created":
                    {
                        destFile = log.ReadLine();
                        backupFile = log.ReadLine();
                        Directory.CreateDirectory(Path.GetDirectoryName(destFile));
                        File.Copy(backupFile, destFile, true);
                        break;
                    }
                case "Changed":
                    {
                        destFile = log.ReadLine();
                        backupFile = log.ReadLine();
                        Directory.CreateDirectory(Path.GetDirectoryName(destFile));
                        File.Copy(backupFile, destFile, true);
                        break;
                    }

                case "Deleted":
                    {
                        string deletedFile = log.ReadLine();
                        File.Delete(log.ReadLine());
                        break;
                    }

                case "Renamed":
                    {
                        destFile = log.ReadLine();
                        movedFile = log.ReadLine();
                        File.Move(movedFile, destFile);
                        break;
                    }

                default:
                    break;
            }
        }

        public void Recovery(DateTime recDateTime)
        {
            if (fileSystemWatcher.EnableRaisingEvents == true)
            {
                Console.WriteLine("Wile watcher in work you cann`t have recovery!");
                return;
            }

            StreamReader log = null;
            DateTime logData;
            string action;
            string[] allDirectoryFiles = Directory.GetFiles(currentFolder, "*" + filter, SearchOption.AllDirectories);
            foreach (var file in allDirectoryFiles)
            {
                File.Delete(file);
            }

            using (log = new StreamReader(logFile))
            {
                logData = DateTime.Parse(log.ReadLine());
                if (recDateTime < logData)
                {
                    recDateTime = logData;
                }

                while (logData <= recDateTime)
                {
                    action = log.ReadLine();
                    FileRecovery(log, action);
                    if (log.Peek() < 0)
                    {
                        break;
                    }

                    logData = DateTime.Parse(log.ReadLine());
                }

                Console.WriteLine("Recovery finished");
            }
        }

        private void FullBackup()
        {
            string[] allDirectoryFiles = Directory.GetFiles(currentFolder, "*" + filter, SearchOption.AllDirectories);
            SaveFile("Created", allDirectoryFiles);
        }

        private void FirstStart()
        {
            try
            {
                Directory.CreateDirectory(backupFolder);
            }
            catch (ArgumentException argEx)
            {

                Console.WriteLine(argEx.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            string[] allDirectoryFiles = Directory.GetFiles(backupFolder);
            foreach (var file in allDirectoryFiles)
            {
                File.Delete(file);
            }

            try
            {
                File.Create(logFile).Close();
            }
            catch (ArgumentException argEx)
            {

                Console.WriteLine(argEx.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            FullBackup();
        }

        public FileMonitor(string currentFolder, string backupFolder, string filter, bool monitorOn)
        {
            this.currentFolder = currentFolder;
            this.backupFolder = backupFolder;
            this.filter = filter;
            this.logFile = Path.Combine(backupFolder, "log.txt");
            if (!Directory.Exists(backupFolder) || !File.Exists(logFile))
            {
                FirstStart();
            }
            else
            {
                fileQuantity = Directory.GetFiles(this.backupFolder, "Backup*" + filter, SearchOption.AllDirectories).Length;
            }

            fileSystemWatcher = new FileSystemWatcher(currentFolder);
            fileSystemWatcher.IncludeSubdirectories = true;
            fileSystemWatcher.Changed += FileWatcherOnChanged;
            fileSystemWatcher.Created += FileWatcherOnCreated;
            fileSystemWatcher.Deleted += FileWatcherOnDeleted;
            fileSystemWatcher.Renamed += FileWatcherOnRenamed;
            fileSystemWatcher.EnableRaisingEvents = monitorOn;
        }
    }

    class Program
    {
        static void Main(string[] args)
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
            } while (key != "q");
        }
    }
}

