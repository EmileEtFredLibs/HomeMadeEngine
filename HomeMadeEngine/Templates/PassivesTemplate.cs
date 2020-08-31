using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMadeEngine.Templates
{
    [Serializable]
    public class PassivesTemplate
    {
        //------------------------------------------------------------------------------------------------------------
        // FIELDS
        //____________________________________________________________________________________________________________
        public PassiveType Type { get; set; }
        public PassiveName Name { get; set; }
        public List<StatsTemplate>? Stat { get; set; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        // MAIN CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public PassivesTemplate(PassiveType p_type, PassiveName p_name, List<StatsTemplate>? p_stat)
        {
            this.Type = p_type;
            this.Name = p_name;
            this.Stat = p_stat;
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public PassivesTemplate(PassiveType p_type, PassiveName p_name) : this(p_type, p_name, null) { }

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
        public void ReduceTimer(int p_turn)
        {
            if (this.Stat != null)
                for(int i=this.Stat.Count; i>0; i--)
                {
                    if (this.Stat[i].Timer != null) 
                    {
                        if (this.Stat[i].Timer > p_turn)
                            this.Stat[i].Timer -= p_turn;
                        else
                        {
                            this.Stat.RemoveAt(i);
                            i--;
                        }
                    } 
                }
        }
        public void ReduceTimer() => this.ReduceTimer(1);
    }
}
