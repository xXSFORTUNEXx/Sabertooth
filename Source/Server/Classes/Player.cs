using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using System.Xml;
using static System.Environment;
using System.IO;
using System.Threading;

namespace Server.Classes
{
    class Player
    {
        public string Name { get; set; }    //define name property
        public string Pass { get; set; }    //define password
        public NetConnection Connection;        //define network connection
        public int X { get; set; }  //define x
        public int Y { get; set; }  //define y
        public int Map { get; set; }    //define map
        public int Direction { get; set; }  //define direction
        public int AimDirection { get; set; }
        public int Sprite { get; set; } //define sprite
        public int Level { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Hunger { get; set; }
        public int Hydration { get; set; }
        public int Experience { get; set; }
        public int Money { get; set; }
        public int Armor { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Endurance { get; set; }
        public int Stamina { get; set; }
        public bool Attacking { get; set; }
        public int Step;
        
        //Create some default ones for testing
        //public Item mainWeapon = new Item("Assult Rifle", 1, 100, 0, (int)ItemType.RangedWeapon, 250, 0, 0, 0, 0, 0, 0, 0, 30, 30, (int)AmmoType.AssaultRifle);
        public Item mainWeapon = new Item();
        public Item offWeapon = new Item();

        public int PistolAmmo { get; set; }
        public int AssaultAmmo { get; set; }
        public int RocketAmmo { get; set; }
        public int GrenadeAmmo { get; set; }

        public int hungerTick;
        public int hydrationTick;
        public int reloadTick;
        public int attackTick;

        public Player(string name, string pass, int x, int y, int direction, int aimdirection, int map, int level, int health, int exp, int money, 
                      int armor, int hunger, int hydration, int str, int agi, int end, int sta, int defaultAmmo, NetConnection conn)
        {
            Name = name;
            Pass = pass;
            X = x;
            Y = y;
            Map = map;
            Direction = direction;
            AimDirection = aimdirection;
            Level = level;
            Experience = exp;
            Money = money;
            Armor = armor;
            Hunger = hunger;
            Hydration = hydration;
            Strength = str;
            Agility = agi;
            Endurance = end;
            Stamina = sta;
            Connection = conn;
            FindMaxHealth();
            Health = MaxHealth;
            hungerTick = TickCount;
            hydrationTick = TickCount;
            PistolAmmo = defaultAmmo;
            AssaultAmmo = defaultAmmo;
            RocketAmmo = 5;
            GrenadeAmmo = 3;

            mainWeapon = new Item("Pistol", 1, 50, 0, (int)ItemType.RangedWeapon, 1000, 1500, 0, 0, 0, 0, 0, 0, 0, 8, 8, (int)AmmoType.Pistol);
            offWeapon = new Item("Knife", 1, 100, 0, (int)ItemType.MeleeWeapon, 650, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (int)ItemType.None);
        }

        public Player(string name, string pass, int x, int y, int direction, int aimdirection, int map, int level, int health, int exp, int money,
                      int armor, int hunger, int hydration, int str, int agi, int end, int sta, int defaultAmmo)
        {
            Name = name;
            Pass = pass;
            X = x;
            Y = y;
            Map = map;
            Direction = direction;
            AimDirection = aimdirection;
            Level = level;
            Experience = exp;
            Money = money;
            Armor = armor;
            Hunger = hunger;
            Hydration = hydration;
            Strength = str;
            Agility = agi;
            Endurance = end;
            Stamina = sta;
            FindMaxHealth();
            Health = MaxHealth;
            PistolAmmo = defaultAmmo;
            AssaultAmmo = defaultAmmo;
            RocketAmmo = 5;
            GrenadeAmmo = 3;

            mainWeapon = new Item("Pistol", 1, 50, 0, (int)ItemType.RangedWeapon, 750, 1500, 0, 0, 0, 0, 0, 0, 0, 8, 8, (int)AmmoType.Pistol);
            offWeapon = new Item("Knife", 1, 100, 0, (int)ItemType.MeleeWeapon, 650, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (int)ItemType.None);
        }

        public Player(string name, string pass, NetConnection conn)
        {
            Name = name;
            Pass = pass;
            Connection = conn;
            FindMaxHealth();
            hungerTick = TickCount;
            hydrationTick = TickCount;
        }

        public Player(NetConnection conn)
        {
            Connection = conn;
        }

        public Player() { }

        public void FindMaxHealth()
        {
            MaxHealth = 100 + (Endurance * 5) + (Level * 10);
        }

        public void RegenHealth()
        {
            if (Health < MaxHealth)
            {
                Health += (Stamina * 10);
            }

            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }

        public void VitalLoss(string vital)
        {
            if (vital == "food")
            {
                if (Hunger <= 0)
                {
                    //Basically we start die
                    Console.WriteLine("We start to die...");
                }
                else
                {
                    Hunger -= 10;
                }
            }

            if (vital == "water")
            {
                if (Hydration <= 0)
                {
                    //Basically we start die
                    Console.WriteLine("We start to die...");
                }
                else
                {
                    Hydration -= 10;
                }
            }
        }

        public void RemoveBulletFromClip(NetServer s_Server, Player[] s_Player, int index)
        {
            if (mainWeapon.Clip > 0)
            {
                mainWeapon.Clip -= 1;
                attackTick = TickCount;
            }

            if (mainWeapon.Clip == 0)
            {
                ReloadClip();
                reloadTick = TickCount;
            }

            SendUpdateAmmo(s_Server, s_Player, index);
        }

        public void ReloadClip()
        {
            if (mainWeapon.Clip == 0)
            {
                switch (mainWeapon.ammoType)
                {
                    case (int)AmmoType.None:
                        return;
                    case (int)AmmoType.Pistol:
                        if (PistolAmmo > mainWeapon.maxClip)
                        {
                            mainWeapon.Clip = mainWeapon.maxClip;
                            PistolAmmo -= mainWeapon.maxClip;                            
                        }
                        else
                        {
                            mainWeapon.Clip = PistolAmmo;
                            PistolAmmo = 0;
                        }
                        break;
                    case (int)AmmoType.AssaultRifle:
                        if (AssaultAmmo > mainWeapon.maxClip)
                        {
                            mainWeapon.Clip = mainWeapon.maxClip;
                            AssaultAmmo -= mainWeapon.maxClip;
                        }
                        else
                        {
                            mainWeapon.Clip = AssaultAmmo;
                            AssaultAmmo = 0;
                        }
                        break;
                    case (int)AmmoType.Rocket:
                        if (RocketAmmo > mainWeapon.maxClip)
                        {
                            mainWeapon.Clip = mainWeapon.maxClip;
                            RocketAmmo = -mainWeapon.maxClip;
                        }
                        else
                        {
                            mainWeapon.Clip = RocketAmmo;
                            RocketAmmo = 0;
                        }
                        break;
                    case (int)AmmoType.Grenade:
                        if (GrenadeAmmo > mainWeapon.maxClip)
                        {
                            mainWeapon.Clip = mainWeapon.maxClip;
                            GrenadeAmmo = -mainWeapon.maxClip;
                        }
                        else
                        {
                            mainWeapon.Clip = GrenadeAmmo;
                            GrenadeAmmo = 0;
                        }
                        break;
                }
            }
            else
            {
                switch (mainWeapon.ammoType)
                {
                    case (int)AmmoType.None:
                        return;
                    case (int)AmmoType.Pistol:
                        if (PistolAmmo > (mainWeapon.maxClip - mainWeapon.Clip))
                        {
                            PistolAmmo -= (mainWeapon.maxClip - mainWeapon.Clip);
                            mainWeapon.Clip = mainWeapon.maxClip;
                            reloadTick = TickCount;
                        }
                        else
                        {
                            mainWeapon.Clip += PistolAmmo;
                            PistolAmmo = 0;
                            reloadTick = TickCount;
                        }
                        break;
                    case (int)AmmoType.AssaultRifle:
                        if (AssaultAmmo > (mainWeapon.maxClip - mainWeapon.Clip))
                        {
                            AssaultAmmo -= (mainWeapon.maxClip - mainWeapon.Clip);
                            mainWeapon.Clip = mainWeapon.maxClip;
                        }
                        else
                        {
                            mainWeapon.Clip += AssaultAmmo;
                            AssaultAmmo = 0;
                        }
                        break;
                    case (int)AmmoType.Rocket:
                        if (AssaultAmmo > (mainWeapon.maxClip - mainWeapon.Clip))
                        {
                            AssaultAmmo -= (mainWeapon.maxClip - mainWeapon.Clip);
                            mainWeapon.Clip = mainWeapon.maxClip;
                        }
                        else
                        {
                            mainWeapon.Clip += AssaultAmmo;
                            AssaultAmmo = 0;
                        }
                        break;
                    case (int)AmmoType.Grenade:
                        if (GrenadeAmmo > (mainWeapon.maxClip - mainWeapon.Clip))
                        {
                            GrenadeAmmo -= (mainWeapon.maxClip - mainWeapon.Clip);
                            mainWeapon.Clip = mainWeapon.maxClip;
                        }
                        else
                        {
                            mainWeapon.Clip += GrenadeAmmo;
                            GrenadeAmmo = 0;
                        }
                        break;
                }
            }
        }

        //Send updated ammo reserve and clip
        public void SendUpdateAmmo(NetServer s_Server, Player[] s_Player, int index)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            if (s_Player[index].Connection != null)
            {
                outMSG.Write((byte)PacketTypes.UpdateAmmo);
                outMSG.WriteVariableInt32(index);
                outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Clip);
                outMSG.WriteVariableInt32(s_Player[index].PistolAmmo);
                outMSG.WriteVariableInt32(s_Player[index].AssaultAmmo);
                outMSG.WriteVariableInt32(s_Player[index].RocketAmmo);
                outMSG.WriteVariableInt32(s_Player[index].GrenadeAmmo);
                s_Server.SendMessage(outMSG, s_Player[index].Connection, NetDeliveryMethod.ReliableSequenced, 4);
            }
        }

        public void SavePlayerXML()
        {
            XmlWriterSettings userData = new XmlWriterSettings();
            userData.Indent = true;
            string sname = Name.ToLower();

            XmlWriter writer = XmlWriter.Create("Players/" + sname + ".xml", userData);
            LogWriter.WriteLog("Player XML file saved " + sname, "Server");
            writer.WriteStartDocument();
            //writer.WriteComment("This file is generated by the server.");
            writer.WriteStartElement("PlayerData");
            writer.WriteElementString("Name", Name);
            writer.WriteElementString("Password", Pass);
            writer.WriteElementString("X", X.ToString());
            writer.WriteElementString("Y", Y.ToString());
            writer.WriteElementString("Map", Map.ToString());
            writer.WriteElementString("Direction", Direction.ToString());
            writer.WriteElementString("AimDirection", AimDirection.ToString());
            writer.WriteElementString("Sprite", Sprite.ToString());
            writer.WriteElementString("Level", Level.ToString());
            writer.WriteElementString("Health", Health.ToString());
            writer.WriteElementString("Experience", Experience.ToString());
            writer.WriteElementString("Money", Money.ToString());
            writer.WriteElementString("Armor", Armor.ToString());
            writer.WriteElementString("Hunger", Hunger.ToString());
            writer.WriteElementString("Hydration", Hydration.ToString());
            writer.WriteElementString("Strength", Strength.ToString());
            writer.WriteElementString("Agility", Agility.ToString());
            writer.WriteElementString("Endurance", Endurance.ToString());
            writer.WriteElementString("Stamina", Stamina.ToString());
            writer.WriteElementString("PistolAmmo", PistolAmmo.ToString());
            writer.WriteElementString("AssaultAmmo", AssaultAmmo.ToString());
            writer.WriteElementString("RocketAmmo", RocketAmmo.ToString());
            writer.WriteElementString("GrenadeAmmo", GrenadeAmmo.ToString());

            writer.WriteStartElement("MainWeaponData");
            writer.WriteElementString("Name", mainWeapon.Name);
            writer.WriteElementString("Clip", mainWeapon.Clip.ToString());
            writer.WriteElementString("MaxClip", mainWeapon.maxClip.ToString());
            writer.WriteElementString("Sprite", mainWeapon.Sprite.ToString());
            writer.WriteElementString("Damage", mainWeapon.Damage.ToString());
            writer.WriteElementString("Armor", mainWeapon.Armor.ToString());
            writer.WriteElementString("Type", mainWeapon.Type.ToString());
            writer.WriteElementString("AttackSpeed", mainWeapon.AttackSpeed.ToString());
            writer.WriteElementString("ReloadSpeed", mainWeapon.ReloadSpeed.ToString());
            writer.WriteElementString("HealthRestore", mainWeapon.HealthRestore.ToString());
            writer.WriteElementString("HungerRestore", mainWeapon.HungerRestore.ToString());
            writer.WriteElementString("HydrateRestore", mainWeapon.HydrateRestore.ToString());
            writer.WriteElementString("Strength", mainWeapon.Strength.ToString());
            writer.WriteElementString("Agility", mainWeapon.Agility.ToString());
            writer.WriteElementString("Endurance", mainWeapon.Endurance.ToString());
            writer.WriteElementString("Stamina", mainWeapon.Stamina.ToString());
            writer.WriteElementString("AmmoType", mainWeapon.ammoType.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("OffWeaponData");
            writer.WriteElementString("Name", offWeapon.Name);
            writer.WriteElementString("Clip", offWeapon.Clip.ToString());
            writer.WriteElementString("MaxClip", offWeapon.maxClip.ToString());
            writer.WriteElementString("Sprite", offWeapon.Sprite.ToString());
            writer.WriteElementString("Damage", offWeapon.Damage.ToString());
            writer.WriteElementString("Armor", offWeapon.Armor.ToString());
            writer.WriteElementString("Type", offWeapon.Type.ToString());
            writer.WriteElementString("AttackSpeed", offWeapon.AttackSpeed.ToString());
            writer.WriteElementString("ReloadSpeed", offWeapon.ReloadSpeed.ToString());
            writer.WriteElementString("HealthRestore", offWeapon.HealthRestore.ToString());
            writer.WriteElementString("HungerRestore", offWeapon.HungerRestore.ToString());
            writer.WriteElementString("HydrateRestore", offWeapon.HydrateRestore.ToString());
            writer.WriteElementString("Strength", offWeapon.Strength.ToString());
            writer.WriteElementString("Agility", offWeapon.Agility.ToString());
            writer.WriteElementString("Endurance", offWeapon.Endurance.ToString());
            writer.WriteElementString("Stamina", offWeapon.Stamina.ToString());
            writer.WriteElementString("AmmoType", offWeapon.ammoType.ToString());
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        public void LoadPlayerXML()
        {
            string lname = Name.ToLower();
            XmlReader reader = XmlReader.Create("Players/" + lname + ".xml");
            LogWriter.WriteLog("Player XML file loaded " + lname, "Server");
            reader.ReadToFollowing("Name");
            Name = reader.ReadElementContentAsString();
            reader.ReadToFollowing("Password");
            Pass = reader.ReadElementContentAsString();
            reader.ReadToFollowing("X");
            X = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Y");
            Y = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Map");
            Map = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Direction");
            Direction = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("AimDirection");
            AimDirection = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Sprite");
            Sprite = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Level");
            Level = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Health");
            Health = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Experience");
            Experience = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Money");
            Money = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Armor");
            Armor = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Hunger");
            Hunger = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Hydration");
            Hydration = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Strength");
            Strength = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Agility");
            Agility = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Endurance");
            Endurance = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Stamina");
            Stamina = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("PistolAmmo");
            PistolAmmo = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("AssaultAmmo");
            AssaultAmmo = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("RocketAmmo");
            RocketAmmo = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("GrenadeAmmo");
            GrenadeAmmo = reader.ReadElementContentAsInt();
            FindMaxHealth();
            //MainwWeapon
            reader.ReadToFollowing("Name");
            mainWeapon.Name = reader.ReadElementContentAsString();
            reader.ReadToFollowing("Clip");
            mainWeapon.Clip = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("MaxClip");
            mainWeapon.maxClip = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Sprite");
            mainWeapon.Sprite = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Damage");
            mainWeapon.Damage = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Armor");
            mainWeapon.Armor = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Type");
            mainWeapon.Type = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("AttackSpeed");
            mainWeapon.AttackSpeed = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("ReloadSpeed");
            mainWeapon.ReloadSpeed = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("HealthRestore");
            mainWeapon.HealthRestore = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("HungerRestore");
            mainWeapon.HungerRestore = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("HydrateRestore");
            mainWeapon.HydrateRestore = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Strength");
            mainWeapon.Strength = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Agility");
            mainWeapon.Agility = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Endurance");
            mainWeapon.Endurance = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Stamina");
            mainWeapon.Stamina = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("AmmoType");
            mainWeapon.ammoType = reader.ReadElementContentAsInt();
            //Offweapon
            reader.ReadToFollowing("Name");
            offWeapon.Name = reader.ReadElementContentAsString();
            reader.ReadToFollowing("Clip");
            offWeapon.Clip = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("MaxClip");
            offWeapon.maxClip = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Sprite");
            offWeapon.Sprite = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Damage");
            offWeapon.Damage = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Armor");
            offWeapon.Armor = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Type");
            offWeapon.Type = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("AttackSpeed");
            offWeapon.AttackSpeed = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("ReloadSpeed");
            offWeapon.ReloadSpeed = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("HealthRestore");
            offWeapon.HealthRestore = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("HungerRestore");
            offWeapon.HungerRestore = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("HydrateRestore");
            offWeapon.HydrateRestore = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Strength");
            offWeapon.Strength = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Agility");
            offWeapon.Agility = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Endurance");
            offWeapon.Endurance = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("Stamina");
            offWeapon.Stamina = reader.ReadElementContentAsInt();
            reader.ReadToFollowing("AmmoType");
            offWeapon.ammoType = reader.ReadElementContentAsInt();
            reader.Close();
        }
    }

    public enum Directions
    {
        Down,
        Left,
        Right,
        Up,
        DownLeft,
        DownRight,
        UpLeft,
        UpRight
    }
}
