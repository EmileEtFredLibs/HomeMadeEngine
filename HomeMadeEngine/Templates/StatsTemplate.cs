using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
        public int? Timer { get; set; }
        public StatType Type { get; set; }
        public DamageType? Element { get; set; }
        public double Flat { get; set; }
        public double Multi { get; set; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        // MAIN CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public StatsTemplate(string p_name, int? p_timer ,StatType p_stat, DamageType? p_dmg, double p_flat, double p_multi)
        {
            this.Name = p_name;
            this.Timer = p_timer;
            this.Type = p_stat;
            this.Element = p_dmg;
            this.Flat = p_flat;
            this.Multi = p_multi;
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public StatsTemplate(string p_name, StatType p_stat) : this(p_name, null, p_stat, null, 0, 1) { }

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
    }
}
