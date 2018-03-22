# osu!Copier
An application that can copy all music in your osu! folder to any other specified folder

## Interesting stuff
osu!Copier seperates all the high level code from the low level code into two namespaces, this means that you can use all of the low level code that osu!Copier uses in your own code with relative ease. You will need to use the beatmap API smoogipoo made <a href = "https://github.com/smoogipoo/osu-BMAPI"> found here </a>.

Example <br>
Move all songs from osu! folder to another folder using only one method
```
string InDir = @"C:\Users\Shieyn\AppData\Local\osu!";
string OutDIr = @"C:\osu music"

var result = OsuLib.Perform (InDir, OutDir, true);
if (result == 0)
{
  Console.WriteLine ("something went wrong");
}
```
