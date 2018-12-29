using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public FileMonitor(string currentFolder, string backupFolder, string filter)
        {

            this.currentFolder = currentFolder;
            this.backupFolder = backupFolder;
            this.archivFolder = Path.Combine(this.backupFolder, "archiv");
            this.filter = filter;
            this.logFile = Path.Combine(backupFolder, "log.txt");
            //Start(backupFolder, filter);

            this.fileSystemWatcher = new FileSystemWatcher(currentFolder);
            this.fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Attributes | NotifyFilters.CreationTime;
            this.fileSystemWatcher.InternalBufferSize = 1000;
            this.fileSystemWatcher.IncludeSubdirectories = true;
            this.fileSystemWatcher.Changed += this.FileWatcherOnChanged;
            this.fileSystemWatcher.Created += this.FileWatcherOnCreated;
            this.fileSystemWatcher.Deleted += this.FileWatcherOnDeleted;
            this.fileSystemWatcher.Renamed += this.FileWatcherOnRenamed;
            this.fileSystemWatcher.EnableRaisingEvents = MonitorOn;
        }

        public void Recovery(DateTime recoveryDateTime)
        {
            //if (this.fileSystemWatcher.EnableRaisingEvents == true)
            //{
            //    Console.WriteLine("Wile watcher in work you cann`t have recovery!");
            //    Console.WriteLine("To stop monoring folder press 's':");
            //    if (Console.ReadLine() == "s")
            //    {
            //        this.fileSystemWatcher.EnableRaisingEvents = false;
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}

            if (!File.Exists(this.logFile))
            {
                Console.WriteLine("Cann`t find log file! Try start monitoring on folder");
                return;
            }

            string allLogData;
            using (var log = new StreamReader(this.logFile))
            {
                allLogData = log.ReadToEnd().Replace("\r\n", "");
            }

            if (allLogData == null)
            {
                Console.WriteLine("Log file empty!");
                return;
            }
            //preparing log data
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
            //finish
            //prepare destination folder
            string[] destinationDirectoryFiles = Directory.GetFiles(this.currentFolder, this.filter, SearchOption.AllDirectories);
            foreach (var file in destinationDirectoryFiles)
            {
                File.Delete(file);
            }
            //finish
            //restore from begining

            DateTime dataEvent;
            for (int line = 0; line < allLogDataArray.GetLength(1); line++)
            {
                switch (allLogDataArray[line,2])
                {
                    case "Created":
                        RestoreCreatedAndChanged(allLogDataArray, line);
                        break;
                    case "Changed":
                        RestoreCreatedAndChanged(allLogDataArray, line);
                        break;
                    case "Deleted":
                        RestoreDeleted(allLogDataArray, line);
                        break;
                    case "Renamed":
                        RestoreRenamed(allLogDataArray, line);
                        break;
                }

                dataEvent = DateTime.Parse(allLogDataArray[line, 1]);
                if (recoveryDateTime < dataEvent )
                {
                    break;
                }
            }
            //finish

            //position = 0;
            //Console.WriteLine(quantity);
            //foreach (var item in allLogData.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries))
            //{
            //    allLogDataArray[position / 5, position % 5] = item;
            //    position++;
            //}

            //position = 0;
            //foreach (var item in allLogDataArray)
            //{
            //    Console.WriteLine(item + "|");
            //    if (position % 5 == 0)
            //    {
            //        Console.WriteLine();
            //    }
            //    position++;

            //}
            //Console.WriteLine(allLogData);
            //    //logData = DateTime.Parse(log.ReadLine());
            //    //if (recoveryDateTime < logData)
            //    //{
            //    //    recoveryDateTime = logData;
            //    //}

            //    //while (logData <= recoveryDateTime)
            //    //{
            //    //    action = log.ReadLine();
            //    //    this.FileRecovery(log, action);
            //    //    if (log.Peek() < 0)
            //    //    {
            //    //        break;
            //    //    }

            //    //    logData = DateTime.Parse(log.ReadLine());
            //    //}

            //    Console.WriteLine("Recovery finished");




            ////StreamReader log = null;
            ////DateTime logData;
            //string action;
            //string[] allDirectoryFiles = Directory.GetFiles(this.currentFolder, "*" + this.filter, SearchOption.AllDirectories);
            //foreach (var file in allDirectoryFiles)
            //{
            //    File.Delete(file);
            //}

            //using (log = new StreamReader(this.logFile))
            //{
            //    logData = DateTime.Parse(log.ReadLine());
            //    if (recoveryDateTime < logData)
            //    {
            //        recoveryDateTime = logData;
            //    }

            //    while (logData <= recoveryDateTime)
            //    {
            //        action = log.ReadLine();
            //        this.FileRecovery(log, action);
            //        if (log.Peek() < 0)
            //        {
            //            break;
            //        }

            //        logData = DateTime.Parse(log.ReadLine());
            //    }

            //    Console.WriteLine("Recovery finished");
            //}
        }

        private void RestoreRenamed(string[,] allLogDataArray, int line)
        {
            throw new NotImplementedException();
        }

        private void RestoreDeleted(string[,] allLogDataArray, int line)
        {
            File.Delete(allLogDataArray[line,3]);
        }

        private void RestoreCreatedAndChanged(string[,] allLogDataArray, int line)
        {
            //string fileShortName = allLogDataArray[line, 3].Substring(allLogDataArray[line, 3].LastIndexOf('\\') + 1);
            //string destinationPath = allLogDataArray[line, 3].Substring(0, allLogDataArray[line, 3].Length - fileShortName.Length - 1);
            File.Copy(Path.Combine(this.archivFolder, "Backup" + allLogDataArray[line, 0] + ".txt"), allLogDataArray[line, 3]);
            //File.Copy(Path.Combine(this.archivFolder, "Backup" + allLogDataArray[line,0] + ".txt"), destinationPath);
            
                //0|29.12.2018 0:15:16|Created|D:\Tests\XT-2018Q4\Epam.Task05\Epam.Task05.BackupSystem\bin\Debug\1.txt|C:\Backup\archiv\Backup0.txt|
        }

        private void SaveFile(string fileEvent, params string[] filesName)
        {
            using (this.log = new StreamWriter(this.logFile, true))
            {
                foreach (var file in filesName)
                {
                    this.id++;
                    string backupFileName = Path.Combine(this.archivFolder, "Backup" + this.id.ToString() + ".txt");
                    File.Copy(file, Path.Combine(this.archivFolder, backupFileName));
                    this.log.WriteLine(id + "|" + DateTime.Now.ToString() + "|" + fileEvent + "|" + file + "|" + backupFileName + "|");
                }
            }
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
                this.log.WriteLine(id + "|" + DateTime.Now.ToString() + "|" + "Renamed" + "|" + systemEvent.FullPath + "|" + systemEvent.OldFullPath + "|");
            }
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
                this.log.WriteLine(id + "|" + DateTime.Now.ToString() + "|" + "Deleted" + "|" + systemEvent.FullPath + "| empty |");
            }
        }

        private void FileWatcherOnCreated(object sender, FileSystemEventArgs systemEvent)
        {
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
            this.SaveFile("Changed", systemEvent.FullPath);
        }

        //private void MonitorOn()
        //{
        //    this.fileSystemWatcher.EnableRaisingEvents = true;
        //}

        //private void MonitorOff()
        //{
        //    this.fileSystemWatcher.EnableRaisingEvents = false;
        //}

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

        public void Start()
        {
            //if (this.MonitorOn)
            //{
                if (!Directory.Exists(this.backupFolder) || !File.Exists(this.logFile))
                {
                    this.id = 0;
                    this.FirstStart();
                }
                else
                {
                    this.id = Directory.GetFiles(this.backupFolder, "Backup*" + this.filter, SearchOption.AllDirectories).Length;
                    //CheckArchiv();
                }

            //}
        }

        //private void CheckArchiv()
        //{
        //    string[] archivFiles = Directory.GetFiles(this.archivFolder, "Backup*" + filter, SearchOption.AllDirectories);
        //    //string fileShortName = allLogDataArray[line, 3].Substring(allLogDataArray[line, 3].LastIndexOf('\\') + 1);
        //    var 
        //    for (int i = 0; i < id; i++)
        //    {
        //        if (archivFiles[i].Substring(archivFiles[i].LastIndexOf('\\')) + 1 != string.Concat(Backup + id.ToString + ".txt"))
        //        {
        //            this.id = Directory.GetFiles(this.backupFolder, "Backup*" + filter, SearchOption.AllDirectories).Length + 1;

        //        }

        //    }
        //}

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
