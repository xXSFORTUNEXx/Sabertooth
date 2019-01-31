using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SabertoothServer.Globals;
using static System.Environment;
using static System.Convert;
using Lidgren.Network;

namespace SabertoothServer
{
    public class Instance
    {
        public string Name { get; set; }

        public static Map iMap = new Map();

        public Instance()
        {
            Name = "Instance Test";
            CreateInstanceMap();
        }

        private void CreateInstanceMap()
        {
            iMap.Name = "Instance Test";
            iMap.Id = 10;
            iMap.TopMap = 0;
            iMap.BottomMap = 0;
            iMap.LeftMap = 0;
            iMap.RightMap = 0;
            iMap.Revision = 0;
            iMap.Brightness = 0;
            iMap.IsInstance = true;

            for (int i = 0; i < 10; i++)
            {
                iMap.m_MapNpc[i] = new MapNpc("None", 0, 0, 0);
            }

            for (int i = 0; i < 20; i++)
            {
                iMap.r_MapNpc[i] = new MapNpc("None", 0, 0, 0);
            }

            for (int i = 0; i < 20; i++)
            {
                iMap.m_MapItem[i] = new MapItem("None", 0, 0, 0);
            }

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    iMap.Ground[x, y] = new Tile();
                    iMap.Mask[x, y] = new Tile();
                    iMap.Fringe[x, y] = new Tile();
                    iMap.MaskA[x, y] = new Tile();
                    iMap.FringeA[x, y] = new Tile();

                    iMap.Ground[x, y].TileX = 0;
                    iMap.Ground[x, y].TileY = 0;
                    iMap.Ground[x, y].TileW = 32;
                    iMap.Ground[x, y].TileH = 32;
                    iMap.Ground[x, y].Tileset = 0;
                    iMap.Ground[x, y].Type = (int)TileType.None;
                    iMap.Ground[x, y].SpawnNum = 0;
                    iMap.Ground[x, y].SpawnAmount = 0;

                    iMap.Mask[x, y].TileX = 0;
                    iMap.Mask[x, y].TileY = 0;
                    iMap.Mask[x, y].TileW = 32;
                    iMap.Mask[x, y].TileH = 32;
                    iMap.Mask[x, y].Tileset = 0;

                    iMap.Fringe[x, y].TileX = 0;
                    iMap.Fringe[x, y].TileY = 0;
                    iMap.Fringe[x, y].TileW = 0;
                    iMap.Fringe[x, y].TileH = 0;
                    iMap.Fringe[x, y].Tileset = 0;

                    iMap.MaskA[x, y].TileX = 0;
                    iMap.MaskA[x, y].TileY = 0;
                    iMap.MaskA[x, y].TileW = 0;
                    iMap.MaskA[x, y].TileH = 0;
                    iMap.MaskA[x, y].Tileset = 0;

                    iMap.FringeA[x, y].TileX = 0;
                    iMap.FringeA[x, y].TileY = 0;
                    iMap.FringeA[x, y].TileW = 0;
                    iMap.FringeA[x, y].TileH = 0;
                    iMap.FringeA[x, y].Tileset = 0;
                }
            }
        }
    }
}
