using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeMadeEngine.Templates
{
    [Serializable]
    public class ItemsTemplate
    {
        //------------------------------------------------------------------------------------------------------------
        // FIELDS
        //____________________________________________________________________________________________________________
        public string Name { get; set; }
        public EquipementSlot? Slot { get; set; }
        public ItemRarity Rarity { get; set; }
        public List<StatsTemplate>? Stats { get; set; }
        public bool? Equipped {get;set;}
        public int? Timer { get; set; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        // MAIN CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public ItemsTemplate(string p_name, EquipementSlot? p_slot, ItemRarity p_rarity, List<StatsTemplate>? p_stats, bool? p_equipped, int? p_timer)
        {
            this.Name = p_name;
            this.Slot = p_slot;
            this.Rarity = p_rarity;
            this.Stats = p_stats;
            this.Equipped = p_equipped;
            this.Timer = p_timer;
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public ItemsTemplate(string p_name, ItemRarity p_rarity, List<StatsTemplate>? p_stats) : this(p_name,null,p_rarity,p_stats, null, null) { }
        public ItemsTemplate(string p_name, ItemRarity p_rarity): this(p_name, p_rarity, null) { }

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
                        if (j != i && this.Stats[i].Element == this.Stats[j].Element)
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
