using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMadeEngine.Character
{
    [Serializable]
    public class EquipementsTemplate
    {
        //------------------------------------------------------------------------------------------------------------
        // FIELDS
        //____________________________________________________________________________________________________________
        public string name { get; set; }
        public EquipementSlot? slot { get; set; }
        public ItemRarity rarity { get; set; }
        public StatsTemplate[]? stats { get; set; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        // MAIN CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public EquipementsTemplate(string p_name, EquipementSlot? p_slot, ItemRarity p_rarity, StatsTemplate[]? p_stats)
        {
            this.name = p_name;
            this.slot = p_slot;
            this.rarity = p_rarity;
            this.stats = p_stats;
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
    }
}
