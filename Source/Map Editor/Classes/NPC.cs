using Microsoft.VisualBasic;
using SFML.Graphics;
using System;
using System.IO;
using System.Windows.Forms;
using static Microsoft.VisualBasic.Interaction;


namespace Editor.Classes
{
    class NPC
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Direction { get; set; }
        public int Sprite { get; set; }
        public int Step { get; set; }
        public int Owner { get; set; }
        public int Behavior { get; set; }
        public int SpawnTime { get; set; }
        public Texture[] npcTextures = new Texture[200];
        public Sprite npcSprite = new Sprite();

        public NPC() { }

        public NPC(string name, int x, int y, int direction, int sprite, int step, int owner, int behavior, int spawnTime)
        {
            Name = name;
            X = x;
            Y = y;
            Direction = direction;
            Sprite = sprite;
            Step = step;
            Owner = owner;
            Behavior = behavior;
            SpawnTime = spawnTime;
        }

        public NPC(int x, int y)
        {
            Name = "Default";
            X = x;
            Y = y;
            Direction = (int)Directions.Down;
            Sprite = 0;
            Step = 0;
            Owner = 0;
            Behavior = (int)BehaviorType.Friendly;
            SpawnTime = 5000;
        }

        public void SaveNPC()
        {
            SaveFileDialog saveNpcDialog = new SaveFileDialog();

            saveNpcDialog.Filter = "Bin Files (*.bin)|*.bin";
            saveNpcDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "NPCS";
            saveNpcDialog.FilterIndex = 1;
            saveNpcDialog.ShowDialog();

            if (saveNpcDialog.FileName != "")
            {
                FileStream npcStream = File.OpenWrite(saveNpcDialog.FileName);
                BinaryWriter binaryWriter = new BinaryWriter(npcStream);

                binaryWriter.Write(Name);
                binaryWriter.Write(X);
                binaryWriter.Write(Y);
                binaryWriter.Write(Direction);
                binaryWriter.Write(Sprite);
                binaryWriter.Write(Step);
                binaryWriter.Write(Owner);
                binaryWriter.Write(Behavior);
                binaryWriter.Write(SpawnTime);
                binaryWriter.Flush();
                binaryWriter.Close();
            }
        }

        public void LoadNPC()
        {
            OpenFileDialog loadMapDialog = new OpenFileDialog();

            loadMapDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "NPCS";
            loadMapDialog.Filter = "Bin Files (*.bin)|*.bin";
            loadMapDialog.FilterIndex = 1;
            loadMapDialog.ShowDialog();

            try
            {
                if (loadMapDialog.FileName != null)
                {
                    FileStream fileStream = File.OpenRead(loadMapDialog.FileName);
                    BinaryReader binaryReader = new BinaryReader(fileStream);

                    Name = binaryReader.ReadString();
                    X = binaryReader.ReadInt32();
                    Y = binaryReader.ReadInt32();
                    Direction = binaryReader.ReadInt32();
                    Sprite = binaryReader.ReadInt32();
                    Step = binaryReader.ReadInt32();
                    Owner = binaryReader.ReadInt32();
                    Behavior = binaryReader.ReadInt32();
                    SpawnTime = binaryReader.ReadInt32();
                    binaryReader.Close();
                }
            }
            catch (Exception e)
            {
                MsgBox(e.GetType() + ": " + e.Message, MsgBoxStyle.Critical, "Error");
            }
        }
    }

    public enum BehaviorType
    {
        Friendly,
        Passive,
        Aggressive
    }

    public enum Directions
    {
        Down,
        Left,
        Right,
        Up,
        DownLeft,
        DownRight,
        UpLeft,
        UpRight
    }
}
