        static void CheckDirectories()
        {
            bool exists = false;

            if (!Exists("Players"))
            {
                CreateDirectory("Players");
                exists = true;
            }
            if (!Exists("Maps"))
            {
                CreateDirectory("Maps");
                exists = true;
            }
            if (!Exists("NPCS"))
            {
                CreateDirectory("NPCS");
                exists = true;
            }
            if (!Exists("Items"))
            {
                CreateDirectory("Items");
                exists = true;
            }
            if (!Exists("Projectiles"))
            {
                CreateDirectory("Projectiles");
                exists = true;
            }
            if (!Exists("Database"))
            {
                CreateDirectory("Database");
                CreateDatabase();
            }
            if (exists)
            {
                WriteLog("Directories created...", "Server");
            }
        }