using System;
using System.IO;
using System.Linq;

namespace dotnet_2022_study
{
    public class FileManager
    {
        public string Path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        public void CreateFile(string filename)
        {
            if (!File.Exists($@"{Path}\{filename}") && !Directory.Exists(($@"{Path}\{filename}")))
            {
                using (File.Create($@"{Path}\{filename}"))
                {
                }
                Console.WriteLine($"{filename} created in {Path}");
            }
            else
            {
                Console.WriteLine($"{filename} already exists in {Path}");
            }
        }

        public void DeleteFile(string filename)
        {
            var filepath = $@"{Path}\{filename}";
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
                Console.WriteLine($"{filename} deleted in {Path}");
            }
            else
            {
                Console.WriteLine($"{filename} doesn`t exist in {Path}");
            }
        }

        public void RenameFile(string oldname, string newname )
        {
            if (File.Exists($@"{Path}\{oldname}"))
            {
                File.Move($@"{Path}\{oldname}", $@"{Path}\{newname}");
                Console.WriteLine($"{oldname} changed to {newname}");
            }
            else
            {
                Console.WriteLine($"{oldname} file doesn`t exist in {Path}");
            }
        }
        
        
        public void CreateDirectory(string dirname)
        {
            var newDir = $@"{Path}\{dirname}";
            if (!Directory.Exists(newDir) && !File.Exists(newDir))
            {
                Directory.CreateDirectory(newDir);
                Console.WriteLine($" {newDir} created");
            }
            else
            {
                Console.WriteLine($"{newDir} already exists");
            }
           
        }
        
        public void DeleteDirectory(string dirname)
        {
            var dirPath = $@"{Path}\{dirname}";
            if (Directory.Exists(dirPath))
            {
                Directory.Delete(dirPath);
                Console.WriteLine($" {dirPath} deleted");
            }
            else
            {
                Console.WriteLine($"directory {dirPath} doesn`t exist");
            }
        }
        
        public void RenameDirectory(string oldname, string newname)
        {
            var oldpath = $@"{Path}\{oldname}";
            var newpath = $@"{Path}\{newname}";
            if (Directory.Exists(oldpath))
            {
                Directory.Move(oldpath, newpath);
                Console.WriteLine($"{oldname} changed to {newname}");
            }
            else
            {
                Console.WriteLine($" {oldname} directory doesn`t exist");
            }

        }

        public void ChangeDirectory(string[] commline)
        {
            if (commline.Length > 1)
            {
                if (commline[1] == ".." && commline.Length == 3 && Directory.Exists($@"{Path}\{commline[2]}"))
                {
                    Path = $@"{Path}\{commline[2]}";
                }
                else if(commline[1] == ".." && commline.Length <3) 
                {
                    var cut = Path.LastIndexOf("\\");
                    Path = Path.Substring(0, cut);
                    Console.WriteLine(Path);
                }
                else if (Directory.Exists(commline[1]))
                {
                    Path =  commline[1];
                }
                
                else
                {
                    Console.WriteLine("there is no directory with such path");
                }
            }
           
        }

        public void DirectoryInfo(string[] input)
        {
            if (input.Length > 1)
            {
                switch (input[1])
                {
                   case "-s" : Print(Directory.GetFiles(Path).OrderBy(f => new FileInfo(f).Length));
                       break;
                   case "-t" : Print(Directory.GetFiles(Path).OrderBy(f => new FileInfo(f).Extension));
                       break;
                   case "-n": Print(Directory.GetFiles(Path).OrderBy(f => new FileInfo(f).Name));
                       break;
                   case "-h":
                       DirectoryInfo directory = new DirectoryInfo(Path);
                       FileInfo[] file = directory.GetFiles();
                       var filtered = file.Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden));
                       foreach (var f in filtered)
                       {
                          Console.WriteLine(f);
                       }
                       break;
                   case "-a" :  var files = Directory.GetFiles(Path);
                       Console.WriteLine(Path);
                       foreach (var y in files)
                       {
                           var a = new FileInfo(y);
                           Console.WriteLine(
                               $"{y} \n -size: {a.Length} b \n -date created: {a.CreationTime} \n -last accessed: {a.LastAccessTime} \n");
                       }
                       break;
                   default: Console.WriteLine("wrong input");
                       break;
                }
            }
            else
            {
                var files = Directory.GetFileSystemEntries(Path);
                foreach (var y in files)
                {
                    Console.WriteLine(y.Substring(Path.Length));
                }
            }
        }
        
        public void Print(IOrderedEnumerable<string> sorted)
        {
            Console.WriteLine(Path);
            foreach (var s in sorted) Console.WriteLine(s);
        }

        public void FindSubstring(string path, string substring)
        {
            string filepath = $@"{Path}\{path}";
            
            using (StreamReader sr = File.OpenText(filepath))
            {
                string lines = File.ReadAllText((filepath));
                bool isMatch = false;
                
                for (int x = 0; x < lines.Length - 1; x++)
                {
                    if (lines.Contains(substring))
                    {
                        sr.Close();
                        Console.WriteLine("there is a match");
                        isMatch = true;
                        break;
                    }
                }
                if (!isMatch)
                {
                    sr.Close();
                    Console.WriteLine("there is no match");
                }
            }
            
        }

        public void PrintFile(string path)
        {

            using (StreamReader file = new StreamReader($@"{Path}\{path}"))
            {
                string ln;

                while ((ln = file.ReadLine()) != null && ln.Length <= 200)
                {
                    Console.WriteLine(ln);
                }

                if (ln.Length < 200)
                {
                    Console.WriteLine("File has less than 200 symbols");
                }

                file.Close();
            }
        }

        public void Help()
        {
            Console.WriteLine("available commands: \n mkdir - creates directory \n rmdir - deletes directory \n rndir - renames directory \n cd - changes directory \n ls - show all files in directory " +
                              "\n     -a  - shows all files and their properties \n     -s - sorts all files by size \n     -t - sorts all files by extention \n     -h - hides hidden files " +
                              "\n newfl - creates a new file \n del - deletes file \n sub - searches for a substring \n print - prints first 200 symbols  \n quit - quits");
        }
    }
}