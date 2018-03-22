using System;
using System.Diagnostics;
using CopyAPI;
namespace osuCopier
{
    public class CLI
    {
        string InDir;
        string outDir;

        public CLI()
        {
            Configure();
            Extract();
            Open();
            Console.ReadLine();
        }

        void Configure()
        {
            Console.WriteLine("provide osu's ROOT directory, this is just the folder with the osu.exe in it");
            InDir = Console.ReadLine();
            Console.WriteLine("provide the directory to spit out to, leave a * to indicate spitting in the program's directory");
            outDir = Console.ReadLine();
        }

        void Extract()
        {
            OsuLib.Perform(InDir, outDir, true);
        }

        void Open()
        {
            Console.WriteLine("Opening the folder in a seperate window");
            Console.WriteLine("Shieyn 2018, code available on github");
            Process.Start(outDir);
        }
    }
}
