using HomeMadeEngine.Character;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMadeEngine
{
    [Serializable]
    public struct StatsTemplate
    {
        public string name;
        public StatsType type;
        public DamageType? dmg;
        public double flat;
        public double multi;
    }
    [Serializable]
    public struct ActionsTemplate
    {
        public string name;
        public int cost;
        public Func<CharacterTemplate, CharacterTemplate[], bool> action;
    }
    [Serializable]
    public struct BuffsTemplate
    {
        public Buffs name;
        public int timer;
        public StatsTemplate[]? stat;
    }
    [Serializable]
    public struct DebuffsTemplate
    {
        public Debuffs name;
        public int timer;
        public StatsTemplate[]? stat;
    }
    [Serializable]
    public struct EquipementsTemplate
    {
        public string name;
        public EquipementSlot? slot;
        public ItemRarity rarity;
        public StatsTemplate[]? stats;
    }
}
