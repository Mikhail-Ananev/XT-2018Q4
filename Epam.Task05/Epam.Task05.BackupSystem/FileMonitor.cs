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
        private readonly string archivFolder;
        private readonly string backupFolder;
        private readonly string logFile;
        private readonly string filter;
        private StreamWriter log = null;
        private long id;

        public FileMonitor(string currentFolder, string backupFolder, string filter, bool monitorOn)
        {
            
            this.currentFolder = currentFolder;
            this.backupFolder = backupFolder;
            this.archivFolder = Path.Combine(this.backupFolder, "archiv");
            this.filter = filter;
            this.logFile = Path.Combine(backupFolder, "log.txt");
            if (!Directory.Exists(backupFolder) || !File.Exists(this.logFile))
            {
                this.id = 0;
                this.FirstStart();
            }
            else
            {
                this.id = Directory.GetFiles(this.backupFolder, "Backup*" + filter, SearchOption.AllDirectories).Length + 1;
            }

            this.fileSystemWatcher = new FileSystemWatcher(currentFolder);
            this.fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Attributes | NotifyFilters.CreationTime;
            this.fileSystemWatcher.InternalBufferSize = 1000;
            this.fileSystemWatcher.IncludeSubdirectories = true;
            this.fileSystemWatcher.Changed += this.FileWatcherOnChanged;
            this.fileSystemWatcher.Created += this.FileWatcherOnCreated;
            this.fileSystemWatcher.Deleted += this.FileWatcherOnDeleted;
            this.fileSystemWatcher.Renamed += this.FileWatcherOnRenamed;
            this.fileSystemWatcher.EnableRaisingEvents = monitorOn;
        }

        //public void Recovery(DateTime recDateTime)
        //{
        //    if (this.fileSystemWatcher.EnableRaisingEvents == true)
        //    {
        //        Console.WriteLine("Wile watcher in work you cann`t have recovery!");
        //        return;
        //    }

        //    StreamReader log = null;
        //    DateTime logData;
        //    string action;
        //    string[] allDirectoryFiles = Directory.GetFiles(this.currentFolder, "*" + this.filter, SearchOption.AllDirectories);
        //    foreach (var file in allDirectoryFiles)
        //    {
        //        File.Delete(file);
        //    }

        //    using (log = new StreamReader(this.logFile))
        //    {
        //        logData = DateTime.Parse(log.ReadLine());
        //        if (recDateTime < logData)
        //        {
        //            recDateTime = logData;
        //        }

        //        while (logData <= recDateTime)
        //        {
        //            action = log.ReadLine();
        //            this.FileRecovery(log, action);
        //            if (log.Peek() < 0)
        //            {
        //                break;
        //            }

        //            logData = DateTime.Parse(log.ReadLine());
        //        }

        //        Console.WriteLine("Recovery finished");
        //    }
        //}

        private void SaveFile(string fileEvent, params string[] filesName)
        {
            using (this.log = new StreamWriter(this.logFile, true))
            {
                foreach (var file in filesName)
                {
                    string backupFileName = Path.Combine(this.archivFolder, "Backup" + this.id.ToString() + ".txt");
                    File.Copy(file, Path.Combine(this.archivFolder, backupFileName));
                    this.log.WriteLine(id + "|||" + DateTime.Now.ToString() + "|||" + fileEvent + "|||" + file + "|||" + backupFileName + "|!|");
                    this.id++;
                }
            }

            this.log.Close();
        }

        private void FileWatcherOnRenamed(object sender, RenamedEventArgs systemEvent)
        {
            if (!systemEvent.FullPath.Contains(this.currentFolder) || !validationExtension(systemEvent))
            {
                FileWatcherOnDeleted(sender, systemEvent);
                return;
            }

            Console.WriteLine($"File: {systemEvent.OldFullPath} renamed to {systemEvent.FullPath}");
            using (this.log = new StreamWriter(this.logFile, true))
            {
                this.log.WriteLine(id + "|||" + DateTime.Now.ToString() + "|||" + "Renamed" + "|||" + systemEvent.FullPath + "|||" + systemEvent.OldFullPath + "|!|");
            }

            this.log.Close();
        }

        private bool validationExtension(FileSystemEventArgs systemEvent)
        {
            var fileInfo = new FileInfo(systemEvent.FullPath);
            string fileExtension = fileInfo.Extension.Substring(fileInfo.Extension.LastIndexOf('.') + 1);
            string filterExtension = this.filter.Substring(this.filter.LastIndexOf('.') + 1);
            return fileExtension == filterExtension;
        }

        private void FileWatcherOnDeleted(object sender, FileSystemEventArgs systemEvent)
        {
            Console.WriteLine("File: {0} deleted", systemEvent.FullPath);
            using (this.log = new StreamWriter(this.logFile, true))
            {
                this.log.WriteLine(id + "|||" + DateTime.Now.ToString() + "|||" + "Deleted" + "|||" + systemEvent.FullPath + "|!|");
            }

            this.log.Close();
        }

        private void FileWatcherOnCreated(object sender, FileSystemEventArgs systemEvent)
        {
            //if (!File.Exists(systemEvent.FullPath))
            //{
            //    return;
            //}

            if (!validationExtension(systemEvent))
            {
                return;
            }

            Console.WriteLine("File: {0} created", systemEvent.FullPath);
            this.SaveFile("Created", systemEvent.FullPath);
        }

        private void FileWatcherOnChanged(object sender, FileSystemEventArgs systemEvent)
        {
            if (!systemEvent.FullPath.Contains(this.currentFolder) || !validationExtension(systemEvent))
            {
                FileWatcherOnDeleted(sender, systemEvent);
                return;
            }

            Console.WriteLine("File: {0} changed", systemEvent.FullPath);
            //using (this.log = new StreamWriter(this.logFile, true))
            //{
                //this.log.WriteLine(id + "|||" + DateTime.Now.ToString() + "|||" + "Changed" + "|||" + systemEvent.FullPath + "|!|");
                this.SaveFile("Changed", systemEvent.FullPath);
            //}

            //this.log.Close();
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

        private void CreateArchiv()
        {
            string[] allDirectoryFiles = Directory.GetFiles(this.currentFolder, "*" + this.filter, SearchOption.AllDirectories);
            if (allDirectoryFiles != null)
            {
                this.SaveFile("Created", allDirectoryFiles);
            }
        }

        private void FirstStart()
        {
            try
            {
                Directory.CreateDirectory(this.backupFolder);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (this.backupFolder == this.currentFolder)
            {
                throw new Exception("You cann`t watching backup folder!");
            }

            if (Directory.Exists(this.archivFolder))
            {
                if (Directory.GetFiles(this.archivFolder, "Backup*.txt") != null)
                {
                    foreach (var file in Directory.GetFiles(this.archivFolder, "Backup*.txt"))
                    {
                        File.Delete(file);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(this.archivFolder);
            }

            try
            {
                File.Create(this.logFile).Close();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            this.CreateArchiv();
        }
    }
}
