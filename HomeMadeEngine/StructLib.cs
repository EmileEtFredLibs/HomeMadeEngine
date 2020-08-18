using HomeMadeEngine.Character;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMadeEngine
{
    [Serializable]
    public struct StatsTemplates
    {
        public string name;
        public StatType type;
        public DamageType? dmg;
        public double flat;
        public double multi;
    }
    [Serializable]
    public struct ActionsTemplates
    {
        public string name;
        public int cost;
        public int index;
    }
    [Serializable]
    public struct BuffsTemplates
    {
        public Buff name;
        public int timer;
        public StatsTemplates[]? stat;
    }
    [Serializable]
    public struct DebuffsTemplates
    {
        public Debuff name;
        public int timer;
        public StatsTemplates[]? stat;
    }
    [Serializable]
    public struct EquipementsTemplates
    {
        public string name;
        public EquipementSlot? slot;
        public ItemRarity rarity;
        public StatsTemplates[]? stats;
    }
}
