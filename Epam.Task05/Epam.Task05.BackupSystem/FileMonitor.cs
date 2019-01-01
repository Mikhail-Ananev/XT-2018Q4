using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        private long id;
        private ConcurrentQueue<string[]> fileToLogQueue = new ConcurrentQueue<string[]>();
        private DateTime timeEvent;

        public FileMonitor(string currentFolder, string backupFolder, string filter)
        {
            this.currentFolder = currentFolder;
            this.backupFolder = backupFolder;
            this.archivFolder = Path.Combine(this.backupFolder, "archiv");
            this.filter = filter;
            this.logFile = Path.Combine(backupFolder, "log.txt");
            this.fileSystemWatcher = new FileSystemWatcher(currentFolder);
            this.fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Attributes | NotifyFilters.CreationTime;
            this.fileSystemWatcher.InternalBufferSize = 1000;
            this.fileSystemWatcher.IncludeSubdirectories = true;
            this.fileSystemWatcher.Changed += this.FileWatcherOnChanged;
            this.fileSystemWatcher.Created += this.FileWatcherOnCreated;
            this.fileSystemWatcher.Deleted += this.FileWatcherOnDeleted;
            this.fileSystemWatcher.Renamed += this.FileWatcherOnRenamed;
            this.fileSystemWatcher.EnableRaisingEvents = this.MonitorOn;
            Thread th1 = new Thread(this.StartLogCopy);
            th1.Start();

        }

        public bool MonitorOn
        {
            get => this.fileSystemWatcher.EnableRaisingEvents;
            set
            {
                if (value == true)
                {
                    this.fileSystemWatcher.EnableRaisingEvents = true;
                }
                else
                {
                    this.fileSystemWatcher.EnableRaisingEvents = false;
                }
            }
        }

        public void Start()
        {
            if (!Directory.Exists(this.backupFolder) || !File.Exists(this.logFile))
            {
                this.id = 0;
                this.FirstStart();
            }
            else
            {
                this.id = Directory.GetFiles(this.backupFolder, "Backup*" + this.filter, SearchOption.AllDirectories).Length;
                //// CheckArchiv();
            }

            //this.RunLog();
        }

        //public void RunLog()
        //{
        //   // PROBLEMS BEGIN HERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //    Thread th1 = new Thread(this.StartLogCopy);
        //    // if (th1.ThreadState != ThreadState.Stopped)

        //    //if (th1.ThreadState == ThreadState.Unstarted || )
        //    //{
        //        th1.Start();
        //    //}

        //}

        public void Recovery(DateTime recoveryDateTime)
        {
            if (!File.Exists(this.logFile))
            {
                Console.WriteLine("Cann`t find log file! Try start monitoring on folder");
                return;
            }

            string allLogData;
            using (var log = new StreamReader(this.logFile))
            {
                allLogData = log.ReadToEnd().Replace("\r\n", string.Empty);
            }

            if (allLogData == null)
            {
                Console.WriteLine("Log file empty!");
                return;
            }

            string[,] allLogDataArray = this.PrepareLogData(allLogData);
            this.ClearDestinationFolder();
            DateTime dataEvent;
            for (int line = 0; line < allLogDataArray.GetLength(1); line++)
            {
                switch (allLogDataArray[line, 2])
                {
                    case "Created":
                    case "Changed":
                        this.RestoreCreatedAndChanged(allLogDataArray, line);
                        break;
                    case "Deleted":
                        this.RestoreDeleted(allLogDataArray, line);
                        break;
                    case "Renamed":
                        this.RestoreRenamed(allLogDataArray, line);
                        break;
                }

                dataEvent = DateTime.Parse(allLogDataArray[line, 1]);
                if (recoveryDateTime < dataEvent)
                {
                    break;
                }
            }
        }

        private void StartLogCopy()
        {
            while (this.fileSystemWatcher.EnableRaisingEvents)
            {
                if (!this.fileToLogQueue.IsEmpty)
                {
                    using (var log = new StreamWriter(this.logFile, true))
                    {
                        while (this.fileToLogQueue.TryDequeue(out string[] lineInLog))
                        {
                            var lineInLogStr = lineInLog[0] + "|" + lineInLog[1] + "|" + lineInLog[2] + "|" + lineInLog[3] + "|" + lineInLog[4] + "|";
                            log.WriteLine(lineInLogStr);
                            if (lineInLog[2] == "Created" || lineInLog[2] == "Changed")
                            {
                                File.Copy(lineInLog[3], Path.Combine(this.archivFolder, lineInLog[4]));
                            }
                        }
                    }

                    Console.WriteLine("HERE I AM");
                }
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

        private string[,] PrepareLogData(string allLogData)
        {
            char[] charSeparators = { '|' };
            var quantity = allLogData.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries).Length;
            string[,] allLogDataArray = new string[quantity / 5, 5];
            int position = 0;
            foreach (var logElement in allLogData.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries))
            {
                if (position < quantity - 1)
                {
                    allLogDataArray[position / 5, position % 5] = logElement;
                }

                position++;
            }

            return allLogDataArray;
        }

        private void ClearDestinationFolder()
        {
            string[] destinationDirectoryFiles = Directory.GetFiles(this.currentFolder, this.filter, SearchOption.AllDirectories);
            foreach (var file in destinationDirectoryFiles)
            {
                File.Delete(file);
            }
        }

        private void RestoreRenamed(string[,] allLogDataArray, int line)
        {
            File.Copy(allLogDataArray[line, 4], allLogDataArray[line, 3]);
            File.Delete(allLogDataArray[line, 3]);
        }

        private void RestoreDeleted(string[,] allLogDataArray, int line)
        {
            File.Delete(allLogDataArray[line, 3]);
        }

        private void RestoreCreatedAndChanged(string[,] allLogDataArray, int line)
        {
            File.Copy(Path.Combine(this.archivFolder, "Backup" + allLogDataArray[line, 0] + ".txt"), allLogDataArray[line, 3]);
        }

        private void FileWatcherOnRenamed(object sender, RenamedEventArgs systemEvent)
        {
            if (!File.Exists(systemEvent.FullPath))
            {
                return;
            }

            if (!systemEvent.FullPath.Contains(this.currentFolder) || !this.ValidationExtension(systemEvent))
            {
                this.FileWatcherOnDeleted(sender, systemEvent);
                return;
            }

            this.fileToLogQueue.Enqueue(new string[] { this.id.ToString(), DateTime.Now.ToString(), "Renamed", systemEvent.FullPath, systemEvent.OldFullPath });
        }

        private void FileWatcherOnDeleted(object sender, FileSystemEventArgs systemEvent)
        {
            if (!File.Exists(systemEvent.FullPath))
            {
                return;
            }

            this.fileToLogQueue.Enqueue(new string[] { this.id.ToString(), DateTime.Now.ToString(), "Deleted", systemEvent.FullPath, "empty" });
        }

        private void FileWatcherOnCreated(object sender, FileSystemEventArgs systemEvent)
        {
            if (!File.Exists(systemEvent.FullPath))
            {
                return;
            }

            if (!this.ValidationExtension(systemEvent))
            {
                return;
            }

            this.SaveFile("Created", systemEvent.FullPath);
        }

        private void FileWatcherOnChanged(object sender, FileSystemEventArgs systemEvent)
        {
            if (!File.Exists(systemEvent.FullPath))
            {
                return;
            }

            if (!systemEvent.FullPath.Contains(this.currentFolder) || !this.ValidationExtension(systemEvent))
            {
                this.FileWatcherOnDeleted(sender, systemEvent);
                return;
            }

            this.SaveFile("Changed", systemEvent.FullPath);
        }

        private void SaveFile(string fileEvent, params string[] filesName)
        {
            foreach (var file in filesName)
            {
                this.id++;
                string backupFileName = Path.Combine(this.archivFolder, "Backup" + this.id.ToString() + ".txt");
                this.timeEvent = DateTime.Now;
                this.fileToLogQueue.Enqueue(new string[] { this.id.ToString(), this.timeEvent.ToString(), fileEvent, file, backupFileName });
            }
        }

        private bool ValidationExtension(FileSystemEventArgs systemEvent)
        {
            var fileInfo = new FileInfo(systemEvent.FullPath);
            string fileExtension = fileInfo.Extension.Substring(fileInfo.Extension.LastIndexOf('.') + 1);
            string filterExtension = this.filter.Substring(this.filter.LastIndexOf('.') + 1);
            return fileExtension == filterExtension;
        }
        
        private void CreateArchiv()
        {
            string[] allDirectoryFiles = Directory.GetFiles(this.currentFolder, "*" + this.filter, SearchOption.AllDirectories);
            if (allDirectoryFiles != null)
            {
                this.SaveFile("Created", allDirectoryFiles);
            }
        }
    }
}
