        public void LoadProjectile(int projNum)
        {
            FileStream fileStream = File.OpenRead("Items/Item" + projNum + ".bin");
            BinaryReader binaryReader = new BinaryReader(fileStream);
            LogWriter.WriteLog("Loading projectile...", "Server");
            try
            {
                Name = binaryReader.ReadString();
                Damage = binaryReader.ReadInt32();
                Range = binaryReader.ReadInt32();
                Sprite = binaryReader.ReadInt32();
                Type = binaryReader.ReadInt32();
                Speed = binaryReader.ReadInt32();
            }
            catch (Exception e)
            {
                MsgBox(e.GetType() + ": " + e.Message, MsgBoxStyle.Critical, "Error");
            }
            binaryReader.Close();
        }

        public void SaveProjectile(int projNum)
        {
            FileStream fileStream = File.OpenWrite("Projectiles/Projectile" + projNum + ".bin");
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            LogWriter.WriteLog("Saving default Projectiles...", "Server");
            binaryWriter.Write(Name);
            binaryWriter.Write(Damage);
            binaryWriter.Write(Range);
            binaryWriter.Write(Sprite);
            binaryWriter.Write(Type);
            binaryWriter.Write(Speed);
            binaryWriter.Flush();
            binaryWriter.Close();
        }