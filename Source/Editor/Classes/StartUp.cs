using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Editor.Classes
{
    class StartUp
    {
        // Run to check how many lines of code my project has
        //Ctrl+Shift+F, use regular expression, ^(?([^\r\n])\s)*[^\s+?/]+[^\n]*$

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        [STAThread]
        static void Main(string[] args) //main entry point
        {
            var handle = GetConsoleWindow();

            Console.Title = "Sabertooth Editor Suite";    //name the console window, eventually we wont have it anymore but until then its a debug gold
            Editor editor = new Editor();  //Create class for map editor

            Console.WriteLine("Checking for existing map...");
            if (!File.Exists("Maps/Map.bin"))   //check and see if the map file exists
            {
                DefaultMap(editor.e_Map);  //call the default map method
            }
            Console.WriteLine("Loading map...");
            editor.e_Map.LoadDeafultMap();    //load the map.bin from /maps/

            Console.WriteLine("Checking for npc directory...");
            if (!Directory.Exists("NPCS"))
            {
                Directory.CreateDirectory("NPCS");
            }
            Thread.Sleep(500);
            //ShowWindow(handle, SW_HIDE);
            editor.EditorLoop();  //like with any other game lets start the loop
        }

        static void DefaultMap(Map newMap)  //void for creating a default map when one isnt present in our /maps/ directory
        {
            Console.WriteLine("Creating default map...");   //let the debugging know whats the scoop

            newMap.Name = "Home";   //name our map

            //Create all of the tiles with new classes and make sure they all have values of 0
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    newMap.Ground[x, y] = new Tile();
                    newMap.Mask[x, y] = new Tile();
                    newMap.Fringe[x, y] = new Tile();
                    newMap.MaskA[x, y] = new Tile();
                    newMap.FringeA[x, y] = new Tile();

                    //Ground
                    newMap.Ground[x, y].tileX = 0;
                    newMap.Ground[x, y].tileY = 0;
                    newMap.Ground[x, y].tileW = 0;
                    newMap.Ground[x, y].tileH = 0;
                    newMap.Ground[x, y].Tileset = 0;
                    newMap.Ground[x, y].type = 0;
                    newMap.Ground[x, y].spawnNum = 0;
                    //Mask
                    newMap.Mask[x, y].tileX = 0;
                    newMap.Mask[x, y].tileY = 0;
                    newMap.Mask[x, y].tileW = 0;
                    newMap.Mask[x, y].tileH = 0;
                    newMap.Mask[x, y].Tileset = 0;
                    //Fringe
                    newMap.Fringe[x, y].tileX = 0;
                    newMap.Fringe[x, y].tileY = 0;
                    newMap.Fringe[x, y].tileW = 0;
                    newMap.Fringe[x, y].tileH = 0;
                    newMap.Fringe[x, y].Tileset = 0;

                    newMap.MaskA[x, y].tileX = 0;
                    newMap.MaskA[x, y].tileY = 0;
                    newMap.MaskA[x, y].tileW = 0;
                    newMap.MaskA[x, y].tileH = 0;
                    newMap.MaskA[x, y].Tileset = 0;

                    newMap.FringeA[x, y].tileX = 0;
                    newMap.FringeA[x, y].tileY = 0;
                    newMap.FringeA[x, y].tileW = 0;
                    newMap.FringeA[x, y].tileH = 0;
                    newMap.FringeA[x, y].Tileset = 0;
                }
            }

            SaveMap(newMap);   //save what we have made so it can be loaded once we are done here
        }

        static void SaveMap(Map mapNum)
        {
            FileStream fileStream = File.OpenWrite("Maps/Map.bin");
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);

            binaryWriter.Write(mapNum.Name);

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    //Ground
                    binaryWriter.Write(mapNum.Ground[x, y].tileX);
                    binaryWriter.Write(mapNum.Ground[x, y].tileY);
                    binaryWriter.Write(mapNum.Ground[x, y].tileW);
                    binaryWriter.Write(mapNum.Ground[x, y].tileH);
                    binaryWriter.Write(mapNum.Ground[x, y].Tileset);
                    binaryWriter.Write(mapNum.Ground[x, y].type);
                    binaryWriter.Write(mapNum.Ground[x, y].spawnNum);
                    //Mask
                    binaryWriter.Write(mapNum.Mask[x, y].tileX);
                    binaryWriter.Write(mapNum.Mask[x, y].tileY);
                    binaryWriter.Write(mapNum.Mask[x, y].tileW);
                    binaryWriter.Write(mapNum.Mask[x, y].tileH);
                    binaryWriter.Write(mapNum.Mask[x, y].Tileset);
                    //Fringe
                    binaryWriter.Write(mapNum.Fringe[x, y].tileX);
                    binaryWriter.Write(mapNum.Fringe[x, y].tileY);
                    binaryWriter.Write(mapNum.Fringe[x, y].tileW);
                    binaryWriter.Write(mapNum.Fringe[x, y].tileH);
                    binaryWriter.Write(mapNum.Fringe[x, y].Tileset);

                    binaryWriter.Write(mapNum.MaskA[x, y].tileX);
                    binaryWriter.Write(mapNum.MaskA[x, y].tileY);
                    binaryWriter.Write(mapNum.MaskA[x, y].tileW);
                    binaryWriter.Write(mapNum.MaskA[x, y].tileH);
                    binaryWriter.Write(mapNum.MaskA[x, y].Tileset);

                    binaryWriter.Write(mapNum.FringeA[x, y].tileX);
                    binaryWriter.Write(mapNum.FringeA[x, y].tileY);
                    binaryWriter.Write(mapNum.FringeA[x, y].tileW);
                    binaryWriter.Write(mapNum.FringeA[x, y].tileH);
                    binaryWriter.Write(mapNum.FringeA[x, y].Tileset);
                }
            }

            binaryWriter.Flush();
            binaryWriter.Close();
        }
    }
}
