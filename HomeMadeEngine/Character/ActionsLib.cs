using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeMadeEngine.Character
{
    public class ActionsLib
    {
        public static bool Cure1(CharacterTemplate p_caster, CharacterTemplate p_target)
        {
            if (p_target.Debuffs.Any(a => a.name == Debuffs.Unhealable))
                return false;
            p_target.Heal(1);
            return true;
        }
        public static bool Attack1(CharacterTemplate p_caster, CharacterTemplate p_target)
        {
            p_target.Hurt(1);
            return true;
        }
    }
}
