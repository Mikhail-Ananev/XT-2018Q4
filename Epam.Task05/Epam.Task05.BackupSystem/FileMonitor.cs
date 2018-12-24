using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task05.BackupSystem
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

        public FileMonitor(string currentFolder, string backupFolder, string filter, bool monitorOn)
        {
            this.currentFolder = currentFolder;
            this.backupFolder = backupFolder;
            this.filter = filter;
            this.logFile = Path.Combine(backupFolder, "log.txt");
            if (!Directory.Exists(backupFolder) || !File.Exists(this.logFile))
            {
                this.FirstStart();
            }
            else
            {
                this.fileQuantity = Directory.GetFiles(this.backupFolder, "Backup*" + filter, SearchOption.AllDirectories).Length;
            }

            this.fileSystemWatcher = new FileSystemWatcher(currentFolder);
            this.fileSystemWatcher.IncludeSubdirectories = true;
            this.fileSystemWatcher.Changed += this.FileWatcherOnChanged;
            this.fileSystemWatcher.Created += this.FileWatcherOnCreated;
            this.fileSystemWatcher.Deleted += this.FileWatcherOnDeleted;
            this.fileSystemWatcher.Renamed += this.FileWatcherOnRenamed;
            this.fileSystemWatcher.EnableRaisingEvents = monitorOn;
        }

        public void Recovery(DateTime recDateTime)
        {
            if (this.fileSystemWatcher.EnableRaisingEvents == true)
            {
                Console.WriteLine("Wile watcher in work you cann`t have recovery!");
                return;
            }

            StreamReader log = null;
            DateTime logData;
            string action;
            string[] allDirectoryFiles = Directory.GetFiles(this.currentFolder, "*" + this.filter, SearchOption.AllDirectories);
            foreach (var file in allDirectoryFiles)
            {
                File.Delete(file);
            }

            using (log = new StreamReader(this.logFile))
            {
                logData = DateTime.Parse(log.ReadLine());
                if (recDateTime < logData)
                {
                    recDateTime = logData;
                }

                while (logData <= recDateTime)
                {
                    action = log.ReadLine();
                    this.FileRecovery(log, action);
                    if (log.Peek() < 0)
                    {
                        break;
                    }

                    logData = DateTime.Parse(log.ReadLine());
                }

                Console.WriteLine("Recovery finished");
            }
        }

        private void SaveFile(string fileEvent, params string[] storedFilesName)
        {
            using (this.log = new StreamWriter(this.logFile, true))
            {
                string data = DateTime.Now.ToString();
                foreach (var fileName in storedFilesName)
                {
                    string backupFileName = Path.Combine(this.backupFolder, "Backup" + this.fileQuantity.ToString() + ".txt");
                    File.Copy(fileName, Path.Combine(this.backupFolder, backupFileName));
                    this.log.WriteLine(data);
                    this.log.WriteLine(fileEvent);
                    this.log.WriteLine(fileName);
                    this.log.WriteLine(backupFileName);
                    this.fileQuantity++;
                }
            }

            this.log.Close();
        }

        private void FileWatcherOnRenamed(object sender, RenamedEventArgs systemEvent)
        {
            if (!File.Exists(systemEvent.FullPath))
            {
                return;
            }

            var fileInfo = new FileInfo(systemEvent.OldFullPath);
            if (fileInfo.Extension != this.filter)
            {
                return;
            }

            Console.WriteLine("File: {0} renamed to {1}", systemEvent.OldFullPath, systemEvent.FullPath);
            string newFileName = systemEvent.FullPath;
            string oldFileName = systemEvent.OldFullPath;
            using (this.log = new StreamWriter(this.logFile, true))
            {
                this.log.WriteLine(DateTime.Now.ToString());
                this.log.WriteLine("Renamed");
                this.log.WriteLine(newFileName);
                this.log.WriteLine(oldFileName);
            }

            this.log.Close();
        }

        private void FileWatcherOnDeleted(object sender, FileSystemEventArgs systemEvent)
        {
            var fileInfo = new FileInfo(systemEvent.FullPath);
            if (fileInfo.Extension != this.filter)
            {
                return;
            }

            Console.WriteLine("File: {0} deleted", systemEvent.FullPath);
            string deletedFileName = systemEvent.FullPath;
            using (this.log = new StreamWriter(this.logFile, true))
            {
                this.log.WriteLine(DateTime.Now.ToString());
                this.log.WriteLine("Deleted");
                this.log.WriteLine(deletedFileName);
            }

            this.log.Close();
        }

        private void FileWatcherOnCreated(object sender, FileSystemEventArgs systemEvent)
        {
            if (!File.Exists(systemEvent.FullPath))
            {
                return;
            }

            var fileInfo = new FileInfo(systemEvent.FullPath);
            if (fileInfo.Extension != this.filter)
            {
                return;
            }

            Console.WriteLine("File: {0} created", systemEvent.FullPath);
            string createdFileName = systemEvent.FullPath;
            this.SaveFile("Created", createdFileName);
        }

        private void FileWatcherOnChanged(object sender, FileSystemEventArgs systemEvent)
        {
            try
            {
                this.fileSystemWatcher.EnableRaisingEvents = false;
                if (!File.Exists(systemEvent.FullPath))
                {
                    return;
                }

                var fileInfo = new FileInfo(systemEvent.FullPath);
                if (fileInfo.Extension != this.filter)
                {
                    return;
                }

                Console.WriteLine("File: {0} changed", systemEvent.FullPath);
                string storedFileName = systemEvent.FullPath;
                this.SaveFile("Changed", storedFileName);
            }
            finally
            {
                this.fileSystemWatcher.EnableRaisingEvents = true;
            }
        }

        private void MonitorOn()
        {
            this.fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void MonitorOff()
        {
            this.fileSystemWatcher.EnableRaisingEvents = false;
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
                    {
                        break;
                    }
            }
        }

        private void FullBackup()
        {
            string[] allDirectoryFiles = Directory.GetFiles(this.currentFolder, "*" + this.filter, SearchOption.AllDirectories);
            this.SaveFile("Created", allDirectoryFiles);
        }

        private void FirstStart()
        {
            try
            {
                Directory.CreateDirectory(this.backupFolder);
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            string[] allDirectoryFiles = Directory.GetFiles(this.backupFolder);
            foreach (var file in allDirectoryFiles)
            {
                File.Delete(file);
            }

            try
            {
                File.Create(this.logFile).Close();
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            this.FullBackup();
        }
    }
}
