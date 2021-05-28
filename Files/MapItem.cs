    public class MapItem : Drawable
    {
        #region Properties
        public string Name { get; set; }
        public int Sprite { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Type { get; set; }
        public int AttackSpeed { get; set; }
        public int HealthRestore { get; set; }
        public int ManaRestore { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Stamina { get; set; }
        public int Energy { get; set; }
        public int Value { get; set; }
        public int Price { get; set; }
        public int Rarity { get; set; }
        public int CoolDown { get; set; }
        public int AddMaxHealth { get; set; }
        public int AddMaxMana { get; set; }
        public int BonusXP { get; set; }
        public int SpellNum { get; set; }
        public bool Stackable { get; set; }
        public int MaxStack { get; set; }

        public int ItemNum { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        #endregion

        public bool IsSpawned;
        static int spritePics = Directory.GetFiles("Resources/Items/", "*", SearchOption.TopDirectoryOnly).Length;
        VertexArray itemPic = new VertexArray(PrimitiveType.Quads, 4);
        Texture[] c_ItemSprite = new Texture[spritePics];

        public MapItem()
        {
            for (int i = 0; i < spritePics; i++)
            {
                c_ItemSprite[i] = new Texture("Resources/Items/" + (i + 1) + ".png");
            }
        }

        public MapItem(string name, int x, int y, int itemnum)
        {
            Name = name;
            X = x;
            Y = y;
            ItemNum = itemnum;

            for (int i = 0; i < spritePics; i++)
            {
                c_ItemSprite[i] = new Texture("Resources/Items/" + (i + 1) + ".png");
            }
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            itemPic[0] = new Vertex(new Vector2f((X * PIC_X), (Y * PIC_Y)), new Vector2f(0, 0));
            itemPic[1] = new Vertex(new Vector2f((X * PIC_X) + PIC_X, (Y * PIC_Y)), new Vector2f(32, 0));
            itemPic[2] = new Vertex(new Vector2f((X * PIC_X) + PIC_X, (Y * PIC_Y) + PIC_Y), new Vector2f(PIC_X, PIC_Y));
            itemPic[3] = new Vertex(new Vector2f((X * PIC_X), (Y * PIC_X) + PIC_Y), new Vector2f(0, PIC_Y));

            if (Sprite > 0)
            {
                states.Texture = c_ItemSprite[Sprite - 1];
                target.Draw(itemPic, states);
            }
        }
    }