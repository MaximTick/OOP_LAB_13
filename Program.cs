using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace _13lab
{
    public delegate void Info(string info);
    class TMALog
    {
        private static int count = 0;       
        public void Write(string info)
        {
            count++;
            Console.WriteLine("Запись...");

            StreamWriter sw = File.AppendText(@"D:\3sem\OOP\13lab\TMALog.txt");
            sw.WriteLine(info);
            sw.Close();
            Console.WriteLine("__________");
        }
        public void Count()
        {
            Console.WriteLine("Количество записей в файле: " + count);
        }
        public void Exercise6()
        {
            Console.WriteLine("За прошлый час:");
            string[] readText = File.ReadAllLines(@"D:\3sem\OOP\13lab\TMALog.txt");
            foreach (string str in readText)
            {
                string str2 = str.Substring(str.IndexOf("2017") + 5);
                string[] str3 = str2.Split(':');

                string time = Convert.ToString(DateTime.Now);
                time = time.Substring(time.IndexOf("2017") + 5);
                string[] time2 = time.Split(':');

                if(Convert.ToInt32(str3[0]) - 1 == Convert.ToInt32(time2[0]))
                {
                    Console.WriteLine(str);
                }
            }
        }
    }
    class TMADiskInfo
    {

        public event Info Write;
        DriveInfo D = new DriveInfo(@"D:\");
        DriveInfo C = new DriveInfo(@"C:\");

        public void PrintAccessMemory()
        {
            Console.WriteLine("Свободного места на диске D: " + D.TotalFreeSpace);
            string str = "PrintAccessMemory is called!!! Time:" + DateTime.Now;
            Write(str);
        }
        public void PrintFileSystem()
        {
            Console.WriteLine("Формат ФС:" + D.DriveFormat);
            Console.WriteLine("Тип диска:" + D.DriveType);
            string str = "PrintFileSystem is called!!! Time: " + DateTime.Now;
            Write(str);
        }
        public void PrintInfoAboutDisks()
        {
            Console.WriteLine("Информация о диске C");
            Console.WriteLine("Имя: " + C.Name);
            Console.WriteLine("Объём: " + C.TotalSize);
            Console.WriteLine("Доступный объём: " + C.TotalFreeSpace);
            Console.WriteLine("Метка тома: " + C.VolumeLabel);
            Console.WriteLine();
            Console.WriteLine("Информация о диске D");
            Console.WriteLine("Имя: " + D.Name);
            Console.WriteLine("Объём: " + D.TotalSize);
            Console.WriteLine("Доступный объём: " + D.TotalFreeSpace);
            Console.WriteLine("Метка тома: " + D.VolumeLabel);
            string str = "PrintInfoAboutDisks is called!!! Time: " + DateTime.Now;
            Write(str);
        }
    }
    class TMAFileInfo
    {
        public event Info Write;
        FileInfo FI = new FileInfo(@"D:\3sem\OOP\13lab\TMALog.txt");

        public void AllPath()
        {
            Console.WriteLine("Полный путь к файлу:" + FI.FullName);
            string str = "AllPath is called!!! Time:" + DateTime.Now;
            Write(str);
        }
        public void PrintInfoAboutFile()
        {
            Console.WriteLine("Размер: " + FI.Length);
            Console.WriteLine("Расширение: " + FI.Extension);
            Console.WriteLine("Имя: " + FI.Name);
            string str = "PrintInfoAboutFile is called!!! Time:" + DateTime.Now;
            Write(str);
        }
        public void DateCreate()
        {
            Console.WriteLine("Время создания: " + FI.CreationTime);
            string str = "DateCreate is called!!! Time:" + DateTime.Now;
            Write(str);
        }
        
    }
    class TMADirInfo
    {
        public event Info Write;
        DirectoryInfo DI = new DirectoryInfo(@"D:\3sem\OOP\13lab\");
        
        public void CountFiles()
        {
            Console.WriteLine("Количество файлов в директории: " + DI.GetFiles().Length);
            string str = "CountFiles is called!!! Time:" + DateTime.Now;
            Write(str);
        }
        public void DateCreate()
        {
            Console.WriteLine("Дата создания директории: " + DI.CreationTime);
            string str = "HowManyFiles is called!!! Time:" + DateTime.Now;
            Write(str);
        }
        public void CountDirectory()
        {
            Console.WriteLine("Количество поддиректориев: " + DI.GetDirectories().Length);
            string str = "CountDirectory is called!!! Time:" + DateTime.Now;
            Write(str);
        }
        public void ListParentDir()
        {
            Console.WriteLine("Список родительский директориев:");
            var dirs = DI.Parent.GetDirectories();
            foreach(var info in dirs)
            {
                Console.WriteLine(info.Name);
            }
            string str = "ListParentDir is called!!! Time:" + DateTime.Now;
            Write(str);
        }
    }
    class TMAFileManager
    {
        public event Info Write;

        public void Exercise1()
        {
            Console.WriteLine("Список файлов и каталогов диска D:");
            DirectoryInfo DI = new DirectoryInfo(@"D:\3sem");
            var dirs = DI.GetDirectories();
            var files = DI.GetFiles();

            Directory.CreateDirectory(@"D:\3sem\TMAInspect");
            if (!File.Exists(@"D:\3sem\TMAInspect\TMAdirinfo.txt"))
            {
                File.Create(@"D:\3sem\TMAInspect\TMAdirinfo.txt");
            }

            StreamWriter sw = new StreamWriter(@"D:\3sem\TMAInspect\TMAdirinfo.txt");
                        
            
            foreach (var info in dirs)
            {
                Console.WriteLine(info.Name);
                sw.WriteLine(info.Name);
            }
            Console.WriteLine();
            foreach (var info in files)
            {
                Console.WriteLine(info.Name);
                sw.WriteLine(info.Name);
            }
            sw.Close();

            try
            {
                File.Copy(@"D:\3sem\TMAInspect\TMAdirinfo.txt", @"D:\3sem\TMAInspect\TMAdirsinfo.txt");
                File.Delete(@"D:\3sem\TMAInspect\TMAdirinfo.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            string str = "Exercise1 is called!!! Time:" + DateTime.Now;
            Write(str);
        } 
        public void Exercise2()
        {
            Directory.CreateDirectory(@"D:\3sem\TMAFiles");
            DirectoryInfo FI = new DirectoryInfo(@"D:\3sem\TMAInspect");
            var files = FI.GetFiles();
            foreach(var info in files)
            {
                if(info.Extension == ".bin")
                {
                    if (!File.Exists($@"D:\3sem\TMAFiles\{info.Name}"))
                    {
                        File.Copy($@"D:\3sem\TMAInspect\{info.Name}", $@"D:\3sem\TMAFiles\{info.Name}");
                    }
                }               
            }

            string sourceDirectory = @"D:\3sem\TMAFiles";
            string destinationDirectory = @"D:\3sem\TMAInspect";

            try
            {
                Directory.Move(sourceDirectory, destinationDirectory);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            string str = "Exercise2 is called!!! Time:" + DateTime.Now;
            Write(str);
        }
        public void ArchiveFiles()
        {
            try
            {
                string startPath = @"D:\3sem\TMAFiles";
                string zipPath = @"D:\3sem\result.zip";
                string extractPath = @"D:\3sem\";

                ZipFile.CreateFromDirectory(startPath, zipPath);
                ZipFile.ExtractToDirectory(zipPath, extractPath);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            string str = "ArchiveFiles is called!!! Time:" + DateTime.Now;
            Write(str);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TMALog log = new TMALog();

                TMADiskInfo d = new TMADiskInfo();
                d.Write += log.Write;
                d.PrintAccessMemory();
                d.PrintFileSystem();
                d.PrintInfoAboutDisks();

                TMAFileInfo f = new TMAFileInfo();
                f.Write += log.Write;
                f.AllPath();
                f.PrintInfoAboutFile();
                f.DateCreate();

                TMADirInfo i = new TMADirInfo();
                i.Write += log.Write;
                i.CountFiles();
                i.DateCreate();
                i.CountDirectory();
                i.ListParentDir();

                TMAFileManager m = new TMAFileManager();
                m.Write += log.Write;
                m.Exercise1();
                m.Exercise2();
                m.ArchiveFiles();
                Console.WriteLine();

                log.Count();
                log.Exercise6();                
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("This file not exist");
            }
            Console.ReadKey();
        }
    }
}
