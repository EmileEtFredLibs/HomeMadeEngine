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
        public string Name { get; set; }
        public EquipementSlot? Slot { get; set; }
        public ItemRarity Rarity { get; set; }
        public StatsTemplate[]? Stats { get; set; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        // MAIN CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public EquipementsTemplate(string p_name, EquipementSlot? p_slot, ItemRarity p_rarity, StatsTemplate[]? p_stats)
        {
            this.Name = p_name;
            this.Slot = p_slot;
            this.Rarity = p_rarity;
            this.Stats = p_stats;
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
    }
}
