using System;

namespace dotnet_2022_study
{
    public class Program
    {
        

       static void Main()
        {
            FileManager fileManager = new FileManager();

            Console.WriteLine("Welcome to the file manager. Type 'help' if you want to see the list of commands");
            var cmmd = Console.ReadLine();
            while (cmmd != "quit"){
                string[] commd = cmmd?.Replace(@"\s+", " ").Split(" ");
                switch (commd?[0])
                {
                    case "mkdir":
                        if (commd.Length > 1)
                        {
                            fileManager.CreateDirectory(commd[1]);
                        }

                        break;
                    case "rmdir" :
                        if (commd.Length > 1)
                        {
                            fileManager.DeleteDirectory(commd[1]);
                        }

                        break;
                    case "rndir" :
                        if (commd.Length > 2)
                        {
                            fileManager.RenameDirectory(commd[1], commd[2]);
                        }
                        

                        break;
                    case "cd":
                    {
                        fileManager.ChangeDirectory(commd);
                    }
                        break;
                    case "newfl" :
                        if (commd.Length > 1)
                        {
                            fileManager.CreateFile(commd[1]);
                        }
                        break;  
                    case "rnfl" :
                        if (commd.Length > 2)
                        {
                            fileManager.RenameFile(commd[1], commd[2]);
                        }
                        break;
                    case "del"  :
                        if (commd.Length > 1)
                        {
                            fileManager.DeleteFile(commd[1]);
                        }
                        break;
                    case "ls":
                    {
                        fileManager.DirectoryInfo(commd);
                    }
                        break;
                    case "sub" :
                        if (commd.Length > 2)
                        {
                            fileManager.FindSubstring(commd[1], commd[2]);
                        }
                        break;
                    case "print" :
                        if (commd.Length > 1)
                        {
                            fileManager.PrintFile(commd[1]);
                        }
                        break;
                    case "help":
                    {
                        fileManager.Help();
                    }
                        break;
                    default: Console.WriteLine("wrong input");
                        break;
                }
                Console.Write(fileManager.Path + "> ");
                cmmd = Console.ReadLine();
            }
        }

    }
}
