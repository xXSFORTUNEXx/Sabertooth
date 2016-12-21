        ///Regular check methods
        //Run our AI script to get the npc's moving and killing shit
        void CheckNpcAI(NetServer s_Server)
        {
            if (TickCount - aiTick > aiTime)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int n = 0; n < 10; n++)
                    {
                        if (s_Map[i].mapNpc[n].isSpawned == true)
                        {
                            int canMove = RND.Next(0, 100);
                            int dir = RND.Next(0, 3);

                            s_Map[i].mapNpc[n].NpcAI(canMove, dir, s_Map[i]);

                            if (s_Map[i].mapNpc[n].didMove == true)
                            {
                                s_Map[i].mapNpc[n].didMove = false;
                                
                                for (int p = 0; p < 5; p++)
                                {
                                    if (s_Player[p].Connection != null && s_Player[p].Map == i)
                                    {
                                        handleData.SendMapNpcData(s_Server, s_Player[p].Connection, s_Map[i], n);
                                    }
                                }
                            }
                        }
                    }
                }
                aiTick = TickCount;
            }
        }

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

public void NpcAI(int canMove, int dir, Map movementMap)
{
    //check if we moved
    didMove = false;

    if (canMove > 80)
    {
        //check directions
        switch (dir)
        {
            //down
            case (int)Directions.Down:
                //check if they are going out of bounds
                if (Y < 49)
                {
                    //Check to see if the next tile is blocked
                    if (movementMap.Ground[X, Y + 1].Type == (int)TileType.Blocked)
                    {
                        //just change the direction and exit
                        Direction = (int)Directions.Down;
                        didMove = true;
                        return;
                    }
                    //move the npcs over
                    Y += 1;
                    Direction = (int)Directions.Down;
                    didMove = true;
                }
                break;

            case (int)Directions.Left:
                if (X > 1)
                {
                    if (movementMap.Ground[X - 1, Y].Type == (int)TileType.Blocked)
                    {
                        Direction = (int)Directions.Left;
                        didMove = true;
                        return;
                    }
                    X -= 1;
                    Direction = (int)Directions.Left;
                    didMove = true;
                }
                break;

            case (int)Directions.Right:
                if (X < 49)
                {
                    if (movementMap.Ground[X + 1, Y].Type == (int)TileType.Blocked)
                    {
                        Direction = (int)Directions.Right;
                        didMove = true;
                        return;
                    }
                    X += 1;
                    Direction = (int)Directions.Right;
                    didMove = true;
                }
                break;

            case (int)Directions.Up:
                if (Y > 1)
                {
                    if (movementMap.Ground[X, Y - 1].Type == (int)TileType.Blocked)
                    {
                        Direction = (int)Directions.Up;
                        didMove = true;
                        return;
                    }
                    Y -= 1;
                    Direction = (int)Directions.Up;
                    didMove = true;
                }
                break;
        }

        if (didMove == true)
        {
            if (Step == 3) { Step = 0; } else { Step += 1; }
        }
    }
}