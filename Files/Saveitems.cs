        public void SaveItem(int itemNum)
        {
            FileStream fileStream = File.OpenWrite("Items/Item" + itemNum + ".bin");
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            LogWriter.WriteLog("Saving default Items...", "Server");
            binaryWriter.Write(Name);
            binaryWriter.Write(Sprite);
            binaryWriter.Write(Damage);
            binaryWriter.Write(Armor);
            binaryWriter.Write(Type);
            binaryWriter.Write(AttackSpeed);
            binaryWriter.Write(ReloadSpeed);
            binaryWriter.Write(HealthRestore);
            binaryWriter.Write(HungerRestore);
            binaryWriter.Write(HydrateRestore);
            binaryWriter.Write(Strength);
            binaryWriter.Write(Agility);
            binaryWriter.Write(Endurance);
            binaryWriter.Write(Stamina);
            binaryWriter.Write(Clip);
            binaryWriter.Write(maxClip);
            binaryWriter.Write(ammoType);
            binaryWriter.Flush();
            binaryWriter.Close();
        }

        public void LoadItem(int itemNum)
        {
            FileStream fileStream = File.OpenRead("Items/Item" + itemNum + ".bin");
            BinaryReader binaryReader = new BinaryReader(fileStream);
            LogWriter.WriteLog("Loading item...", "Server");
            try
            {
                Name = binaryReader.ReadString();
                Sprite = binaryReader.ReadInt32();
                Damage = binaryReader.ReadInt32();
                Armor = binaryReader.ReadInt32();
                Type = binaryReader.ReadInt32();
                AttackSpeed = binaryReader.ReadInt32();
                ReloadSpeed = binaryReader.ReadInt32();
                HealthRestore = binaryReader.ReadInt32();
                HungerRestore = binaryReader.ReadInt32();
                HydrateRestore = binaryReader.ReadInt32();
                Strength = binaryReader.ReadInt32();
                Agility = binaryReader.ReadInt32();
                Endurance = binaryReader.ReadInt32();
                Stamina = binaryReader.ReadInt32();
                Clip = binaryReader.ReadInt32();
                maxClip = binaryReader.ReadInt32();
                ammoType = binaryReader.ReadInt32();
            }
            catch (Exception e)
            {
                MsgBox(e.GetType() + ": " + e.Message, MsgBoxStyle.Critical, "Error");
            }
            binaryReader.Close();
        }