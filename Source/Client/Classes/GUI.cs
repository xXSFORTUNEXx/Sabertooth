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
        static NetClient srvrClient;
        Canvas srvrCanvas;
        Gwen.Font srvrFont;
        Player[] srvrPlayer;
        ClientConfig svrcConfig;
        public WindowControl debugWindow;
        public int guiIndex;
        Label debugFps;
        Label debugName;
        Label debugX;
        Label debugY;
        Label debugDir;
        Label debugSprite;
        Label debugIP;
        Label debugPort;
        Label debugLatency;
        Label debugPacketsIn;
        Label debugPacketsOut;

        WindowControl mainWindow;
        Button mainbuttonLog;
        Button mainbuttonReg;
        Button mainbuttonExit;

        WindowControl regWindow;
        Label unregLabel;
        Label pwregLabel;
        Label repwLabel;
        TextBox unregBox;
        TextBoxPassword pwregBox;
        TextBoxPassword repwBox;
        Button regButton;
        Button canregButton;

        WindowControl logWindow;
        Label unlogLabel;
        Label pwloglabel;
        TextBox unlogBox;
        TextBoxPassword pwlogBox;
        Button logButton;
        Button canlogButton;

        public WindowControl chatWindow;
        public ListBox outputChat;
        public TextBox inputChat;

        public GUI(NetClient svrClient, Canvas svrCanvas, Gwen.Font svrFont, Gwen.Renderer.SFML gwenRenderer, Player[] svrPlayer, ClientConfig cConfig)
        {
            srvrClient = svrClient;
            srvrCanvas = svrCanvas;
            srvrFont = svrFont;
            srvrPlayer = svrPlayer;
            svrcConfig = cConfig;
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
            CreateLoginWindow(button.GetCanvas(), svrcConfig);
        }

        private void CheckMainWindowRegister(Base control, ClickedEventArgs e)
        {
            Button button = control as Button;
            CreateRegisterWindow(button.GetCanvas());
        }

        private void CheckMainWindowExit(Base control, ClickedEventArgs e)
        {
            srvrClient.Disconnect("Shutting Down");
            Thread.Sleep(500);
            Exit(0);
        }

        public void CreateLoginWindow(Base parent, ClientConfig cConfig)
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
            if (svrcConfig.saveCreds == "1")
            {
                unlogBox.Text = cConfig.savedUser;
            }
            pwloglabel = new Label(logWindow);
            pwloglabel.SetPosition(25, 75);
            pwloglabel.Text = "Password:";

            pwlogBox = new TextBoxPassword(logWindow);
            pwlogBox.SetPosition(25, 95);
            pwlogBox.SetSize(140, 25);
            if (svrcConfig.saveCreds == "1")
            {
                pwlogBox.Text = cConfig.savedPass;
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
                if (srvrClient.ServerConnection == null)
                {
                    parent.Hide();
                    MsgBox("Client is not connected to server!", "Not Connected", srvrCanvas);
                    return;
                }
                string username = unlogBox.Text;
                string password = pwlogBox.Text;

                if (svrcConfig.saveCreds == "1")
                {
                    svrcConfig.savedUser = username;
                    svrcConfig.savedPass = password;
                    svrcConfig.SaveConfig();
                }

                NetOutgoingMessage outMSG = srvrClient.CreateMessage();
                outMSG.Write((byte)PacketTypes.Login);
                outMSG.Write(username);
                outMSG.Write(password);
                srvrClient.SendMessage(outMSG, srvrClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                parent.Hide();
            }
            else
            {
                parent.Hide();
                MsgBox("Please fill out the login info.", "Login Failed", srvrCanvas);
            }
        }

        private void CheckLogWindowSubmit(Base control, EventArgs e)
        {
            Base parent = control.Parent;

            if (unlogBox.Text != "" && pwlogBox.Text != "")
            {
                if (srvrClient.ServerConnection == null)
                {
                    parent.Hide();
                    MsgBox("Client is not connected to server!", "Not Connected", srvrCanvas);
                    return;
                }
                string username = unlogBox.Text;
                string password = pwlogBox.Text;

                if (svrcConfig.saveCreds == "1")
                {
                    svrcConfig.savedUser = username;
                    svrcConfig.savedPass = password;
                    svrcConfig.SaveConfig();
                }

                NetOutgoingMessage outMSG = srvrClient.CreateMessage();
                outMSG.Write((byte)PacketTypes.Login);
                outMSG.Write(username);
                outMSG.Write(password);
                srvrClient.SendMessage(outMSG, srvrClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                parent.Hide();
            }
            else
            {
                parent.Hide();
                MsgBox("Please fill out the login info.", "Login Failed", srvrCanvas);
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
                if (srvrClient.ServerConnection == null)
                {
                    parent.Hide();
                    MsgBox("Client is not connected to server!", "Not Connected", srvrCanvas);
                    return;
                }

                if (pwregBox.Text == repwBox.Text)
                {
                    string username = unregBox.Text;
                    string password = pwregBox.Text;
                    NetOutgoingMessage outMSG = srvrClient.CreateMessage();
                    outMSG.Write((byte)PacketTypes.Register);
                    outMSG.Write(username);
                    outMSG.Write(password);
                    srvrClient.SendMessage(outMSG, srvrClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                    parent.Hide();
                }
                else
                {
                    parent.Hide();
                    MsgBox("Passwords do not match!", "Retry", srvrCanvas);
                }
            }
            else
            {
                parent.Hide();
                MsgBox("Please fill out account info for registration.", "Error", srvrCanvas);
            }
        }

        private void CheckRegWindowSubmit(Base control, EventArgs e)
        {
            Base parent = control.Parent;

            if (unregBox.Text != "" && pwregBox.Text != "" && repwBox.Text != "")
            {
                if (srvrClient.ServerConnection == null)
                {
                    parent.Hide();
                    MsgBox("Client is not connected to server!", "Not Connected", srvrCanvas);
                    return;
                }

                if (pwregBox.Text == repwBox.Text)
                {
                    string username = unregBox.Text;
                    string password = pwregBox.Text;
                    NetOutgoingMessage outMSG = srvrClient.CreateMessage();
                    outMSG.Write((byte)PacketTypes.Register);
                    outMSG.Write(username);
                    outMSG.Write(password);
                    srvrClient.SendMessage(outMSG, srvrClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                    parent.Hide();
                }
                else
                {
                    parent.Hide();
                    MsgBox("Passwords do not match!", "Retry", srvrCanvas);
                }
            }
            else
            {
                parent.Hide();
                MsgBox("Please fill out account info for registration.", "Error", srvrCanvas);
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
                NetOutgoingMessage outMSG = srvrClient.CreateMessage();
                outMSG.Write((byte)PacketTypes.ChatMessage);
                outMSG.Write(msg);
                srvrClient.SendMessage(outMSG, srvrClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
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

        public void UpdateWindowPositions(Player[] svrPlayer, int index)
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

        public void MsgBox(string msg, string caption, Canvas svrCanvas)
        {
            MessageBox msgBox = new MessageBox(svrCanvas, msg, caption);
            msgBox.Position(Gwen.Pos.Center);
        }

        public void CreateDebugWindow(Base parent)
        {
            debugWindow = new WindowControl(parent.GetCanvas());
            debugWindow.Title = "Debug";
            debugWindow.SetSize(200, 200);
            debugWindow.SetPosition(10, 10);
            debugWindow.DisableResizing();

            debugFps = new Label(debugWindow);
            debugFps.SetPosition(10, 5);
            debugFps.Text = "FPS: ";

            debugName = new Label(debugWindow);
            debugName.SetPosition(10, 15);
            debugName.Text = "Name: Player";

            debugX = new Label(debugWindow);
            debugX.SetPosition(10, 25);
            debugX.Text = "X: ?";

            debugY = new Label(debugWindow);
            debugY.SetPosition(10, 35);
            debugY.Text = "Y: ?";

            debugDir = new Label(debugWindow);
            debugDir.SetPosition(10, 45);
            debugDir.Text = "Direction : ?";

            debugSprite = new Label(debugWindow);
            debugSprite.SetPosition(10, 55);
            debugSprite.Text = "Sprite: ?";

            debugIP = new Label(debugWindow);
            debugIP.SetPosition(10, 65);
            debugIP.Text = "IP Address: ?";

            debugPort = new Label(debugWindow);
            debugPort.SetPosition(10, 75);
            debugPort.Text = "Port: ?";

            debugLatency = new Label(debugWindow);
            debugLatency.SetPosition(10, 85);
            debugLatency.Text = "Latency: ?";

            debugPacketsIn = new Label(debugWindow);
            debugPacketsIn.SetPosition(10, 95);
            debugPacketsIn.Text = "Packets In: ?";

            debugPacketsOut = new Label(debugWindow);
            debugPacketsOut.SetPosition(10, 105);
            debugPacketsOut.Text = "Packets Out: ?";
        }

        public void UpdateDebugWindow(int fps, Player[] svrPlayer, int drawIndex)
        {
            if (debugWindow != null)
            {
                debugWindow.Title = "Debug Window - Admin";
                //debugWindow.SetPosition(0, 0);
                debugFps.Text = "FPS: " + fps;
                debugName.Text = "Name: " + svrPlayer[drawIndex].Name + " (" + drawIndex + ")";
                debugX.Text = "X: " + (svrPlayer[drawIndex].X + svrPlayer[drawIndex].offsetX);
                debugY.Text = "Y: " + (svrPlayer[drawIndex].Y + svrPlayer[drawIndex].offsetY);
                debugDir.Text = "Direction: " + svrPlayer[drawIndex].Direction;
                debugSprite.Text = "Sprite: " + svrPlayer[drawIndex].Sprite;
                debugIP.Text = "IP Address: " + srvrClient.ServerConnection.RemoteEndPoint.Address.ToString();
                debugPort.Text = "Port: " + srvrClient.ServerConnection.RemoteEndPoint.Port.ToString();
                debugLatency.Text = "Latency: " + srvrClient.ServerConnection.AverageRoundtripTime.ToString("#.###") + "ms";
                debugPacketsIn.Text = "Packets Received: " + srvrClient.Statistics.ReceivedPackets.ToString();
                debugPacketsOut.Text = "Packets Sent: " + srvrClient.Statistics.SentPackets.ToString();
            } 
        }
    }
}
