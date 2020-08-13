using System;
using System.Collections.Generic;
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
        public static int AttackMethod(List<DamageTemplate> p_attacker, List<DamageTemplate> p_defender)
        {
            double damageDone = 0;
            foreach (DamageTemplate atkStat in p_attacker)
            {
                if (atkStat.type == DamageType.Unmidigatable)
                {
                    Console.WriteLine("{0} * {1} = {2} {3}", 
                        atkStat.flat, atkStat.multi , (int)(atkStat.flat * atkStat.multi), atkStat.type);
                    damageDone += (int)(atkStat.flat * atkStat.multi);
                }
                else
                {
                    double damageRatio = 0;
                    foreach (DamageTemplate defStat in p_defender)
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
        public static List<DamageTemplate> DamageTypeGrouper(CharacterTemplate p_char, bool p_type)
        {
            List<DamageTemplate> charDamage = new List<DamageTemplate>();
            foreach(StatsTemplate stats in p_char.Stats)
            {
                if (stats.damage != null)
                    DamageTypeAdapter(ref charDamage, (DamageTemplate)stats.damage, p_type);
            }
            foreach (BuffsTemplate buff in p_char.Buffs)
            {
                if (buff.stat != null)
                {
                    foreach (StatsTemplate stats in buff.stat)
                    {
                        if (stats.damage != null)
                            DamageTypeAdapter(ref charDamage, (DamageTemplate)stats.damage, p_type);
                    }
                }
            }
            foreach (DebuffsTemplate debuff in p_char.Debuffs)
            {
                if (debuff.stat != null)
                {
                    foreach (StatsTemplate stats in debuff.stat)
                    {
                        if (stats.damage != null)
                            DamageTypeAdapter(ref charDamage, (DamageTemplate)stats.damage, p_type);
                    }
                }
            }
            return charDamage;
        }
        public static List<DamageTemplate> DamageTypeGrouper(CharacterTemplate[] p_char, bool p_type)
        {
            List<DamageTemplate> charDamage = new List<DamageTemplate>();
            if (p_char.Length > 0)
            {
                foreach (CharacterTemplate character in p_char)
                {
                    foreach (StatsTemplate stats in character.Stats)
                    {
                        if (stats.damage != null)
                            DamageTypeAdapter(ref charDamage, (DamageTemplate)stats.damage, p_type);
                    }
                    foreach (BuffsTemplate buff in character.Buffs)
                    {
                        if (buff.stat!=null)
                        {
                            foreach (StatsTemplate stats in buff.stat)
                            {
                                if (stats.damage != null)
                                    DamageTypeAdapter(ref charDamage, (DamageTemplate)stats.damage, p_type);
                            }
                        }
                    }
                    foreach (DebuffsTemplate debuff in character.Debuffs)
                    {
                        if (debuff.stat != null)
                        {
                            foreach (StatsTemplate stats in debuff.stat)
                            {
                                if (stats.damage!=null)
                                    DamageTypeAdapter(ref charDamage, (DamageTemplate)stats.damage, p_type);
                            }
                        }
                    }
                }
            }
            return charDamage;
        }
        public static void DamageTypeAdapter(ref List<DamageTemplate> p_listDmg, DamageTemplate p_damage, bool p_atk)
        {
            if (p_damage.atk == p_atk)
            {
                int index = p_listDmg.FindIndex((c) => c.type == p_damage.type);
                if (index >= 0)
                {
                    double flats = p_listDmg[index].flat + p_damage.flat;
                    double multis = p_listDmg[index].multi * p_damage.multi;
                    DamageType types = p_damage.type;
                    p_listDmg.RemoveAt(index);
                    p_listDmg.Add(new DamageTemplate
                    {
                        atk = p_atk,
                        flat = flats,
                        multi = multis,
                        type = types
                    });
                }
                else
                {
                    p_listDmg.Add(new DamageTemplate
                    {
                        atk = p_atk,
                        flat = p_damage.flat,
                        multi = p_damage.multi,
                        type = p_damage.type
                    });
                }
            }
        }
    }
}
