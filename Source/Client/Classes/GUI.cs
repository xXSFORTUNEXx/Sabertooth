using Gwen.Control;
using static System.Environment;
using Lidgren.Network;
using System.Drawing;
using System.Threading;
using System;

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
        Label d_Stats;
        Label d_Level;
        Label d_Health;
        Label d_Hunger;
        Label d_Hydration;
        Label d_Exp;
        Label d_Money;
        Label d_Armor;
        Label d_Strength;
        Label d_Agility;
        Label d_Endurance;
        Label d_Stamina;
        Label d_MainClip;
        Label d_PistolAmmo;
        Label d_AssaultAmmo;
        Label d_AttackSpeed;
        Label d_ReloadSpeed;
        Label d_Points;

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

        public GUI(NetClient c_Client, Canvas c_Canvas, Gwen.Font c_Font, Gwen.Renderer.SFML gwenRenderer, Player[] c_Player, ClientConfig c_Config)
        {
            GUI.c_Client = c_Client;
            this.c_Canvas = c_Canvas;
            this.c_Font = c_Font;
            this.c_Player = c_Player;
            this.c_Config = c_Config;
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

        public void UpdateWindowPositions(Player[] c_Player, int index)
        {
            if (mainWindow != null)
            {
                mainWindow.Position(Gwen.Pos.Center);
            }
            if (regWindow != null)
            {
                regWindow.Position(Gwen.Pos.Center);
            }
            if (logWindow != null)
            {
                logWindow.Position(Gwen.Pos.Center);
            }
            if (chatWindow != null)
            {
                chatWindow.Position(Gwen.Pos.Bottom);
                chatWindow.Position(Gwen.Pos.Left);
            }
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
            d_Window.SetSize(200, 355);
            d_Window.SetPosition(10, 10);
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

            d_Stats = new Label(d_Window);
            d_Stats.SetPosition(10, 130);
            d_Stats.Text = "Stats:";

            d_Level = new Label(d_Window);
            d_Level.SetPosition(10, 140);
            d_Level.Text = "Level: ?";

            d_Health = new Label(d_Window);
            d_Health.SetPosition(10, 150);
            d_Health.Text = "Health: ?";

            d_Hunger = new Label(d_Window);
            d_Hunger.SetPosition(10, 160);
            d_Hunger.Text = "Hunger: ?";

            d_Hydration = new Label(d_Window);
            d_Hydration.SetPosition(10, 170);
            d_Hydration.Text = "Hydration: ?";

            d_Exp = new Label(d_Window);
            d_Exp.SetPosition(10, 180);
            d_Exp.Text = "Experience: ?";

            d_Money = new Label(d_Window);
            d_Money.SetPosition(10, 190);
            d_Money.Text = "Money: ?";

            d_Armor = new Label(d_Window);
            d_Armor.SetPosition(10, 200);
            d_Armor.Text = "Armor: ?";

            d_Strength = new Label(d_Window);
            d_Strength.SetPosition(10, 210);
            d_Strength.Text = "Strength: ?";

            d_Agility = new Label(d_Window);
            d_Agility.SetPosition(10, 220);
            d_Agility.Text = "Agility: ?";

            d_Endurance = new Label(d_Window);
            d_Endurance.SetPosition(10, 230);
            d_Endurance.Text = "Endurance: ?";

            d_Stamina = new Label(d_Window);
            d_Stamina.SetPosition(10, 240);
            d_Stamina.Text = "Stamina: ?";

            d_MainClip = new Label(d_Window);
            d_MainClip.SetPosition(10, 250);
            d_MainClip.Text = "Clip: ?";

            d_PistolAmmo = new Label(d_Window);
            d_PistolAmmo.SetPosition(10, 260);
            d_PistolAmmo.Text = "Pistol Ammo: ?";

            d_AssaultAmmo = new Label(d_Window);
            d_AssaultAmmo.SetPosition(10, 270);
            d_AssaultAmmo.Text = "Assault Ammo: ?";

            d_AttackSpeed = new Label(d_Window);
            d_AttackSpeed.SetPosition(10, 280);
            d_AttackSpeed.Text = "Attack Speed: ?";

            d_ReloadSpeed = new Label(d_Window);
            d_ReloadSpeed.SetPosition(10, 290);
            d_ReloadSpeed.Text = "Reload Speed: ?";

            d_Points = new Label(d_Window);
            d_Points.SetPosition(10, 300);
            d_Points.Text = "Points: ?";
        }

        public void UpdateDebugWindow(int fps, Player[] c_Player, int drawIndex)
        {
            if (d_Window != null && c_Player[drawIndex] != null)
            {
                int p_level = c_Player[drawIndex].Level;

                d_Window.Title = "Debug Window - Admin";
                d_Window.SetPosition(0, 0);
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

                d_Level.Text = "Level: " + c_Player[drawIndex].Level;
                d_Health.Text = "Health: " + c_Player[drawIndex].Health + " / " + c_Player[drawIndex].MaxHealth;
                d_Hunger.Text = "Hunger: " + c_Player[drawIndex].Hunger + " / 100";
                d_Hydration.Text = "Hydration: " + c_Player[drawIndex].Hydration + " / 100";
                d_Exp.Text = "Experience: " + c_Player[drawIndex].Experience + " / " + (p_level * 1000);
                d_Money.Text = "Money: " + c_Player[drawIndex].Money;
                d_Armor.Text = "Armor: " + c_Player[drawIndex].Armor;
                d_Strength.Text = "Strength: " + c_Player[drawIndex].Strength;
                d_Agility.Text = "Agility: " + c_Player[drawIndex].Agility;
                d_Endurance.Text = "Endurance: " + c_Player[drawIndex].Endurance;
                d_Stamina.Text = "Stamina: " + c_Player[drawIndex].Stamina;
                d_MainClip.Text = "Clip: " + c_Player[drawIndex].mainWeapon.Clip + " / " + c_Player[drawIndex].mainWeapon.maxClip;
                d_PistolAmmo.Text = "Pistol Ammo: " + c_Player[drawIndex].PistolAmmo;
                d_AssaultAmmo.Text = "Assault Ammo: " + c_Player[drawIndex].AssaultAmmo;
                d_AttackSpeed.Text = "Attack Speed: " + c_Player[drawIndex].mainWeapon.AttackSpeed;
                d_ReloadSpeed.Text = "Reload Speed: " + c_Player[drawIndex].mainWeapon.ReloadSpeed;
                d_Points.Text = "Points: " + c_Player[drawIndex].Points;
            } 
        }
    }
}
