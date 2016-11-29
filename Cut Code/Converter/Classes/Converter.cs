using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Converter.Classes
{
    class Converter
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.Title = "Converter";
            MapConverter mapConv = new MapConverter();
            CheckDirectories();
            mapConv.ConvertMaps();
        }
        
        static void CheckDirectories()
        {
            if (!Directory.Exists("Maps"))
            {
                Directory.CreateDirectory("Maps");
            }
        }
    }

    class MapConverter
    {
        public Map convertMap = new Map();

        public MapConverter() { }

        public void ConvertMaps()
        {
            Console.WriteLine("Loading old map...");
            convertMap.LoadMap();
            Console.WriteLine("Converting and saving new map...");
            convertMap.SaveMap();
            Console.WriteLine("Map converted and saved successfully. Press any key to continue");
            Console.ReadKey();
        }
    }
}
