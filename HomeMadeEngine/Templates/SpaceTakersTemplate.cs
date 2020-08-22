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
        public SpaceTakersTemplate(SpaceTakersTemplate p_spot)
        {
            this.Type = p_spot.Type;
            this.Character = p_spot.Character;
            this.Object = p_spot.Object;
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
        public void ChangeType(SpaceTaker p_type) => this.Type = p_type;
        public void ChangeCharacter(SpaceTaker p_type, CharacterTemplate p_char)
        {
            this.Type = p_type;
            this.Character = p_char;
            this.Object = null;
        }
        public void ChangeObject(SpaceTaker p_type, int p_int)
        {
            this.Type = p_type;
            this.Character = null;
            this.Object = p_int;
        }
    }
}
