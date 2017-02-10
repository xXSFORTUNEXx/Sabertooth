    class UpperMap : Drawable
    {
        VertexArray f_vertices = new VertexArray();
        VertexArray f2_vertices = new VertexArray();
        Texture TileSet = new Texture("Resources/Tilesets/1.png");

        public UpperMap()
        {
            f_vertices.PrimitiveType = PrimitiveType.Quads;
            f_vertices.Resize(50 * 50 * 32);
            f2_vertices.PrimitiveType = PrimitiveType.Quads;
            f2_vertices.Resize(50 * 50 * 32);
        }

        public void Load(Map c_Map)
        {
            if (c_Map == null) { return; }

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    int fx = (x * 32);
                    int fy = (y * 32);
                    int index = (x + y * 50) * 4;

                    int tx = (c_Map.Fringe[x, y].tileX);
                    int ty = (c_Map.Fringe[x, y].tileY);
                    int w = (c_Map.Fringe[x, y].tileW);
                    int h = (c_Map.Fringe[x, y].tileH);

                    f_vertices[(uint)index + 0] = new Vertex(new Vector2f(fx, fy), new Vector2f(tx, ty));
                    f_vertices[(uint)index + 1] = new Vertex(new Vector2f(fx + w, fy), new Vector2f(tx + w, ty));
                    f_vertices[(uint)index + 2] = new Vertex(new Vector2f(fx + w, fy + h), new Vector2f(tx + w, ty + h));
                    f_vertices[(uint)index + 3] = new Vertex(new Vector2f(fx, fy + h), new Vector2f(tx, ty + h));

                    int mx = (c_Map.FringeA[x, y].tileX);
                    int my = (c_Map.FringeA[x, y].tileY);
                    int mw = (c_Map.FringeA[x, y].tileW);
                    int mh = (c_Map.FringeA[x, y].tileH);

                    f2_vertices[(uint)index + 0] = new Vertex(new Vector2f(fx, fy), new Vector2f(mx, my));
                    f2_vertices[(uint)index + 1] = new Vertex(new Vector2f(fx + mw, fy), new Vector2f(mx + mw, my));
                    f2_vertices[(uint)index + 2] = new Vertex(new Vector2f(fx + mw, fy + mh), new Vector2f(mx + mw, my + mh));
                    f2_vertices[(uint)index + 3] = new Vertex(new Vector2f(fx, fy + mh), new Vector2f(mx, my + mh));
                }
            }
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            states.Texture = TileSet;

            target.Draw(f_vertices, states);
            target.Draw(f2_vertices, states);
        }
    }

    class LowerMap : Drawable
    {
        VertexArray g_vertices = new VertexArray();
        VertexArray m_vertices = new VertexArray();
        VertexArray m2_vertices = new VertexArray();
        RenderStates[] rStates = new RenderStates[3];
        Map l_Map;
        const int tileSets = 2;
        Texture[] TileSet = new Texture[tileSets];

        public LowerMap()
        {
            g_vertices.PrimitiveType = PrimitiveType.Quads;
            g_vertices.Resize(50 * 50 * 32);
            m_vertices.PrimitiveType = PrimitiveType.Quads;
            m_vertices.Resize(50 * 50 * 32);
            m2_vertices.PrimitiveType = PrimitiveType.Quads;
            m2_vertices.Resize(50 * 50 * 32);
            rStates[0] = new RenderStates();
            rStates[1] = new RenderStates();
            rStates[2] = new RenderStates();
            for (int i = 0; i < tileSets; i++)
            {
                TileSet[i] = new Texture("Resources/Tilesets/" + (i + 1) + ".png");
            }
        }

        public void Load(Map c_Map)
        {
            l_Map = c_Map;

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    int fx = (x * 32);
                    int fy = (y * 32);
                    int index = (x + y * 50) * 4;
                    int tx = (l_Map.Ground[x, y].tileX);
                    int ty = (l_Map.Ground[x, y].tileY);
                    int w = (l_Map.Ground[x, y].tileW);
                    int h = (l_Map.Ground[x, y].tileH);
                    rStates[0].Texture = TileSet[c_Map.Ground[x, y].Tileset];
                    g_vertices[(uint)index + 0] = new Vertex(new Vector2f(fx, fy), new Vector2f(tx, ty));
                    g_vertices[(uint)index + 1] = new Vertex(new Vector2f(fx + w, fy), new Vector2f(tx + w, ty));
                    g_vertices[(uint)index + 2] = new Vertex(new Vector2f(fx + w, fy + h), new Vector2f(tx + w, ty + h));
                    g_vertices[(uint)index + 3] = new Vertex(new Vector2f(fx, fy + h), new Vector2f(tx, ty + h));

                    if (c_Map.Ground[x, y].Tileset == 1)
                    {
                        Console.WriteLine("Break");
                    }

                    int mx = (l_Map.Mask[x, y].tileX);
                    int my = (l_Map.Mask[x, y].tileY);
                    int mw = (l_Map.Mask[x, y].tileW);
                    int mh = (l_Map.Mask[x, y].tileH);
                    rStates[1].Texture = TileSet[c_Map.Mask[x, y].Tileset];
                    m_vertices[(uint)index + 0] = new Vertex(new Vector2f(fx, fy), new Vector2f(mx, my));
                    m_vertices[(uint)index + 1] = new Vertex(new Vector2f(fx + mw, fy), new Vector2f(mx + mw, my));
                    m_vertices[(uint)index + 2] = new Vertex(new Vector2f(fx + mw, fy + mh), new Vector2f(mx + mw, my + mh));
                    m_vertices[(uint)index + 3] = new Vertex(new Vector2f(fx, fy + mh), new Vector2f(mx, my + mh));                    

                    int m2x = (l_Map.MaskA[x, y].tileX);
                    int m2y = (l_Map.MaskA[x, y].tileY);
                    int m2w = (l_Map.MaskA[x, y].tileW);
                    int m2h = (l_Map.MaskA[x, y].tileH);
                    rStates[2].Texture = TileSet[c_Map.MaskA[x, y].Tileset];
                    m2_vertices[(uint)index + 0] = new Vertex(new Vector2f(fx, fy), new Vector2f(m2x, m2y));
                    m2_vertices[(uint)index + 1] = new Vertex(new Vector2f(fx + m2w, fy), new Vector2f(m2x + m2w, m2y));
                    m2_vertices[(uint)index + 2] = new Vertex(new Vector2f(fx + m2w, fy + m2h), new Vector2f(m2x + m2w, m2y + m2h));
                    m2_vertices[(uint)index + 3] = new Vertex(new Vector2f(fx, fy + m2h), new Vector2f(m2x, m2y + m2h));                    
                }
            }
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            states.Texture = rStates[0].Texture;
            target.Draw(g_vertices, states);

            states.Texture = rStates[1].Texture;
            target.Draw(m_vertices, states);

            states.Texture = rStates[2].Texture;
            target.Draw(m2_vertices, states);
        }
    }