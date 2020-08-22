using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMadeEngine.Templates
{
    public class SpaceTakersTemplate
    {
        //------------------------------------------------------------------------------------------------------------
        // FIELDS
        //____________________________________________________________________________________________________________
        public SpaceTaker Type { get; private set; }
        public CharacterTemplate? Character { get; private set; }
        public int? Object { get; private set; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        // MAIN CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public SpaceTakersTemplate(SpaceTaker p_type, CharacterTemplate p_character)
        {
            this.Type = p_type;
            this.Character = p_character;
            this.Object = null;
        }
        public SpaceTakersTemplate(SpaceTaker p_type, int p_obj)
        {
            this.Type = p_type;
            this.Character = null;
            this.Object = p_obj;
        }
        public SpaceTakersTemplate(SpaceTaker p_type)
        {
            if ((int)p_type != 0) 
                throw new ArgumentException("CAN'T CREATE A SPACETAKER NOT EMPTY WITHOUT THEIR ATTACHED");
            this.Type = p_type;
            this.Character = null;
            this.Object = null;
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
    }
}
