using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SabertoothServer.Globals;
using static System.Environment;
using static System.Convert;

namespace SabertoothServer
{
    public class Instance
    {
        public string Name { get; set; }
        public int CurrentWave { get; set; }
        public int TotalWaves { get; set; }
        public int TotalWaveNpcs { get; set; }
        public int WaveNpcsKilled { get; set; }

        public bool[] PlayerReady = new bool[MAX_PARTY];
        public int NextWaveTime;

        public Player[] iPlayer = new Player[MAX_PARTY];
        public Map iMap = new Map();

        public Instance()
        {
            Name = "Zombies";
            CreateInstanceMap();
            CreateNpcs(iMap);
        }

        private void CreateInstanceMap()
        {
            iMap.Name = "Zombies";
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

        private void CreateNpcs(Map map)
        {
            for (int i = 0; i < MAX_NPCS; i++)
            {
                map.m_MapNpc[i].Name = "Zombie";
                map.m_MapNpc[i].Sprite = 4;
                map.m_MapNpc[i].Health = 100;
                map.m_MapNpc[i].MaxHealth = 100;
                map.m_MapNpc[i].Behavior = (int)BehaviorType.Aggressive;
                map.m_MapNpc[i].Range = 50;
                map.m_MapNpc[i].Damage = 50;
                map.m_MapNpc[i].SpawnTime = 1000;
                map.m_MapNpc[i].X = 10;
                map.m_MapNpc[i].Y = 10;
            }

            for (int i = 0; i < MAX_MAP_POOL_NPCS; i++)
            {
                map.r_MapNpc[i].Name = "Zombie";
                map.r_MapNpc[i].Sprite = 4;
                map.r_MapNpc[i].Health = 100;
                map.r_MapNpc[i].MaxHealth = 100;
                map.r_MapNpc[i].Behavior = (int)BehaviorType.Aggressive;
                map.r_MapNpc[i].Range = 50;
                map.r_MapNpc[i].Damage = 50;
                map.r_MapNpc[i].SpawnTime = 1000;
                map.r_MapNpc[i].X = 10;
                map.r_MapNpc[i].Y = 10;
            }
        }

        public void PlayerJoinInstance(Player player)
        {
            int i = OpenInstanceSlot();
            if (i < MAX_PARTY)
            {
                iPlayer[i] = new Player();
                iPlayer[i] = player;
                iPlayer[i].iPoints = 0;
                iPlayer[i].iKills = 0;
            }
        }

        private int TotalPlayers()
        {
            int n = 0;
            for (int i = 0; i < MAX_PARTY; i++)
            {
                if (iPlayer[i].Name != null)
                {
                    n += 1;
                }
            }
            return n;
        }

        private int OpenInstanceSlot()
        {
            for (int i = 0; i < MAX_PARTY; i++)
            {
                if (iPlayer[i].Name == null)
                {
                    return i;
                }
            }
            return 4;
        }
    }
}
