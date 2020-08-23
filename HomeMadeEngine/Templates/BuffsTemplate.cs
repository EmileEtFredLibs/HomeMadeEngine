using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMadeEngine.Templates
{
    [Serializable]
    public class BuffsTemplate
    {
        //------------------------------------------------------------------------------------------------------------
        // FIELDS
        //____________________________________________________________________________________________________________
        public Buff Name { get; set; }
        public int Timer { get; set; }
        public List<StatsTemplate>? Stat { get; set; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        // MAIN CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public BuffsTemplate(Buff p_name, int p_timer, List<StatsTemplate>? p_stat)
        {
            this.Name = p_name;
            this.Timer = p_timer;
            this.Stat = p_stat;
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public BuffsTemplate(Buff p_name, int p_timer) : this(p_name, p_timer, null) { }

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
    }
}
