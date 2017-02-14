using Gwen.Control;
using static System.Environment;
using Lidgren.Network;
using System.Threading;
using System;
using SFML.Graphics;
using SFML.System;
using static System.Convert;
using System.Text;
using System.Data.SQLite;

namespace Client.Classes
{
    class GUI
    {
        #region Main Classes
        static NetClient c_Client;
        Canvas c_Canvas;
        Gwen.Font c_Font;
        Player[] c_Player;
        Shop[] c_Shop;
        Item[] c_Item;
        Chat[] c_Chat;
        ClientConfig c_Config;
        public bool Ready;
        #endregion

        #region DebugWindow
        public WindowControl d_Window;
        public int g_Index;
        public Label d_Controller;
        public Label d_ConDir;
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
        #endregion

        #region Main Menu UI
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
        Label shopHuRestore;
        Label shopHyRestore;
        Label shopStr;
        Label shopAgi;
        Label shopEdu;
        Label shopSta;
        Label shopClip;
        Label shopMClip;
        Label shopASpeed;
        Label shopRSpeed;
        Label shopAmmo;
        Label shopType;
        Label shopValue;
        Label shopPrice;
        #endregion

        #region Bank
        public WindowControl bankWindow;
        ImagePanel[] bankPic = new ImagePanel[50];
        Button bankClose;

        public WindowControl bankStatWindow;
        ImagePanel bankStatPic;
        Label bankName;
        Label bankDamage;
        Label bankArmor;
        Label bankHeRestore;
        Label bankHuRestore;
        Label bankHyRestore;
        Label bankStr;
        Label bankAgi;
        Label bankEdu;
        Label bankSta;
        Label bankClip;
        Label bankMClip;
        Label bankASpeed;
        Label bankRSpeed;
        Label bankAmmo;
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

        #region CharTab
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
        #endregion

        #region PackTab
        public TabButton packTab;
        ImagePanel[] invPic = new ImagePanel[25];

        public WindowControl statWindow;
        ImagePanel statPic;
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
        Label packASpeed;
        Label packRSpeed;
        Label packAmmo;
        Label packType;
        Label packValue;
        Label packPrice;
        #endregion

        #region EquipTab
        public TabButton equipTab;
        ImagePanel equipMain;
        ImagePanel equipOff;
        ImagePanel equipChest;
        ImagePanel equipLegs;
        ImagePanel equipFeet;
        GroupBox equipAmmo;
        Label pistolAmmo;
        Label assaultAmmo;
        Label rocketAmmo;
        Label grenadeAmmo;

        GroupBox equipBonus;
        Label equipArmor;
        Label equipMDamage;
        Label equipODamage;
        Label equipStr;
        Label equipAgi;
        Label equipEnd;
        Label equipSta;
        #endregion

        #region SkillsTab
        public TabButton skillsTab;
        #endregion

        #region MissionTab
        public TabButton missionTab;
        #endregion

        #region OptionsTab
        public TabButton optionsTab;
        Button optLog;
        #endregion

        public GUI(NetClient c_Client, Canvas c_Canvas, Gwen.Font c_Font, Gwen.Renderer.SFML gwenRenderer, Player[] c_Player, ClientConfig c_Config, Shop[] c_Shop, Item[] c_Item, Chat[] c_Chat)
        {
            GUI.c_Client = c_Client;
            this.c_Canvas = c_Canvas;
            this.c_Font = c_Font;
            this.c_Player = c_Player;
            this.c_Config = c_Config;
            this.c_Shop = c_Shop;
            this.c_Item = c_Item;
            this.c_Chat = c_Chat;
        }

        #region Update Voids
        public void UpdateBankWindow(Player c_Player)
        {
            if (bankWindow != null && bankWindow.IsVisible)
            {
                bankWindow.Title = c_Player.Name + "'s Bank";
                for (int i = 0; i < 50; i++)
                {
                    if (c_Player.Bank[i].Name != "None")
                    {
                        bankPic[i].ImageName = "Resources/Items/" + c_Player.Bank[i].Sprite+ ".png";
                        bankPic[i].Show();
                    }
                    else
                    {
                        bankPic[i].Hide();
                    }
                    if (bankPic[i].IsHovered)
                    {
                        if (c_Player.Bank[i].Name != "None")
                        {
                            SetBankStateWindow(bankPic[i].X, bankPic[i].Y, c_Player.Bank[i]);
                            break;
                        }
                    }
                    else
                    {
                        RemoveBankStateWindow();
                    }
                }
            }
        }

        public void UpdateShopWindow(Shop c_Shop)
        {
            if (shopWindow != null && shopWindow.IsVisible)
            {
                shopWindow.Title = c_Shop.Name;
                for (int i = 0; i < 25; i++)
                {
                    if (c_Shop.shopItem[i].Name != "None")
                    {
                        shopPic[i].ImageName = "Resources/Items/" + c_Item[c_Shop.shopItem[i].ItemNum - 1].Sprite + ".png";
                        shopPic[i].Show();
                    }
                    else
                    {
                        shopPic[i].Hide();
                    }
                    if (shopPic[i].IsHovered)
                    {
                        if (c_Shop.shopItem[i].Name != "None")
                        {
                            SetShopStatWindow(shopPic[i].X, shopPic[i].Y, c_Item[c_Shop.shopItem[i].ItemNum - 1], c_Shop.shopItem[i].Cost);
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

        public void UpdateMenuWindow(Player c_Player)
        {
            if (menuWindow != null && c_Player != null && menuWindow.IsVisible)
            {
                //if (charTab.HasFocus)
                {
                    #region Character
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
                    #endregion
                }
                //if (packTab.HasFocus)
                {
                    #region BackPack
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
                                SetStatWindow(invPic[i].X, invPic[i].Y, c_Player.Backpack[i]);
                                break;
                            }
                        }
                        else
                        {
                            RemoveStatWindow();
                        }
                    }
                    #endregion
                }
                //if (equipTab.HasFocus)
                {
                    #region Equipment
                    if (c_Player.mainWeapon.Name != "None")
                    {
                        equipMain.ImageName = "Resources/Items/" + c_Player.mainWeapon.Sprite + ".png";
                        equipMain.Show();
                    }
                    else
                    {
                        equipMain.Hide();
                    }

                    if (c_Player.offWeapon.Name != "None")
                    {
                        equipOff.ImageName = "Resources/Items/" + c_Player.offWeapon.Sprite + ".png";
                        equipOff.Show();
                    }
                    else
                    {
                        equipOff.Hide();
                    }

                    if (c_Player.Chest.Name != "None")
                    {
                        equipChest.ImageName = "Resources/Items/" + c_Player.Chest.Sprite + ".png";
                        equipChest.Show();
                    }
                    else
                    {
                        equipChest.Hide();
                    }

                    if (c_Player.Legs.Name != "None")
                    {
                        equipLegs.ImageName = "Resources/Items/" + c_Player.Legs.Sprite + ".png";
                        equipLegs.Show();
                    }
                    else
                    {
                        equipLegs.Hide();
                    }

                    if (c_Player.Feet.Name != "None")
                    {
                        equipFeet.ImageName = "Resources/Items/" + c_Player.Feet.Sprite + ".png";
                        equipFeet.Show();
                    }
                    else
                    {
                        equipFeet.Hide();
                    }

                    if (equipMain.IsHovered)
                    {
                        SetStatWindow(equipMain.X, equipMain.Y, c_Player.mainWeapon);
                    }
                    else if (equipOff.IsHovered)
                    {
                        SetStatWindow(equipMain.X, equipMain.Y, c_Player.offWeapon);
                    }
                    else if (equipChest.IsHovered)
                    {
                        SetStatWindow(equipChest.X, equipChest.Y, c_Player.Chest);
                    }
                    else if (equipLegs.IsHovered)
                    {
                        SetStatWindow(equipLegs.X, equipLegs.Y, c_Player.Legs);
                    }
                    else if (equipFeet.IsHovered)
                    {
                        SetStatWindow(equipFeet.X, equipFeet.Y, c_Player.Feet);
                    }
                    else
                    {
                        //RemoveStatWindow();
                    }
                    
                    pistolAmmo.Text = "Pistol Ammo: " + c_Player.PistolAmmo;
                    assaultAmmo.Text = "Assault Ammo: " + c_Player.AssaultAmmo;
                    rocketAmmo.Text = "Rocket Ammo: " + c_Player.RocketAmmo;
                    grenadeAmmo.Text = "Grenade Ammo: " + c_Player.GrenadeAmmo;

                    equipMDamage.Text = "Main Damage: " + c_Player.mainWeapon.Damage;
                    equipODamage.Text = "Off Damage: " + c_Player.offWeapon.Damage;
                    equipArmor.Text = "Armor: " + c_Player.ArmorBonus(true);
                    equipStr.Text = "Strength: " + c_Player.StrengthBonus(true);
                    equipAgi.Text = "Agility: " + c_Player.AgilityBonus(true);
                    equipEnd.Text = "Endurance: " + c_Player.EnduranceBonus(true);
                    equipSta.Text = "Stamina: " + c_Player.StaminaBonus(true);
                    #endregion
                }
            }
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
                if (c_Client.ServerConnection != null)
                {
                    d_IP.Text = "IP Address: " + c_Client.ServerConnection.RemoteEndPoint.Address.ToString();
                    d_Port.Text = "Port: " + c_Client.ServerConnection.RemoteEndPoint.Port.ToString();
                    d_Latency.Text = "Latency: " + c_Client.ServerConnection.AverageRoundtripTime.ToString(".0#0").TrimStart('0', '.', '0') + "ms";
                    d_packetsIn.Text = "Packets Received: " + c_Client.Statistics.ReceivedPackets.ToString();
                    d_packetsOut.Text = "Packets Sent: " + c_Client.Statistics.SentPackets.ToString();
                }
            }
        }

        void SetStatWindow(int x, int y, Item statItem)
        {
            int locX = (x + 350);
            int locY = (y + 170);
            statWindow.SetPosition(locX, locY);
            statWindow.Title = statItem.Name;
            statPic.ImageName = "Resources/Items/" + statItem.Sprite + ".png";
            packDamage.Hide();
            packArmor.Hide();
            packHeRestore.Hide();
            packHuRestore.Hide();
            packHyRestore.Hide();
            packStr.Hide();
            packAgi.Hide();
            packEdu.Hide();
            packSta.Hide();
            packClip.Hide();
            packMClip.Hide();
            packASpeed.Hide();
            packRSpeed.Hide();
            packType.Hide();
            packAmmo.Hide();
            packValue.Hide();
            packPrice.Hide();

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
                case (int)ItemType.RangedWeapon:
                    packType.Text = "Ranged Weapon";
                    break;
                case (int)ItemType.MeleeWeapon:
                    packType.Text = "Melee Weapon";
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
                case (int)ItemType.FirstAid:
                    packType.Text = "First Aid";
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

            if (statItem.HungerRestore > 0)
            {
                n += 10;
                packHuRestore.SetPosition(3, n);
                packHuRestore.Text = "Hunger Restore: " + statItem.HungerRestore;
                packHuRestore.Show();
            }

            if (statItem.HydrateRestore > 0)
            {
                n += 10;
                packHyRestore.SetPosition(3, n);
                packHyRestore.Text = "Hydration Restore: " + statItem.HydrateRestore;
                packHyRestore.Show();
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

            if (statItem.Endurance > 0)
            {
                n += 10;
                packEdu.SetPosition(3, n);
                packEdu.Text = "Endurance: " + statItem.Endurance;
                packEdu.Show();
            }

            if (statItem.Stamina > 0)
            {
                n += 10;
                packSta.SetPosition(3, n);
                packSta.Text = "Stamina: " + statItem.Stamina;
                packSta.Show();
            }

            if (statItem.Clip > 0)
            {
                n += 10;
                packClip.SetPosition(3, n);
                packClip.Text = "Clip: " + statItem.Clip;
                packClip.Show();
            }

            if (statItem.MaxClip > 0)
            {
                n += 10;
                packMClip.SetPosition(3, n);
                packMClip.Text = "Max Clip: " + statItem.MaxClip;
                packMClip.Show();
            }

            if (statItem.ItemAmmoType > 0)
            {
                n += 10;
                packAmmo.SetPosition(3, n);
                switch (statItem.ItemAmmoType)
                {
                    case (int)AmmoType.Pistol:
                        packAmmo.Text = "Pistol Ammo";
                        break;
                    case (int)AmmoType.AssaultRifle:
                        packAmmo.Text = "Assault Ammo";
                        break;
                    case (int)AmmoType.Rocket:
                        packAmmo.Text = "Rocket Ammo";
                        break;
                    case (int)AmmoType.Grenade:
                        packAmmo.Text = "Grenade Ammo";
                        break;
                    default:
                        packAmmo.Text = "None";
                        break;
                }
                packAmmo.Show();
            }

            if (statItem.AttackSpeed > 0)
            {
                n += 10;
                packASpeed.SetPosition(3, n);
                packASpeed.Text = "Attack Speed: " + ((float)statItem.AttackSpeed / 1000).ToString("#.##") + " s";
                packASpeed.Show();
            }

            if (statItem.ReloadSpeed > 0)
            {
                n += 10;
                packRSpeed.SetPosition(3, n);
                packRSpeed.Text = "Reload Speed: " + ((float)statItem.ReloadSpeed / 1000).ToString("#.##") + " s";
                packRSpeed.Show();
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

            statWindow.Show();
        }

        void SetShopStatWindow(int x, int y, Item statItem, int price)
        {
            int locX = (x + 300);
            int locY = (y + 50);
            shopStatWindow.SetPosition(locX, locY);
            shopStatWindow.Title = statItem.Name;
            shopStatPic.ImageName = "Resources/Items/" + statItem.Sprite + ".png";       
            shopDamage.Hide();
            shopArmor.Hide();
            shopHeRestore.Hide();
            shopHuRestore.Hide();
            shopHyRestore.Hide();
            shopStr.Hide();
            shopAgi.Hide();
            shopEdu.Hide();
            shopSta.Hide();
            shopClip.Hide();
            shopMClip.Hide();
            shopASpeed.Hide();
            shopRSpeed.Hide();
            shopType.Hide();
            shopAmmo.Hide();
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
                    shopName.TextColor = System.Drawing.Color.Brown;
                    break;
                case (int)Rarity.Admin:
                    shopName.TextColor = System.Drawing.Color.Red;
                    break;
            }
            int n = 15;
            shopType.SetPosition(3, n);
            switch (statItem.Type)
            {
                case (int)ItemType.RangedWeapon:
                    shopType.Text = "Ranged Weapon";
                    break;
                case (int)ItemType.MeleeWeapon:
                    shopType.Text = "Melee Weapon";
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
                case (int)ItemType.FirstAid:
                    shopType.Text = "First Aid";
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

            if (statItem.HungerRestore > 0)
            {
                n += 10;
                shopHuRestore.SetPosition(3, n);
                shopHuRestore.Text = "Hunger Restore: " + statItem.HungerRestore;
                shopHuRestore.Show();
            }

            if (statItem.HydrateRestore > 0)
            {
                n += 10;
                shopHyRestore.SetPosition(3, n);
                shopHyRestore.Text = "Hydration Restore: " + statItem.HydrateRestore;
                shopHyRestore.Show();
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

            if (statItem.Endurance > 0)
            {
                n += 10;
                shopEdu.SetPosition(3, n);
                shopEdu.Text = "Endurance: " + statItem.Endurance;
                shopEdu.Show();
            }

            if (statItem.Stamina > 0)
            {
                n += 10;
                shopSta.SetPosition(3, n);
                shopSta.Text = "Stamina: " + statItem.Stamina;
                shopSta.Show();
            }

            if (statItem.Clip > 0)
            {
                n += 10;
                shopClip.SetPosition(3, n);
                shopClip.Text = "Clip: " + statItem.Clip;
                shopClip.Show();
            }

            if (statItem.MaxClip > 0)
            {
                n += 10;
                shopMClip.SetPosition(3, n);
                shopMClip.Text = "Max Clip: " + statItem.MaxClip;
                shopMClip.Show();
            }

            if (statItem.ItemAmmoType > 0)
            {
                n += 10;
                shopAmmo.SetPosition(3, n);
                switch (statItem.ItemAmmoType)
                {
                    case (int)AmmoType.Pistol:
                        shopAmmo.Text = "Pistol Ammo";
                        break;
                    case (int)AmmoType.AssaultRifle:
                        shopAmmo.Text = "Assault Ammo";
                        break;
                    case (int)AmmoType.Rocket:
                        shopAmmo.Text = "Rocket Ammo";
                        break;
                    case (int)AmmoType.Grenade:
                        shopAmmo.Text = "Grenade Ammo";
                        break;
                    default:
                        shopAmmo.Text = "None";
                        break;
                }
                shopAmmo.Show();
            }

            if (statItem.AttackSpeed > 0)
            {
                n += 10;
                shopASpeed.SetPosition(3, n);
                shopASpeed.Text = "Attack Speed: " + ((float)statItem.AttackSpeed / 1000).ToString("#.##") + " s";
                shopASpeed.Show();
            }

            if (statItem.ReloadSpeed > 0)
            {
                n += 10;
                shopRSpeed.SetPosition(3, n);
                shopRSpeed.Text = "Reload Speed: " + ((float)statItem.ReloadSpeed / 1000).ToString("#.##") + " s";
                shopRSpeed.Show();
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

        void SetBankStateWindow(int x, int y, Item bankItem)
        {
            int locX = (x + 50);
            int locY = (y + 50);
            bankStatWindow.SetPosition(locX, locY);
            bankStatWindow.Title = bankItem.Name;
            bankStatPic.ImageName = "Resources/Items/" + bankItem.Sprite + ".png";
            bankDamage.Hide();
            bankArmor.Hide();
            bankHeRestore.Hide();
            bankHuRestore.Hide();
            bankHyRestore.Hide();
            bankStr.Hide();
            bankAgi.Hide();
            bankEdu.Hide();
            bankSta.Hide();
            bankClip.Hide();
            bankMClip.Hide();
            bankASpeed.Hide();
            bankRSpeed.Hide();
            bankType.Hide();
            bankAmmo.Hide();
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
                    bankName.TextColor = System.Drawing.Color.Brown;
                    break;
                case (int)Rarity.Admin:
                    bankName.TextColor = System.Drawing.Color.Red;
                    break;
            }
            int n = 15;
            bankType.SetPosition(3, n);
            switch (bankItem.Type)
            {
                case (int)ItemType.RangedWeapon:
                    bankType.Text = "Ranged Weapon";
                    break;
                case (int)ItemType.MeleeWeapon:
                    bankType.Text = "Melee Weapon";
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
                case (int)ItemType.FirstAid:
                    bankType.Text = "First Aid";
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

            if (bankItem.HungerRestore > 0)
            {
                n += 10;
                bankHuRestore.SetPosition(3, n);
                bankHuRestore.Text = "Hunger Restore: " + bankItem.HungerRestore;
                bankHuRestore.Show();
            }

            if (bankItem.HydrateRestore > 0)
            {
                n += 10;
                bankHyRestore.SetPosition(3, n);
                bankHyRestore.Text = "Hydration Restore: " + bankItem.HydrateRestore;
                bankHyRestore.Show();
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

            if (bankItem.Endurance > 0)
            {
                n += 10;
                bankEdu.SetPosition(3, n);
                bankEdu.Text = "Endurance: " + bankItem.Endurance;
                bankEdu.Show();
            }

            if (bankItem.Stamina > 0)
            {
                n += 10;
                bankSta.SetPosition(3, n);
                bankSta.Text = "Stamina: " + bankItem.Stamina;
                bankSta.Show();
            }

            if (bankItem.Clip > 0)
            {
                n += 10;
                bankClip.SetPosition(3, n);
                bankClip.Text = "Clip: " + bankItem.Clip;
                bankClip.Show();
            }

            if (bankItem.MaxClip > 0)
            {
                n += 10;
                bankMClip.SetPosition(3, n);
                bankMClip.Text = "Max Clip: " + bankItem.MaxClip;
                bankMClip.Show();
            }

            if (bankItem.ItemAmmoType > 0)
            {
                n += 10;
                bankAmmo.SetPosition(3, n);
                switch (bankItem.ItemAmmoType)
                {
                    case (int)AmmoType.Pistol:
                        bankAmmo.Text = "Pistol Ammo";
                        break;
                    case (int)AmmoType.AssaultRifle:
                        bankAmmo.Text = "Assault Ammo";
                        break;
                    case (int)AmmoType.Rocket:
                        bankAmmo.Text = "Rocket Ammo";
                        break;
                    case (int)AmmoType.Grenade:
                        bankAmmo.Text = "Grenade Ammo";
                        break;
                    default:
                        bankAmmo.Text = "None";
                        break;
                }
                bankAmmo.Show();
            }

            if (bankItem.AttackSpeed > 0)
            {
                n += 10;
                bankASpeed.SetPosition(3, n);
                bankASpeed.Text = "Attack Speed: " + ((float)bankItem.AttackSpeed / 1000).ToString("#.##") + " s";
                bankASpeed.Show();
            }

            if (bankItem.ReloadSpeed > 0)
            {
                n += 10;
                bankRSpeed.SetPosition(3, n);
                bankRSpeed.Text = "Reload Speed: " + ((float)bankItem.ReloadSpeed / 1000).ToString("#.##") + " s";
                bankRSpeed.Show();
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

        void RemoveStatWindow()
        {
            statWindow.SetPosition(200, 10);
            statWindow.Hide();
        }

        void RemoveShopStatWindow()
        {
            shopStatWindow.SetPosition(200, 10);
            shopStatWindow.Hide();
        }

        void RemoveBankStateWindow()
        {
            bankStatWindow.SetPosition(200, 10);
            bankStatWindow.Hide();
        }

        public void UpdateNpcChatWindow(Chat c_Chat)
        {
            npcChatName.Text = c_Chat.Name;

            npcChatMessage.Clear();
            string msg = c_Chat.MainMessage;
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

                if (c_Chat.Option[i] != "None")
                {
                    npcChatOption[i].Text = c_Chat.Option[i];
                    npcChatOption[i].Show();
                }
            }
        }
        #endregion

        #region Menu Events
        private void CheckLogOutSubmit(Base control, ClickedEventArgs e)
        {
            c_Canvas.Dispose();
            c_Client.Disconnect("bye");
            Thread.Sleep(500);
            Exit(0);
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

                int result = 0;                
                using (var conn = new SQLiteConnection("Data Source=Cache/MapCache.db;Version=3;"))
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        conn.Open();
                        cmd.CommandText = "SELECT COUNT(*) FROM MAPS";
                        object queue = cmd.ExecuteScalar();
                        result = ToInt32(queue);
                    }
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

        private void EquipOff_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            int index = g_Index;

            if (c_Player[index].offWeapon.Name != "None")
            {
                NetOutgoingMessage outMSG = c_Client.CreateMessage();
                outMSG.Write((byte)PacketTypes.UnequipItem);
                outMSG.WriteVariableInt32(index);
                outMSG.WriteVariableInt32((int)EquipSlots.OffWeapon);
                c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            }
            equipTab.Focus();
        }

        private void EquipMain_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            int index = g_Index;

            if (c_Player[index].mainWeapon.Name != "None")
            {
                NetOutgoingMessage outMSG = c_Client.CreateMessage();
                outMSG.Write((byte)PacketTypes.UnequipItem);
                outMSG.WriteVariableInt32(index);
                outMSG.WriteVariableInt32((int)EquipSlots.MainWeapon);
                c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            }
            equipTab.Focus();
        }

        private void EquipChest_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            int index = g_Index;

            if (c_Player[index].Chest.Name != "None")
            {
                NetOutgoingMessage outMSG = c_Client.CreateMessage();
                outMSG.Write((byte)PacketTypes.UnequipItem);
                outMSG.WriteVariableInt32(index);
                outMSG.WriteVariableInt32((int)EquipSlots.Chest);
                c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            }
            equipTab.Focus();
        }

        private void EquipLegs_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            int index = g_Index;

            if (c_Player[index].Legs.Name != "None")
            {
                NetOutgoingMessage outMSG = c_Client.CreateMessage();
                outMSG.Write((byte)PacketTypes.UnequipItem);
                outMSG.WriteVariableInt32(index);
                outMSG.WriteVariableInt32((int)EquipSlots.Legs);
                c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            }
            equipTab.Focus();
        }

        private void EquipFeet_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            int index = g_Index;

            if (c_Player[index].Feet.Name != "None")
            {
                NetOutgoingMessage outMSG = c_Client.CreateMessage();
                outMSG.Write((byte)PacketTypes.UnequipItem);
                outMSG.WriteVariableInt32(index);
                outMSG.WriteVariableInt32((int)EquipSlots.Feet);
                c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            }
            equipTab.Focus();
        }

        private void InvPic_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            ImagePanel invPicE = (ImagePanel)sender;
            int itemSlot = ToInt32(invPicE.Name);
            int index = g_Index;

            if (c_Player[index].inShop)
            {
                if (c_Player[index].Backpack[itemSlot].Name != "None")
                {
                    NetOutgoingMessage outMSG = c_Client.CreateMessage();
                    outMSG.Write((byte)PacketTypes.SellItem);
                    outMSG.WriteVariableInt32(index);
                    outMSG.WriteVariableInt32(itemSlot);
                    outMSG.WriteVariableInt32(c_Player[index].shopNum);
                    c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                }
            }
            else if (c_Player[index].inBank)
            {
                if (c_Player[index].Backpack[itemSlot].Name != "None")
                {
                    NetOutgoingMessage outMSG = c_Client.CreateMessage();
                    outMSG.Write((byte)PacketTypes.DepositItem);
                    outMSG.WriteVariableInt32(index);
                    outMSG.WriteVariableInt32(itemSlot);
                    c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                }
            }
            else
            {
                if (c_Player[index].Backpack[itemSlot].Name != "None")
                {
                    NetOutgoingMessage outMSG = c_Client.CreateMessage();
                    outMSG.Write((byte)PacketTypes.EquipItem);
                    outMSG.WriteVariableInt32(index);
                    outMSG.WriteVariableInt32(itemSlot);
                    c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                }
            }
            packTab.Focus();
        }

        private void InvPic_RightClicked(Base sender, ClickedEventArgs arguments)
        {
            ImagePanel invPicE = (ImagePanel)sender;
            int itemSlot = ToInt32(invPicE.Name);
            int index = g_Index;

            if (c_Player[index].Backpack[itemSlot].Name != "None")
            {
                NetOutgoingMessage outMSG = c_Client.CreateMessage();
                outMSG.Write((byte)PacketTypes.DropItem);
                outMSG.WriteVariableInt32(index);
                outMSG.WriteVariableInt32(itemSlot);
                c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            }
            packTab.Focus(); 
        }

        private void ShopPic_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            ImagePanel shopPicE = (ImagePanel)sender;
            int shopSlot = ToInt32(shopPicE.Name);
            int index = g_Index;
            int shopIndex = c_Player[index].shopNum;

            if (c_Shop[shopIndex].shopItem[shopSlot].Name != "None")
            {
                NetOutgoingMessage outMSG = c_Client.CreateMessage();
                outMSG.Write((byte)PacketTypes.BuyItem);
                outMSG.WriteVariableInt32(shopSlot);
                outMSG.WriteVariableInt32(index);
                outMSG.WriteVariableInt32(shopIndex);
                c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            }
        }

        private void ChatOption_Clicked(Base sender, ClickedEventArgs arguments)
        {
            Button chatOption = (Button)sender;
            int optionSlot = ToInt32(chatOption.Name);
            int index = g_Index;
            int chatIndex = c_Player[index].chatNum;

            NetOutgoingMessage outMSG = c_Client.CreateMessage();
            outMSG.Write((byte)PacketTypes.NextChat);
            outMSG.WriteVariableInt32(optionSlot);
            outMSG.WriteVariableInt32(chatIndex);
            outMSG.WriteVariableInt32(index);
            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
        }

        private void CloseShop_Clicked(Base sender, ClickedEventArgs arguments)
        {
            int index = g_Index;

            c_Player[index].inShop = false;
            c_Player[index].shopNum = 0;
            shopWindow.Close();
            charTab.Show();
            equipTab.Show();
            skillsTab.Show();
            missionTab.Show();
            optionsTab.Show();
        }

        private void BankPick_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            ImagePanel bankPicE = (ImagePanel)sender;
            int bankSlot = ToInt32(bankPicE.Name);
            int index = g_Index;

            if (c_Player[index].Bank[bankSlot].Name != "None")
            {
                NetOutgoingMessage outMSG = c_Client.CreateMessage();
                outMSG.Write((byte)PacketTypes.WithdrawItem);
                outMSG.WriteVariableInt32(index);
                outMSG.WriteVariableInt32(bankSlot);
                c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            }
        }

        private void CloseBank_Clicked(Base sender, ClickedEventArgs arguments)
        {
            int index = g_Index;

            c_Player[index].inBank = false;
            bankWindow.Close();
            charTab.Show();
            equipTab.Show();
            skillsTab.Show();
            missionTab.Show();
            optionsTab.Show();
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

            shopHuRestore = new Label(shopStatWindow);
            shopHuRestore.SetPosition(3, 45);
            shopHuRestore.Text = "Hunger Restore: ?";

            shopHyRestore = new Label(shopStatWindow);
            shopHyRestore.SetPosition(3, 55);
            shopHyRestore.Text = "Hydration Restore: ?";

            shopStr = new Label(shopStatWindow);
            shopStr.SetPosition(3, 65);
            shopStr.Text = "Strength: ?";

            shopAgi = new Label(shopStatWindow);
            shopAgi.SetPosition(3, 75);
            shopAgi.Text = "Agility: ?";

            shopEdu = new Label(shopStatWindow);
            shopEdu.SetPosition(3, 85);
            shopEdu.Text = "Endurance: ?";

            shopSta = new Label(shopStatWindow);
            shopSta.SetPosition(3, 95);
            shopSta.Text = "Stamina: ?";

            shopClip = new Label(shopStatWindow);
            shopClip.SetPosition(3, 105);
            shopClip.Text = "Clip: ?";

            shopMClip = new Label(shopStatWindow);
            shopMClip.SetPosition(3, 115);
            shopMClip.Text = "Max Clip: ?";

            shopASpeed = new Label(shopStatWindow);
            shopASpeed.SetPosition(3, 125);
            shopASpeed.Text = "Attack Speed: ?";

            shopRSpeed = new Label(shopStatWindow);
            shopRSpeed.SetPosition(3, 135);
            shopRSpeed.Text = "Reload Speed: ?";

            shopType = new Label(shopStatWindow);
            shopType.SetPosition(3, 145);
            shopType.Text = "Type: ?";

            shopAmmo = new Label(shopStatWindow);
            shopAmmo.SetPosition(3, 155);
            shopAmmo.Text = "Ammo Type: ?";

            shopValue = new Label(shopStatWindow);
            shopValue.SetPosition(3, 160);
            shopValue.Text = "Value: ?";

            shopPrice = new Label(shopStatWindow);
            shopPrice.SetPosition(3, 160);
            shopPrice.Text = "Price: ?";
            #endregion
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
                bankPic[i].DoubleClicked += BankPick_DoubleClicked;

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

            bankHuRestore = new Label(bankStatWindow);
            bankHuRestore.SetPosition(3, 45);
            bankHuRestore.Text = "Hunger Restore: ?";

            bankHyRestore = new Label(bankStatWindow);
            bankHyRestore.SetPosition(3, 55);
            bankHyRestore.Text = "Hydration Restore: ?";

            bankStr = new Label(bankStatWindow);
            bankStr.SetPosition(3, 65);
            bankStr.Text = "Strength: ?";

            bankAgi = new Label(bankStatWindow);
            bankAgi.SetPosition(3, 75);
            bankAgi.Text = "Agility: ?";

            bankEdu = new Label(bankStatWindow);
            bankEdu.SetPosition(3, 85);
            bankEdu.Text = "Endurance: ?";

            bankSta = new Label(bankStatWindow);
            bankSta.SetPosition(3, 95);
            bankSta.Text = "Stamina: ?";

            bankClip = new Label(bankStatWindow);
            bankClip.SetPosition(3, 105);
            bankClip.Text = "Clip: ?";

            bankMClip = new Label(bankStatWindow);
            bankMClip.SetPosition(3, 115);
            bankMClip.Text = "Max Clip: ?";

            bankASpeed = new Label(bankStatWindow);
            bankASpeed.SetPosition(3, 125);
            bankASpeed.Text = "Attack Speed: ?";

            bankRSpeed = new Label(bankStatWindow);
            bankRSpeed.SetPosition(3, 135);
            bankRSpeed.Text = "Reload Speed: ?";

            bankType = new Label(bankStatWindow);
            bankType.SetPosition(3, 145);
            bankType.Text = "Type: ?";

            bankAmmo = new Label(bankStatWindow);
            bankAmmo.SetPosition(3, 155);
            bankAmmo.Text = "Ammo Type: ?";

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

            packHuRestore = new Label(statWindow);
            packHuRestore.SetPosition(3, 45);
            packHuRestore.Text = "Hunger Restore: ?";

            packHyRestore = new Label(statWindow);
            packHyRestore.SetPosition(3, 55);
            packHyRestore.Text = "Hydration Restore: ?";

            packStr = new Label(statWindow);
            packStr.SetPosition(3, 65);
            packStr.Text = "Strength: ?";

            packAgi = new Label(statWindow);
            packAgi.SetPosition(3, 75);
            packAgi.Text = "Agility: ?";

            packEdu = new Label(statWindow);
            packEdu.SetPosition(3, 85);
            packEdu.Text = "Endurance: ?";

            packSta = new Label(statWindow);
            packSta.SetPosition(3, 95);
            packSta.Text = "Stamina: ?";

            packClip = new Label(statWindow);
            packClip.SetPosition(3, 105);
            packClip.Text = "Clip: ?";

            packMClip = new Label(statWindow);
            packMClip.SetPosition(3, 115);
            packMClip.Text = "Max Clip: ?";

            packASpeed = new Label(statWindow);
            packASpeed.SetPosition(3, 125);
            packASpeed.Text = "Attack Speed: ?";

            packRSpeed = new Label(statWindow);
            packRSpeed.SetPosition(3, 135);
            packRSpeed.Text = "Reload Speed: ?";

            packType = new Label(statWindow);
            packType.SetPosition(3, 145);
            packType.Text = "Type: ?";

            packAmmo = new Label(statWindow);
            packAmmo.SetPosition(3, 155);
            packAmmo.Text = "Ammo Type: ?";

            packValue = new Label(statWindow);
            packValue.SetPosition(3, 160);
            packValue.Text = "Value: ?";

            packPrice = new Label(statWindow);
            packPrice.SetPosition(3, 160);
            packPrice.Text = "Price: ?";
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
            #endregion

            packTab = menuTabs.AddPage("Backpack");

            #region Backpack
            int n = 0;
            int c = 0;
            for (int i = 0; i < 25; i++)
            {
                invPic[i] = new ImagePanel(packTab.Page);
                invPic[i].SetSize(32, 32);
                invPic[i].SetPosition(3 + (c * 40), 5 + (n * 40));
                invPic[i].Name = i.ToString();
                invPic[i].DoubleClicked += InvPic_DoubleClicked;
                invPic[i].RightClicked += InvPic_RightClicked;

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

            equipOff = new ImagePanel(equipTab.Page);
            equipOff.SetPosition(65, 5);
            equipOff.SetSize(32, 32);
            equipOff.DoubleClicked += EquipOff_DoubleClicked;

            equipChest = new ImagePanel(equipTab.Page);
            equipChest.SetPosition(45, 50);
            equipChest.SetSize(32, 32);
            equipChest.DoubleClicked += EquipChest_DoubleClicked;

            equipLegs = new ImagePanel(equipTab.Page);
            equipLegs.SetPosition(45, 95);
            equipLegs.SetSize(32, 32);
            equipLegs.DoubleClicked += EquipLegs_DoubleClicked;

            equipFeet = new ImagePanel(equipTab.Page);
            equipFeet.SetPosition(45, 140);
            equipFeet.SetSize(32, 32);
            equipFeet.DoubleClicked += EquipFeet_DoubleClicked;

            equipAmmo = new GroupBox(equipTab.Page);
            equipAmmo.SetPosition(190, 10);
            equipAmmo.SetSize(115, 75);
            equipAmmo.Text = "Ammo Supply:";

            pistolAmmo = new Label(equipAmmo);
            pistolAmmo.SetPosition(3, 5);
            pistolAmmo.Text = "Pistol Ammo: ?";

            assaultAmmo = new Label(equipAmmo);
            assaultAmmo.SetPosition(3, 15);
            assaultAmmo.Text = "Assault Ammo: ?";

            rocketAmmo = new Label(equipAmmo);
            rocketAmmo.SetPosition(3, 25);
            rocketAmmo.Text = "Rocket Ammo: ?";

            grenadeAmmo = new Label(equipAmmo);
            grenadeAmmo.SetPosition(3, 35);
            grenadeAmmo.Text = "Grenade Ammo: ?";

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

            equipEnd = new Label(equipBonus);
            equipEnd.SetPosition(3, 55);
            equipEnd.Text = "Endurnace: ?";

            equipSta = new Label(equipBonus);
            equipSta.SetPosition(3, 65);
            equipSta.Text = "Stamina: ?";
            #endregion

            skillsTab = menuTabs.AddPage("Skills");

            missionTab = menuTabs.AddPage("Missions");

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
            d_Window.SetSize(200, 185);
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

            d_Controller = new Label(d_Window);
            d_Controller.SetPosition(10, 125);
            d_Controller.Text = "Controller: ?";

            d_ConDir = new Label(d_Window);
            d_ConDir.SetPosition(10, 135);
            d_ConDir.Text = "Dir: ?";
        }

        public void CreatNpcChatWindow(Base parent, Chat c_Chat)
        {
            npcChatWindow = new WindowControl(parent.GetCanvas());
            npcChatWindow.Title = c_Chat.Name;
            npcChatWindow.SetSize(405, 325);
            npcChatWindow.Position(Gwen.Pos.Center);
            npcChatWindow.DisableResizing();
            npcChatWindow.IsClosable = false;

            npcChatName = new Label(npcChatWindow);
            npcChatName.Text = c_Chat.Name;
            npcChatName.SetPosition(5, 5);

            npcChatMessage = new ListBox(npcChatWindow);
            npcChatMessage.RenderColor = System.Drawing.Color.Transparent;
            npcChatMessage.SetBounds(5, 20, 380, 220);

            string msg = c_Chat.MainMessage;
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

            int n = 0;
            for (int i = 0; i < 4; i++)
            {
                npcChatOption[i] = new Button(npcChatWindow);
                npcChatOption[i].SetSize(90, 25);
                npcChatOption[i].Name = i.ToString();
                npcChatOption[i].Clicked += ChatOption_Clicked;           
                npcChatOption[i].Hide();

                if (c_Chat.Option[i] != "None")
                {
                    npcChatOption[i].Text = c_Chat.Option[i];
                    npcChatOption[i].SetPosition(8 + (n * 95), 255);
                    n += 1;
                    npcChatOption[i].Show();
                }                                
            }
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

        public void MsgBox(string msg, string caption, Canvas c_Canvas)
        {
            MessageBox msgBox = new MessageBox(c_Canvas, msg, caption);
            msgBox.Position(Gwen.Pos.Center);
        }
        #endregion
    }

    class MiniMap : Drawable
    {
        Map m_Map;
        Player c_Player;
        VertexArray m_Blocked = new VertexArray(PrimitiveType.Quads, 4);
        VertexArray m_Player = new VertexArray(PrimitiveType.Quads, 4);
        VertexArray m_Npc = new VertexArray(PrimitiveType.Quads, 4);
        VertexArray m_Item = new VertexArray(PrimitiveType.Quads, 4);
        VertexArray m_NpcSpawn = new VertexArray(PrimitiveType.Quads, 4);
        VertexArray m_NpcAvoid = new VertexArray(PrimitiveType.Quads, 4);
        Texture t_Mini = new Texture("Resources/Tilesets/minimap.png");

        public void UpdateMiniMap(Player c_Player, Map c_Map)
        {
            m_Map = c_Map;
            this.c_Player = c_Player;
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            int minX = (c_Player.X + 12) - 12;
            int minY = (c_Player.Y + 9) - 9;
            int maxX = (c_Player.X + 12) + 13;
            int maxY = (c_Player.Y + 9) + 11;
            states.Texture = t_Mini;

            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    if (x > 0 && y > 0 && x < 50 && y < 50)
                    {
                        int fx = (x * 12) - (minX * 12) + 500;
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
                        if ((c_Player.X + 12) == x && (c_Player.Y + 9) == y)
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

    class HUD : Drawable
    {
        Font d_Font = new Font("Resources/Fonts/Arial.ttf");

        VertexArray h_Bar = new VertexArray();
        Text h_Text = new Text();
        float h_barLength;

        VertexArray e_Bar = new VertexArray();
        Text e_Text = new Text();
        float e_barLength;

        VertexArray c_Bar = new VertexArray();
        Text c_Text = new Text();
        float c_barLength;

        VertexArray hu_Bar = new VertexArray();
        Text hu_Text = new Text();
        float hu_barLength;

        VertexArray hy_Bar = new VertexArray();
        Text hy_Text = new Text();
        float hy_barLength;

        const int f_Size = 175;

        public HUD()
        {
            h_Bar.PrimitiveType = PrimitiveType.Quads;
            h_Bar.Resize(4);

            h_Text.Font = d_Font;
            h_Text.CharacterSize = 16;
            h_Text.Color = Color.White;
            h_Text.Style = Text.Styles.Bold;
            h_Text.Position = new Vector2f(13, 14);

            e_Bar.PrimitiveType = PrimitiveType.Quads;
            e_Bar.Resize(4);

            e_Text.Font = d_Font;
            e_Text.CharacterSize = 16;
            e_Text.Color = Color.Black;
            e_Text.Style = Text.Styles.Bold;
            e_Text.Position = new Vector2f(13, 49);

            c_Bar.PrimitiveType = PrimitiveType.Quads;
            c_Bar.Resize(4);

            c_Text.Font = d_Font;
            c_Text.CharacterSize = 16;
            c_Text.Color = Color.White;
            c_Text.Style = Text.Styles.Bold;
            c_Text.Position = new Vector2f(13, 84);

            hu_Bar.PrimitiveType = PrimitiveType.Quads;
            hu_Bar.Resize(4);

            hu_Text.Font = d_Font;
            hu_Text.CharacterSize = 16;
            hu_Text.Color = Color.White;
            hu_Text.Style = Text.Styles.Bold;
            hu_Text.Position = new Vector2f(13, 119);

            hy_Bar.PrimitiveType = PrimitiveType.Quads;
            hy_Bar.Resize(4);

            hy_Text.Font = d_Font;
            hy_Text.CharacterSize = 16;
            hy_Text.Color = Color.White;
            hy_Text.Style = Text.Styles.Bold;
            hy_Text.Position = new Vector2f(13, 154);
        }

        public void UpdateHealthBar(Player c_Player)
        {
            h_barLength = ((float)c_Player.Health / c_Player.MaxHealth) * f_Size;

            h_Bar[0] = new Vertex(new Vector2f(10, 10), Color.Red);
            h_Bar[1] = new Vertex(new Vector2f(h_barLength + 10, 10), Color.Red);
            h_Bar[2] = new Vertex(new Vector2f(h_barLength + 10, 40), Color.Red);
            h_Bar[3] = new Vertex(new Vector2f(10, 40), Color.Red);

            h_Text.DisplayedString = "Health: " + c_Player.Health + " / " + c_Player.MaxHealth;
        }

        public void UpdateExpBar(Player c_Player)
        {
            e_Text.DisplayedString = "XP: " + c_Player.Experience + " / " + (c_Player.Level * 1000);

            e_barLength = ((float)c_Player.Experience / (c_Player.Level * 1000)) * f_Size;

            e_Bar[0] = new Vertex(new Vector2f(10, 45), Color.Yellow);
            e_Bar[1] = new Vertex(new Vector2f(e_barLength + 10, 45), Color.Yellow);
            e_Bar[2] = new Vertex(new Vector2f(e_barLength + 10, 75), Color.Yellow);
            e_Bar[3] = new Vertex(new Vector2f(10, 75), Color.Yellow);
        }

        public void UpdateClipBar(Player c_Player)
        {
            if (c_Player.mainWeapon.Name != "None")
            {
                if (TickCount - c_Player.reloadTick < c_Player.mainWeapon.ReloadSpeed)
                {
                    c_Text.DisplayedString = "Reloading...";
                    c_barLength = ((float)(TickCount - c_Player.reloadTick) / c_Player.mainWeapon.ReloadSpeed) * f_Size;
                }
                else
                {
                    c_Text.DisplayedString = "Clip: " + c_Player.mainWeapon.Clip + " / " + c_Player.mainWeapon.MaxClip;
                    c_barLength = ((float)c_Player.mainWeapon.Clip / c_Player.mainWeapon.MaxClip) * f_Size;
                }
            }
            else { c_Text.DisplayedString = "None"; c_barLength = f_Size; }

            c_Bar[0] = new Vertex(new Vector2f(10, 80), Color.Green);
            c_Bar[1] = new Vertex(new Vector2f(c_barLength + 10, 80), Color.Red);
            c_Bar[2] = new Vertex(new Vector2f(c_barLength + 10, 110), Color.Yellow);
            c_Bar[3] = new Vertex(new Vector2f(10, 110), Color.Blue);
        }

        public void UpdateHungerBar(Player c_Player)
        {
            hu_Text.DisplayedString = "Hunger: " + c_Player.Hunger + " / 100";

            hu_barLength = ((float)c_Player.Hunger / 100) * f_Size;

            hu_Bar[0] = new Vertex(new Vector2f(10, 115), Color.Green);
            hu_Bar[1] = new Vertex(new Vector2f(hu_barLength + 10, 115), Color.Green);
            hu_Bar[2] = new Vertex(new Vector2f(hu_barLength + 10, 145), Color.Green);
            hu_Bar[3] = new Vertex(new Vector2f(10, 145), Color.Green);
        }

        public void UpdateHydrationBar(Player c_Player)
        {
            hy_Text.DisplayedString = "Hydration: " + c_Player.Hydration + " / 100";

            hy_barLength = ((float)c_Player.Hydration / 100) * f_Size;

            hy_Bar[0] = new Vertex(new Vector2f(10, 150), Color.Blue);
            hy_Bar[1] = new Vertex(new Vector2f(hy_barLength + 10, 150), Color.Blue);
            hy_Bar[2] = new Vertex(new Vector2f(hy_barLength + 10, 180), Color.Blue);
            hy_Bar[3] = new Vertex(new Vector2f(10, 180), Color.Blue);
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(h_Bar, states);
            target.Draw(e_Bar, states);
            target.Draw(c_Bar, states);
            target.Draw(hu_Bar, states);
            target.Draw(hy_Bar, states);
            target.Draw(h_Text);
            target.Draw(e_Text);
            target.Draw(c_Text);
            target.Draw(hu_Text);
            target.Draw(hy_Text);
        }
    }
}
