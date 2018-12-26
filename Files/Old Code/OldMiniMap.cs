            int minX = (c_Player.X + 12) - 12;
            int minY = (c_Player.Y + 9) - 9;
            int maxX = (c_Player.X + 12) + 13;
            int maxY = (c_Player.Y + 9) + 11;
            m_Map = new VertexArray();
            m_Map.PrimitiveType = PrimitiveType.Quads;
            m_Map.Resize((uint)maxX * (uint)maxY * 12);

            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    if (x > 0 && y > 0 && x < 50 && y < 50)
                    {
                        int fx = (x * 12) - (minX * 12) + 500;
                        int fy = (y * 12) - (minY * 12);
                        int index = (x + y * (maxX + maxY)) * 4;
                        int tx = 0;
                        int ty = 0;
                        int w = 12;
                        int h = 12;

                        if (c_Map.Ground[x, y].Type == (int)TileType.Blocked)
                        {
                            m_Map[(uint)index + 0] = new Vertex(new Vector2f(fx, fy), new Vector2f(tx, ty));
                            m_Map[(uint)index + 1] = new Vertex(new Vector2f(fx + w, fy), new Vector2f(tx + w, ty));
                            m_Map[(uint)index + 2] = new Vertex(new Vector2f(fx + w, fy + h), new Vector2f(tx + w, ty + h));
                            m_Map[(uint)index + 3] = new Vertex(new Vector2f(fx, fy + h), new Vector2f(tx, ty + h));
                        }
                        if (c_Map.Ground[x, y].Type == (int)TileType.NpcSpawn)
                        {
                            m_Map[(uint)index + 0] = new Vertex(new Vector2f(fx, fy), Color.Blue);
                            m_Map[(uint)index + 1] = new Vertex(new Vector2f(fx + w, fy), Color.Transparent);
                            m_Map[(uint)index + 2] = new Vertex(new Vector2f(fx + w, fy + h), Color.Blue);
                            m_Map[(uint)index + 3] = new Vertex(new Vector2f(fx, fy + h), Color.Transparent);
                        }
                        if (c_Map.Ground[x, y].Type == (int)TileType.SpawnPool)
                        {
                            m_Map[(uint)index + 0] = new Vertex(new Vector2f(fx, fy), Color.Magenta);
                            m_Map[(uint)index + 1] = new Vertex(new Vector2f(fx + w, fy), Color.Transparent);
                            m_Map[(uint)index + 2] = new Vertex(new Vector2f(fx + w, fy + h), Color.Magenta);
                            m_Map[(uint)index + 3] = new Vertex(new Vector2f(fx, fy + h), Color.Transparent);
                        }
                        if (c_Map.Ground[x, y].Type == (int)TileType.NpcAvoid)
                        {
                            m_Map[(uint)index + 0] = new Vertex(new Vector2f(fx, fy), Color.Black);
                            m_Map[(uint)index + 1] = new Vertex(new Vector2f(fx + w, fy), Color.Transparent);
                            m_Map[(uint)index + 2] = new Vertex(new Vector2f(fx + w, fy + h), Color.Black);
                            m_Map[(uint)index + 3] = new Vertex(new Vector2f(fx, fy + h), Color.Transparent);
                        }
                        tx = 12;
                        for (int i = 0; i < 20; i++)
                        {
                            if (i < 10)
                            {
                                if (c_Map.m_MapNpc[i].IsSpawned)
                                {
                                    if (c_Map.m_MapNpc[i].X == x && c_Map.m_MapNpc[i].Y == y)
                                    {
                                        if (c_Map.m_MapNpc[i].Behavior == (int)BehaviorType.ShopOwner || c_Map.m_MapNpc[i].Behavior == (int)BehaviorType.Friendly || c_Map.m_MapNpc[i].Behavior == (int)BehaviorType.Passive) { tx = 24; }
                                        m_Map[(uint)index + 0] = new Vertex(new Vector2f(fx, fy), Color.Yellow, new Vector2f(tx, ty));
                                        m_Map[(uint)index + 1] = new Vertex(new Vector2f(fx + w, fy), Color.Yellow, new Vector2f(tx + w, ty));
                                        m_Map[(uint)index + 2] = new Vertex(new Vector2f(fx + w, fy + h), Color.Yellow, new Vector2f(tx + w, ty + h));
                                        m_Map[(uint)index + 3] = new Vertex(new Vector2f(fx, fy + h), Color.Yellow, new Vector2f(tx, ty + h));
                                    }
                                }
                            }
                            if (c_Map.r_MapNpc[i].IsSpawned)
                            {
                                if (c_Map.r_MapNpc[i].X == x && c_Map.r_MapNpc[i].Y == y)
                                {
                                    if (c_Map.r_MapNpc[i].Behavior == (int)BehaviorType.ShopOwner || c_Map.r_MapNpc[i].Behavior == (int)BehaviorType.Friendly || c_Map.r_MapNpc[i].Behavior == (int)BehaviorType.Passive) { tx = 24; }
                                    m_Map[(uint)index + 0] = new Vertex(new Vector2f(fx, fy), Color.Yellow, new Vector2f(tx, ty));
                                    m_Map[(uint)index + 1] = new Vertex(new Vector2f(fx + w, fy), Color.Yellow, new Vector2f(tx + w, ty));
                                    m_Map[(uint)index + 2] = new Vertex(new Vector2f(fx + w, fy + h), Color.Yellow, new Vector2f(tx + w, ty + h));
                                    m_Map[(uint)index + 3] = new Vertex(new Vector2f(fx, fy + h), Color.Yellow, new Vector2f(tx, ty + h));
                                }
                            }
                            if (c_Map.m_MapItem[i].IsSpawned)
                            {
                                if (c_Map.m_MapItem[i].X == x && c_Map.m_MapItem[i].Y == y)
                                {
                                    tx = 48;
                                    m_Map[(uint)index + 0] = new Vertex(new Vector2f(fx, fy), Color.Magenta, new Vector2f(tx, ty));
                                    m_Map[(uint)index + 1] = new Vertex(new Vector2f(fx + w, fy), Color.Magenta, new Vector2f(tx + w, ty));
                                    m_Map[(uint)index + 2] = new Vertex(new Vector2f(fx + w, fy + h), Color.Magenta, new Vector2f(tx + w, ty + h));
                                    m_Map[(uint)index + 3] = new Vertex(new Vector2f(fx, fy + h), Color.Magenta, new Vector2f(tx, ty + h));
                                }
                            }
                        }
                        if ((c_Player.X + 12) == x && (c_Player.Y + 9) == y)
                        {
                            tx = 60;
                            m_Map[(uint)index + 0] = new Vertex(new Vector2f(fx, fy), Color.White, new Vector2f(tx, ty));
                            m_Map[(uint)index + 1] = new Vertex(new Vector2f(fx + w, fy), Color.White, new Vector2f(tx + w, ty));
                            m_Map[(uint)index + 2] = new Vertex(new Vector2f(fx + w, fy + h), Color.White, new Vector2f(tx + w, ty + h));
                            m_Map[(uint)index + 3] = new Vertex(new Vector2f(fx, fy + h), Color.White, new Vector2f(tx, ty + h));
                        }
                    }
                }
            }