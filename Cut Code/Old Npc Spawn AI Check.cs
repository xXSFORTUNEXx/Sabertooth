
        //Check to see if we need to perhaps spawn an npc
        void CheckNPCSpawn(NetServer s_Server)
        {
            //Check for map spawning
            if (TickCount - spawnTick > spawnTime)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int x = 0; x < 50; x++)
                    {
                        for (int y = 0; y < 50; y++)
                        {
                            if (s_Map[i].Ground[x, y].Type == (int)TileType.NPCSpawn)
                            {
                                int npcNum = s_Map[i].Ground[x, y].SpawnNum;

                                if (s_Map[i].mapNpc[npcNum].isSpawned == false)
                                {
                                    s_Map[i].mapNpc[npcNum].X = x;
                                    s_Map[i].mapNpc[npcNum].Y = y;
                                    s_Map[i].mapNpc[npcNum].isSpawned = true;

                                    for (int p = 0; p < 5; p++)
                                    {
                                        if (s_Player[p].Connection != null && i == s_Player[p].Map)
                                        {
                                            handleData.SendMapNpcData(s_Server, s_Player[p].Connection, s_Map[i], npcNum);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            spawnTick = TickCount;
        }

