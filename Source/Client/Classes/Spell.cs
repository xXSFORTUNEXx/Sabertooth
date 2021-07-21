using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace SabertoothClient
{
    public class Spell
    {
        public string Name { get; set; }

        public int ID { get; set; }
        public int Level { get; set; }
        public int Icon { get; set; }
        public int Vital { get; set; } //was orig damage but vital makes more since cause it can be damage or health for hots/dots
        public int HealthCost { get; set; } //this is if a spell cost health instead of mana to cast (not sure about this yet)
        public int ManaCost { get; set; }   //probably the norm resource for casting spells/buffs
        public int CoolDown { get; set; }   //time it takes before the spell can be casted again
        public int CastTime { get; set; }   //time it takes to cast the spell
        public int Charges { get; set; }    //you can learn a spell with charges and restore the charges at a well or something
        public int TotalTick { get; set; } //Total amount of time for a hot/dot
        public int TickInterval { get; set; }   //how often the dot/hot should tick damage/heal
        public int SpellType { get; set; }
        public int Range { get; set; }
        public int Animation { get; set; }
        public bool AOE { get; set; }   //area of effect
        public int Distance { get; set; }

        public Spell() { }

        public Spell(string name,
            int icon, int level, int vital, int hpcost, int mpcost, int cooldown, int casttime, int charges, int totaltick, int tickint, int spelltype, 
            int range, bool aoe, int distance)
        {
            Name = name;

            Level = level;
            Icon = icon;
            Vital = vital;
            HealthCost = hpcost;
            ManaCost = mpcost;
            CoolDown = cooldown;
            CastTime = casttime;
            Charges = charges;
            TotalTick = totaltick;
            TickInterval = tickint;
            SpellType = spelltype;
            Range = range;
            AOE = aoe;
            Distance = distance;
        }
    }

    public enum SpellType : int
    {
        None,
        Damage,
        Heal,
        Buff,
        Debuff,
        Dash,
        Shield
    }
}
