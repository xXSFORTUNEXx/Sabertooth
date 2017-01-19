        public void SaveNPC(int npcNum)
        {
            FileStream fileStream = File.OpenWrite("NPCS/Npc" + npcNum + ".bin");
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            LogWriter.WriteLog("Saving default Npcs...", "Server");
            binaryWriter.Write(Name);
            binaryWriter.Write(X);
            binaryWriter.Write(Y);
            binaryWriter.Write(Direction);
            binaryWriter.Write(Sprite);
            binaryWriter.Write(Step);
            binaryWriter.Write(Owner);
            binaryWriter.Write(Behavior);
            binaryWriter.Write(SpawnTime);
            binaryWriter.Write(Health);
            binaryWriter.Write(maxHealth);
            binaryWriter.Write(Damage);
            binaryWriter.Flush();
            binaryWriter.Close();
        }

        public void LoadNPC(int npcNum)
        {
            FileStream fileStream = File.OpenRead("NPCS/Npc" + npcNum + ".bin");
            BinaryReader binaryReader = new BinaryReader(fileStream);
            LogWriter.WriteLog("Loading npc...", "Server");
            try
            {
                Name = binaryReader.ReadString();
                X = binaryReader.ReadInt32();
                Y = binaryReader.ReadInt32();
                Direction = binaryReader.ReadInt32();
                Sprite = binaryReader.ReadInt32();
                Step = binaryReader.ReadInt32();
                Owner = binaryReader.ReadInt32();
                Behavior = binaryReader.ReadInt32();
                SpawnTime = binaryReader.ReadInt32();
                Health = binaryReader.ReadInt32();
                maxHealth = binaryReader.ReadInt32();
                Damage = binaryReader.ReadInt32();
            }
            catch (Exception e)
            {
                MsgBox(e.GetType() + ": " + e.Message, MsgBoxStyle.Critical, "Error");
            }
            binaryReader.Close();
        }