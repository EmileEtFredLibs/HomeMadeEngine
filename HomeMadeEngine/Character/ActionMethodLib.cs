using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text;

namespace HomeMadeEngine.Character
{
    public class ActionMethodLib
    {
        //------------------------------------------------------------------------------------------------------------
        // ATTACKS
        //____________________________________________________________________________________________________________
        /// <summary>
        /// Calculate the total damage that get through the target's defenses.
        /// </summary>
        /// <param name="p_attacker">Array of the damage type</param>
        /// <param name="p_defender">Array of the armor type</param>
        /// <returns> Total damage that got through. </returns>
        public static int AttackMethod(List<StatsTemplate> p_attacker, List<StatsTemplate> p_defender)
        {
            double damageDone = 0;
            foreach (StatsTemplate atkStat in p_attacker)
            {
                if (atkStat.dmg == DamageType.Unmidigatable)
                {
                    Console.WriteLine("{0} * {1} = {2} {3}", 
                        atkStat.flat, atkStat.multi , (int)(atkStat.flat * atkStat.multi), atkStat.type);
                    damageDone += (int)(atkStat.flat * atkStat.multi);
                }
                else
                {
                    double damageRatio = 0;
                    foreach (StatsTemplate defStat in p_defender)
                    {
                        if (atkStat.type == defStat.type)
                        {
                            damageRatio = (double)DefenseMethod((int)(defStat.flat * defStat.multi));
                            Console.WriteLine("({0} * {1}) * {2} * {3} = {4} {5}",
                                atkStat.flat, atkStat.multi, defStat.flat, defStat.multi,
                                (int)((atkStat.flat * atkStat.multi) * damageRatio), atkStat.type);
                        }
                    }
                    
                    damageDone += (int)((atkStat.flat * atkStat.multi) * damageRatio);
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
        /// <param name="p_timer">Number of turn that the buff will last</param>
        /// <param name="p_stats">Effects of the buff</param>
        public static void Buff(CharacterTemplate p_character, Buffs p_buff, int p_timer, StatsTemplate[]? p_stats)
            => p_character.ApplyBuff(new BuffsTemplate { name= p_buff, timer = p_timer, stat = p_stats });
        //------------------------------------------------------------------------------------------------------------
        // APPLYING A DEBUFF
        //____________________________________________________________________________________________________________
        /// <summary>
        /// General function to apply a buff
        /// </summary>
        /// <param name="p_character">Character on which the debuff is applied</param>
        /// <param name="p_debuff">Debuff that is applied</param>
        /// <param name="p_timer">Number of turn that the debuff will last</param>
        /// <param name="p_stats">Effects of the debuff</param>
        public static void Debuff(CharacterTemplate p_character, Debuffs p_debuff, int p_timer, StatsTemplate[]? p_stats) 
            => p_character.ApplyDebuff(new DebuffsTemplate { name = p_debuff, timer = p_timer, stat = p_stats });
        //------------------------------------------------------------------------------------------------------------
        // DEFENSE AGAINST AN ATTACK
        //____________________________________________________________________________________________________________
        /// <summary>
        /// Calculate de pourcentage of damage that get through an amount of armor
        /// </summary>
        /// <param name="p_armor">Amount of armor</param>
        /// <returns>Pourcentage of damage that get through</returns>
        public static double DefenseMethod(int p_armor) => 1.0 / (1.0 + ((double)p_armor / 10.0));

        //------------------------------------------------------------------------------------------------------------
        // DAMAGE GROUPER (OFFENSE AND DEFENSE)
        //____________________________________________________________________________________________________________
        public static List<StatsTemplate> DamageTypeGrouper(CharacterTemplate p_char, StatsType p_type)
        {
            List<StatsTemplate> charDamage = new List<StatsTemplate>();
            foreach(StatsTemplate stats in p_char.Stats)
            {
                 DamageTypeAdapter(ref charDamage, stats, p_type);
            }
            foreach (BuffsTemplate buff in p_char.Buffs)
            {
                if (buff.stat != null)
                {
                    foreach (StatsTemplate stats in buff.stat)
                    {
                            DamageTypeAdapter(ref charDamage, stats, p_type);
                    }
                }
            }
            foreach (DebuffsTemplate debuff in p_char.Debuffs)
            {
                if (debuff.stat != null)
                {
                    foreach (StatsTemplate stats in debuff.stat)
                    {
                            DamageTypeAdapter(ref charDamage, stats, p_type);
                    }
                }
            }
            return charDamage;
        }
        public static List<StatsTemplate> DamageTypeGrouper(CharacterTemplate[] p_char, StatsType p_type)
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
                        if (buff.stat!=null)
                        {
                            foreach (StatsTemplate stats in buff.stat)
                            {
                                    DamageTypeAdapter(ref charDamage, stats, p_type);
                            }
                        }
                    }
                    foreach (DebuffsTemplate debuff in character.Debuffs)
                    {
                        if (debuff.stat != null)
                        {
                            foreach (StatsTemplate stats in debuff.stat)
                            {
                                    DamageTypeAdapter(ref charDamage, stats, p_type);
                            }
                        }
                    }
                }
            }
            return charDamage;
        }
        public static void DamageTypeAdapter(ref List<StatsTemplate> p_listDmg, StatsTemplate p_damage, StatsType p_type)
        {
            if (p_damage.dmg != null)
            {
                if (p_damage.type == p_type)
                {
                    int index = p_listDmg.FindIndex((c) => c.type == p_damage.type);
                    if (index >= 0)
                    {
                        string names = p_listDmg[index].name;
                        double flats = p_listDmg[index].flat + p_damage.flat;
                        double multis = p_listDmg[index].multi * p_damage.multi;
                        DamageType? types = p_damage.dmg;
                        StatsType stats = p_damage.type;
                        p_listDmg.RemoveAt(index);
                        p_listDmg.Add(new StatsTemplate
                        {
                            name = names,
                            flat = flats,
                            multi = multis,
                            dmg = types,
                            type= stats

                        });
                    }
                    else
                    {
                        p_listDmg.Add(new StatsTemplate
                        {
                            flat = p_damage.flat,
                            multi = p_damage.multi,
                            type = p_damage.type,
                            dmg = p_damage.dmg,
                            name = p_damage.name
                        });
                    }
                }
            }
        }
    }
}
