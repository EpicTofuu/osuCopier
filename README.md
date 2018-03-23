# osu!Copier
An application that can copy all music in your osu! folder to any other specified folder

## Use
Ever wanted to listen to your osu! library but hate opening the client in the background? Simply use osu!Copier. With osu!Copier, you can copy all of your osu! music to any folder you want so you can listen to your entire osu! library using whatever music player you would like to use.

I'm not all that sure about how building works, so just download visual studio (or some other C# compiler) and build everything yourself.

Simply follow the on screen instructions to copy all of your songs.

osu!Copier makes sure to copy over only new songs so you can constantly update a song library without having to wait for osu!Copier to recopy and overwrite your entire 3000 song library every time you only want to copy the 8 latest maps you've downloaded.

osu!Copier is in no way affiliated with osu! or ppy. It is simply a small project I made for my own purposes.

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
