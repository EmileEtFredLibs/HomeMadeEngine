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
                if (atkStat.Element == DamageType.Unmidigatable)
                {
                    Console.WriteLine("{0} * {1} = {2} {3}", 
                        atkStat.Flat, atkStat.Multi , (int)(atkStat.Flat * atkStat.Multi), atkStat.Type);
                    damageDone += (int)(atkStat.Flat * atkStat.Multi);
                }
                else
                {
                    double damageRatio = 0;
                    foreach (StatsTemplate defStat in p_defender)
                    {
                        if (atkStat.Type == defStat.Type)
                        {
                            damageRatio = (double)DefenseMethod((int)(defStat.Flat * defStat.Multi));
                            Console.WriteLine("({0} * {1}) * {2} * {3} = {4} {5}",
                                atkStat.Flat, atkStat.Multi, defStat.Flat, defStat.Multi,
                                (int)((atkStat.Flat * atkStat.Multi) * damageRatio), atkStat.Type);
                        }
                    }
                    
                    damageDone += (int)((atkStat.Flat * atkStat.Multi) * damageRatio);
                }
            }
            Console.WriteLine(damageDone);
            return (int)damageDone;
        }
        //------------------------------------------------------------------------------------------------------------
        // APPLYING A ŦEMPORARY PASSIVES
        //____________________________________________________________________________________________________________
        /// <summary>
        /// General function to apply a buff
        /// </summary>
        /// <param name="p_character">Character on which the buff is applied</param>
        /// <param name="p_buff">Buff that is applied</param>
        /// <param name="p_timer">Number of turns that the buff will last</param>
        /// <param name="p_stats">Effects of the buff</param>
        public static void ApplyGenPassive(CharacterTemplate p_character, PassiveType p_type, PassiveName p_buff, List<StatsTemplate>? p_stats)
            => p_character.ApplyPassive(new PassivesTemplate(p_type, p_buff, p_stats));
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
            foreach (PassivesTemplate passive in p_char.Passives)
            {
                if (passive.Stat != null)
                {
                    foreach (StatsTemplate stats in passive.Stat)
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
                    foreach (PassivesTemplate buff in character.Passives)
                    {
                        if (buff.Stat!=null)
                        {
                            foreach (StatsTemplate stats in buff.Stat)
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
            if (p_damage.Element != null)
            {
                if (p_damage.Type == p_type)
                {
                    int index = p_listDmg.FindIndex((c) => c.Type == p_damage.Type);
                    if (index >= 0)
                    {
                        string names = p_listDmg[index].Name;
                        double flats = p_listDmg[index].Flat + p_damage.Flat;
                        double multis = p_listDmg[index].Multi * p_damage.Multi;
                        DamageType? types = p_damage.Element;
                        StatType stats = p_damage.Type;
                        p_listDmg.RemoveAt(index);
                        p_listDmg.Add(new StatsTemplate(names, null, stats, types, flats, multis));
                    }
                    else
                    {
                        p_listDmg.Add(new StatsTemplate(p_damage.Name, p_damage.Timer, p_damage.Type, p_damage.Element, p_damage.Flat, p_damage.Multi));
                    }
                }
            }
        }
        
    }
}
