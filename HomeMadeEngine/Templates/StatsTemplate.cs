using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMadeEngine.Templates
{
    [Serializable]
    public class StatsTemplate
    {
        //------------------------------------------------------------------------------------------------------------
        // FIELDS
        //____________________________________________________________________________________________________________
        public string Name { get; set; }
        public StatType Type { get; set; }
        public DamageType? Dmg { get; set; }
        public double Flat { get; set; }
        public double Multi { get; set; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        // MAIN CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public StatsTemplate(string p_name, StatType p_stat, DamageType? p_dmg, double p_flat, double p_multi)
        {
            this.Name = p_name;
            this.Type = p_stat;
            this.Dmg = p_dmg;
            this.Flat = p_flat;
            this.Multi = p_multi;
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        // public StatsTemplate(string p_name, StatType p_stat, double p_flat, double p_multi) : this(p_name, p_stat, null, p_flat, p_multi);

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
    }
}
