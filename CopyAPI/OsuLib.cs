using System;
using System.Collections.Generic;
using System.IO;
using BMAPI.v1;

namespace CopyAPI
{
    public static class OsuLib
    {
        /// <summary>
        /// Just call this
        /// </summary>
        /// <param name="osu"></param>
        /// <param name="Out"></param>
        /// <param name="allowConsole"></param>
        /// <returns></returns>
        public static int Perform(string osu, string Out, bool allowConsole)
        {
            if (allowConsole)
                Console.WriteLine("pending approval...");

            if (!File.Exists(Path.Combine(osu, "osu!.exe")))
            {
                if (allowConsole)
                    Console.WriteLine("ERROR: {0} is not a valid path ensure that a osu!.exe can be found", osu);

                return 0;
            }

            Console.WriteLine("SUCCESS: {0} is a valid path", osu);
            Console.WriteLine("Approved!");
            Console.WriteLine("Logging beatmaps...");

            List<Song> songData = new List<Song>();
            songData = logOsu(osu);

            Console.WriteLine("SUCCESS: finished logging {0} beatmaps", songData.Count);
            Console.WriteLine("Initialising migration...");

            foreach (Song s in songData)
            {
                Console.WriteLine(s.beatmap.Title);
            }

            migrate(songData, osu, Out);

            return 1;
        }

        public static string songsDir(string osu)
        {
            return Path.Combine(osu, "Songs");
        }

        public static List <Song> logOsu(string osu)
        {
            List<Song> songs = new List<Song>();

            try
            {
                string workingDir = songsDir(osu);

                foreach (string folder in Directory.EnumerateDirectories(workingDir))
                {
                    foreach (string file in Directory.EnumerateFiles(folder, "*.osu"))
                    {
                        try
                        {
                            Beatmap beatmap = new Beatmap(file);

                            songs.Add(new Song()
                            {
                                beatmap = beatmap,
                                directory = folder
                            });

                            Console.WriteLine("Logged beatmap: {0}", Path.GetFileName (file));
                        }
                        catch
                        {
                            Console.WriteLine("WARNING: Could not log {0}", file);
                        }
                    }
                }

                return songs;
            }
            catch
            {
                throw new Exception("could not log correctly");
            }
        }

        public static int migrate(List <Song> songs, string osu, string Out)
        {
            if (!Directory.Exists(Out))
            {
                Console.WriteLine("ERROR: {0} doesn't exist or it cannot be written to, throwing an exception", Out);
                return 0;
                throw new Exception("out directory doesn't exist or cannot be written to");
            }

            int i = 0;

            foreach (Song s in songs)
            {
                i++;

                string newAudioName;
                string newTitle;

                List<char> BAD_CHARS_LIST = new List<char>();
                BAD_CHARS_LIST.AddRange(Path.GetInvalidFileNameChars());
                BAD_CHARS_LIST.AddRange(Path.GetInvalidPathChars());
                char[] BAD_CHARS_ARRAY = BAD_CHARS_LIST.ToArray();

                newAudioName = string.Concat(s.beatmap.AudioFilename.Split(BAD_CHARS_ARRAY, StringSplitOptions.RemoveEmptyEntries));
                newTitle = string.Concat(s.beatmap.Title.Split(BAD_CHARS_ARRAY, StringSplitOptions.RemoveEmptyEntries));

                string outpath = Path.Combine(Out, newTitle + "(" + newAudioName + ")" + ".mp3");

                //Checks if the original file exists at all
                if (File.Exists(Path.Combine(s.directory, s.beatmap.AudioFilename)))
                {
                    //Checks if the file has already been written
                    if (!File.Exists(outpath))
                    {
                        Console.WriteLine("{1} out of {2} | Writing {0}", s.beatmap.Title, i, songs.Count);
                        File.Copy(Path.Combine(s.directory, newAudioName), outpath, true);
                    }
                    else
                    {
                        Console.WriteLine("skipping {0}, it has already been written", s.beatmap.Title);
                    }
                }
                else
                {
                    Console.WriteLine("WARNING: {0} does not exist despite a .osu pointing to it", s.beatmap.Title);
                }
            }

            Console.WriteLine("Finished!");
            return 1;
        }
    }

    public class Song
    {
        public Beatmap beatmap;
        public string directory;
    }
}
