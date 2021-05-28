using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static SabertoothClient.Globals;
using System.IO;

namespace SabertoothClient
{
    public class Item : Drawable
    {
        // Stored Variables
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sprite { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Type { get; set; }
        public int AttackSpeed { get; set; }
        public int HealthRestore { get; set; }
        public int ManaRestore { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Stamina { get; set; }
        public int Energy { get; set; }
        public int Value { get; set; }
        public int Price { get; set; }
        public int Rarity { get; set; }
        public int CoolDown { get; set; }        
        public int AddMaxHealth { get; set; }
        public int AddMaxMana { get; set; }
        public int BonusXP { get; set; }
        public int SpellNum { get; set; }
        public bool Stackable { get; set; }
        public int MaxStack { get; set; }

        //volatile variables
        public bool OnCoolDown { get; set; }    //So we can track for showing/clearing from the client
        public int cooldownTick; //track the cooldown 

        public Item() { }

        public Item(ItemType type)
        {
            Type = (int)type;
        }

        public Item(string name, int sprite, int damage, int armor, int type, int attackspeed, int hpRestore, int mprestore,
            int str, int agi, int intel, int ene, int sta, int value, int price, int rarity, int cooldown, int addmaxhp, int addmaxmp, int bonusxp, int spellnum, bool stack, int maxstack)
        {
            Name = name;
            Sprite = sprite;
            Damage = damage;
            Armor = armor;
            Type = type;
            AttackSpeed = attackspeed;
            HealthRestore = hpRestore;
            ManaRestore = mprestore;
            Strength = str;
            Agility = agi;
            Intelligence = intel;
            Energy = ene;
            Stamina = sta;
            Value = value;
            Price = price;
            Rarity = rarity;
            CoolDown = cooldown;
            AddMaxHealth = addmaxhp;
            AddMaxMana = addmaxmp;
            BonusXP = bonusxp;
            SpellNum = spellnum;
            Stackable = stack;
            MaxStack = maxstack;
        }

        public virtual void Draw(RenderTarget target, RenderStates states) { }
    }

    public enum ItemType
    {
        None,
        OffHand,
        MainHand,
        Currency,
        Food,
        Drink,
        Potion,
        Shirt,
        Pants,
        Shoes,
        Book,
        Other
    }

    public enum Rarity
    {
        Normal,
        Uncommon,
        Rare,
        UltraRare,
        Legendary,
        Admin
    }
}
