using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeMadeEngine.Templates
{
    [Serializable]
    public class EquipementsTemplate
    {
        //------------------------------------------------------------------------------------------------------------
        // FIELDS
        //____________________________________________________________________________________________________________
        public string Name { get; set; }
        public EquipementSlot? Slot { get; set; }
        public ItemRarity Rarity { get; set; }
        public List<StatsTemplate>? Stats { get; set; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        // MAIN CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public EquipementsTemplate(string p_name, EquipementSlot? p_slot, ItemRarity p_rarity, List<StatsTemplate>? p_stats)
        {
            this.Name = p_name;
            this.Slot = p_slot;
            this.Rarity = p_rarity;
            this.Stats = p_stats;
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public EquipementsTemplate(string p_name, ItemRarity p_rarity, List<StatsTemplate>? p_stats)
        {
            this.Name = p_name;
            this.Slot = null;
            this.Rarity = p_rarity;
            this.Stats = p_stats;
        }
        public EquipementsTemplate(string p_name, ItemRarity p_rarity)
        {
            this.Name = p_name;
            this.Slot = null;
            this.Rarity = p_rarity;
            this.Stats = null;
        }

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
        public void GroupStat()
        {
            if (this.Stats != null && this.Stats.Count > 1)
            {
                for (int i= this.Stats.Count; i>0; i--)
                {
                    for (int j = this.Stats.Count; j > 0; j--)
                    {
                        if (j != i && this.Stats[i].Dmg == this.Stats[j].Dmg)
                        {
                            this.Stats[i].Flat += this.Stats[j].Flat;
                            this.Stats[i].Multi *= this.Stats[j].Multi;
                            this.Stats.RemoveAt(j);
                            j--;
                            i--;
                        }
                    }
                }
            }
        }
    }
}
