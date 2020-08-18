using HomeMadeEngine.Action;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMadeEngine.Templates
{
    [Serializable]
    public class ActionsTemplate
    {
        //------------------------------------------------------------------------------------------------------------
        // FIELDS
        //____________________________________________________________________________________________________________
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Index { get; set; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        // MAIN CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public ActionsTemplate(string p_name, int p_cost, int p_index)
        {
            this.Name = p_name;
            this.Cost = p_cost;
            this.Index = p_index;
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
        public bool UseAction(CharacterTemplate p_caster, CharacterTemplate[] p_target) => ActionsLib.Action[this.Index](p_caster, p_target);
        
    }
}
