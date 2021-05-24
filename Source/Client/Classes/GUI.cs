using Gwen.Control;
using static System.Environment;
using Lidgren.Network;
using System.Threading;
using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static System.Convert;
using System.Text;
using System.Data.SQLite;
using static SabertoothClient.Client;
using static SabertoothClient.Globals;
using AccountKeyGenClass;

namespace SabertoothClient
{
    public class GUI
    {
        #region Main Classes
        static Player player;
        public bool Ready;
        #endregion

        #region DebugWindow
        public WindowControl d_Window;
        public Label d_Controller;
        public Label d_ConDir;
        Label d_ConButton;
        Label d_Axis;
        Label d_mouseX;
        Label d_mouseY;
        Label d_tMouseX;
        Label d_tMouseY;
        public Label d_Region;
        Label d_FPS;
        Label d_Name;
        Label d_X;
        Label d_Y;
        Label d_Map;
        Label d_Dir;
        Label d_aDir;
        Label d_Sprite;
        Label d_IP;
        Label d_Port;
        Label d_Latency;
        Label d_packetsIn;
        Label d_packetsOut;
        #endregion

        #region Main Menu UI
        public static WindowControl loadWindow;
        Label loadLabel;

        public WindowControl mainWindow;
        Button mainbuttonLog;
        Button mainbuttonReg;
        Button mainbuttonOpt;
        Button mainbuttonExit;

        public WindowControl regWindow;
        Label unregLabel;
        Label emailregLabel;
        Label pwregLabel;
        Label repwLabel;
        TextBox unregBox;
        TextBox emailregBox;
        TextBoxPassword pwregBox;
        TextBoxPassword repwBox;
        Button regButton;
        Button canregButton;

        public WindowControl logWindow;
        Label unlogLabel;
        Label pwloglabel;
        TextBox unlogBox;
        TextBoxPassword pwlogBox;
        LabeledCheckBox logRemember;
        Button logButton;
        Button canlogButton;

        public WindowControl optWindow;
        LabeledCheckBox enableFullscreen;
        LabeledCheckBox enableVsync;
        TextBox optipAddress;
        TextBox optPort;
        Label optipLabel;
        Label optportLabel;
        Button saveoptButton;
        Button canoptButton;

        public WindowControl activeWindow;
        Label activeLabel;
        TextBox activeBox;
        Button activeOK;
        Button activeCancel;
        #endregion

        #region Chat Box
        public WindowControl chatWindow;
        public ListBox outputChat;
        public TextBox inputChat;
        #endregion

        #region Shop Window
        public WindowControl shopWindow;
        ImagePanel[] shopPic = new ImagePanel[25];
        Button closeShop;

        public WindowControl shopStatWindow;
        ImagePanel shopStatPic;
        Label shopName;
        Label shopDamage;
        Label shopArmor;
        Label shopHeRestore;
        Label shopMaRestore;
        Label shopStr;
        Label shopAgi;
        Label shopInt;
        Label shopEng;
        Label shopSta;
        Label shopASpeed;
        Label shopType;
        Label shopValue;
        Label shopPrice;
        #endregion

        #region Hotbar
        public ImagePanel hotBarWindow;
        ImagePanel[] hotbarPic = new ImagePanel[10];
        Label[] hotBarLabel = new Label[10];
        bool isMoveHotBar;
        int hotBarSlot;
        int currentSlot;
        #endregion

        #region Bank
        public WindowControl bankWindow;
        ImagePanel[] bankPic = new ImagePanel[50];
        Label[] bankPicValue = new Label[50];
        Button bankClose;
        bool isMoveBank;
        int oldBankSlot;

        public WindowControl bankStatWindow;
        ImagePanel bankStatPic;
        Label bankName;
        Label bankDamage;
        Label bankArmor;
        Label bankHeRestore;
        Label bankMaRestore;
        Label bankStr;
        Label bankAgi;
        Label bankInt;
        Label bankEng;
        Label bankSta;
        Label bankASpeed;
        Label bankType;
        Label bankValue;
        Label bankPrice;
        #endregion

        #region Npc Chat Window
        public WindowControl npcChatWindow;
        Label npcChatName;
        ListBox npcChatMessage;
        Button[] npcChatOption = new Button[4];
        #endregion

        #region Menu Window
        public WindowControl menuWindow;
        public TabControl menuTabs;
        #endregion

        #region Chest Window
        public WindowControl chestWindow;
        ImagePanel[] chestPic = new ImagePanel[10];
        Button chestClose;

        public WindowControl chestStatWindow;
        ImagePanel chestStatPic;
        Label chestName;
        Label chestDamage;
        Label chestArmor;
        Label chestHeRestore;
        Label chestMaRestore;
        Label chestStr;
        Label chestAgi;
        Label chestInt;
        Label chestEng;
        Label chestSta;
        Label chestASpeed;
        Label chestType;
        Label chestValue;
        Label chestPrice;
        #endregion

        #region CharTab
        public TabButton charTab;
        Label charName;
        Label charLevel;
        Label charExp;
        Label charWallet;
        Label charHealth;
        Label charMana;
        Label charArmor;
        Label charStr;
        Label charAgi;
        Label charInt;
        Label charSta;
        Label charEng;
        Label playTime;
        #endregion

        #region PackTab
        public TabButton packTab;
        ImagePanel[] invPic = new ImagePanel[25];
        Label[] invValue = new Label[25];
        bool isMoveInv;
        int oldInvSlot;

        public WindowControl statWindow;
        ImagePanel statPic;
        Label packName;
        Label packDamage;
        Label packArmor;
        Label packHeRestore;
        Label packMaRestore;
        Label packStr;
        Label packAgi;
        Label packInt;
        Label packEng;
        Label packSta;
        Label packASpeed;
        Label packType;
        Label packValue;
        Label packPrice;
        Label packStack;
        #endregion

        #region EquipTab
        public TabButton equipTab;
        ImagePanel equipMain;
        ImagePanel equipOff;
        ImagePanel equipChest;
        ImagePanel equipLegs;
        ImagePanel equipFeet;

        GroupBox equipBonus;
        Label equipArmor;
        Label equipMDamage;
        Label equipODamage;
        Label equipStr;
        Label equipAgi;
        Label equipInt;
        Label equipSta;
        Label equipEng;
        #endregion

        #region StackWindow
        WindowControl stackWindow;
        Label stackItemName;
        Label stackItemValue;
        HorizontalSlider stackSlider;
        int tranAmount;
        Button stackOk;
        Button stackCncl;
        #endregion

        #region SpellsTab
        public TabButton spellsTab;
        #endregion

        #region QuestTab
        public TabButton questTab;
        public ListBox questList;
        public ListBox questDetails;
        #endregion

        #region OptionsTab
        public TabButton optionsTab;
        Button optLog;
        #endregion

        public GUI() { }

        #region Update Voids
        public void SetIndexPlayer()
        {
            player = players[HandleData.myIndex];
        }

        public void UpdateBankWindow()
        {
            if (bankWindow != null && bankWindow.IsVisible)
            {
                bankWindow.Title = player.Name + "'s Bank";
                for (int i = 0; i < MAX_BANK_SLOTS; i++)
                {
                    if (player.Bank[i].Name != "None" && player.Bank[i].Sprite > 0)
                    {
                        bankPic[i].ImageName = "Resources/Items/" + player.Bank[i].Sprite + ".png";
                        Gwen.DragDrop.Package package = new Gwen.DragDrop.Package();
                        package = bankPic[i].DragAndDrop_GetPackage(bankPic[i].X, bankPic[i].Y);
                        package.IsDraggable = true;
                    }
                    else
                    {
                        bankPic[i].ImageName = "Resources/Skins/EmptyBkg.png";
                        //bankPic[i].ImageName = "Resources/Skins/EmptyBkg_Test.png";
                        Gwen.DragDrop.Package package = new Gwen.DragDrop.Package();
                        package = bankPic[i].DragAndDrop_GetPackage(bankPic[i].X, bankPic[i].Y);
                        package.IsDraggable = false;
                    }

                    if (player.Bank[i].Stackable)
                    {
                        bankPicValue[i].Text = "x" + player.Bank[i].Value.ToString();
                        bankPicValue[i].TextColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        bankPicValue[i].Text = "";
                    }

                    if (bankPic[i].IsHovered && isMoveBank)
                    {
                        if (Gwen.DragDrop.DragAndDrop.SourceControl == null)
                        {
                            if (!Gwen.Input.InputHandler.IsLeftMouseDown)
                            {
                                player.SendSwapBankSlots(oldBankSlot, i);
                                oldBankSlot = -1;
                                isMoveBank = false;
                            }
                        }
                    }

                    if (bankPic[i].IsHovered)
                    {
                        if (player.Bank[i].Name != "None")
                        {
                            SetStatWindow(bankPic[i].X, bankPic[i].Y, player.Bank[i]);
                        }
                    }
                }
            }
        }

        public void UpdateChestWindow()
        {
            if (chestWindow != null && chestWindow.IsVisible)
            {
                int chestNum = player.chestNum;
                chestWindow.Title = chests[chestNum].Name;
                for (int i = 0; i < 10; i++)
                {
                    if (chests[chestNum].ChestItem[i].Name != "None")
                    {
                        chestPic[i].ImageName = "Resources/Items/" + items[chests[chestNum].ChestItem[i].ItemNum - 1].Sprite + ".png";
                        chestPic[i].Show();
                    }
                    else
                    {
                        chestPic[i].Hide();
                    }
                    if (chestPic[i].IsHovered)
                    {
                        if (chests[chestNum].ChestItem[i].Name != "None")
                        {
                            SetChestStatWindow(chestPic[i].X, chestPic[i].Y, items[chests[chestNum].ChestItem[i].ItemNum - 1]);
                            break;
                        }
                    }
                    else
                    {
                        RemoveChestStatWindow();
                    }
                }
            }
        }

        public void UpdateShopWindow()
        {
            if (shopWindow != null && shopWindow.IsVisible)
            {
                int shopNum = player.shopNum;
                shopWindow.Title = shops[shopNum].Name;
                for (int i = 0; i < 25; i++)
                {
                    if (shops[shopNum].shopItem[i].Name != "None")
                    {
                        shopPic[i].ImageName = "Resources/Items/" + items[shops[shopNum].shopItem[i].ItemNum - 1].Sprite + ".png";
                        shopPic[i].Show();
                    }
                    else
                    {
                        shopPic[i].Hide();
                    }
                    if (shopPic[i].IsHovered)
                    {
                        if (shops[shopNum].shopItem[i].Name != "None")
                        {
                            SetShopStatWindow(shopPic[i].X, shopPic[i].Y, items[shops[shopNum].shopItem[i].ItemNum - 1], shops[shopNum].shopItem[i].Cost);
                            break;
                        }
                    }
                    else
                    {
                        RemoveShopStatWindow();
                    }
                }
            }
        }

        public void UpdateHotBar()
        {
            if (hotBarWindow != null && hotBarWindow.IsVisible)
            {
                for (int i = 0; i < MAX_PLAYER_HOTBAR; i++)
                {
                    if (player.hotBar[i].InvNumber > -1)
                    {
                        hotbarPic[i].ImageName = "Resources/Items/" + player.Backpack[player.hotBar[i].InvNumber].Sprite + ".png";
                    }
                    else if (player.hotBar[i].SpellNumber > -1)
                    {
                        //spell stuff
                    }
                    else
                    {
                        hotbarPic[i].ImageName = "Resources/Skins/HotBarIcon.png";
                    }

                    if (hotbarPic[i].IsHovered && isMoveHotBar)
                    {
                        if (Gwen.DragDrop.DragAndDrop.SourceControl == null)
                        {
                            if (!Gwen.Input.InputHandler.IsLeftMouseDown)
                            {
                                player.SendUpdateHotbar(hotBarSlot, i);
                                hotBarSlot = -1;
                                oldInvSlot = -1;
                                isMoveHotBar = false;
                                isMoveInv = false;
                            }
                        }
                    }
                }
            }
        }

        public void UpdateMenuWindow()
        {
            if (menuWindow != null && player != null && menuWindow.IsVisible)
            {
                if (charTab.IsActive)
                {
                    #region Character
                    charName.Text = player.Name;
                    charLevel.Text = "Level: " + player.Level;
                    charExp.Text = "Experience: " + player.Experience + " / " + (player.Level * 450);
                    charWallet.Text = "Wallet: " + player.Wallet + "G";               

                    charHealth.Text = "Health: " + player.Health + " / " + player.MaxHealth;
                    charMana.Text = "Mana: " + player.Mana + " / " + player.MaxMana;

                    charArmor.Text = "Armor: " + player.Armor;
                    charStr.Text = "Strength: " + player.Strength;
                    charAgi.Text = "Agility: " + player.Agility;
                    charInt.Text = "Intelligence: " + player.Intelligence;
                    charSta.Text = "Stamina: " + player.Stamina;
                    charEng.Text = "Energy: " + player.Energy;

                    playTime.Text = "Play Time: " + player.PlayDays + "D " + player.PlayHours + "H " + player.PlayMinutes + "M " + player.PlaySeconds + "S";                  
                    #endregion
                }
                if (packTab.IsActive)
                {
                    #region BackPack                    
                    for (int i = 0; i < MAX_INV_SLOTS; i++)
                    {
                        if (player.Backpack[i].Name != "None" && player.Backpack[i].Sprite > 0)
                        {
                            invPic[i].ImageName = "Resources/Items/" + player.Backpack[i].Sprite + ".png";
                            Gwen.DragDrop.Package package = new Gwen.DragDrop.Package();
                            package = invPic[i].DragAndDrop_GetPackage(invPic[i].X, invPic[i].Y);
                            package.IsDraggable = true;
                        }
                        else
                        {
                            invPic[i].ImageName = "Resources/Skins/EmptyBkg.png";
                            //invPic[i].ImageName = "Resources/Skins/EmptyBkg_Test.png";
                            Gwen.DragDrop.Package package = new Gwen.DragDrop.Package();
                            package = invPic[i].DragAndDrop_GetPackage(invPic[i].X, invPic[i].Y);
                            package.IsDraggable = false;
                        }

                        if (player.Backpack[i].Stackable)
                        {
                            invValue[i].Text = "x" + player.Backpack[i].Value.ToString();
                            invValue[i].TextColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            invValue[i].Text = "";
                        }

                        if (invPic[i].IsHovered && isMoveInv)
                        {
                            if (Gwen.DragDrop.DragAndDrop.SourceControl == null)
                            {
                                if (!Gwen.Input.InputHandler.IsLeftMouseDown)
                                {
                                    player.SendSwapInvSlots(oldInvSlot, i);
                                    if (IsItemOnHotBar(oldInvSlot)) { player.SendUpdateHotbar(i, currentSlot); }
                                    oldInvSlot = -1;                                    
                                    isMoveInv = false;
                                }
                            }
                        }

                        if (invPic[i].IsHovered)
                        {
                            if (player.Backpack[i].Name != "None")
                            {
                                SetStatWindow(invPic[i].X, invPic[i].Y, player.Backpack[i]);
                            }
                        }
                    }
                    #endregion
                }
                if (equipTab.IsActive)
                {
                    #region Equipment
                    if (player.MainHand.Name != "None")
                    {
                        equipMain.ImageName = "Resources/Items/" + player.MainHand.Sprite + ".png";
                    }
                    else
                    {
                        equipMain.ImageName = "Resources/Skins/EmptyBkg.png";
                    }

                    if (player.OffHand.Name != "None")
                    {
                        equipOff.ImageName = "Resources/Items/" + player.OffHand.Sprite + ".png";
                    }
                    else
                    {
                        equipOff.ImageName = "Resources/Skins/EmptyBkg.png";
                    }

                    if (player.Chest.Name != "None")
                    {
                        equipChest.ImageName = "Resources/Items/" + player.Chest.Sprite + ".png";
                    }
                    else
                    {
                        equipChest.ImageName = "Resources/Skins/EmptyBkg.png";
                    }

                    if (player.Legs.Name != "None")
                    {
                        equipLegs.ImageName = "Resources/Items/" + player.Legs.Sprite + ".png";
                    }
                    else
                    {
                        equipLegs.ImageName = "Resources/Skins/EmptyBkg.png";
                    }

                    if (player.Feet.Name != "None")
                    {
                        equipFeet.ImageName = "Resources/Items/" + player.Feet.Sprite + ".png";                        
                    }
                    else
                    {
                        equipFeet.ImageName = "Resources/Skins/EmptyBkg.png";
                    }

                    if (equipMain.IsHovered && player.MainHand.Name != "None")
                    {
                        SetStatWindow(equipMain.X, equipMain.Y, player.MainHand);
                    }
                    else if (equipOff.IsHovered && player.OffHand.Name != "None")
                    {
                        SetStatWindow(equipMain.X, equipMain.Y, player.OffHand);
                    }
                    else if (equipChest.IsHovered && player.Chest.Name != "None")
                    {
                        SetStatWindow(equipChest.X, equipChest.Y, player.Chest);
                    }
                    else if (equipLegs.IsHovered && player.Legs.Name != "None")
                    {
                        SetStatWindow(equipLegs.X, equipLegs.Y, player.Legs);
                    }
                    else if (equipFeet.IsHovered && player.Feet.Name != "None")
                    {
                        SetStatWindow(equipFeet.X, equipFeet.Y, player.Feet);
                    }

                    equipMDamage.Text = "Main Damage: " + player.MainHand.Damage;
                    equipODamage.Text = "Off Damage: " + player.OffHand.Damage;
                    equipArmor.Text = "Armor: " + player.ArmorBonus(true);
                    equipStr.Text = "Strength: " + player.StrengthBonus(true);
                    equipAgi.Text = "Agility: " + player.AgilityBonus(true);
                    equipInt.Text = "Intelligence: " + player.IntelligenceBonus(true);
                    equipSta.Text = "Stamina: " + player.StaminaBonus(true);
                    equipEng.Text = "Energy: " + player.EnergyBonus(true);
                    #endregion
                }
            }
        }

        bool IsItemOnHotBar(int slot)
        {
            for (int i = 0; i < MAX_PLAYER_HOTBAR; i++)
            {
                if (player.hotBar[i].InvNumber == slot)
                {
                    currentSlot = i;
                    return true;
                }
            }
            currentSlot = -1;
            return false;            
        }

        public void UpdateDebugWindow(int fps)
        {
            if (d_Window != null && player != null && d_Window.IsVisible)
            {
                d_Window.Title = "Debug Window - Admin";
                d_FPS.Text = "FPS: " + fps;
                d_Name.Text = "Name: " + player.Name + " (" + HandleData.myIndex + ")";
                d_X.Text = "X: " + (player.X + player.OffsetX);
                d_Y.Text = "Y: " + (player.Y + player.OffsetY);
                d_Map.Text = "Map: " + player.Map;
                d_Dir.Text = "Direction: " + player.Direction;
                d_aDir.Text = "Aim Direction: " + player.AimDirection;
                d_Sprite.Text = "Sprite: " + player.Sprite;

                if (SabertoothClient.netClient.ServerConnection != null)
                {
                    d_IP.Text = "IP Address: " + SabertoothClient.netClient.ServerConnection.RemoteEndPoint.Address.ToString();
                    d_Port.Text = "Port: " + SabertoothClient.netClient.ServerConnection.RemoteEndPoint.Port.ToString();
                    d_Latency.Text = "Latency: " + SabertoothClient.netClient.ServerConnection.AverageRoundtripTime.ToString(".0#0").TrimStart('0', '.', '0') + "ms";
                    d_packetsIn.Text = "Packets Received: " + SabertoothClient.netClient.Statistics.ReceivedPackets.ToString();
                    d_packetsOut.Text = "Packets Sent: " + SabertoothClient.netClient.Statistics.SentPackets.ToString();
                }

                if (Joystick.IsConnected(0))
                {
                    uint bcount = Joystick.GetButtonCount(0);
                    string buttonNum = "None";
                    for (uint i = 0; i < bcount; i++)
                    {
                        if (Joystick.IsButtonPressed(0, i))
                        {
                            buttonNum = i.ToString();
                            break;
                        }
                    }
                    d_ConButton.Text = "Button: " + buttonNum;

                    if (Joystick.GetAxisPosition(0, Joystick.Axis.V) > 35)
                    {
                        d_Axis.Text = "Axis: V";
                    }
                    else if (Joystick.GetAxisPosition(0, Joystick.Axis.U) > 35)
                    {
                        d_Axis.Text = "Axis: U";
                    }
                    else if (Joystick.GetAxisPosition(0, Joystick.Axis.R) > 35)
                    {
                        d_Axis.Text = "Axis: R";
                    }
                    else if (Joystick.GetAxisPosition(0, Joystick.Axis.Z) > 25 || Joystick.GetAxisPosition(0, Joystick.Axis.Z) < -25)
                    {
                        d_Axis.Text = "Axis: Z";
                    }
                }

                d_mouseX.Text = "Mouse X: " + Gwen.Input.InputHandler.MousePosition.X;
                d_mouseY.Text = "Mouse Y: " + Gwen.Input.InputHandler.MousePosition.Y;
                d_tMouseX.Text = "Tile Mouse X: " + (Gwen.Input.InputHandler.MousePosition.X / PIC_X);
                d_tMouseY.Text = "Tile Mouse Y: " + (Gwen.Input.InputHandler.MousePosition.Y / PIC_Y);
            }
        }

        void SetStatWindow(int x, int y, Item statItem)
        {
            int locX = (x + INV_STAT_WINDOW_X);
            int locY = (y + INV_STAT_WINDOW_Y);
            statWindow.SetPosition(locX, locY);
            statWindow.Title = statItem.Name;
            statPic.ImageName = "Resources/Items/" + statItem.Sprite + ".png";
            packDamage.Hide();
            packArmor.Hide();
            packHeRestore.Hide();
            packMaRestore.Hide();
            packStr.Hide();
            packAgi.Hide();
            packInt.Hide();
            packEng.Hide();
            packSta.Hide();
            packASpeed.Hide();
            packType.Hide();
            packValue.Hide();
            packPrice.Hide();
            packStack.Hide();

            packName.Text = statItem.Name;
            switch (statItem.Rarity)
            {
                case (int)Rarity.Normal:
                    packName.TextColor = System.Drawing.Color.Gray;
                    break;
                case (int)Rarity.Uncommon:
                    packName.TextColor = System.Drawing.Color.LightGreen;
                    break;
                case (int)Rarity.Rare:
                    packName.TextColor = System.Drawing.Color.Blue;
                    break;
                case (int)Rarity.UltraRare:
                    packName.TextColor = System.Drawing.Color.Purple;
                    break;
                case (int)Rarity.Legendary:
                    packName.TextColor = System.Drawing.Color.Orange;
                    break;
                case (int)Rarity.Admin:
                    packName.TextColor = System.Drawing.Color.Red;
                    break;
            }
            int n = 15;
            packType.SetPosition(3, n);
            switch (statItem.Type)
            {
                case (int)ItemType.MainHand:
                    packType.Text = "Main Hand";
                    break;
                case (int)ItemType.OffHand:
                    packType.Text = "Off Hand";
                    break;
                case (int)ItemType.Currency:
                    packType.Text = "Currency";
                    break;
                case (int)ItemType.Food:
                    packType.Text = "Food";
                    break;
                case (int)ItemType.Drink:
                    packType.Text = "Drink";
                    break;
                case (int)ItemType.Potion:
                    packType.Text = "Potion";
                    break;
                case (int)ItemType.Shirt:
                    packType.Text = "Chest";
                    break;
                case (int)ItemType.Pants:
                    packType.Text = "Legs";
                    break;
                case (int)ItemType.Shoes:
                    packType.Text = "Feet";
                    break;
                case (int)ItemType.Book:
                    packType.Text = "Book";
                    break;
                default:
                    packType.Text = "Other";
                    break;
            }
            packType.Show();
            if (statItem.Damage > 0)
            {
                n += 10;
                packDamage.SetPosition(3, n);
                packDamage.Text = "Damage: " + statItem.Damage;
                packDamage.Show();
            }

            if (statItem.Armor > 0)
            {
                n += 10;
                packArmor.SetPosition(3, n);
                packArmor.Text = "Armor: " + statItem.Armor;
                packArmor.Show();
            }

            if (statItem.HealthRestore > 0)
            {
                n += 10;
                packHeRestore.SetPosition(3, n);
                packHeRestore.Text = "Health Restore: " + statItem.HealthRestore;
                packHeRestore.Show();
            }

            if (statItem.ManaRestore> 0)
            {
                n += 10;
                packMaRestore.SetPosition(3, n);
                packMaRestore.Text = "Mana Restore: " + statItem.ManaRestore;
                packMaRestore.Show();
            }

            if (statItem.Strength > 0)
            {
                n += 10;
                packStr.SetPosition(3, n);
                packStr.Text = "Strength: " + statItem.Strength;
                packStr.Show();
            }

            if (statItem.Agility > 0)
            {
                n += 10;
                packAgi.SetPosition(3, n);
                packAgi.Text = "Agility: " + statItem.Agility;
                packAgi.Show();
            }

            if (statItem.Intelligence > 0)
            {
                n += 10;
                packInt.SetPosition(3, n);
                packInt.Text = "Intelligence: " + statItem.Intelligence;
                packInt.Show();
            }

            if (statItem.Energy > 0)
            {
                n += 10;
                packEng.SetPosition(3, n);
                packEng.Text = "Energy: " + statItem.Energy;
                packEng.Show();
            }

            if (statItem.Stamina > 0)
            {
                n += 10;
                packSta.SetPosition(3, n);
                packSta.Text = "Stamina: " + statItem.Stamina;
                packSta.Show();
            }

            if (statItem.AttackSpeed > 0)
            {
                n += 10;
                packASpeed.SetPosition(3, n);
                packASpeed.Text = "Attack Speed: " + ((float)statItem.AttackSpeed / 1000).ToString("#.##") + " s";
                packASpeed.Show();
            }

            if (statItem.Value > 1)
            {
                n += 10;
                packValue.SetPosition(3, n);
                packValue.Text = "Value: " + statItem.Value;
                packValue.Show();
            }

            if (statItem.Price > 0)
            {
                n += 10;
                packPrice.SetPosition(3, n);
                packPrice.Text = "Price: " + statItem.Price;
                packPrice.Show();
            }

            if (statItem.MaxStack > 1)
            {
                n += 10;
                packStack.SetPosition(3, n);
                packStack.Text = "Max Stack: " + statItem.MaxStack;
                packStack.Show();
            }

            statWindow.Show();
        }

        void SetShopStatWindow(int x, int y, Item statItem, int price)
        {
            int locX = (x + SHOP_STAT_WINDOW_X);
            int locY = (y + SHOP_STAT_WINDOW_Y);
            shopStatWindow.SetPosition(locX, locY);
            shopStatWindow.Title = statItem.Name;
            shopStatPic.ImageName = "Resources/Items/" + statItem.Sprite + ".png";       
            shopDamage.Hide();
            shopArmor.Hide();
            shopHeRestore.Hide();
            shopMaRestore.Hide();
            shopStr.Hide();
            shopAgi.Hide();
            shopInt.Hide();
            shopEng.Hide();
            shopSta.Hide();
            shopASpeed.Hide();
            shopType.Hide();
            shopValue.Hide();
            shopPrice.Hide();

            shopName.Text = statItem.Name;
            switch (statItem.Rarity)
            {
                case (int)Rarity.Normal:
                    shopName.TextColor = System.Drawing.Color.Gray;
                    break;
                case (int)Rarity.Uncommon:
                    shopName.TextColor = System.Drawing.Color.Green;
                    break;
                case (int)Rarity.Rare:
                    shopName.TextColor = System.Drawing.Color.Blue;
                    break;
                case (int)Rarity.UltraRare:
                    shopName.TextColor = System.Drawing.Color.Purple;
                    break;
                case (int)Rarity.Legendary:
                    shopName.TextColor = System.Drawing.Color.Orange;
                    break;
                case (int)Rarity.Admin:
                    shopName.TextColor = System.Drawing.Color.Red;
                    break;
            }
            int n = 15;
            shopType.SetPosition(3, n);
            switch (statItem.Type)
            {
                case (int)ItemType.MainHand:
                    shopType.Text = "Main Hand";
                    break;
                case (int)ItemType.OffHand:
                    shopType.Text = "Off Hand";
                    break;
                case (int)ItemType.Currency:
                    shopType.Text = "Currency";
                    break;
                case (int)ItemType.Food:
                    shopType.Text = "Food";
                    break;
                case (int)ItemType.Drink:
                    shopType.Text = "Drink";
                    break;
                case (int)ItemType.Potion:
                    shopType.Text = "Potion";
                    break;
                case (int)ItemType.Shirt:
                    shopType.Text = "Chest";
                    break;
                case (int)ItemType.Pants:
                    shopType.Text = "Legs";
                    break;
                case (int)ItemType.Shoes:
                    shopType.Text = "Feet";
                    break;
                case (int)ItemType.Book:
                    shopType.Text = "Book";
                    break;
                default:
                    shopType.Text = "Other";
                    break;
            }
            shopType.Show();
            if (statItem.Damage > 0)
            {
                n += 10;
                shopDamage.SetPosition(3, n);
                shopDamage.Text = "Damage: " + statItem.Damage;
                shopDamage.Show();
            }

            if (statItem.Armor > 0)
            {
                n += 10;
                shopArmor.SetPosition(3, n);
                shopArmor.Text = "Armor: " + statItem.Armor;
                shopArmor.Show();
            }

            if (statItem.HealthRestore > 0)
            {
                n += 10;
                shopHeRestore.SetPosition(3, n);
                shopHeRestore.Text = "Health Restore: " + statItem.HealthRestore;
                shopHeRestore.Show();
            }

            if (statItem.ManaRestore > 0)
            {
                n += 10;
                shopMaRestore.SetPosition(3, n);
                shopMaRestore.Text = "Mana Restore: " + statItem.ManaRestore;
                shopMaRestore.Show();
            }

            if (statItem.Strength > 0)
            {
                n += 10;
                shopStr.SetPosition(3, n);
                shopStr.Text = "Strength: " + statItem.Strength;
                shopStr.Show();
            }

            if (statItem.Agility > 0)
            {
                n += 10;
                shopAgi.SetPosition(3, n);
                shopAgi.Text = "Agility: " + statItem.Agility;
                shopAgi.Show();
            }

            if (statItem.Intelligence > 0)
            {
                n += 10;
                shopInt.SetPosition(3, n);
                shopInt.Text = "Intelligence: " + statItem.Intelligence;
                shopInt.Show();
            }

            if (statItem.Energy > 0)
            {
                n += 10;
                shopEng.SetPosition(3, n);
                shopEng.Text = "Energy: " + statItem.Energy;
                shopEng.Show();
            }

            if (statItem.Stamina > 0)
            {
                n += 10;
                shopSta.SetPosition(3, n);
                shopSta.Text = "Stamina: " + statItem.Stamina;
                shopSta.Show();
            }

            if (statItem.AttackSpeed > 0)
            {
                n += 10;
                shopASpeed.SetPosition(3, n);
                shopASpeed.Text = "Attack Speed: " + ((float)statItem.AttackSpeed / 1000).ToString("#.##") + " s";
                shopASpeed.Show();
            }

            if (statItem.Value > 1)
            {
                n += 10;
                shopValue.SetPosition(3, n);
                shopValue.Text = "Value: " + statItem.Value;
                shopValue.Show();
            }

            if (statItem.Price > 0)
            {
                n += 10;
                shopPrice.SetPosition(3, n);
                if (price > 1) { shopPrice.Text = "Price: " + price; }
                else { shopPrice.Text = "Price: " + statItem.Price; }
                shopPrice.Show();
            }

            shopStatWindow.Show();
        }

        void SetBankStatWindow(int x, int y, Item bankItem)
        {
            int locX = (x + 50);
            int locY = (y + 50);
            bankStatWindow.SetPosition(locX, locY);
            bankStatWindow.Title = bankItem.Name;
            bankStatPic.ImageName = "Resources/Items/" + bankItem.Sprite + ".png";
            bankDamage.Hide();
            bankArmor.Hide();
            bankHeRestore.Hide();
            bankMaRestore.Hide();
            bankStr.Hide();
            bankAgi.Hide();
            bankInt.Hide();
            bankEng.Hide();
            bankSta.Hide();
            bankASpeed.Hide();
            bankType.Hide();
            bankValue.Hide();
            bankPrice.Hide();

            bankName.Text = bankItem.Name;
            switch (bankItem.Rarity)
            {
                case (int)Rarity.Normal:
                    bankName.TextColor = System.Drawing.Color.Gray;
                    break;
                case (int)Rarity.Uncommon:
                    bankName.TextColor = System.Drawing.Color.Green;
                    break;
                case (int)Rarity.Rare:
                    bankName.TextColor = System.Drawing.Color.Blue;
                    break;
                case (int)Rarity.UltraRare:
                    bankName.TextColor = System.Drawing.Color.Purple;
                    break;
                case (int)Rarity.Legendary:
                    bankName.TextColor = System.Drawing.Color.Orange;
                    break;
                case (int)Rarity.Admin:
                    bankName.TextColor = System.Drawing.Color.Red;
                    break;
            }
            int n = 15;
            bankType.SetPosition(3, n);
            switch (bankItem.Type)
            {
                case (int)ItemType.MainHand:
                    bankType.Text = "Main Hand";
                    break;
                case (int)ItemType.OffHand:
                    bankType.Text = "Off Hand";
                    break;
                case (int)ItemType.Currency:
                    bankType.Text = "Currency";
                    break;
                case (int)ItemType.Food:
                    bankType.Text = "Food";
                    break;
                case (int)ItemType.Drink:
                    bankType.Text = "Drink";
                    break;
                case (int)ItemType.Potion:
                    bankType.Text = "Potion";
                    break;
                case (int)ItemType.Shirt:
                    bankType.Text = "Chest";
                    break;
                case (int)ItemType.Pants:
                    bankType.Text = "Legs";
                    break;
                case (int)ItemType.Shoes:
                    bankType.Text = "Feet";
                    break;
                case (int)ItemType.Book:
                    bankType.Text = "Book";
                    break;
                default:
                    bankType.Text = "Other";
                    break;
            }
            bankType.Show();
            if (bankItem.Damage > 0)
            {
                n += 10;
                bankDamage.SetPosition(3, n);
                bankDamage.Text = "Damage: " + bankItem.Damage;
                bankDamage.Show();
            }

            if (bankItem.Armor > 0)
            {
                n += 10;
                bankArmor.SetPosition(3, n);
                bankArmor.Text = "Armor: " + bankItem.Armor;
                bankArmor.Show();
            }

            if (bankItem.HealthRestore > 0)
            {
                n += 10;
                bankHeRestore.SetPosition(3, n);
                bankHeRestore.Text = "Health Restore: " + bankItem.HealthRestore;
                bankHeRestore.Show();
            }

            if (bankItem.ManaRestore> 0)
            {
                n += 10;
                bankMaRestore.SetPosition(3, n);
                bankMaRestore.Text = "Mana Restore: " + bankItem.ManaRestore;
                bankMaRestore.Show();
            }

            if (bankItem.Strength > 0)
            {
                n += 10;
                bankStr.SetPosition(3, n);
                bankStr.Text = "Strength: " + bankItem.Strength;
                bankStr.Show();
            }

            if (bankItem.Agility > 0)
            {
                n += 10;
                bankAgi.SetPosition(3, n);
                bankAgi.Text = "Agility: " + bankItem.Agility;
                bankAgi.Show();
            }

            if (bankItem.Intelligence > 0)
            {
                n += 10;
                bankInt.SetPosition(3, n);
                bankInt.Text = "Intelligence: " + bankItem.Intelligence;
                bankInt.Show();
            }

            if (bankItem.Energy > 0)
            {
                n += 10;
                bankEng.SetPosition(3, n);
                bankEng.Text = "Energy: " + bankItem.Energy;
                bankEng.Show();
            }

            if (bankItem.Stamina > 0)
            {
                n += 10;
                bankSta.SetPosition(3, n);
                bankSta.Text = "Stamina: " + bankItem.Stamina;
                bankSta.Show();
            }

            if (bankItem.AttackSpeed > 0)
            {
                n += 10;
                bankASpeed.SetPosition(3, n);
                bankASpeed.Text = "Attack Speed: " + ((float)bankItem.AttackSpeed / 1000).ToString("#.##") + " s";
                bankASpeed.Show();
            }

            if (bankItem.Value > 1)
            {
                n += 10;
                bankValue.SetPosition(3, n);
                bankValue.Text = "Value: " + bankItem.Value;
                bankValue.Show();
            }

            if (bankItem.Price > 0)
            {
                n += 10;
                bankPrice.SetPosition(3, n);
                bankPrice.Text = "Price: " + bankItem.Price;
                bankPrice.Show();
            }

            bankStatWindow.Show();
        }

        void SetChestStatWindow(int x, int y, Item chestItem)
        {
            int locX = (x + 450);
            int locY = (y + 60);
            chestStatWindow.SetPosition(locX, locY);
            chestStatWindow.Title = chestItem.Name;
            chestStatPic.ImageName = "Resources/Items/" + chestItem.Sprite + ".png";
            chestDamage.Hide();
            chestArmor.Hide();
            chestHeRestore.Hide();
            chestMaRestore.Hide();
            chestStr.Hide();
            chestAgi.Hide();
            chestInt.Hide();
            chestEng.Hide();
            chestSta.Hide();
            chestASpeed.Hide();
            chestType.Hide();
            chestValue.Hide();
            chestPrice.Hide();

            chestName.Text = chestItem.Name;
            switch (chestItem.Rarity)
            {
                case (int)Rarity.Normal:
                    chestName.TextColor = System.Drawing.Color.Gray;
                    break;
                case (int)Rarity.Uncommon:
                    chestName.TextColor = System.Drawing.Color.Green;
                    break;
                case (int)Rarity.Rare:
                    chestName.TextColor = System.Drawing.Color.Blue;
                    break;
                case (int)Rarity.UltraRare:
                    chestName.TextColor = System.Drawing.Color.Purple;
                    break;
                case (int)Rarity.Legendary:
                    chestName.TextColor = System.Drawing.Color.Brown;
                    break;
                case (int)Rarity.Admin:
                    chestName.TextColor = System.Drawing.Color.Red;
                    break;
            }
            int n = 15;
            chestType.SetPosition(3, n);
            switch (chestItem.Type)
            {
                case (int)ItemType.MainHand:
                    chestType.Text = "Main Hand";
                    break;
                case (int)ItemType.OffHand:
                    chestType.Text = "Off Hand";
                    break;
                case (int)ItemType.Currency:
                    chestType.Text = "Currency";
                    break;
                case (int)ItemType.Food:
                    chestType.Text = "Food";
                    break;
                case (int)ItemType.Drink:
                    chestType.Text = "Drink";
                    break;
                case (int)ItemType.Potion:
                    chestType.Text = "Potion";
                    break;
                case (int)ItemType.Shirt:
                    chestType.Text = "Chest";
                    break;
                case (int)ItemType.Pants:
                    chestType.Text = "Legs";
                    break;
                case (int)ItemType.Shoes:
                    chestType.Text = "Feet";
                    break;
                case (int)ItemType.Book:
                    chestType.Text = "Book";
                    break;
                default:
                    chestType.Text = "Other";
                    break;
            }
            chestType.Show();
            if (chestItem.Damage > 0)
            {
                n += 10;
                chestDamage.SetPosition(3, n);
                chestDamage.Text = "Damage: " + chestItem.Damage;
                chestDamage.Show();
            }

            if (chestItem.Armor > 0)
            {
                n += 10;
                chestArmor.SetPosition(3, n);
                chestArmor.Text = "Armor: " + chestItem.Armor;
                chestArmor.Show();
            }

            if (chestItem.HealthRestore > 0)
            {
                n += 10;
                chestHeRestore.SetPosition(3, n);
                chestHeRestore.Text = "Health Restore: " + chestItem.HealthRestore;
                chestHeRestore.Show();
            }

            if (chestItem.ManaRestore > 0)
            {
                n += 10;
                chestMaRestore.SetPosition(3, n);
                chestMaRestore.Text = "Mana Restore: " + chestItem.ManaRestore;
                chestMaRestore.Show();
            }

            if (chestItem.Strength > 0)
            {
                n += 10;
                chestStr.SetPosition(3, n);
                chestStr.Text = "Strength: " + chestItem.Strength;
                chestStr.Show();
            }

            if (chestItem.Agility > 0)
            {
                n += 10;
                chestAgi.SetPosition(3, n);
                chestAgi.Text = "Agility: " + chestItem.Agility;
                chestAgi.Show();
            }

            if (chestItem.Intelligence > 0)
            {
                n += 10;
                chestInt.SetPosition(3, n);
                chestInt.Text = "Intelligence: " + chestItem.Intelligence;
                chestInt.Show();
            }

            if (chestItem.Energy > 0)
            {
                n += 10;
                chestEng.SetPosition(3, n);
                chestEng.Text = "Energy: " + chestItem.Energy;
                chestEng.Show();
            }

            if (chestItem.Stamina > 0)
            {
                n += 10;
                chestSta.SetPosition(3, n);
                chestSta.Text = "Stamina: " + chestItem.Stamina;
                chestSta.Show();
            }

            if (chestItem.AttackSpeed > 0)
            {
                n += 10;
                chestASpeed.SetPosition(3, n);
                chestASpeed.Text = "Attack Speed: " + ((float)chestItem.AttackSpeed / 1000).ToString("#.##") + " s";
                chestASpeed.Show();
            }

            if (chestItem.Value > 1)
            {
                n += 10;
                chestValue.SetPosition(3, n);
                chestValue.Text = "Value: " + chestItem.Value;
                chestValue.Show();
            }

            if (chestItem.Price > 0)
            {
                n += 10;
                chestPrice.SetPosition(3, n);
                chestPrice.Text = "Price: " + chestItem.Price;
                chestPrice.Show();
            }

            chestStatWindow.Show();
        }

        public void RemoveStatWindow()
        {
            statWindow.SetPosition(200, 10);
            statWindow.Hide();
        }

        void RemoveShopStatWindow()
        {
            shopStatWindow.SetPosition(200, 10);
            shopStatWindow.Hide();
        }

        void RemoveBankStatWindow()
        {
            bankStatWindow.SetPosition(200, 10);
            bankStatWindow.Hide();
        }

        void RemoveChestStatWindow()
        {
            chestStatWindow.SetPosition(200, 10);
            chestStatWindow.Hide();
        }

        public void UpdateNpcChatWindow(int chatNum)
        {
            npcChatName.Text = chats[chatNum].Name;

            npcChatMessage.Clear();
            string msg = chats[chatNum].MainMessage;
            int msgLength = msg.Length;
            int maxLength = 70;

            if (msgLength > maxLength)
            {
                int lines = msgLength / maxLength;
                string[] splitMsg = new string[lines];
                int start = 0;

                for (int i = 0; i < lines; i++)
                {
                    splitMsg[i] = msg.Substring(start, maxLength);
                    start += maxLength;
                    npcChatMessage.AddRow(splitMsg[i]);
                }
            }
            else
            {
                npcChatMessage.AddRow(msg);
            }

            for (int i = 0; i < 4; i++)
            {
                npcChatOption[i].Hide();

                if (chats[chatNum].Option[i] != "None")
                {
                    npcChatOption[i].Text = chats[chatNum].Option[i];
                    npcChatOption[i].Show();
                }
            }
        }
        #endregion

        #region Menu Events
        private void CheckLogOutSubmit(Base control, ClickedEventArgs e)
        {
            if (SabertoothClient.netClient.ServerConnection != null)
            {
                players[HandleData.myIndex].SendUpdatePlayerTime();
            }

            canvas.Dispose();
            SabertoothClient.netClient.Disconnect("shutdown");
            Thread.Sleep(500);
            Exit(0);
        }

        private void CheckMainWindowLogin(Base control, ClickedEventArgs e)
        {
            Button button = control as Button;
            CreateLoginWindow(button.GetCanvas());
        }

        private void CheckMainWindowOptions(Base control, ClickedEventArgs e)
        {
            Button button = control as Button;
            CreateOptionsWindow(button.GetCanvas());
        }

        private void CheckMainWindowRegister(Base control, ClickedEventArgs e)
        {
            Button button = control as Button;
            CreateRegisterWindow(button.GetCanvas());
        }

        private void CheckMainWindowExit(Base control, ClickedEventArgs e)
        {
            SabertoothClient.netClient.Disconnect("Shutting Down");
            Thread.Sleep(500);
            Exit(0);
        }

        private void CheckLogWindowCancel(Base control, ClickedEventArgs e)
        {
            Button button = control as Button;
            Base parent = button.Parent;
            parent.Hide();
        }

        private void CheckOptionsWindowCancel(Base control, ClickedEventArgs e)
        {
            Button button = control as Button;
            Base parent = button.Parent;
            parent.Hide();
        }

        private void CheckActiveWindowCancel(Base control, ClickedEventArgs e)
        {
            SabertoothClient.netClient.Disconnect("Shutting Down");
            Thread.Sleep(500);
            Exit(0);
        }

        private void CheckActiveWindowOK(Base control, EventArgs e)
        {
            Base parent = control.Parent;

            if (activeBox.Text != "")
            {
                if (activeBox.Text.Length == 25)
                {
                    string key = activeBox.Text;

                    NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                    outMSG.Write((byte)PacketTypes.AccountKey);
                    outMSG.WriteVariableInt32(HandleData.tempIndex);
                    outMSG.Write(key);
                    SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                    parent.Hide();
                }
                else
                {
                    MsgBox("Key is too short. Please enter 25 character key.", "Activation Failed", canvas);
                }
            }
            else
            {
                MsgBox("Please enter a valid 25 character key.", "Activation Failed", canvas);
            }
        }

        private void CheckLogWindowLogin(Base control, EventArgs e)
        {
            Base parent = control.Parent;

            if (unlogBox.Text != "" && pwlogBox.Text != "")
            {
                if (SabertoothClient.netClient.ServerConnection == null)
                {
                    parent.Hide();
                    MsgBox("Client is not connected to server!", "Not Connected", canvas);
                    return;
                }
                string username = unlogBox.Text;
                string password = pwlogBox.Text;

                if (logRemember.IsChecked == true)
                {
                    SabertoothClient.Remember = true;
                    SabertoothClient.Username = username;
                    SabertoothClient.Password = password;
                    SabertoothClient.SaveConfiguration();
                }
                else
                {
                    SabertoothClient.Remember = false;
                    SabertoothClient.Username = null;
                    SabertoothClient.Password = null;
                    SabertoothClient.SaveConfiguration();
                }

                int result = 0;                
                using (var conn = new SQLiteConnection("Data Source=MapCache.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        cmd.CommandText = "SELECT COUNT(*) FROM MAPS";
                        object queue = cmd.ExecuteScalar();
                        result = ToInt32(queue);
                    }
                }

                NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                outMSG.Write((byte)PacketTypes.Login);
                outMSG.Write(username);
                outMSG.Write(password);
                outMSG.Write(VERSION);
                SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                parent.Hide();
            }
            else
            {
                parent.Hide();
                MsgBox("Please fill out the login info.", "Login Failed", canvas);
            }
        }

        private void CheckLogWindowSubmit(Base control, EventArgs e)
        {
            Base parent = control.Parent;

            if (unlogBox.Text != "" && pwlogBox.Text != "")
            {
                if (SabertoothClient.netClient.ServerConnection == null)
                {
                    parent.Hide();
                    MsgBox("Client is not connected to server!", "Not Connected", canvas);
                    return;
                }
                string username = unlogBox.Text;
                string password = pwlogBox.Text;

                if (logRemember.IsChecked == true)
                {
                    SabertoothClient.Remember = true;
                    SabertoothClient.Username = username;
                    SabertoothClient.Password = password;
                    SabertoothClient.SaveConfiguration();
                }
                else
                {
                    SabertoothClient.Remember = false;
                    SabertoothClient.Username = null;
                    SabertoothClient.Password = null;
                    SabertoothClient.SaveConfiguration();
                }

                NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                outMSG.Write((byte)PacketTypes.Login);
                outMSG.Write(username);
                outMSG.Write(password);
                outMSG.Write(VERSION);
                SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                parent.Hide();
            }
            else
            {
                parent.Hide();
                MsgBox("Please fill out the login info.", "Login Failed", canvas);
            }
        }

        private void CheckRegWindowCancel(Base control, ClickedEventArgs e)
        {
            Base parent = control.Parent;
            parent.Hide();
        }

        private void CheckRegWindowRegister(Base control, ClickedEventArgs e)
        {
            Base parent = control.Parent;

            if (unregBox.Text != "" && pwregBox.Text != "" && repwBox.Text != "" && emailregBox.Text != "")
            {
                if (SabertoothClient.netClient.ServerConnection == null)
                {
                    parent.Hide();
                    MsgBox("Client is not connected to server!", "Not Connected", canvas);
                    return;
                }

                if (pwregBox.Text == repwBox.Text)
                {
                    if (Email.ValidEmailAddress(emailregBox.Text))
                    {
                        string username = unregBox.Text;
                        string password = pwregBox.Text;
                        string email = emailregBox.Text;
                        NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                        outMSG.Write((byte)PacketTypes.Register);
                        outMSG.Write(username);
                        outMSG.Write(password);
                        outMSG.Write(email);
                        SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                        parent.Hide();
                    }
                    else
                    {
                        parent.Hide();
                        MsgBox("Invalid email address!", "Retry", canvas);
                    }
                }
                else
                {
                    parent.Hide();
                    MsgBox("Passwords do not match!", "Retry", canvas);
                }
            }
            else
            {
                parent.Hide();
                MsgBox("Please fill out account info for registration.", "Error", canvas);
            }
        }

        private void CheckRegWindowSubmit(Base control, EventArgs e)
        {
            Base parent = control.Parent;

            if (unregBox.Text != "" && pwregBox.Text != "" && repwBox.Text != "" && emailregBox.Text != "")
            {
                if (SabertoothClient.netClient.ServerConnection == null)
                {
                    parent.Hide();
                    MsgBox("Client is not connected to server!", "Not Connected", canvas);
                    return;
                }

                if (pwregBox.Text == repwBox.Text)
                {
                    if (Email.ValidEmailAddress(emailregBox.Text))
                    {
                        string username = unregBox.Text;
                        string password = pwregBox.Text;
                        string email = emailregBox.Text;
                        NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                        outMSG.Write((byte)PacketTypes.Register);
                        outMSG.Write(username);
                        outMSG.Write(password);
                        outMSG.Write(email);
                        SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                        parent.Hide();
                    }
                    else
                    {
                        parent.Hide();
                        MsgBox("Invalid email address!", "Retry", canvas);
                    }
                }
                else
                {
                    parent.Hide();
                    MsgBox("Passwords do not match!", "Retry", canvas);
                }
            }
            else
            {
                parent.Hide();
                MsgBox("Please fill out account info for registration.", "Error", canvas);
            }
        }

        private void CheckChatWindowSubmit(Base control, EventArgs e)
        {
            if (inputChat.Text != "")
            {
                string msg = inputChat.Text;
                NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                outMSG.Write((byte)PacketTypes.ChatMessage);
                outMSG.Write(msg);
                SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                inputChat.Text = "";
            }
        }

        private void CheckOptionWindowSave(Base control, EventArgs e)
        {
            Base parent = control.Parent;

            if (enableFullscreen.IsChecked) { SabertoothClient.Fullscreen = true; }
            else { SabertoothClient.Fullscreen = false; }

            if (enableVsync.IsChecked) { SabertoothClient.VSync = true; renderWindow.SetFramerateLimit(0); }
            else { SabertoothClient.VSync = false; renderWindow.SetFramerateLimit(MAX_FPS); }

            renderWindow.SetVerticalSyncEnabled(SabertoothClient.VSync);

            SabertoothClient.IPAddress = optipAddress.Text;
            SabertoothClient.Port = optPort.Text;

            SabertoothClient.SaveConfiguration();
            parent.Hide();
        }

        private void EquipOff_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            
            if (player.OffHand.Name != "None")
            {
                NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                outMSG.Write((byte)PacketTypes.UnequipItem);
                outMSG.WriteVariableInt32(HandleData.myIndex);
                outMSG.WriteVariableInt32((int)EquipSlots.OffWeapon);
                SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            }
            equipTab.Focus();
        }

        private void EquipMain_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            

            if (player.MainHand.Name != "None")
            {
                NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                outMSG.Write((byte)PacketTypes.UnequipItem);
                outMSG.WriteVariableInt32(HandleData.myIndex);
                outMSG.WriteVariableInt32((int)EquipSlots.MainWeapon);
                SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            }
            equipTab.Focus();
        }

        private void EquipChest_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            

            if (player.Chest.Name != "None")
            {
                NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                outMSG.Write((byte)PacketTypes.UnequipItem);
                outMSG.WriteVariableInt32(HandleData.myIndex);
                outMSG.WriteVariableInt32((int)EquipSlots.Chest);
                SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            }
            equipTab.Focus();
        }

        private void EquipLegs_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            

            if (player.Legs.Name != "None")
            {
                NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                outMSG.Write((byte)PacketTypes.UnequipItem);
                outMSG.WriteVariableInt32(HandleData.myIndex);
                outMSG.WriteVariableInt32((int)EquipSlots.Legs);
                SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            }
            equipTab.Focus();
        }

        private void EquipFeet_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            

            if (player.Feet.Name != "None")
            {
                NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                outMSG.Write((byte)PacketTypes.UnequipItem);
                outMSG.WriteVariableInt32(HandleData.myIndex);
                outMSG.WriteVariableInt32((int)EquipSlots.Feet);
                SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            }
            equipTab.Focus();
        }

        private void InvPic_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            ImagePanel invPicE = (ImagePanel)sender;
            int itemSlot = ToInt32(invPicE.Name);
            

            if (player.inShop)
            {
                if (player.Backpack[itemSlot].Name != "None")
                {
                    NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                    outMSG.Write((byte)PacketTypes.SellItem);
                    outMSG.WriteVariableInt32(HandleData.myIndex);
                    outMSG.WriteVariableInt32(itemSlot);
                    outMSG.WriteVariableInt32(player.shopNum);
                    SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                }
            }
            else if (player.inBank)
            {
                if (player.Backpack[itemSlot].Name != "None")
                {
                    if (player.Backpack[itemSlot].Stackable && player.Backpack[itemSlot].Value > 1)
                    {
                        CreateValueMoveWindow(sender, player.Backpack[itemSlot].Name, player.Backpack[itemSlot].Value, itemSlot, 2);
                        return;
                    }

                    tranAmount = 1;

                    NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                    outMSG.Write((byte)PacketTypes.DepositItem);
                    outMSG.WriteVariableInt32(HandleData.myIndex);
                    outMSG.WriteVariableInt32(itemSlot);
                    outMSG.WriteVariableInt32(tranAmount);
                    SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                    Logging.WriteMessageLog("Deposit Item Sent");
                }
            }
            else
            {
                if (player.Backpack[itemSlot].Name != "None")
                {
                    NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                    outMSG.Write((byte)PacketTypes.EquipItem);
                    outMSG.WriteVariableInt32(HandleData.myIndex);
                    outMSG.WriteVariableInt32(itemSlot);
                    SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                }
            }
            packTab.Focus();
        }

        private void HotBar_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            ImagePanel hotBarPicE = (ImagePanel)sender;
            int hotBarSlot = ToInt32(hotBarPicE.Name);

            player.SendUpdateHotbar(-1, hotBarSlot);
        }

        private void InvPic_RightClicked(Base sender, ClickedEventArgs arguments)
        {
            ImagePanel invPicE = (ImagePanel)sender;
            int itemSlot = ToInt32(invPicE.Name);
            
            if (player.Backpack[itemSlot].Name != "None")
            {
                NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                outMSG.Write((byte)PacketTypes.DropItem);
                outMSG.WriteVariableInt32(HandleData.myIndex);
                outMSG.WriteVariableInt32(itemSlot);
                SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            }
            packTab.Focus(); 
        }

        private void InvPic_HoverEnter(Base sender, EventArgs arguments)
        {
            ImagePanel invPicE = (ImagePanel)sender;

            if (Gwen.DragDrop.DragAndDrop.SourceControl != null)
            {
                Gwen.DragDrop.Package package = new Gwen.DragDrop.Package();
                package = Gwen.DragDrop.DragAndDrop.SourceControl.DragAndDrop_GetPackage(Gwen.DragDrop.DragAndDrop.SourceControl.X, Gwen.DragDrop.DragAndDrop.SourceControl.Y);

                if (package.Name == "Inv")
                {
                    if (Gwen.Input.InputHandler.IsLeftMouseDown)
                    {
                        oldInvSlot = ToInt32(Gwen.DragDrop.DragAndDrop.SourceControl.Name);
                        isMoveInv = true;
                    }
                }
            }
        }

        private void InvPic_HoverLeave(Base sender, EventArgs arguments)
        {
            RemoveStatWindow();
        }

        private void ShopPic_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            ImagePanel shopPicE = (ImagePanel)sender;
            int shopSlot = ToInt32(shopPicE.Name);
            
            int shopIndex = player.shopNum;

            if (shops[shopIndex].shopItem[shopSlot].Name != "None")
            {
                NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                outMSG.Write((byte)PacketTypes.BuyItem);
                outMSG.WriteVariableInt32(shopSlot);
                outMSG.WriteVariableInt32(HandleData.myIndex);
                outMSG.WriteVariableInt32(shopIndex);
                SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            }
        }

        private void ChatOption_Clicked(Base sender, ClickedEventArgs arguments)
        {
            Button chatOption = (Button)sender;
            int optionSlot = ToInt32(chatOption.Name);
            
            int chatIndex = player.chatNum;

            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            outMSG.Write((byte)PacketTypes.NextChat);
            outMSG.WriteVariableInt32(optionSlot);
            outMSG.WriteVariableInt32(chatIndex);
            outMSG.WriteVariableInt32(HandleData.myIndex);
            SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        private void CloseShop_Clicked(Base sender, ClickedEventArgs arguments)
        {
            

            player.inShop = false;
            player.shopNum = 0;
            shopWindow.Close();
            charTab.Show();
            equipTab.Show();
            spellsTab.Show();
            questTab.Show();
            optionsTab.Show();
        }

        private void BankPic_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            ImagePanel bankPicE = (ImagePanel)sender;
            int bankSlot = ToInt32(bankPicE.Name);
            
            if (player.Bank[bankSlot].Name != "None")
            {
                if (player.Bank[bankSlot].Stackable && player.Bank[bankSlot].Value > 1)
                {
                    CreateValueMoveWindow(sender, player.Bank[bankSlot].Name, player.Bank[bankSlot].Value, bankSlot, 1);
                    return;
                }

                tranAmount = 1;
                NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                outMSG.Write((byte)PacketTypes.WithdrawItem);
                outMSG.WriteVariableInt32(HandleData.myIndex);
                outMSG.WriteVariableInt32(bankSlot);
                outMSG.WriteVariableInt32(tranAmount);
                SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                Logging.WriteMessageLog("Withdraw Item Sent");
            }
        }

        private void BankPic_HoverEnter(Base sender, EventArgs arguments)
        {
            if (Gwen.DragDrop.DragAndDrop.SourceControl != null)
            {
                Gwen.DragDrop.Package package = new Gwen.DragDrop.Package();
                package = Gwen.DragDrop.DragAndDrop.SourceControl.DragAndDrop_GetPackage(Gwen.DragDrop.DragAndDrop.SourceControl.X, Gwen.DragDrop.DragAndDrop.SourceControl.Y);

                if (package.Name == "Bank")
                {
                    if (Gwen.Input.InputHandler.IsLeftMouseDown)
                    {
                        oldBankSlot = ToInt32(Gwen.DragDrop.DragAndDrop.SourceControl.Name);
                        isMoveBank = true;
                    }
                }
            }
        }

        private void BankPic_HoverLeave(Base sender, EventArgs arguments)
        {
            RemoveStatWindow();
        }

        private void HotBar_HoverEnter(Base sender, EventArgs arguments)
        {
            ImagePanel hotBarPicE = (ImagePanel)sender;

            if (Gwen.DragDrop.DragAndDrop.SourceControl != null)
            {
                Gwen.DragDrop.Package package = new Gwen.DragDrop.Package();
                package = Gwen.DragDrop.DragAndDrop.SourceControl.DragAndDrop_GetPackage(Gwen.DragDrop.DragAndDrop.SourceControl.X, Gwen.DragDrop.DragAndDrop.SourceControl.Y);

                if (package.Name == "Inv")
                {
                    if (Gwen.Input.InputHandler.IsLeftMouseDown)
                    {
                        hotBarSlot = ToInt32(Gwen.DragDrop.DragAndDrop.SourceControl.Name);
                        isMoveHotBar = true;
                    }
                }
            }
        }

        private void Equip_HoverLeave(Base sender, EventArgs arguments)
        {
            RemoveStatWindow();
        }

        private void CloseBank_Clicked(Base sender, ClickedEventArgs arguments)
        {
            

            player.inBank = false;
            bankWindow.Close();
            charTab.Show();
            equipTab.Show();
            spellsTab.Show();
            questTab.Show();
            optionsTab.Show();
        }

        private void ChestPic_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            ImagePanel chestPicE = (ImagePanel)sender;
            int chestSlot = ToInt32(chestPicE.Name);
            
            int chestIndex = player.chestNum;

            if (chests[chestIndex].ChestItem[chestSlot].Name != "None")
            {
                NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
                outMSG.Write((byte)PacketTypes.TakeChestItem);
                outMSG.WriteVariableInt32(HandleData.myIndex);
                outMSG.WriteVariableInt32(chestSlot);
                outMSG.WriteVariableInt32(chestIndex);
                SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            }
        }

        private void CloseChest_Clicked(Base sender, ClickedEventArgs arguments)
        {
            
            player.inChest = false;
            player.chestNum = 0;
            chestWindow.Close();
            charTab.Show();
            equipTab.Show();
            spellsTab.Show();
            questTab.Show();
            optionsTab.Show();
        }

        private void QuestList_Clicked(Base sender, ItemSelectedEventArgs arguments)
        {            
            int SelectedIndex = questList.SelectedRowIndex;
            int questId = players[HandleData.myIndex].QuestList[SelectedIndex];
            int questStatus = players[HandleData.myIndex].QuestStatus[SelectedIndex];

            if (questId == 0) { questDetails.Clear(); return; }

            questDetails.Clear();            
            questDetails.AddRow(quests[questId - 1].Name);

            switch (questStatus)
            {
                case (int)QuestStatus.NotStarted:
                    questDetails.AddRow("Status: Not Started");
                    break;

                case (int)QuestStatus.Inprogress:
                    questDetails.AddRow("Status: Inprogress");
                    break;

                case (int)QuestStatus.Complete:
                    questDetails.AddRow("Status: Complete");
                    break;
            }
            questDetails.AddRow("Details:");
            string msg = quests[questId - 1].Description;
            int msgLength = msg.Length;
            int maxLength = 30;

            if (msgLength > maxLength)
            {
                string[] splitMsg = new string[2];
                int splitLength = msgLength - maxLength;
                splitMsg[0] = msg.Substring(0, maxLength);
                splitMsg[1] = msg.Substring(maxLength, splitLength);
                questDetails.AddRow(splitMsg[0]);
                questDetails.AddRow(splitMsg[1]);
            }
            else
            {
                questDetails.AddRow(msg);
            }
        }

        private void StackSlider_ValueChanged(Base sender, EventArgs arguments)
        {
            HorizontalSlider horizontalSlider = (HorizontalSlider)sender;

            tranAmount = ToInt32(horizontalSlider.Value);
            stackItemValue.Text = "x" + tranAmount + " / " + stackSlider.Name;
        }

        private void StackOk_Pressed(Base sender, EventArgs arguments)
        {
            NetOutgoingMessage outMSG = SabertoothClient.netClient.CreateMessage();
            int moveType = ToInt32(stackItemValue.Name);
            if (moveType == 1)
            {
                outMSG.Write((byte)PacketTypes.WithdrawItem);
                outMSG.WriteVariableInt32(HandleData.myIndex);
                int slot = ToInt32(stackItemName.Name);
                outMSG.WriteVariableInt32(slot);
                outMSG.WriteVariableInt32(tranAmount);
                SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                Logging.WriteMessageLog("Withdraw Item Sent");
            }
            else if (moveType == 2)
            {
                outMSG.Write((byte)PacketTypes.DepositItem);
                outMSG.WriteVariableInt32(HandleData.myIndex);
                int slot = ToInt32(stackItemName.Name);
                outMSG.WriteVariableInt32(slot);
                outMSG.WriteVariableInt32(tranAmount);
                SabertoothClient.netClient.SendMessage(outMSG, SabertoothClient.netClient.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                Logging.WriteMessageLog("Deposit Item Sent");
            }
            stackWindow.Close();
        }

        private void StackCncl_Pressed(Base sender, EventArgs arguments)
        {
            tranAmount = 1;
            stackWindow.Close();
        }
        #endregion

        #region Window Creation
        public void CreateShopWindow(Base parent)
        {
            shopWindow = new WindowControl(parent.GetCanvas());
            shopWindow.SetSize(350, 300);
            shopWindow.Position(Gwen.Pos.Top);
            shopWindow.Position(Gwen.Pos.Right);
            shopWindow.DisableResizing();
            shopWindow.IsClosable = false;
            shopWindow.Title = "Shop Name";

            int n = 0;
            int c = 0;
            for (int i = 0; i < 25; i++)
            {
                shopPic[i] = new ImagePanel(shopWindow);
                shopPic[i].SetSize(32, 32);
                shopPic[i].SetPosition(3 + (c * 40), 5 + (n * 40));
                shopPic[i].Name = i.ToString();
                shopPic[i].DoubleClicked += ShopPic_DoubleClicked;

                c += 1;
                if (c > 4) { c = 0; }
                if (i == 4 || i == 9 || i == 14 || i == 19) { n += 1; }
            }

            closeShop = new Button(shopWindow);
            closeShop.SetPosition(225, 10);
            closeShop.SetSize(100, 25);
            closeShop.Text = "Close";
            closeShop.Clicked += CloseShop_Clicked;

            #region Shop Stat Window
            shopStatWindow = new WindowControl(parent.GetCanvas());
            shopStatWindow.SetPosition(200, 10);
            shopStatWindow.SetSize(155, 180);
            shopStatWindow.IsClosable = false;
            shopStatWindow.Title = "Item Name";
            shopStatWindow.DisableResizing();
            shopStatWindow.Hide();

            shopStatPic = new ImagePanel(shopStatWindow);
            shopStatPic.SetPosition(105, 5);
            shopStatPic.SetSize(32, 32);

            shopName = new Label(shopStatWindow);
            shopName.SetPosition(3, 5);
            shopName.Text = "Name: ?";
            shopName.BringToFront();

            shopDamage = new Label(shopStatWindow);
            shopDamage.SetPosition(3, 15);
            shopDamage.Text = "Damage: ?";

            shopArmor = new Label(shopStatWindow);
            shopArmor.SetPosition(3, 25);
            shopArmor.Text = "Armor: ?";

            shopHeRestore = new Label(shopStatWindow);
            shopHeRestore.SetPosition(3, 35);
            shopHeRestore.Text = "Health Restore: ?";

            shopMaRestore = new Label(shopStatWindow);
            shopMaRestore.SetPosition(3, 45);
            shopMaRestore.Text = "Mana Restore: ?";

            shopStr = new Label(shopStatWindow);
            shopStr.SetPosition(3, 65);
            shopStr.Text = "Strength: ?";

            shopAgi = new Label(shopStatWindow);
            shopAgi.SetPosition(3, 75);
            shopAgi.Text = "Agility: ?";

            shopInt = new Label(shopStatWindow);
            shopInt.SetPosition(3, 85);
            shopInt.Text = "Intelligence: ?";

            shopEng = new Label(shopStatWindow);
            shopEng.SetPosition(3, 95);
            shopEng.Text = "Energy: ?";

            shopSta = new Label(shopStatWindow);
            shopSta.SetPosition(3, 105);
            shopSta.Text = "Stamina: ?";

            shopASpeed = new Label(shopStatWindow);
            shopASpeed.SetPosition(3, 125);
            shopASpeed.Text = "Attack Speed: ?";

            shopType = new Label(shopStatWindow);
            shopType.SetPosition(3, 145);
            shopType.Text = "Type: ?";

            shopValue = new Label(shopStatWindow);
            shopValue.SetPosition(3, 160);
            shopValue.Text = "Value: ?";

            shopPrice = new Label(shopStatWindow);
            shopPrice.SetPosition(3, 160);
            shopPrice.Text = "Price: ?";
            #endregion
        }

        public void CreateChestWindow(Base parent)
        {
            chestWindow = new WindowControl(parent.GetCanvas());
            chestWindow.Position(Gwen.Pos.Center, -107, -77);
            chestWindow.SetSize(215, 155);
            chestWindow.DisableResizing();
            chestWindow.IsClosable = false;

            int n = 0;
            int c = 0;
            for (int i = 0; i < 10; i++)
            {
                chestPic[i] = new ImagePanel(chestWindow);
                chestPic[i].SetSize(32, 32);
                chestPic[i].SetPosition(3 + (c * 40), 5 + (n * 40));
                chestPic[i].Name = i.ToString();
                chestPic[i].DoubleClicked += ChestPic_DoubleClicked;

                c += 1;
                if (c > 4) { c = 0; }
                if (i == 4 || i == 9) { n += 1; }
            }

            chestClose = new Button(chestWindow);
            chestClose.SetSize(100, 25);
            chestClose.SetPosition(0, 95);
            chestClose.Text = "Close";
            chestClose.Clicked += CloseChest_Clicked;

            #region Chest Stat Window
            chestStatWindow = new WindowControl(parent.GetCanvas());
            chestStatWindow.SetPosition(200, 10);
            chestStatWindow.SetSize(155, 180);
            chestStatWindow.IsClosable = false;
            chestStatWindow.Title = "Item Name";
            chestStatWindow.DisableResizing();
            chestStatWindow.Hide();

            chestStatPic = new ImagePanel(chestStatWindow);
            chestStatPic.SetPosition(105, 5);
            chestStatPic.SetSize(32, 32);

            chestName = new Label(chestStatWindow);
            chestName.SetPosition(3, 5);
            chestName.Text = "Name: ?";
            chestName.BringToFront();

            chestDamage = new Label(chestStatWindow);
            chestDamage.SetPosition(3, 15);
            chestDamage.Text = "Damage: ?";

            chestArmor = new Label(chestStatWindow);
            chestArmor.SetPosition(3, 25);
            chestArmor.Text = "Armor: ?";

            chestHeRestore = new Label(chestStatWindow);
            chestHeRestore.SetPosition(3, 35);
            chestHeRestore.Text = "Health Restore: ?";

            chestMaRestore = new Label(chestStatWindow);
            chestMaRestore.SetPosition(3, 45);
            chestMaRestore.Text = "Mana Restore: ?";

            chestStr = new Label(chestStatWindow);
            chestStr.SetPosition(3, 65);
            chestStr.Text = "Strength: ?";

            chestAgi = new Label(chestStatWindow);
            chestAgi.SetPosition(3, 75);
            chestAgi.Text = "Agility: ?";

            chestInt = new Label(chestStatWindow);
            chestInt.SetPosition(3, 85);
            chestInt.Text = "Intelligence: ?";

            chestEng = new Label(chestStatWindow);
            chestEng.SetPosition(3, 95);
            chestEng.Text = "Energy: ?";

            chestSta = new Label(chestStatWindow);
            chestSta.SetPosition(3, 105);
            chestSta.Text = "Stamina: ?";

            chestASpeed = new Label(chestStatWindow);
            chestASpeed.SetPosition(3, 125);
            chestASpeed.Text = "Attack Speed: ?";

            chestType = new Label(chestStatWindow);
            chestType.SetPosition(3, 145);
            chestType.Text = "Type: ?";

            chestValue = new Label(chestStatWindow);
            chestValue.SetPosition(3, 160);
            chestValue.Text = "Value: ?";

            chestPrice = new Label(chestStatWindow);
            chestPrice.SetPosition(3, 160);
            chestPrice.Text = "Price: ?";
            #endregion
        }

        public void CreateHotBarWindow(Base parent)
        {
            hotBarWindow = new ImagePanel(parent.GetCanvas());
            hotBarWindow.SetSize(331, 34);
            //hotBarWindow.SetPosition(450, 730);
            hotBarWindow.Position(Gwen.Pos.Bottom);
            hotBarWindow.Position(Gwen.Pos.CenterH);
            hotBarWindow.ImageName = "Resources/Skins/HotbarBkg.png";

            for (int i = 0; i < MAX_PLAYER_HOTBAR; i++)
            {
                hotbarPic[i] = new ImagePanel(hotBarWindow);
                hotbarPic[i].SetSize(32, 32);
                hotbarPic[i].SetPosition(1 + ((i * 32) + (i * 1)), 1);
                hotbarPic[i].Name = i.ToString();
                hotbarPic[i].ImageName = "Resources/Skins/HotBarIcon.png";
                hotbarPic[i].DragAndDrop_SetPackage(true, "Hotbar");
                hotbarPic[i].HoverEnter += HotBar_HoverEnter;
                hotbarPic[i].DoubleClicked += HotBar_DoubleClicked;                

                hotBarLabel[i] = new Label(hotbarPic[i]);
                hotBarLabel[i].SetPosition(3, 1);
                hotBarLabel[i].Text = (i + 1).ToString();
                if (i == 9) { hotBarLabel[i].Text = "0"; }
                hotBarLabel[i].TextColor = System.Drawing.Color.Yellow;
            }            
        }

        public void CreateBankWindow(Base parent)
        {
            bankWindow = new WindowControl(parent.GetCanvas());
            bankWindow.SetSize(405, 260);
            bankWindow.Position(Gwen.Pos.Top);
            bankWindow.Position(Gwen.Pos.Left);
            bankWindow.DisableResizing();
            bankWindow.IsClosable = false;

            int n = 0;
            int c = 0;
            for (int i = 0; i < 50; i++)
            {
                bankPic[i] = new ImagePanel(bankWindow);
                bankPic[i].SetSize(32, 32);
                bankPic[i].SetPosition(1 + (c * 40), 5 + (n * 40));
                bankPic[i].Name = i.ToString();
                bankPic[i].DragAndDrop_SetPackage(true, "Bank");
                bankPic[i].DoubleClicked += BankPic_DoubleClicked;
                bankPic[i].HoverEnter += BankPic_HoverEnter;
                bankPic[i].HoverLeave += BankPic_HoverLeave;

                bankPicValue[i] = new Label(bankWindow);
                bankPicValue[i].SetSize(16, 10);
                bankPicValue[i].SetPosition(24 + (c * 40), 22 + (n * 40));
                bankPicValue[i].Text = "x1";
                bankPicValue[i].TextColor = System.Drawing.Color.Red;

                c += 1;
                if (c > 9) { c = 0; }
                if (i == 9 || i == 19 || i == 29 || i == 39 || i == 49) { n += 1; }
            }

            bankClose = new Button(bankWindow);
            bankClose.SetSize(100, 25);
            bankClose.SetPosition(0, 205);
            bankClose.Text = "Close";
            bankClose.Clicked += CloseBank_Clicked;

            #region Bank Stat Window
            bankStatWindow = new WindowControl(parent.GetCanvas());
            bankStatWindow.SetPosition(200, 10);
            bankStatWindow.SetSize(155, 180);
            bankStatWindow.IsClosable = false;
            bankStatWindow.Title = "Item Name";
            bankStatWindow.DisableResizing();
            bankStatWindow.Hide();

            bankStatPic = new ImagePanel(bankStatWindow);
            bankStatPic.SetPosition(105, 5);
            bankStatPic.SetSize(32, 32);

            bankName = new Label(bankStatWindow);
            bankName.SetPosition(3, 5);
            bankName.Text = "Name: ?";
            bankName.BringToFront();

            bankDamage = new Label(bankStatWindow);
            bankDamage.SetPosition(3, 15);
            bankDamage.Text = "Damage: ?";

            bankArmor = new Label(bankStatWindow);
            bankArmor.SetPosition(3, 25);
            bankArmor.Text = "Armor: ?";

            bankHeRestore = new Label(bankStatWindow);
            bankHeRestore.SetPosition(3, 35);
            bankHeRestore.Text = "Health Restore: ?";

            bankMaRestore = new Label(bankStatWindow);
            bankMaRestore.SetPosition(3, 35);
            bankMaRestore.Text = "Mana Restore: ?";

            bankStr = new Label(bankStatWindow);
            bankStr.SetPosition(3, 65);
            bankStr.Text = "Strength: ?";

            bankAgi = new Label(bankStatWindow);
            bankAgi.SetPosition(3, 75);
            bankAgi.Text = "Agility: ?";

            bankInt = new Label(bankStatWindow);
            bankInt.SetPosition(3, 85);
            bankInt.Text = "Intelligence: ?";

            bankEng = new Label(bankStatWindow);
            bankEng.SetPosition(3, 95);
            bankEng.Text = "Energy: ?";

            bankSta = new Label(bankStatWindow);
            bankSta.SetPosition(3, 105);
            bankSta.Text = "Stamina: ?";

            bankASpeed = new Label(bankStatWindow);
            bankASpeed.SetPosition(3, 125);
            bankASpeed.Text = "Attack Speed: ?";

            bankType = new Label(bankStatWindow);
            bankType.SetPosition(3, 145);
            bankType.Text = "Type: ?";

            bankValue = new Label(bankStatWindow);
            bankValue.SetPosition(3, 160);
            bankValue.Text = "Value: ?";

            bankPrice = new Label(bankStatWindow);
            bankPrice.SetPosition(3, 160);
            bankPrice.Text = "Price: ?";
            #endregion
        }

        public void CreateMenuWindow(Base parent)
        {
            menuWindow = new WindowControl(parent.GetCanvas());
            menuWindow.SetSize(350, 300);
            menuWindow.Position(Gwen.Pos.Bottom);
            menuWindow.Position(Gwen.Pos.Right);
            menuWindow.DisableResizing();
            menuWindow.Title = "Game Menu";
            //menuWindow.IsClosable = false;

            menuTabs = new TabControl(menuWindow);
            menuTabs.SetSize(330, 260);
            menuTabs.SetPosition(5, 5);

            #region Stats Window
            statWindow = new WindowControl(parent.GetCanvas());
            statWindow.SetPosition(200, 10);
            statWindow.SetSize(155, 180);
            statWindow.IsClosable = false;
            statWindow.Title = "Item Name";
            statWindow.DisableResizing();
            statWindow.Hide();

            statPic = new ImagePanel(statWindow);
            statPic.SetPosition(105, 5);
            statPic.SetSize(32, 32);

            packName = new Label(statWindow);
            packName.SetPosition(3, 5);
            packName.Text = "Name: ?";
            packName.BringToFront();

            packDamage = new Label(statWindow);
            packDamage.SetPosition(3, 15);
            packDamage.Text = "Damage: ?";

            packArmor = new Label(statWindow);
            packArmor.SetPosition(3, 25);
            packArmor.Text = "Armor: ?";

            packHeRestore = new Label(statWindow);
            packHeRestore.SetPosition(3, 35);
            packHeRestore.Text = "Health Restore: ?";

            packMaRestore = new Label(statWindow);
            packMaRestore.SetPosition(3, 45);
            packMaRestore.Text = "Mana Restore: ?";

            packStr = new Label(statWindow);
            packStr.SetPosition(3, 65);
            packStr.Text = "Strength: ?";

            packAgi = new Label(statWindow);
            packAgi.SetPosition(3, 75);
            packAgi.Text = "Agility: ?";

            packInt = new Label(statWindow);
            packInt.SetPosition(3, 85);
            packInt.Text = "Intelligence: ?";

            packEng = new Label(statWindow);
            packEng.SetPosition(3, 95);
            packEng.Text = "Energy: ?";

            packSta = new Label(statWindow);
            packSta.SetPosition(3, 105);
            packSta.Text = "Stamina: ?";

            packASpeed = new Label(statWindow);
            packASpeed.SetPosition(3, 125);
            packASpeed.Text = "Attack Speed: ?";

            packType = new Label(statWindow);
            packType.SetPosition(3, 145);
            packType.Text = "Type: ?";

            packValue = new Label(statWindow);
            packValue.SetPosition(3, 165);
            packValue.Text = "Value: ?";

            packPrice = new Label(statWindow);
            packPrice.SetPosition(3, 185);
            packPrice.Text = "Price: ?";

            packStack = new Label(statWindow);
            packStack.SetPosition(3, 205);
            packStack.Text = "Max Stack: ?";
            #endregion

            charTab = menuTabs.AddPage("Character");

            #region Character
            charName = new Label(charTab.Page);
            charName.SetPosition(10, 5);
            charName.Text = "Name: ?";

            charLevel = new Label(charTab.Page);
            charLevel.SetPosition(10, 20);
            charLevel.Text = "Level: ?";

            charExp = new Label(charTab.Page);
            charExp.SetPosition(10, 30);
            charExp.Text = "Experience: ?";

            charWallet = new Label(charTab.Page);
            charWallet.SetPosition(10, 40);
            charWallet.Text = "Money: ?";

            charHealth = new Label(charTab.Page);
            charHealth.SetPosition(10, 75);
            charHealth.Text = "Health: ?";

            charMana = new Label(charTab.Page);
            charMana.SetPosition(10, 85);
            charMana.Text = "Health: ?";

            charArmor = new Label(charTab.Page);
            charArmor.SetPosition(10, 110);
            charArmor.Text = "Armor: ?";

            charStr = new Label(charTab.Page);
            charStr.SetPosition(10, 120);
            charStr.Text = "Strength: ?";

            charAgi = new Label(charTab.Page);
            charAgi.SetPosition(10, 130);
            charAgi.Text = "Agility: ?";

            charInt = new Label(charTab.Page);
            charInt.SetPosition(10, 140);
            charInt.Text = "Endurance: ?";

            charSta = new Label(charTab.Page);
            charSta.SetPosition(10, 150);
            charSta.Text = "Stamina: ?";

            charEng = new Label(charTab.Page);
            charEng.SetPosition(10, 160);
            charEng.Text = "Life Time: ?";

            playTime = new Label(charTab.Page);
            playTime.SetPosition(10, 180);
            playTime.Text = "Play Time: ?";
            #endregion

            packTab = menuTabs.AddPage("Backpack");

            #region Backpack
            int n = 0;
            int c = 0;
            for (int i = 0; i < MAX_INV_SLOTS; i++)
            {
                invPic[i] = new ImagePanel(packTab.Page);
                invPic[i].SetSize(32, 32);
                invPic[i].SetPosition(3 + (c * 40), 5 + (n * 40));
                invPic[i].Name = i.ToString();
                invPic[i].DragAndDrop_SetPackage(true, "Inv");
                invPic[i].DoubleClicked += InvPic_DoubleClicked;
                invPic[i].RightClicked += InvPic_RightClicked;
                invPic[i].HoverEnter += InvPic_HoverEnter;            
                invPic[i].HoverLeave += InvPic_HoverLeave;

                invValue[i] = new Label(packTab.Page);
                invValue[i].SetSize(16, 10);
                invValue[i].SetPosition(24 + (c * 40), 22 + (n * 40));
                invValue[i].Text = "x1";
                invValue[i].TextColor = System.Drawing.Color.Red;

                c += 1;
                if (c > 4) { c = 0; }
                if (i == 4 || i == 9 || i == 14 || i == 19) { n += 1; }
            }
            #endregion

            equipTab = menuTabs.AddPage("Equipment");

            #region Equipment
            equipMain = new ImagePanel(equipTab.Page);
            equipMain.SetPosition(20, 5);
            equipMain.SetSize(32, 32);
            equipMain.DoubleClicked += EquipMain_DoubleClicked;
            equipMain.HoverLeave += Equip_HoverLeave;

            equipOff = new ImagePanel(equipTab.Page);
            equipOff.SetPosition(65, 5);
            equipOff.SetSize(32, 32);
            equipOff.DoubleClicked += EquipOff_DoubleClicked;
            equipOff.HoverLeave += Equip_HoverLeave;

            equipChest = new ImagePanel(equipTab.Page);
            equipChest.SetPosition(45, 50);
            equipChest.SetSize(32, 32);
            equipChest.DoubleClicked += EquipChest_DoubleClicked;
            equipChest.HoverLeave += Equip_HoverLeave;

            equipLegs = new ImagePanel(equipTab.Page);
            equipLegs.SetPosition(45, 95);
            equipLegs.SetSize(32, 32);
            equipLegs.DoubleClicked += EquipLegs_DoubleClicked;
            equipLegs.HoverLeave += Equip_HoverLeave;

            equipFeet = new ImagePanel(equipTab.Page);
            equipFeet.SetPosition(45, 140);
            equipFeet.SetSize(32, 32);
            equipFeet.DoubleClicked += EquipFeet_DoubleClicked;
            equipFeet.HoverLeave += Equip_HoverLeave;

            equipBonus = new GroupBox(equipTab.Page);
            equipBonus.SetPosition(190, 95);
            equipBonus.SetSize(115, 105);
            equipBonus.Text = "Item Bonuses:";

            equipMDamage = new Label(equipBonus);
            equipMDamage.SetPosition(3, 5);
            equipMDamage.Text = "Main Damage: ?";

            equipODamage = new Label(equipBonus);
            equipODamage.SetPosition(3, 15);
            equipODamage.Text = "Off Damage: ?";

            equipArmor = new Label(equipBonus);
            equipArmor.SetPosition(3, 25);
            equipArmor.Text = "Armor: ?";

            equipStr = new Label(equipBonus);
            equipStr.SetPosition(3, 35);
            equipStr.Text = "Strength: ?";

            equipAgi = new Label(equipBonus);
            equipAgi.SetPosition(3, 45);
            equipAgi.Text = "Agility: ?";

            equipInt = new Label(equipBonus);
            equipInt.SetPosition(3, 55);
            equipInt.Text = "Intelligence: ?";

            equipSta = new Label(equipBonus);
            equipSta.SetPosition(3, 65);
            equipSta.Text = "Stamina: ?";

            equipEng = new Label(equipBonus);
            equipEng.SetPosition(3, 75);
            equipEng.Text = "Energy: ?";
            #endregion

            spellsTab = menuTabs.AddPage("Spells");

            #region Spells

            #endregion

            questTab = menuTabs.AddPage("Quests");

            #region Quests
            questList = new ListBox(questTab.Page);
            questList.SetPosition(10, 10);
            questList.SetSize(140, 200);
            questList.RowSelected += QuestList_Clicked;

            questDetails = new ListBox(questTab.Page);
            questDetails.SetPosition(165, 10);
            questDetails.SetSize(140, 200);
            questDetails.IsDisabled = true;

            for (int i = 0; i < MAX_PLAYER_QUEST_LIST; i++)
            {
                if (players[HandleData.myIndex].QuestList[i] == 0)
                {
                    questList.AddRow((i + 1) + ": None");
                }
                else
                {
                    if (quests[players[HandleData.myIndex].QuestList[i] - 1].Name != null)
                    {
                        questList.AddRow((i + 1) + ": " + quests[players[HandleData.myIndex].QuestList[i] - 1].Name);
                    }
                }
            }
            #endregion

            optionsTab = menuTabs.AddPage("Options");

            #region Options
            optLog = new Button(optionsTab.Page);
            optLog.SetPosition(105, 100);
            optLog.Text = "Log Out";
            optLog.Clicked += CheckLogOutSubmit;
            #endregion
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
            mainWindow.SetSize(200, 235);
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

            mainbuttonOpt = new Button(mainWindow);
            mainbuttonOpt.SetSize(100, 25);
            mainbuttonOpt.SetPosition(45, 115);
            mainbuttonOpt.Text = "Options";
            mainbuttonOpt.Clicked += CheckMainWindowOptions;

            mainbuttonExit = new Button(mainWindow);
            mainbuttonExit.SetSize(100, 25);
            mainbuttonExit.SetPosition(45, 150);
            mainbuttonExit.Text = "Exit";
            mainbuttonExit.Clicked += CheckMainWindowExit;
        }

        public void CreateOptionsWindow(Base parent)
        {
            optWindow = new WindowControl(parent.GetCanvas());
            optWindow.Title = "Options";
            optWindow.SetSize(200, 250);
            optWindow.Position(Gwen.Pos.Center);
            optWindow.DisableResizing();
            optWindow.IsClosable = false;

            enableFullscreen = new LabeledCheckBox(optWindow);
            enableFullscreen.Text = "Enable Fullscreen";            
            enableFullscreen.SetPosition(25, 25);
            if (SabertoothClient.Fullscreen) { enableFullscreen.IsChecked = true; }

            enableVsync = new LabeledCheckBox(optWindow);
            enableVsync.Text = "Enable VSync";
            enableVsync.SetPosition(25, 50);
            if (SabertoothClient.VSync) { enableVsync.IsChecked = true; }

            optipLabel = new Label(optWindow);
            optipLabel.SetPosition(25, 85);
            optipLabel.Text = "IP Address:";

            optipAddress = new TextBox(optWindow);
            optipAddress.SetPosition(25, 100);
            optipAddress.SetSize(140, 25);
            optipAddress.Text = SabertoothClient.IPAddress;

            optportLabel = new Label(optWindow);
            optportLabel.SetPosition(25, 130);
            optportLabel.Text = "Port:";

            optPort = new TextBox(optWindow);
            optPort.SetPosition(25, 145);
            optPort.SetSize(140, 25);
            optPort.Text = SabertoothClient.Port;

            saveoptButton = new Button(optWindow);
            saveoptButton.SetPosition(25, 185);
            saveoptButton.SetSize(60, 25);
            saveoptButton.Text = "Save";
            saveoptButton.Clicked += CheckOptionWindowSave;

            canoptButton = new Button(optWindow);
            canoptButton.SetPosition(105, 185);
            canoptButton.SetSize(60, 25);
            canoptButton.Text = "Cancel";
            canoptButton.Clicked += CheckOptionsWindowCancel;
        }

        public void CreateActivateWindow(Base parent)
        {
            activeWindow = new WindowControl(parent.GetCanvas());
            activeWindow.Title = "Activation Key";
            activeWindow.SetSize(300, 135);
            activeWindow.Position(Gwen.Pos.Center);
            activeWindow.IsClosable = false;
            activeWindow.DisableResizing();
            activeWindow.KeyboardInputEnabled = true;

            activeLabel = new Label(activeWindow);
            activeLabel.SetPosition(25, 15);
            activeLabel.Text = "Key:";

            activeBox = new TextBox(activeWindow);
            activeBox.SetPosition(25, 35);
            activeBox.SetSize(235, 25);

            activeOK = new Button(activeWindow);
            activeOK.SetPosition(25, 70);
            activeOK.SetSize(60, 25);
            activeOK.Text = "OK";
            activeOK.Clicked += CheckActiveWindowOK;

            activeCancel = new Button(activeWindow);
            activeCancel.SetPosition(200, 70);
            activeCancel.SetSize(60, 25);
            activeCancel.Text = "Exit";
            activeCancel.Clicked += CheckActiveWindowCancel;
        }

        public void CreateLoginWindow(Base parent)
        {
            logWindow = new WindowControl(parent.GetCanvas());
            logWindow.Title = "Login";
            logWindow.SetSize(200, 250);
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
            if (SabertoothClient.Remember) { unlogBox.Text = SabertoothClient.Username; }

            pwloglabel = new Label(logWindow);
            pwloglabel.SetPosition(25, 75);
            pwloglabel.Text = "Password:";

            pwlogBox = new TextBoxPassword(logWindow);
            pwlogBox.SetPosition(25, 95);
            pwlogBox.SetSize(140, 25);
            if (SabertoothClient.Remember) { pwlogBox.Text = SabertoothClient.Password; }
            //pwlogBox.Focus();
            pwlogBox.SubmitPressed += CheckLogWindowSubmit;

            logRemember = new LabeledCheckBox(logWindow);
            logRemember.Text = "Remember me?";
            logRemember.SetPosition(25, 135);
            if (SabertoothClient.Remember) { logRemember.IsChecked = true; }

            logButton = new Button(logWindow);
            logButton.SetPosition(25, 175);
            logButton.SetSize(60, 25);
            logButton.Text = "Login";
            logButton.Clicked += CheckLogWindowLogin;

            canlogButton = new Button(logWindow);
            canlogButton.SetPosition(105, 175);
            canlogButton.SetSize(60, 25);
            canlogButton.Text = "Cancel";
            canlogButton.Clicked += CheckLogWindowCancel;
        }

        public void CreateRegisterWindow(Base parent)
        {
            regWindow = new WindowControl(parent.GetCanvas());
            regWindow.Title = "Register";
            regWindow.SetSize(200, 335);
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

            emailregLabel = new Label(regWindow);
            emailregLabel.SetPosition(25, 75);
            emailregLabel.Text = "Email Address:";

            emailregBox = new TextBox(regWindow);
            emailregBox.SetPosition(25, 95);
            emailregBox.SetSize(140, 25);

            pwregLabel = new Label(regWindow);
            pwregLabel.SetPosition(25, 135);
            pwregLabel.Text = "Password:";

            pwregBox = new TextBoxPassword(regWindow);
            pwregBox.SetPosition(25, 155);
            pwregBox.SetSize(140, 25);

            repwLabel = new Label(regWindow);
            repwLabel.SetPosition(25, 195);
            repwLabel.Text = "Re-type Password:";

            repwBox = new TextBoxPassword(regWindow);
            repwBox.SetPosition(25, 215);
            repwBox.SetSize(140, 25);
            repwBox.SubmitPressed += CheckRegWindowSubmit;

            regButton = new Button(regWindow);
            regButton.SetPosition(25, 260);
            regButton.SetSize(60, 25);
            regButton.Text = "Register";
            regButton.Clicked += CheckRegWindowRegister;

            canregButton = new Button(regWindow);
            canregButton.SetPosition(105, 260);
            canregButton.SetSize(60, 25);
            canregButton.Text = "Cancel";
            canregButton.Clicked += CheckRegWindowCancel;
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

        public void CreateDebugWindow(Base parent)
        {
            d_Window = new WindowControl(parent.GetCanvas());
            d_Window.Title = "Debug";
            d_Window.SetSize(200, 300);
            d_Window.Position(Gwen.Pos.Top);
            d_Window.Position(Gwen.Pos.Right);
            d_Window.DisableResizing();

            int spacing = 5;

            d_FPS = new Label(d_Window);
            d_FPS.SetPosition(10, spacing);
            d_FPS.Text = "FPS: ";
            spacing += 10;

            d_Name = new Label(d_Window);
            d_Name.SetPosition(10, spacing);
            d_Name.Text = "Name: Player";
            spacing += 10;

            d_X = new Label(d_Window);
            d_X.SetPosition(10, spacing);
            d_X.Text = "X: ?";
            spacing += 10;

            d_Y = new Label(d_Window);
            d_Y.SetPosition(10, spacing);
            d_Y.Text = "Y: ?";
            spacing += 10;

            d_Map = new Label(d_Window);
            d_Map.SetPosition(10, spacing);
            d_Map.Text = "Map: ?";
            spacing += 10;

            d_Dir = new Label(d_Window);
            d_Dir.SetPosition(10, spacing);
            d_Dir.Text = "Direction : ?";
            spacing += 10;

            d_aDir = new Label(d_Window);
            d_aDir.SetPosition(10, spacing);
            d_aDir.Text = "Aim Direction : ?";
            spacing += 10;

            d_Sprite = new Label(d_Window);
            d_Sprite.SetPosition(10, spacing);
            d_Sprite.Text = "Sprite: ?";
            spacing += 10;

            d_IP = new Label(d_Window);
            d_IP.SetPosition(10, spacing);
            d_IP.Text = "IP Address: ?";
            spacing += 10;

            d_Port = new Label(d_Window);
            d_Port.SetPosition(10, spacing);
            d_Port.Text = "Port: ?";
            spacing += 10;

            d_Latency = new Label(d_Window);
            d_Latency.SetPosition(10, spacing);
            d_Latency.Text = "Latency: ?";
            spacing += 10;

            d_packetsIn = new Label(d_Window);
            d_packetsIn.SetPosition(10, spacing);
            d_packetsIn.Text = "Packets In: ?";
            spacing += 10;

            d_packetsOut = new Label(d_Window);
            d_packetsOut.SetPosition(10, spacing);
            d_packetsOut.Text = "Packets Out: ?";
            spacing += 10;

            d_Controller = new Label(d_Window);
            d_Controller.SetPosition(10, spacing);
            d_Controller.Text = "Controller: ?";
            spacing += 10;

            d_ConDir = new Label(d_Window);
            d_ConDir.SetPosition(10, spacing);
            d_ConDir.Text = "Dir: ?";
            spacing += 10;

            d_ConButton = new Label(d_Window);
            d_ConButton.SetPosition(10, spacing);
            d_ConButton.Text = "Button: ?";
            spacing += 10;

            d_Axis = new Label(d_Window);
            d_Axis.SetPosition(10, spacing);
            d_Axis.Text = "Axis: ?";
            spacing += 10;

            d_mouseX = new Label(d_Window);
            d_mouseX.SetPosition(10, spacing);
            d_mouseX.Text = "Mouse X: ?";
            spacing += 10;

            d_mouseY = new Label(d_Window);
            d_mouseY.SetPosition(10, spacing);
            d_mouseY.Text = "Mouse Y: ?";
            spacing += 10;

            d_tMouseX = new Label(d_Window);
            d_tMouseX.SetPosition(10, spacing);
            d_tMouseX.Text = "Tile Mouse X: ?";
            spacing += 10;

            d_tMouseY = new Label(d_Window);
            d_tMouseY.SetPosition(10, spacing);
            d_tMouseY.Text = "Tile Mouse Y: ?";
            spacing += 10;

            d_Region = new Label(d_Window);
            d_Region.SetPosition(10, spacing);
            d_Region.Text = "Mouse Region: ?";
        }

        public void CreateNpcChatWindow(Base parent, int chatNum)
        {
            npcChatWindow = new WindowControl(parent.GetCanvas());
            npcChatWindow.Title = chats[chatNum].Name;
            npcChatWindow.SetSize(405, 325);
            npcChatWindow.Position(Gwen.Pos.Center);
            npcChatWindow.DisableResizing();
            npcChatWindow.IsClosable = false;

            npcChatName = new Label(npcChatWindow);
            npcChatName.Text = chats[chatNum].Name;
            npcChatName.SetPosition(5, 5);

            //Needs to be changed so that words dont get cut off and that enough lines are provided for the max length of the messages
            npcChatMessage = new ListBox(npcChatWindow);
            npcChatMessage.RenderColor = System.Drawing.Color.Transparent;
            npcChatMessage.SetBounds(5, 20, 380, 220);

            //Add quest stuff
            string msg;
            if (chats[chatNum].QuestNum > 0)
            {
                int questNum = chats[chatNum].QuestNum - 1;

                msg = quests[questNum].StartMessage;                             
            }
            else
            {
                msg = chats[chatNum].MainMessage;
            }
            int msgLength = msg.Length;
            int maxLength = 50;

            if (msgLength > maxLength)
            {
                int lines = (msgLength / maxLength);
                string[] splitMsg = new string[lines];
                int start = 0;

                for (int i = 0; i < lines; i++)
                {
                    splitMsg[i] = msg.Substring(start, maxLength);
                    start += maxLength;
                    npcChatMessage.AddRow(splitMsg[i]);
                }
            }
            else
            {
                npcChatMessage.AddRow(msg);
            }

            int n = 0;
            for (int i = 0; i < 4; i++)
            {
                npcChatOption[i] = new Button(npcChatWindow);
                npcChatOption[i].SetSize(90, 25);
                npcChatOption[i].Name = i.ToString();
                npcChatOption[i].Clicked += ChatOption_Clicked;           
                npcChatOption[i].Hide();

                if (chats[chatNum].Option[i] != "None")
                {
                    npcChatOption[i].Text = chats[chatNum].Option[i];
                    npcChatOption[i].SetPosition(8 + (n * 95), 255);
                    n += 1;
                    npcChatOption[i].Show();
                }                                
            }
        }

        void CreateValueMoveWindow(Base parent, string itemName, float maxAmount, int slot, int moveType)
        {
            stackWindow = new WindowControl(parent.GetCanvas());
            stackWindow.Title = "Transfer Stack";
            stackWindow.SetSize(175, 175);
            stackWindow.SetPosition(150, 150);
            stackWindow.DisableResizing();
            stackWindow.IsClosable = false;

            stackItemName = new Label(stackWindow);
            stackItemName.Name = slot.ToString();
            stackItemName.Text = itemName;
            stackItemName.SetPosition(35, 10);

            stackItemValue = new Label(stackWindow);
            stackItemValue.Name = moveType.ToString();
            stackItemValue.Text = "x" + ToInt32(maxAmount / 2) + " / " + maxAmount;          

            stackSlider = new HorizontalSlider(stackWindow);            
            stackSlider.SetSize(76, 15);
            stackSlider.Name = maxAmount.ToString();
            stackSlider.ValueChanged += StackSlider_ValueChanged;
            stackSlider.SetRange(1, maxAmount);
            stackSlider.SnapToNotches = true;
            stackSlider.NotchCount = ToInt32(maxAmount) - 1;
            stackSlider.Value = (maxAmount / 2);       
            tranAmount = ToInt32(stackSlider.Value);            

            stackOk = new Button(stackWindow);
            stackOk.Pressed += StackOk_Pressed;
            stackOk.Text = "Ok";

            stackCncl = new Button(stackWindow);
            stackCncl.Pressed += StackCncl_Pressed;
            stackCncl.Text = "Cancel";
            
            Gwen.Align.PlaceDownLeft(stackItemValue, stackItemName, 10);
            Gwen.Align.PlaceDownLeft(stackSlider, stackItemValue, 10);
            Gwen.Align.PlaceDownLeft(stackOk, stackSlider, 10);
            Gwen.Align.PlaceDownLeft(stackCncl, stackOk, 10);
        }
        #endregion

        #region Other Voids
        public void AddText(string msg)
        {
            if (outputChat == null) { return; }
            outputChat.AddRow(msg);
            outputChat.ScrollToBottom();
            outputChat.UnselectAll();
        }

        public void MsgBox(string msg, string caption, Canvas canvas)
        {
            MessageBox msgBox = new MessageBox(canvas, msg, caption);
            msgBox.Position(Gwen.Pos.Center);
        }
        #endregion
    }

    public class MiniMap : Drawable
    {
        Map m_Map;
        Player player;
        VertexArray m_Blocked = new VertexArray(PrimitiveType.Quads, 4);
        VertexArray m_Player = new VertexArray(PrimitiveType.Quads, 4);
        VertexArray m_Npc = new VertexArray(PrimitiveType.Quads, 4);
        VertexArray m_Item = new VertexArray(PrimitiveType.Quads, 4);
        VertexArray m_NpcSpawn = new VertexArray(PrimitiveType.Quads, 4);
        VertexArray m_NpcAvoid = new VertexArray(PrimitiveType.Quads, 4);
        Texture t_Mini = new Texture("Resources/Minimap.png");

        public void UpdateMiniMap() { }

        public void SetPlayerIndexMap()
        {
            player = players[HandleData.myIndex];
            m_Map = map;
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            int minX;
            int minY;
            int maxX;
            int maxY;

            if (SCREEN_WIDTH == 1024 && SCREEN_HEIGHT == 768)
            {
                minX = (player.X + 16) - 16;
                minY = (player.Y + 11) - 11;
                maxX = (player.X + 16) + 16;
                maxY = (player.Y + 11) + 16;
            }
            else
            {
                minX = (player.X + 12) - 12;
                minY = (player.Y + 9) - 9;
                maxX = (player.X + 12) + 13;
                maxY = (player.Y + 9) + 11;
            }
            states.Texture = t_Mini;

            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    if (x > 0 && y > 0 && x < m_Map.MaxX && y < m_Map.MaxY)
                    {
                        int fx = (x * 12) - (minX * 12) + 640;
                        int fy = (y * 12) - (minY * 12);
                        int tx, ty, w, h;

                        if (m_Map.Ground[x, y].Type == (int)TileType.Blocked)
                        {
                            tx = 0;
                            ty = 0;
                            w = 12;
                            h = 12;
                            m_Blocked[0] = new Vertex(new Vector2f(fx, fy), new Vector2f(tx, ty));
                            m_Blocked[1] = new Vertex(new Vector2f(fx + w, fy), new Vector2f(tx + w, ty));
                            m_Blocked[2] = new Vertex(new Vector2f(fx + w, fy + h), new Vector2f(tx + w, ty + h));
                            m_Blocked[3] = new Vertex(new Vector2f(fx, fy + h), new Vector2f(tx, ty + h));
                            target.Draw(m_Blocked, states);
                        }
                        if (m_Map.Ground[x, y].Type == (int)TileType.NpcSpawn)
                        {
                            w = 12;
                            h = 12;
                            m_NpcSpawn[0] = new Vertex(new Vector2f(fx, fy), Color.Blue);
                            m_NpcSpawn[1] = new Vertex(new Vector2f(fx + w, fy), Color.Transparent);
                            m_NpcSpawn[2] = new Vertex(new Vector2f(fx + w, fy + h), Color.Blue);
                            m_NpcSpawn[3] = new Vertex(new Vector2f(fx, fy + h), Color.Transparent);
                            target.Draw(m_NpcSpawn, states);
                        }
                        if (m_Map.Ground[x, y].Type == (int)TileType.SpawnPool)
                        {
                            w = 12;
                            h = 12;
                            m_NpcSpawn[0] = new Vertex(new Vector2f(fx, fy), Color.Magenta);
                            m_NpcSpawn[1] = new Vertex(new Vector2f(fx + w, fy), Color.Transparent);
                            m_NpcSpawn[2] = new Vertex(new Vector2f(fx + w, fy + h), Color.Magenta);
                            m_NpcSpawn[3] = new Vertex(new Vector2f(fx, fy + h), Color.Transparent);
                            target.Draw(m_NpcSpawn, states);
                        }
                        if (m_Map.Ground[x, y].Type == (int)TileType.NpcAvoid)
                        {
                            w = 12;
                            h = 12;
                            m_NpcAvoid[0] = new Vertex(new Vector2f(fx, fy), Color.Black);
                            m_NpcAvoid[1] = new Vertex(new Vector2f(fx + w, fy), Color.Transparent);
                            m_NpcAvoid[2] = new Vertex(new Vector2f(fx + w, fy + h), Color.Black);
                            m_NpcAvoid[3] = new Vertex(new Vector2f(fx, fy + h), Color.Transparent);
                            target.Draw(m_NpcAvoid, states);
                        }
                        for (int i = 0; i < 20; i++)
                        {
                            if (i < 10)
                            {
                                if (m_Map.m_MapNpc[i].IsSpawned)
                                {
                                    if (m_Map.m_MapNpc[i].X == x && m_Map.m_MapNpc[i].Y == y)
                                    {
                                        tx = 12;
                                        ty = 0;
                                        w = 12;
                                        h = 12;
                                        if (m_Map.m_MapNpc[i].Behavior == (int)BehaviorType.ShopOwner || m_Map.m_MapNpc[i].Behavior == (int)BehaviorType.Friendly || m_Map.m_MapNpc[i].Behavior == (int)BehaviorType.Passive) { tx = 24; }
                                        m_Npc[0] = new Vertex(new Vector2f(fx, fy), Color.Yellow, new Vector2f(tx, ty));
                                        m_Npc[1] = new Vertex(new Vector2f(fx + w, fy), Color.Yellow, new Vector2f(tx + w, ty));
                                        m_Npc[2] = new Vertex(new Vector2f(fx + w, fy + h), Color.Yellow, new Vector2f(tx + w, ty + h));
                                        m_Npc[3] = new Vertex(new Vector2f(fx, fy + h), Color.Yellow, new Vector2f(tx, ty + h));
                                        target.Draw(m_Npc, states);
                                    }
                                }
                            }
                            if (m_Map.r_MapNpc[i].IsSpawned)
                            {
                                if (m_Map.r_MapNpc[i].X == x && m_Map.r_MapNpc[i].Y == y)
                                {
                                    tx = 12;
                                    ty = 0;
                                    w = 12;
                                    h = 12;
                                    if (m_Map.r_MapNpc[i].Behavior == (int)BehaviorType.ShopOwner || m_Map.r_MapNpc[i].Behavior == (int)BehaviorType.Friendly || m_Map.r_MapNpc[i].Behavior == (int)BehaviorType.Passive) { tx = 24; }
                                    m_Npc[0] = new Vertex(new Vector2f(fx, fy), Color.Yellow, new Vector2f(tx, ty));
                                    m_Npc[1] = new Vertex(new Vector2f(fx + w, fy), Color.Yellow, new Vector2f(tx + w, ty));
                                    m_Npc[2] = new Vertex(new Vector2f(fx + w, fy + h), Color.Yellow, new Vector2f(tx + w, ty + h));
                                    m_Npc[3] = new Vertex(new Vector2f(fx, fy + h), Color.Yellow, new Vector2f(tx, ty + h));
                                    target.Draw(m_Npc, states);
                                }
                            }
                            if (m_Map.m_MapItem[i].IsSpawned)
                            {
                                if (m_Map.m_MapItem[i].X == x && m_Map.m_MapItem[i].Y == y)
                                {
                                    tx = 48;
                                    ty = 0;
                                    w = 12;
                                    h = 12;
                                    m_Item[0] = new Vertex(new Vector2f(fx, fy), Color.Magenta, new Vector2f(tx, ty));
                                    m_Item[1] = new Vertex(new Vector2f(fx + w, fy), Color.Magenta, new Vector2f(tx + w, ty));
                                    m_Item[2] = new Vertex(new Vector2f(fx + w, fy + h), Color.Magenta, new Vector2f(tx + w, ty + h));
                                    m_Item[3] = new Vertex(new Vector2f(fx, fy + h), Color.Magenta, new Vector2f(tx, ty + h));
                                    target.Draw(m_Item, states);
                                }
                            }
                        }
                        if ((player.X + player.OffsetX) == x && (player.Y + player.OffsetY) == y)
                        {
                            tx = 60;
                            ty = 0;
                            w = 12;
                            h = 12;
                            m_Player[0] = new Vertex(new Vector2f(fx, fy), Color.White, new Vector2f(tx, ty));
                            m_Player[1] = new Vertex(new Vector2f(fx + w, fy), Color.White, new Vector2f(tx + w, ty));
                            m_Player[2] = new Vertex(new Vector2f(fx + w, fy + h), Color.White, new Vector2f(tx + w, ty + h));
                            m_Player[3] = new Vertex(new Vector2f(fx, fy + h), Color.White, new Vector2f(tx, ty + h));
                            target.Draw(m_Player, states);
                        }
                    }
                }
            }
        }
    }

    public class HUD : Drawable
    {
        Font d_Font = new Font("Resources/Fonts/Arial.ttf");

        VertexArray h_Bar = new VertexArray();
        Text h_Text = new Text();
        float h_barLength;

        VertexArray m_Bar = new VertexArray();
        Text m_Text = new Text();
        float m_barLength;

        VertexArray e_Bar = new VertexArray();
        Text e_Text = new Text();
        float e_barLength;

        const int f_Size = 175;

        Player player;

        public HUD()
        {
            h_Bar.PrimitiveType = PrimitiveType.Quads;
            h_Bar.Resize(4);

            h_Text.Font = d_Font;
            h_Text.CharacterSize = 16;
            h_Text.Color = Color.Black;
            h_Text.Style = Text.Styles.Bold;
            h_Text.Position = new Vector2f(13, 14);

            m_Bar.PrimitiveType = PrimitiveType.Quads;
            m_Bar.Resize(4);

            m_Text.Font = d_Font;
            m_Text.CharacterSize = 16;
            m_Text.Color = Color.Black;
            m_Text.Style = Text.Styles.Bold;
            m_Text.Position = new Vector2f(13, 49);

            e_Bar.PrimitiveType = PrimitiveType.Quads;
            e_Bar.Resize(4);

            e_Text.Font = d_Font;
            e_Text.CharacterSize = 16;
            e_Text.Color = Color.Black;
            e_Text.Style = Text.Styles.Bold;
            e_Text.Position = new Vector2f(13, 84);
        }

        public void SetPlayerIndex()
        {
            player = players[HandleData.myIndex];
        }

        public void UpdateHealthBar()
        {
            h_barLength = ((float)player.Health / player.MaxHealth) * f_Size;

            h_Bar[0] = new Vertex(new Vector2f(10, 10), Color.Red);
            h_Bar[1] = new Vertex(new Vector2f(h_barLength + 10, 10), Color.Red);
            h_Bar[2] = new Vertex(new Vector2f(h_barLength + 10, 40), Color.Red);
            h_Bar[3] = new Vertex(new Vector2f(10, 40), Color.Red);

            h_Text.DisplayedString = "Health: " + player.Health + " / " + player.MaxHealth;
        }

        public void UpdateManaBar()
        {
            m_barLength = ((float)player.Mana / player.MaxMana) * f_Size;

            m_Bar[0] = new Vertex(new Vector2f(10, 45), Color.Blue);
            m_Bar[1] = new Vertex(new Vector2f(m_barLength + 10, 45), Color.Blue);
            m_Bar[2] = new Vertex(new Vector2f(m_barLength + 10, 75), Color.Blue);
            m_Bar[3] = new Vertex(new Vector2f(10, 75), Color.Blue);

            m_Text.DisplayedString = "Mana: " + player.Mana + " / " + player.MaxMana;
        }

        public void UpdateExpBar()
        {
            e_Text.DisplayedString = "XP: " + player.Experience + " / " + (player.Level * 1000);

            e_barLength = ((float)player.Experience / (player.Level * 1000)) * f_Size;

            e_Bar[0] = new Vertex(new Vector2f(10, 80), Color.Yellow);
            e_Bar[1] = new Vertex(new Vector2f(e_barLength + 10, 80), Color.Yellow);
            e_Bar[2] = new Vertex(new Vector2f(e_barLength + 10, 110), Color.Yellow);
            e_Bar[3] = new Vertex(new Vector2f(10, 110), Color.Yellow);
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(h_Bar, states);
            target.Draw(m_Bar, states);
            target.Draw(e_Bar, states);
            target.Draw(h_Text);
            target.Draw(m_Text);
            target.Draw(e_Text);
        }
    }
}
