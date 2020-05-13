using System;
using System.IO;
using System.Linq;

class Renamer
{
    static string Origin = string.Empty;
    static string Destination = string.Empty;
    static int Count = 1;

    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Use: renamer.exe origin destination");
            return;
        }

        Origin = args[0];
        Destination = args[1];

        if (string.IsNullOrEmpty(Origin) || string.IsNullOrEmpty(Destination))
        {
            Console.WriteLine("Wrong parameters..");
            return;
        }

        RenameFiles();
    }

    public static void RenameFiles()
    {
        if (Directory.Exists(Origin) && Directory.Exists(Destination))
        {
            string[] files = Directory.GetFiles(Origin)
                .OrderBy(d => new FileInfo(d).LastWriteTime)
                .ToArray();

            foreach (var f in files)
            {
                string fileExt = f.Substring(f.LastIndexOf('\\') + 1);
                string fileName = Destination + "\\" + Count.ToString().PadLeft(2, '0') + "_" + fileExt;

                File.Move(f, fileName);
                Count++;
            }

            Console.WriteLine("Finished");
        }
    }
}