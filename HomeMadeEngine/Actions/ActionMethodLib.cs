using HomeMadeEngine.Templates;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text;

namespace HomeMadeEngine.Action
{
    public class ActionMethodLib
    {
        //------------------------------------------------------------------------------------------------------------
        // ATTACKS
        //____________________________________________________________________________________________________________
        /// <summary>
        /// Calculate the total damage that gets through the target's defenses.
        /// </summary>
        /// <param name="p_attacker">Array of the damage type</param>
        /// <param name="p_defender">Array of the armor type</param>
        /// <returns> Total damage that got through. </returns>
        public static int AttackMethod(List<StatsTemplate> p_attacker, List<StatsTemplate> p_defender)
        {
            double damageDone = 0;
            foreach (StatsTemplate atkStat in p_attacker)
            {
                if (atkStat.Dmg == DamageType.Unmidigatable)
                {
                    Console.WriteLine("{0} * {1} = {2} {3}", 
                        atkStat.Flat, atkStat.Multi , (int)(atkStat.Flat * atkStat.Multi), atkStat.Stat);
                    damageDone += (int)(atkStat.Flat * atkStat.Multi);
                }
                else
                {
                    double damageRatio = 0;
                    foreach (StatsTemplate defStat in p_defender)
                    {
                        if (atkStat.Stat == defStat.Stat)
                        {
                            damageRatio = (double)DefenseMethod((int)(defStat.Flat * defStat.Multi));
                            Console.WriteLine("({0} * {1}) * {2} * {3} = {4} {5}",
                                atkStat.Flat, atkStat.Multi, defStat.Flat, defStat.Multi,
                                (int)((atkStat.Flat * atkStat.Multi) * damageRatio), atkStat.Stat);
                        }
                    }
                    
                    damageDone += (int)((atkStat.Flat * atkStat.Multi) * damageRatio);
                }
            }
            Console.WriteLine(damageDone);
            return (int)damageDone;
        }
        //------------------------------------------------------------------------------------------------------------
        // APPLYING A BUFF
        //____________________________________________________________________________________________________________
        /// <summary>
        /// General function to apply a buff
        /// </summary>
        /// <param name="p_character">Character on which the buff is applied</param>
        /// <param name="p_buff">Buff that is applied</param>
        /// <param name="p_timer">Number of turns that the buff will last</param>
        /// <param name="p_stats">Effects of the buff</param>
        public static void Buff(CharacterTemplate p_character, Buff p_buff, int p_timer, StatsTemplate[]? p_stats)
            => p_character.ApplyBuff(new BuffsTemplate(p_buff, p_timer, p_stats));
        //------------------------------------------------------------------------------------------------------------
        // APPLYING A DEBUFF
        //____________________________________________________________________________________________________________
        /// <summary>
        /// General function to apply a debuff
        /// </summary>
        /// <param name="p_character">Character on which the debuff is applied</param>
        /// <param name="p_debuff">Debuff that is applied</param>
        /// <param name="p_timer">Number of turns that the debuff will last</param>
        /// <param name="p_stats">Effects of the debuff</param>
        public static void Debuff(CharacterTemplate p_character, Debuff p_debuff, int p_timer, StatsTemplate[]? p_stats)
            => p_character.ApplyDebuff(new DebuffsTemplate(p_debuff, p_timer, p_stats));
        //------------------------------------------------------------------------------------------------------------
        // DEFENSE AGAINST AN ATTACK
        //____________________________________________________________________________________________________________
        /// <summary>
        /// Calculate the percentage of damage that get through an amount of armor
        /// </summary>
        /// <param name="p_armor">Amount of armor</param>
        /// <returns>Percentage of damage that get through</returns>
        public static double DefenseMethod(int p_armor) => 1.0 / (1.0 + ((double)p_armor / 10.0));

        //------------------------------------------------------------------------------------------------------------
        // DAMAGE GROUPER (OFFENSE AND DEFENSE)
        //____________________________________________________________________________________________________________
        public static List<StatsTemplate> DamageTypeGrouper(CharacterTemplate p_char, StatType p_type)
        {
            List<StatsTemplate> charDamage = new List<StatsTemplate>();
            foreach(StatsTemplate stats in p_char.Stats)
            {
                 DamageTypeAdapter(ref charDamage, stats, p_type);
            }
            foreach (BuffsTemplate buff in p_char.Buffs)
            {
                if (buff.Stat != null)
                {
                    foreach (StatsTemplate stats in buff.Stat)
                    {
                            DamageTypeAdapter(ref charDamage, stats, p_type);
                    }
                }
            }
            foreach (DebuffsTemplate debuff in p_char.Debuffs)
            {
                if (debuff.Stat != null)
                {
                    foreach (StatsTemplate stats in debuff.Stat)
                    {
                            DamageTypeAdapter(ref charDamage, stats, p_type);
                    }
                }
            }
            return charDamage;
        }
        public static List<StatsTemplate> DamageTypeGrouper(CharacterTemplate[] p_char, StatType p_type)
        {
            List<StatsTemplate> charDamage = new List<StatsTemplate>();
            if (p_char.Length > 0)
            {
                foreach (CharacterTemplate character in p_char)
                {
                    foreach (StatsTemplate stats in character.Stats)
                    {
                            DamageTypeAdapter(ref charDamage, stats, p_type);
                    }
                    foreach (BuffsTemplate buff in character.Buffs)
                    {
                        if (buff.Stat!=null)
                        {
                            foreach (StatsTemplate stats in buff.Stat)
                            {
                                    DamageTypeAdapter(ref charDamage, stats, p_type);
                            }
                        }
                    }
                    foreach (DebuffsTemplate debuff in character.Debuffs)
                    {
                        if (debuff.Stat != null)
                        {
                            foreach (StatsTemplate stats in debuff.Stat)
                            {
                                    DamageTypeAdapter(ref charDamage, stats, p_type);
                            }
                        }
                    }
                }
            }
            return charDamage;
        }
        public static void DamageTypeAdapter(ref List<StatsTemplate> p_listDmg, StatsTemplate p_damage, StatType p_type)
        {
            if (p_damage.Dmg != null)
            {
                if (p_damage.Stat == p_type)
                {
                    int index = p_listDmg.FindIndex((c) => c.Stat == p_damage.Stat);
                    if (index >= 0)
                    {
                        string names = p_listDmg[index].Name;
                        double flats = p_listDmg[index].Flat + p_damage.Flat;
                        double multis = p_listDmg[index].Multi * p_damage.Multi;
                        DamageType? types = p_damage.Dmg;
                        StatType stats = p_damage.Stat;
                        p_listDmg.RemoveAt(index);
                        p_listDmg.Add(new StatsTemplate(names, stats, types, flats, multis));
                    }
                    else
                    {
                        p_listDmg.Add(new StatsTemplate(p_damage.Name, p_damage.Stat, p_damage.Dmg, p_damage.Flat, p_damage.Multi));
                    }
                }
            }
        }
        
    }
}
