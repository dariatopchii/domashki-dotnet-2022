using System;

namespace dotnet_2022_study
{
    public class Program
    {
        static void Main()
        {

            Console.WriteLine("Welcome to the file manager. Type 'help' if you want to see the list of commands");
            var cmmd = Console.ReadLine();
            while (cmmd != "quit"){
                string[] commd = cmmd?.Replace(@"\s+", " ").Split(" ");
                switch (commd?[0])
                {
                    case "mkdir":
                        if (commd.Length > 1) FileManager.CreateDirectory(commd[1]);
                        break;
                    case "rmdir" :
                        if (commd.Length > 1) FileManager.DeleteDirectory(commd[1]);
                        break;
                    case "rndir" :
                        if (commd.Length > 2) FileManager.RenameDirectory(commd[1], commd[2]);
                        break;
                    case "cd": FileManager.ChangeDirectory(commd);
                        break;
                    case "newfl" :
                        if (commd.Length > 1) FileManager.CreateFile(commd[1]);
                        break;  
                    case "rnfl" :
                        if (commd.Length > 2) FileManager.RenameFile(commd[1], commd[2]);
                        break;
                    case "del"  :
                        if (commd.Length > 1) FileManager.DeleteFile(commd[1]);
                        break;
                    case "ls" : FileManager.DirectoryInfo(commd);
                        break;
                    case "sub" :
                        if (commd.Length > 2) FileManager.FindSubstring(commd[1], commd[2]);
                        break;
                    case "print" :
                        if (commd.Length > 1) FileManager.PrintFile(commd[1]);
                        break;
                    case "help": FileManager.Help();
                        break;
                    default: Console.WriteLine("wrong input");
                        break;
                }
                Console.Write(FileManager.Path + "> ");
                cmmd = Console.ReadLine();
            }
        }

    }
}
