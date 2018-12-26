        ///Init methods
        //Populate an empty database
        void PopulateDatabase()
        {
            s_Database = new SQLiteConnection("Data Source=Database/Sabertooth.db;Version=3;");
            s_Database.Open();
            string sql;

            sql = "SELECT COUNT(*) FROM `ITEMS`";

            SQLiteCommand sql_Command = new SQLiteCommand(sql, s_Database);
            int result = int.Parse(sql_Command.ExecuteScalar().ToString());

            if (result == 0)
            {
                for (int i = 0; i < 50; i++)
                {
                    s_Item[i] = new Item("None", 1, 100, 0, (int)ItemType.None, 1000, 1500, 0, 0, 0, 0, 0, 0, 0, 0, 0, (int)AmmoType.None);
                    s_Item[i].CreateItemInDatabase();
                }
            }

            sql = "SELECT COUNT(*) FROM `PROJECTILES`";

            sql_Command = new SQLiteCommand(sql, s_Database);
            result = int.Parse(sql_Command.ExecuteScalar().ToString());

            if (result == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    s_Proj[i] = new Projectile("None", 100, 50, 1, 0, (int)ProjType.Bullet, 150);
                    s_Proj[i].CreateProjectileInDatabase();
                }
            }

            sql = "SELECT COUNT(*) FROM `NPCS`";

            sql_Command = new SQLiteCommand(sql, s_Database);
            result = int.Parse(sql_Command.ExecuteScalar().ToString());

            if (result == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    s_Npc[i] = new NPC("None", 10, 10, (int)Directions.Down, 0, 0, 0, (int)BehaviorType.Friendly, 5000, 100, 100, 10);
                    s_Npc[i].CreateNpcInDatabase();
                }
            }
        }