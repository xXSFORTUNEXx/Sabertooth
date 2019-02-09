namespace SabertoothServer
{
    public static class Globals
    {
        #region Globals
        public const byte NO = 0;
        public const byte YES = 1;
        public const char CHAR_NO = 'N';
        public const char CHAR_YES = 'Y';
        public const int MAX_PLAYERS = 5;
        public const int MAX_NPCS = 10;
        public const int MAX_ITEMS = 50;
        public const int MAX_PROJECTILES = 10;
        public const int MAX_MAPS = 10;
        public const int MAX_MAP_NPCS = 10;
        public const int MAX_MAP_POOL_NPCS = 20;
        public const int MAX_MAP_ITEMS = 20;
        public const int MAX_MAP_X = 50;
        public const int MAX_MAP_Y = 50;
        public const int MAX_SHOPS = 10;
        public const int MAX_CHATS = 15;
        public const int MAX_CHESTS = 10;
        public const int MAX_CHEST_ITEMS = 10;
        public const int PLAYER_START_X = 9;
        public const int PLAYER_START_Y = 17;
        public const int OFFSET_X = 16;
        public const int OFFSET_Y = 11;
        public const int MAX_PARTY = 4;
        public const int MAX_INSTANCE_NPC = 20;
        public const int MAX_INV_SLOTS = 25;
        public const int MAX_BLOOD_SPLATS = 50;
        public const int MAX_MAP_PROJECTILES = 200;
        #endregion

        #region Config Globals
        public const string GAME_TITLE = "Sabertooth";
        public const string IP_ADDRESS = "10.16.0.3";
        public const int SERVER_PORT = 14242;
        public const float CONNECTION_TIMEOUT = 5.0f;   //Was 25.0
        public const float SIMULATED_RANDOM_LATENCY = 0f;   //0.085f
        public const float SIMULATED_MINIMUM_LATENCY = 0.000f;  //0.065f
        public const float SIMULATED_PACKET_LOSS = 0f;  //0.5f
        public const float SIMULATED_DUPLICATES_CHANCE = 0f; //0.5f
        public const string VERSION = "1.0"; //For beta and alpha
        #endregion

        #region Server Globals
        public const string SMTP_IP_ADDRESS = "mail.fortune.naw";
        public const int SMTP_SERVER_PORT = 25;
        public const string SMTP_USER_CREDS = "webmaster@fortune.naw";
        public const string SMTP_PASS_CREDS = "Nextech789*";
        public const string SQL_SERVER_NAME = @"FDESKTOP-01\SFORTUNESQL";
        public const string SQL_SERVER_DATABASE = "Sabertooth";
        public const string HEALTH_REGEN_TIME = "60000"; //60000 / 1000 = 1 MIN
        public const string HUNGER_DEGEN_TIME = "600000"; //600000 / 1000 = 10 MIN
        public const string HYDRATION_DEGEN_TIME = "300000"; //300000 / 1000 = 5 MIN
        public const string AUTOSAVE_TIME = "300000"; //300000 / 1000 = 5 MIN
        public const string SPAWN_TIME = "1000"; //1000 / 1000 = 1 SECOND
        public const string AI_TIME = "1000"; //1000 / 1000 = 1 SECOND
        public const string NEXT_WAVE_COUNTDOWN = "60000"; //60000 / 1000 = 1 MIN
        public const int SQL_DATABASE_REMOTE = 0;
        public const int SQL_DATABASE_LOCAL = 1;
        public const int A_MILLISECOND = 1000;
        public const int SECONDS_IN_MINUTE = 60;
        public const int MINUTES_IN_HOUR = 60;
        public const int HOURS_IN_DAY = 24;
        public const int DAYS_IN_YEAR = 365;
        #endregion

        #region Editor Globals
        //Editor Globals
        public const uint EDITOR_WIDTH = 800;
        public const uint EDITOR_HEIGHT = 608;
        public const int WHEEL_OPTION_ZOOM = 0;
        public const int WHEEL_OPTION_SCROLL = 1;
        public const int EDITOR_OFFSET_X = 25;
        public const int EDITOR_OFFSET_Y = 19;
        #endregion

        #region Client Globals
        //Client Globals
        public const uint SCREEN_WIDTH = 1024;
        public const uint SCREEN_HEIGHT = 768;
        public const int CANVAS_WIDTH = 1024;
        public const int CANVAS_HEIGHT = 768;
        public const int MAX_FPS = 144;
        //public const Styles SCREEN_STYLE = Styles.Titlebar;
        public const int MAX_DRAWN_PROJECTILES = 200;
        public const int DISCOVERY_TIMER = 6500;    //6500 / 1000 = 6.5 seconds
        public const int PIC_X = 32;
        public const int PIC_Y = 32;
        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;
        #endregion

        #region Screen Globals
        //Screen Globals
        public const int SHOP_STAT_WINDOW_X = 520;
        public const int SHOP_STAT_WINDOW_Y = 65;
        public const int INV_STAT_WINDOW_X = 530;
        public const int INV_STAT_WINDOW_Y = 390;
        #endregion
    }
}
