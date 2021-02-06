using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabertoothServer
{
    public class Spell
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Icon { get; set; }
        public int Damage { get; set; }
        public int HealthCost { get; set; }
        public int ManaCost { get; set; }
        public int CoolDown { get; set; }
        public int Charges { get; set; }        

        public Spell() { }

        public Spell(string name, int icon, int damage, int hpcost, int mpcost, int cooldown, int charges)
        {
            Name = name;
            Icon = icon;
            Damage = damage;
            HealthCost = hpcost;
            ManaCost = mpcost;
            CoolDown = cooldown;
            Charges = charges;
        }
    }
}
