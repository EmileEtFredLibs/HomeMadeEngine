using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static HomeMadeEngine.Character.ActionMethodLib;

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
        public static bool AttackStandart(CharacterTemplate p_caster, CharacterTemplate[] p_target)
        {
            if (p_target.Length > 1 || p_target.Length == 0)
                throw new ArgumentException("Requires 1 target ONLY");
            p_target[0].Hurt(AttackMethod(DamageTypeGrouper(p_caster, true), DamageTypeGrouper(p_target, false)));
            return true;
        }
        public static bool AttackMultiTarget(CharacterTemplate p_caster, CharacterTemplate[] p_target)
        {
            if (p_target.Length == 0)
                throw new ArgumentException("Requires 1 target MINIMUM");
            foreach(CharacterTemplate target in p_target)
                target.Hurt(AttackMethod(DamageTypeGrouper(p_caster, true), DamageTypeGrouper(target, false)));
            return true;
        }
        public static bool BerserkerRage(CharacterTemplate p_caster, CharacterTemplate[] p_target)
        {
            if (p_target.Length > 1 || p_target.Length == 0)
                throw new ArgumentException("Requires 1 target ONLY");
            if (p_target[0].Buffs.Any(a => a.name == Buffs.DamageUp))
            {
                p_caster.RemoveBuff(Buffs.DamageUp);
            }
            if (p_target[0].Debuffs.Any(a => a.name == Debuffs.DefenseDown))
            {
                p_caster.RemoveDebuff(Debuffs.DefenseDown);
            }
            Buff(p_caster, Buffs.DamageUp, 5, new StatsTemplate[] { new StatsTemplate{
                
            }});
            Debuff(p_caster, Debuffs.DefenseDown, 5, null);
            return true;
        }
    }
}
