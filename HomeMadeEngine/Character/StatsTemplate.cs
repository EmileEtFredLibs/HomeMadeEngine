using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMadeEngine.Character
{
    [Serializable]
    public class StatsTemplate
    {
        //------------------------------------------------------------------------------------------------------------
        // FIELDS
        //____________________________________________________________________________________________________________
        public string name { get; set; }
        public StatType type { get; set; }
        public DamageType? dmg { get; set; }
        public double flat { get; set; }
        public double multi { get; set; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        // MAIN CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public StatsTemplate(string p_name, StatType p_stat, DamageType? p_dmg, double p_flat, double p_multi)
        {
            this.name = p_name;
            this.type = p_stat;
            this.dmg = p_dmg;
            this.flat = p_flat;
            this.multi = p_multi;
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        // public StatsTemplate(string p_name, StatType p_stat, double p_flat, double p_multi) : this(p_name, p_stat, null, p_flat, p_multi);

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
    }
}
