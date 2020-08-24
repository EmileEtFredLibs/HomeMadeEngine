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
        public List<StatsTemplate>? Stat { get; set; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        // MAIN CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public BuffsTemplate(Buff p_name, List<StatsTemplate>? p_stat)
        {
            this.Name = p_name;
            this.Stat = p_stat;
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public BuffsTemplate(Buff p_name) : this(p_name, null) { }

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
        public int FindHighestTimer()
        {
            int timer = 0;
            if (this.Stat != null) 
                foreach (StatsTemplate stat in this.Stat)
                    if (stat.Timer != null)
                        if (timer < stat.Timer)
                            timer = (int)stat.Timer;
            return timer;
        }
    }
}
