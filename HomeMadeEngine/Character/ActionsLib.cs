using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeMadeEngine.Character
{
    public class ActionsLib
    {
        public static bool Cure1(CharacterTemplate p_caster, CharacterTemplate[] p_target)
        {
            if (p_target.Length > 1 || p_target.Length == 0)
                throw new ArgumentException("Requires 1 target ONLY");
            if (p_target[0].Debuffs.Any(a => a.name == Debuffs.Unhealable))
                return false;
            p_target[0].Heal(1);
            return true;
        }
        public static bool Attack1(CharacterTemplate p_caster, CharacterTemplate[] p_target)
        {
            if (p_target.Length > 1 || p_target.Length == 0)
                throw new ArgumentException("Requires 1 target ONLY");
            p_target[0].Hurt(1);
            return true;
        }
    }
}
