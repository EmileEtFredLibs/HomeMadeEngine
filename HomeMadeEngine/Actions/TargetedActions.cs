using HomeMadeEngine.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static HomeMadeEngine.Action.ActionMethodLib;

namespace HomeMadeEngine.Action
{
    
    public class TargetedActions
    {
        public static List<Func<CharacterTemplate, CharacterTemplate[], bool>> Library = new List<Func<CharacterTemplate, CharacterTemplate[], bool>>(){
                TargetedActions.Cure1,
                TargetedActions.StandardAttack,
                TargetedActions.AttackMultiTarget,
                TargetedActions.BerserkerRage,
                TargetedActions.Cure2
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
            var randValueLvl1 = new Random();
            int maxHealthRegain = 10;
            int minHealthRegain = 1;
            int healingValue = randValueLvl1.Next(minHealthRegain, maxHealthRegain + 1);
            
            if (p_target.Length > 1 || p_target.Length == 0)
                throw new ArgumentException("Requires 1 target ONLY");
            if (p_target[0].Debuffs.Any(a => a.Name == Debuff.Unhealable))
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
            p_target[0].Hurt(AttackMethod(DamageTypeGrouper(p_caster, StatType.Attack), DamageTypeGrouper(p_target, StatType.Defense)));
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
                target.Hurt(AttackMethod(DamageTypeGrouper(p_caster, StatType.Attack), DamageTypeGrouper(target, StatType.Defense)));
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
            if (p_target[0].Buffs.Any(a => a.Name == PassiveName.DamageUp))
            {
                p_caster.RemoveBuff(PassiveName.DamageUp);
            }
            if (p_target[0].Debuffs.Any(a => a.Name == Debuff.DefenseDown))
            {
                p_caster.RemoveDebuff(Debuff.DefenseDown);
            }
            Buff(p_caster, PassiveName.DamageUp, new List<StatsTemplate> { new StatsTemplate("Physical Damage Up", 5, StatType.Attack, DamageType.Physical, 20, 20) });
            Debuff(p_caster, Debuff.DefenseDown, null);
            return true;
        }
        /// <summary>
        /// Cure Lvl.2 / Index no.4
        /// </summary>
        /// <param name="p_caster">Character activating the cure</param>
        /// <param name="p_target">Character recieving the cure</param>
        /// <returns>Cures the character for 10-20 hp</returns>
        public static bool Cure2(CharacterTemplate p_caster, CharacterTemplate[] p_target)
        {
            var randValueLvl2 = new Random();
            int maxHealthRegain = 20;
            int minHealthRegain = 10;
            int healingValue = randValueLvl2.Next(minHealthRegain, maxHealthRegain + 1);
            if (p_target.Length > 1 || p_target.Length == 0)
                throw new ArgumentException("Requires 1 target ONLY");
            if (p_target[0].Debuffs.Any(a => a.Name == Debuff.Unhealable))
                return false;
            p_target[0].Heal(healingValue);
            return true;
        }
    }
}
