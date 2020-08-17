using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static HomeMadeEngine.Character.ActionMethodLib;

namespace HomeMadeEngine.Character
{
    
    public class ActionsLib
    {
        public static List<Func<CharacterTemplate, CharacterTemplate[], bool>> Action = new List<Func<CharacterTemplate, CharacterTemplate[], bool>>(){
                ActionsLib.Cure1,
                ActionsLib.StandardAttack,
                ActionsLib.AttackMultiTarget,
                ActionsLib.BerserkerRage
            };
        //------------------------------------------------------------------------------------------------------------
        // RANDOM NUMBERS
        //____________________________________________________________________________________________________________

        /// <summary>
        /// Cure Lvl.1
        /// </summary>
        /// <param name="p_caster"></param>
        /// <param name="p_target"></param>
        /// <returns>Cures the character for 1-10 hp</returns>
        public static bool Cure1(CharacterTemplate p_caster, CharacterTemplate[] p_target)
        {
            var randValue = new Random();
            int maxLvlOneHealthRegain = 10;
            int minLvlOneHealthRegain = 1;
            int healingValue = randValue.Next(minLvlOneHealthRegain, maxLvlOneHealthRegain+1);
            
            if (p_target.Length > 1 || p_target.Length == 0)
                throw new ArgumentException("Requires 1 target ONLY");
            if (p_target[0].Debuffs.Any(a => a.name == Debuffs.Unhealable))
                return false;
            p_target[0].Heal(healingValue);
            return true;
        }
        public static bool StandardAttack(CharacterTemplate p_caster, CharacterTemplate[] p_target)
        {
            if (p_target.Length > 1 || p_target.Length == 0)
                throw new ArgumentException("Requires 1 target ONLY");
            p_target[0].Hurt(AttackMethod(DamageTypeGrouper(p_caster, StatsType.Attack), DamageTypeGrouper(p_target, StatsType.Defense)));
            return true;
        }
        public static bool AttackMultiTarget(CharacterTemplate p_caster, CharacterTemplate[] p_target)
        {
            if (p_target.Length == 0)
                throw new ArgumentException("Requires 1 target MINIMUM");
            foreach(CharacterTemplate target in p_target)
                target.Hurt(AttackMethod(DamageTypeGrouper(p_caster, StatsType.Attack), DamageTypeGrouper(target, StatsType.Defense)));
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
