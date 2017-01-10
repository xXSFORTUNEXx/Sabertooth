using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Client.Classes
{
    class Item
    {
        public string Name { get; set; }
        public int Sprite { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Type { get; set; }
        public int AttackSpeed { get; set; }
        public int ReloadSpeed { get; set; }
        public int HealthRestore { get; set; }
        public int HungerRestore { get; set; }
        public int HydrateRestore { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Endurance { get; set; }
        public int Stamina { get; set; }
        public int Clip { get; set; }
        public int maxClip { get; set; }
        public int ammoType { get; set; }
        public int Value { get; set; }

        public Item() { }

        public Item(ItemType type)
        {
            Type = (int)type;
        }

        public Item(string name, int sprite, int damage, int armor, int type, int attackspeed, int reloadspeed,
                    int healthRestore, int foodRestore, int drinkRestore, int str, int agi, int end, int sta, int ammotype)
        {
            Name = name;
            Sprite = sprite;
            Damage = damage;
            Armor = armor;
            Type = type;
            AttackSpeed = attackspeed;
            ReloadSpeed = reloadspeed;
            HealthRestore = healthRestore;
            HungerRestore = foodRestore;
            HydrateRestore = drinkRestore;
            Strength = str;
            Agility = agi;
            Endurance = end;
            Stamina = sta;
            ammoType = ammotype;
        }
    }

    public enum ItemType
    {
        None,
        MeleeWeapon,
        RangedWeapon,
        Currency,
        Food,
        Drink,
        FirstAid,
        Shirt,
        Pants,
        Shoes,
        Other
    }

    public enum AmmoType
    {
        None,
        Pistol,
        AssaultRifle,
        Rocket,
        Grenade
    }
}
