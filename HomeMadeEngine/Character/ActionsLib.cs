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
        /// Cure Lvl.1 / Index no.0
        /// </summary>
        /// <param name="p_caster">Character activating the cure</param>
        /// <param name="p_target">Character recieving the cure</param>
        /// <returns>Cures the character for 1-10 hp</returns>
        public static bool Cure1(CharacterTemplate p_caster, CharacterTemplate[] p_target)
        {
            var randValue = new Random();
            int maxHealthRegain = 10;
            int minHealthRegain = 1;
            int healingValue = randValue.Next(minHealthRegain, maxHealthRegain + 1);
            
            if (p_target.Length > 1 || p_target.Length == 0)
                throw new ArgumentException("Requires 1 target ONLY");
            if (p_target[0].Debuffs.Any(a => a.name == Debuffs.Unhealable))
                return false;
            p_target[0].Heal(healingValue);
            return true;
        }
        /// <summary>
        /// Standard Attack / Index no.1
        /// </summary>
        /// <param name="p_caster">Character attacking</param>
        /// <param name="p_target">Character recieving the attack</param>
        /// <returns>Attack another character</returns>
        public static bool StandardAttack(CharacterTemplate p_caster, CharacterTemplate[] p_target)
        {
            if (p_target.Length > 1 || p_target.Length == 0)
                throw new ArgumentException("Requires 1 target ONLY");
            p_target[0].Hurt(AttackMethod(DamageTypeGrouper(p_caster, StatsType.Attack), DamageTypeGrouper(p_target, StatsType.Defense)));
            return true;
        }
        /// <summary>
        /// Multi Attack / Index no.2
        /// </summary>
        /// <param name="p_caster">Character attacking</param>
        /// <param name="p_target">Character(s) recieving the attack</param>
        /// <returns>Attack more than one other character</returns>
        public static bool AttackMultiTarget(CharacterTemplate p_caster, CharacterTemplate[] p_target)
        {
            if (p_target.Length == 0)
                throw new ArgumentException("Requires 1 target MINIMUM");
            foreach(CharacterTemplate target in p_target)
                target.Hurt(AttackMethod(DamageTypeGrouper(p_caster, StatsType.Attack), DamageTypeGrouper(target, StatsType.Defense)));
            return true;
        }
        /// <summary>
        /// Berserker Rage / Index no.3
        /// </summary>
        /// <param name="p_caster">Character entering a berserker rage</param>
        /// <param name="p_target">Character recieving the berserker rage buffs</param>
        /// <returns>Raise attack while lowering defense for 5 turns</returns>
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
            Buff(p_caster, Buffs.DamageUp, 5, new StatsTemplate[] { new StatsTemplate{} });
            Debuff(p_caster, Debuffs.DefenseDown, 5, null);
            return true;
        }
        /// <summary>
        /// Cure Lvl.2 / Index no.0
        /// </summary>
        /// <param name="p_caster">Character activating the cure</param>
        /// <param name="p_target">Character recieving the cure</param>
        /// <returns>Cures the character for 1-10 hp</returns>
        public static bool Cure2(CharacterTemplate p_caster, CharacterTemplate[] p_target)
        {
            var randValue = new Random();
            int maxHealthRegain = 10;
            int minHealthRegain = 1;
            int healingValue = randValue.Next(minHealthRegain, maxHealthRegain + 1);
            
            if (p_target.Length > 1 || p_target.Length == 0)
                throw new ArgumentException("Requires 1 target ONLY");
            if (p_target[0].Debuffs.Any(a => a.name == Debuffs.Unhealable))
                return false;
            p_target[0].Heal(healingValue);
            return true;
        }
    }
}
