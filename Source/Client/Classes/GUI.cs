using Gwen.Control;
using static System.Environment;
using Lidgren.Network;
using System.Drawing;
using System.Threading;
using System;
using SFML.Graphics;
using SFML.System;

namespace Client.Classes
{
    class GUI
    {
        static NetClient c_Client;
        Canvas c_Canvas;
        Gwen.Font c_Font;
        Player[] c_Player;
        ClientConfig c_Config;

        public WindowControl d_Window;
        public int g_Index;
        Label d_FPS;
        Label d_Name;
        Label d_X;
        Label d_Y;
        Label d_Map;
        Label d_Dir;
        Label d_Sprite;
        Label d_IP;
        Label d_Port;
        Label d_Latency;
        Label d_packetsIn;
        Label d_packetsOut;

        public WindowControl loadWindow;
        Label loadLabel;

        public WindowControl mainWindow;
        Button mainbuttonLog;
        Button mainbuttonReg;
        Button mainbuttonExit;

        public WindowControl regWindow;
        Label unregLabel;
        Label pwregLabel;
        Label repwLabel;
        TextBox unregBox;
        TextBoxPassword pwregBox;
        TextBoxPassword repwBox;
        Button regButton;
        Button canregButton;

        public WindowControl logWindow;
        Label unlogLabel;
        Label pwloglabel;
        TextBox unlogBox;
        TextBoxPassword pwlogBox;
        Button logButton;
        Button canlogButton;

        public WindowControl chatWindow;
        public ListBox outputChat;
        public TextBox inputChat;

        public WindowControl menuWindow;
        TabControl menuTabs;

        public TabButton charTab;
        Label charName;
        Label charLevel;
        Label charExp;
        Label charMoney;
        Label charPoints;
        Label charHealth;
        Label charHunger;
        Label charHydration;
        Label charArmor;
        Label charStr;
        Label charAgi;
        Label charEnd;
        Label charSta;

        TabButton packTab;
        ImagePanel[] invPic = new ImagePanel[25];
        GroupBox packStats;
        Label packName;
        Label packDamage;
        Label packArmor;
        Label packHeRestore;
        Label packHuRestore;
        Label packHyRestore;
        Label packStr;
        Label packAgi;
        Label packEdu;
        Label packSta;
        Label packClip;
        Label packMClip;        

        TabButton equipTab;

        TabButton skillsTab;

        TabButton missionTab;

        TabButton optionsTab;
        Button optLog;

        RenderText hudClip = new RenderText();
        RectangleShape healthBar = new RectangleShape();
        string hudC;
        float barLength;
        Vector2f hudCPos;

        public GUI(NetClient c_Client, Canvas c_Canvas, Gwen.Font c_Font, Gwen.Renderer.SFML gwenRenderer, Player[] c_Player, ClientConfig c_Config)
        {
            GUI.c_Client = c_Client;
            this.c_Canvas = c_Canvas;
            this.c_Font = c_Font;
            this.c_Player = c_Player;
            this.c_Config = c_Config;
        }

        public void UpdateHUD(Player c_Player, RenderWindow c_Window)
        {
            barLength = ((float)c_Player.Health / c_Player.MaxHealth) * 150;
            healthBar.Size = new Vector2f(barLength, 25);
            healthBar.Position = new Vector2f(25, 25);
            healthBar.FillColor = SFML.Graphics.Color.Red;
            hudC = "Clip: " + c_Player.mainWeapon.Clip + " / " + c_Player.mainWeapon.maxClip;
            hudCPos = new Vector2f(25, 50);
            hudClip.DrawText(c_Window, hudC, hudCPos, 16, SFML.Graphics.Color.White);
            c_Window.Draw(healthBar);
        }

        public void CreateMenuWindow(Base parent)
        {
            menuWindow = new WindowControl(parent.GetCanvas());
            menuWindow.SetSize(350, 300);
            menuWindow.Position(Gwen.Pos.Bottom);
            menuWindow.Position(Gwen.Pos.Right);
            menuWindow.DisableResizing();
            menuWindow.Title = "Game Menu";
            menuWindow.IsClosable = false;

            menuTabs = new TabControl(menuWindow);
            menuTabs.SetSize(330, 260);
            menuTabs.SetPosition(5, 5);

            charTab = menuTabs.AddPage("Character");

            charName = new Label(charTab.Page);
            charName.SetPosition(10, 5);
            charName.Text = "Name: ?";

            charLevel = new Label(charTab.Page);
            charLevel.SetPosition(10, 20);
            charLevel.Text = "Level: ?";

            charExp = new Label(charTab.Page);
            charExp.SetPosition(10, 30);
            charExp.Text = "Experience: ?";

            charMoney = new Label(charTab.Page);
            charMoney.SetPosition(10, 40);
            charMoney.Text = "Money: ?";

            charPoints = new Label(charTab.Page);
            charPoints.SetPosition(10, 50);
            charPoints.Text = "Points: ?";

            charHealth = new Label(charTab.Page);
            charHealth.SetPosition(10, 65);
            charHealth.Text = "Health: ?";

            charHunger = new Label(charTab.Page);
            charHunger.SetPosition(10, 75);
            charHunger.Text = "Hunger: ?";

            charHydration = new Label(charTab.Page);
            charHydration.SetPosition(10, 85);
            charHydration.Text = "Hydration: ?";

            charArmor = new Label(charTab.Page);
            charArmor.SetPosition(10, 100);
            charArmor.Text = "Armor: ?";

            charStr = new Label(charTab.Page);
            charStr.SetPosition(10, 110);
            charStr.Text = "Strength: ?";

            charAgi = new Label(charTab.Page);
            charAgi.SetPosition(10, 120);
            charAgi.Text = "Agility: ?";

            charEnd = new Label(charTab.Page);
            charEnd.SetPosition(10, 130);
            charEnd.Text = "Endurance: ?";

            charSta = new Label(charTab.Page);
            charSta.SetPosition(10, 140);
            charSta.Text = "Stamina: ?";

            packTab = menuTabs.AddPage("Backpack");

            int n = 0;
            int c = 0;
            for (int i = 0; i < 25; i++)
            {
                invPic[i] = new ImagePanel(packTab.Page);
                invPic[i].SetSize(32, 32);
                invPic[i].RenderColor = System.Drawing.Color.Gray;
                invPic[i].SetPosition(3 + (c * 40), 5 + (n * 40));    
                c += 1;
                if (c > 4) { c = 0; }
                if (i == 4 || i == 9 || i == 14 || i == 19) { n += 1; }
            }

            packStats = new GroupBox(packTab.Page);
            packStats.SetPosition(200, 10);
            packStats.SetSize(115, 155);
            packStats.Text = "Stats";

            packName = new Label(packStats);
            packName.SetPosition(3, 5);
            packName.Text = "Name: ?";
            packName.BringToFront();

            packDamage = new Label(packStats);
            packDamage.SetPosition(3, 15);
            packDamage.Text = "Damage: ?";

            packArmor = new Label(packStats);
            packArmor.SetPosition(3, 25);
            packArmor.Text = "Armor: ?";

            packHeRestore = new Label(packStats);
            packHeRestore.SetPosition(3, 35);
            packHeRestore.Text = "Health Restore: ?";

            packHuRestore = new Label(packStats);
            packHuRestore.SetPosition(3, 45);
            packHuRestore.Text = "Hunger Restore: ?";

            packHyRestore = new Label(packStats);
            packHyRestore.SetPosition(3, 55);
            packHyRestore.Text = "Hydration Restore: ?";

            packStr = new Label(packStats);
            packStr.SetPosition(3, 65);
            packStr.Text = "Strength: ?";

            packAgi = new Label(packStats);
            packAgi.SetPosition(3, 75);
            packAgi.Text = "Agility: ?";

            packEdu = new Label(packStats);
            packEdu.SetPosition(3, 85);
            packEdu.Text = "Endurance: ?";

            packSta = new Label(packStats);
            packSta.SetPosition(3, 95);
            packSta.Text = "Stamina: ?";

            packClip = new Label(packStats);
            packClip.SetPosition(3, 105);
            packClip.Text = "Clip: ?";

            packMClip = new Label(packStats);
            packMClip.SetPosition(3, 115);
            packMClip.Text = "Max Clip: ?";

            equipTab = menuTabs.AddPage("Equipment");

            skillsTab = menuTabs.AddPage("Skills");

            missionTab = menuTabs.AddPage("Missions");

            optionsTab = menuTabs.AddPage("Options");

            optLog = new Button(optionsTab.Page);
            optLog.SetPosition(105, 100);
            optLog.Text = "Log Out";
            optLog.Clicked += CheckLogOutSubmit;
        }

        private void CheckLogOutSubmit(Base control, ClickedEventArgs e)
        {
            c_Canvas.Dispose();
            c_Client.Shutdown("Shutting Down");
            Thread.Sleep(500);
            Exit(0);
        }

        public void UpdateMenuWindow(Player c_Player)
        {
            if (menuWindow != null && c_Player != null && menuWindow.IsVisible)
            {
                if (charTab.HasFocus)
                {
                    charName.Text = c_Player.Name;
                    charLevel.Text = "Level: " + c_Player.Level;
                    charExp.Text = "Experience: " + c_Player.Experience + " / " + (c_Player.Level * 1000);
                    charMoney.Text = "Money: " + c_Player.Money;
                    charPoints.Text = "Points: " + c_Player.Points;

                    charHealth.Text = "Health: " + c_Player.Health + " / " + c_Player.MaxHealth;
                    charHunger.Text = "Hunger: " + c_Player.Hunger + " / 100";
                    charHydration.Text = "Hydration: " + c_Player.Hydration + " / 100";

                    charArmor.Text = "Armor: " + c_Player.Armor;
                    charStr.Text = "Strength: " + c_Player.Strength;
                    charAgi.Text = "Agility: " + c_Player.Agility;
                    charEnd.Text = "Endurance: " + c_Player.Endurance;
                    charSta.Text = "Stamina: " + c_Player.Stamina;
                }
                if (packTab.HasFocus)
                {
                    for (int i = 0; i < 25; i++)
                    {
                        if (c_Player.Backpack[i].Name != "None" && c_Player.Backpack[i].Sprite > 0)
                        {
                            invPic[i].ImageName = "Resources/Items/" + c_Player.Backpack[i].Sprite + ".png";
                            invPic[i].Show();                    
                        } 
                        else
                        {
                            invPic[i].Hide();
                        }
                        if (invPic[i].IsHovered)
                        {
                            if (c_Player.Backpack[i].Name != "None")
                            {
                                packName.Text = c_Player.Backpack[i].Name;
                                packDamage.Text = "Damage: " + c_Player.Backpack[i].Damage;
                                packArmor.Text = "Armor: " + c_Player.Backpack[i].Armor;
                                packHeRestore.Text = "Health Restore: " + c_Player.Backpack[i].HealthRestore;
                                packHuRestore.Text = "Hunger Restore: " + c_Player.Backpack[i].HungerRestore;
                                packHyRestore.Text = "Hydration Restore: " + c_Player.Backpack[i].HydrateRestore;
                                packStr.Text = "Strength: " + c_Player.Backpack[i].Strength;
                                packAgi.Text = "Agility: " + c_Player.Backpack[i].Agility;
                                packEdu.Text = "Endurance: " + c_Player.Backpack[i].Endurance;
                                packSta.Text = "Stamina: " + c_Player.Backpack[i].Stamina;
                                packClip.Text = "Clip: " + c_Player.Backpack[i].Clip;
                                packMClip.Text = "Max Clip: " + c_Player.Backpack[i].maxClip;
                                packStats.Show();
                                break;
                            }
                        }
                        else
                        {
                            packStats.Hide();
                        }
                    }
                }
            }
        }

        public void CreateLoadingWindow(Base parent)
        {
            loadWindow = new WindowControl(parent.GetCanvas());
            loadWindow.SetSize(250, 75);
            loadWindow.Position(Gwen.Pos.Center);
            loadWindow.DisableResizing();
            loadWindow.IsClosable = false;

            loadLabel = new Label(loadWindow);
            loadLabel.SetPosition(70, 15);
            loadLabel.Text = "Loading...Please Wait...";
        }

        public void CreateMainWindow(Base parent)
        {
            mainWindow = new WindowControl(parent.GetCanvas());
            mainWindow.Title = "Main Menu";
            mainWindow.SetSize(200, 200);
            mainWindow.Position(Gwen.Pos.Center);
            mainWindow.DisableResizing();
            mainWindow.IsClosable = false;

            mainbuttonReg = new Button(mainWindow);
            mainbuttonReg.SetSize(100, 25);
            mainbuttonReg.SetPosition(45, 45);
            mainbuttonReg.Text = "Register";
            mainbuttonReg.Clicked += CheckMainWindowRegister;

            mainbuttonLog = new Button(mainWindow);
            mainbuttonLog.SetSize(100, 25);
            mainbuttonLog.SetPosition(45, 80);
            mainbuttonLog.Text = "Login";
            mainbuttonLog.Clicked += CheckMainWindowLogin;

            mainbuttonExit = new Button(mainWindow);
            mainbuttonExit.SetSize(100, 25);
            mainbuttonExit.SetPosition(45, 115);
            mainbuttonExit.Text = "Exit";
            mainbuttonExit.Clicked += CheckMainWindowExit;
        }

        private void CheckMainWindowLogin(Base control, ClickedEventArgs e)
        {
            Button button = control as Button;
            CreateLoginWindow(button.GetCanvas(), c_Config);
        }

        private void CheckMainWindowRegister(Base control, ClickedEventArgs e)
        {
            Button button = control as Button;
            CreateRegisterWindow(button.GetCanvas());
        }

        private void CheckMainWindowExit(Base control, ClickedEventArgs e)
        {
            c_Client.Disconnect("Shutting Down");
            Thread.Sleep(500);
            Exit(0);
        }

        public void CreateLoginWindow(Base parent, ClientConfig c_Config)
        {
            logWindow = new WindowControl(parent.GetCanvas());
            logWindow.Title = "Login";
            logWindow.SetSize(200, 200);
            logWindow.Position(Gwen.Pos.Center);
            logWindow.IsClosable = false;
            logWindow.DisableResizing();
            logWindow.KeyboardInputEnabled = true;

            unlogLabel = new Label(logWindow);
            unlogLabel.SetPosition(25, 15);
            unlogLabel.Text = "Username:";

            unlogBox = new TextBox(logWindow);
            unlogBox.SetPosition(25, 35);
            unlogBox.SetSize(140, 25);
            unlogBox.Focus();
            if (this.c_Config.saveCreds == "1")
            {
                unlogBox.Text = c_Config.savedUser;
            }
            pwloglabel = new Label(logWindow);
            pwloglabel.SetPosition(25, 75);
            pwloglabel.Text = "Password:";

            pwlogBox = new TextBoxPassword(logWindow);
            pwlogBox.SetPosition(25, 95);
            pwlogBox.SetSize(140, 25);
            if (this.c_Config.saveCreds == "1")
            {
                pwlogBox.Text = c_Config.savedPass;
            }
            //pwlogBox.Focus();
            pwlogBox.SubmitPressed += CheckLogWindowSubmit;

            logButton = new Button(logWindow);
            logButton.SetPosition(25, 135);
            logButton.SetSize(60, 25);
            logButton.Text = "Login";
            logButton.Clicked += CheckLogWindowLogin;

            canlogButton = new Button(logWindow);
            canlogButton.SetPosition(105, 135);
            canlogButton.SetSize(60, 25);
            canlogButton.Text = "Cancel";
            canlogButton.Clicked += CheckLogWindowCancel;
        }

        private void CheckLogWindowCancel(Base control, ClickedEventArgs e)
        {
            Button button = control as Button;
            Base parent = button.Parent;
            parent.Hide();
        }

        private void CheckLogWindowLogin(Base control, EventArgs e)
        {
            Base parent = control.Parent;

            if (unlogBox.Text != "" && pwlogBox.Text != "")
            {
                if (c_Client.ServerConnection == null)
                {
                    parent.Hide();
                    MsgBox("Client is not connected to server!", "Not Connected", c_Canvas);
                    return;
                }
                string username = unlogBox.Text;
                string password = pwlogBox.Text;
                string version = c_Config.version;

                if (c_Config.saveCreds == "1")
                {
                    c_Config.savedUser = username;
                    c_Config.savedPass = password;
                    c_Config.SaveConfig();
                }

                NetOutgoingMessage outMSG = c_Client.CreateMessage();
                outMSG.Write((byte)PacketTypes.Login);
                outMSG.Write(username);
                outMSG.Write(password);
                outMSG.Write(version);
                c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                parent.Hide();
            }
            else
            {
                parent.Hide();
                MsgBox("Please fill out the login info.", "Login Failed", c_Canvas);
            }
        }

        private void CheckLogWindowSubmit(Base control, EventArgs e)
        {
            Base parent = control.Parent;

            if (unlogBox.Text != "" && pwlogBox.Text != "")
            {
                if (c_Client.ServerConnection == null)
                {
                    parent.Hide();
                    MsgBox("Client is not connected to server!", "Not Connected", c_Canvas);
                    return;
                }
                string username = unlogBox.Text;
                string password = pwlogBox.Text;
                string version = c_Config.version;

                if (c_Config.saveCreds == "1")
                {
                    c_Config.savedUser = username;
                    c_Config.savedPass = password;
                    c_Config.SaveConfig();
                }

                NetOutgoingMessage outMSG = c_Client.CreateMessage();
                outMSG.Write((byte)PacketTypes.Login);
                outMSG.Write(username);
                outMSG.Write(password);
                outMSG.Write(version);
                c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                parent.Hide();
            }
            else
            {
                parent.Hide();
                MsgBox("Please fill out the login info.", "Login Failed", c_Canvas);
            }
        }

        public void CreateRegisterWindow(Base parent)
        {
            regWindow = new WindowControl(parent.GetCanvas());
            regWindow.Title = "Register";
            regWindow.SetSize(200, 300);
            regWindow.Position(Gwen.Pos.Center);
            regWindow.IsClosable = false;
            regWindow.DisableResizing();
            regWindow.KeyboardInputEnabled = true;

            unregLabel = new Label(regWindow);
            unregLabel.SetPosition(25, 15);
            unregLabel.Text = "Username:";

            unregBox = new TextBox(regWindow);
            unregBox.SetPosition(25, 35);
            unregBox.SetSize(140, 25);
            unregBox.Focus();

            pwregLabel = new Label(regWindow);
            pwregLabel.SetPosition(25, 75);
            pwregLabel.Text = "Password:";

            pwregBox = new TextBoxPassword(regWindow);
            pwregBox.SetPosition(25, 95);
            pwregBox.SetSize(140, 25);

            repwLabel = new Label(regWindow);
            repwLabel.SetPosition(25, 135);
            repwLabel.Text = "Re-type Password:";

            repwBox = new TextBoxPassword(regWindow);
            repwBox.SetPosition(25, 155);
            repwBox.SetSize(140, 25);
            repwBox.SubmitPressed += CheckRegWindowSubmit;

            regButton = new Button(regWindow);
            regButton.SetPosition(25, 200);
            regButton.SetSize(60, 25);
            regButton.Text = "Register";
            regButton.Clicked += CheckRegWindowRegister;

            canregButton = new Button(regWindow);
            canregButton.SetPosition(105, 200);
            canregButton.SetSize(60, 25);
            canregButton.Text = "Cancel";
            canregButton.Clicked += CheckRegWindowCancel;
        }

        private void CheckRegWindowCancel(Base control, ClickedEventArgs e)
        {
            Base parent = control.Parent;
            parent.Hide();
        }

        private void CheckRegWindowRegister(Base control, ClickedEventArgs e)
        {
            Base parent = control.Parent;

            if (unregBox.Text != "" && pwregBox.Text != "" && repwBox.Text != "")
            {
                if (c_Client.ServerConnection == null)
                {
                    parent.Hide();
                    MsgBox("Client is not connected to server!", "Not Connected", c_Canvas);
                    return;
                }

                if (pwregBox.Text == repwBox.Text)
                {
                    string username = unregBox.Text;
                    string password = pwregBox.Text;
                    NetOutgoingMessage outMSG = c_Client.CreateMessage();
                    outMSG.Write((byte)PacketTypes.Register);
                    outMSG.Write(username);
                    outMSG.Write(password);
                    c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                    parent.Hide();
                }
                else
                {
                    parent.Hide();
                    MsgBox("Passwords do not match!", "Retry", c_Canvas);
                }
            }
            else
            {
                parent.Hide();
                MsgBox("Please fill out account info for registration.", "Error", c_Canvas);
            }
        }

        private void CheckRegWindowSubmit(Base control, EventArgs e)
        {
            Base parent = control.Parent;

            if (unregBox.Text != "" && pwregBox.Text != "" && repwBox.Text != "")
            {
                if (c_Client.ServerConnection == null)
                {
                    parent.Hide();
                    MsgBox("Client is not connected to server!", "Not Connected", c_Canvas);
                    return;
                }

                if (pwregBox.Text == repwBox.Text)
                {
                    string username = unregBox.Text;
                    string password = pwregBox.Text;
                    NetOutgoingMessage outMSG = c_Client.CreateMessage();
                    outMSG.Write((byte)PacketTypes.Register);
                    outMSG.Write(username);
                    outMSG.Write(password);
                    c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                    parent.Hide();
                }
                else
                {
                    parent.Hide();
                    MsgBox("Passwords do not match!", "Retry", c_Canvas);
                }
            }
            else
            {
                parent.Hide();
                MsgBox("Please fill out account info for registration.", "Error", c_Canvas);
            }
        }

        public void CreateChatWindow(Base parent)
        {
            chatWindow = new WindowControl(parent.GetCanvas());
            chatWindow.Title = "Chat";
            chatWindow.SetSize(405, 200);
            chatWindow.Position(Gwen.Pos.Bottom);
            chatWindow.DisableResizing();
            chatWindow.KeyboardInputEnabled = true;
            chatWindow.IsClosable = false;

            outputChat = new ListBox(chatWindow);
            outputChat.SetPosition(0, 0);
            outputChat.SetSize(392, 140);

            inputChat = new TextBox(chatWindow);
            inputChat.SetPosition(0, 145);
            inputChat.SetSize(392, 25);
            inputChat.SubmitPressed += CheckChatWindowSubmit;
        }

        private void CheckChatWindowSubmit(Base control, EventArgs e)
        {
            if (inputChat.Text != "")
            {
                string msg = inputChat.Text;
                NetOutgoingMessage outMSG = c_Client.CreateMessage();
                outMSG.Write((byte)PacketTypes.ChatMessage);
                outMSG.Write(msg);
                c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                inputChat.Text = "";
            }
        }

        public void AddText(string msg)
        {
            if (outputChat == null) { return; }
            outputChat.AddRow(msg);
            outputChat.ScrollToBottom();
            outputChat.UnselectAll();
        }

        public void MsgBox(string msg, string caption, Canvas c_Canvas)
        {
            MessageBox msgBox = new MessageBox(c_Canvas, msg, caption);
            msgBox.Position(Gwen.Pos.Center);
        }

        public void CreateDebugWindow(Base parent)
        {
            d_Window = new WindowControl(parent.GetCanvas());
            d_Window.Title = "Debug";
            d_Window.SetSize(200, 165);
            d_Window.Position(Gwen.Pos.Top);
            d_Window.Position(Gwen.Pos.Right);
            d_Window.DisableResizing();

            d_FPS = new Label(d_Window);
            d_FPS.SetPosition(10, 5);
            d_FPS.Text = "FPS: ";

            d_Name = new Label(d_Window);
            d_Name.SetPosition(10, 15);
            d_Name.Text = "Name: Player";

            d_X = new Label(d_Window);
            d_X.SetPosition(10, 25);
            d_X.Text = "X: ?";

            d_Y = new Label(d_Window);
            d_Y.SetPosition(10, 35);
            d_Y.Text = "Y: ?";

            d_Map = new Label(d_Window);
            d_Map.SetPosition(10, 45);
            d_Map.Text = "Map: ?";

            d_Dir = new Label(d_Window);
            d_Dir.SetPosition(10, 55);
            d_Dir.Text = "Direction : ?";

            d_Sprite = new Label(d_Window);
            d_Sprite.SetPosition(10, 65);
            d_Sprite.Text = "Sprite: ?";

            d_IP = new Label(d_Window);
            d_IP.SetPosition(10, 75);
            d_IP.Text = "IP Address: ?";

            d_Port = new Label(d_Window);
            d_Port.SetPosition(10, 85);
            d_Port.Text = "Port: ?";

            d_Latency = new Label(d_Window);
            d_Latency.SetPosition(10, 95);
            d_Latency.Text = "Latency: ?";

            d_packetsIn = new Label(d_Window);
            d_packetsIn.SetPosition(10, 105);
            d_packetsIn.Text = "Packets In: ?";

            d_packetsOut = new Label(d_Window);
            d_packetsOut.SetPosition(10, 115);
            d_packetsOut.Text = "Packets Out: ?";
        }

        public void UpdateDebugWindow(int fps, Player[] c_Player, int drawIndex)
        {
            if (d_Window != null && c_Player[drawIndex] != null)
            {
                d_Window.Title = "Debug Window - Admin";
                d_FPS.Text = "FPS: " + fps;
                d_Name.Text = "Name: " + c_Player[drawIndex].Name + " (" + drawIndex + ")";
                d_X.Text = "X: " + (c_Player[drawIndex].X + c_Player[drawIndex].offsetX);
                d_Y.Text = "Y: " + (c_Player[drawIndex].Y + c_Player[drawIndex].offsetY);
                d_Map.Text = "Map: " + c_Player[drawIndex].Map;
                d_Dir.Text = "Direction: " + c_Player[drawIndex].Direction;
                d_Sprite.Text = "Sprite: " + c_Player[drawIndex].Sprite;
                d_IP.Text = "IP Address: " + c_Client.ServerConnection.RemoteEndPoint.Address.ToString();
                d_Port.Text = "Port: " + c_Client.ServerConnection.RemoteEndPoint.Port.ToString();
                d_Latency.Text = "Latency: " + c_Client.ServerConnection.AverageRoundtripTime.ToString("#.###") + "ms";
                d_packetsIn.Text = "Packets Received: " + c_Client.Statistics.ReceivedPackets.ToString();
                d_packetsOut.Text = "Packets Sent: " + c_Client.Statistics.SentPackets.ToString();
            } 
        }
    }
}
