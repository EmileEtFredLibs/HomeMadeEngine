using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMadeEngine
{
    [Serializable]
    public enum RessourceTypes { Nothing = 0, Health = 1, Mana = 2, Rage = 3, Energy = 4 }
    [Serializable]
    public enum PassiveType { Buff=0, Debuff=1 }
    [Serializable]
    public enum PassiveName { DefenseUp = 0, DamageUp = 1, ManaCostDown = 2, Defend = 3, ItemBuff = 4 }
    [Serializable]
    public enum Debuff { Unhealable = 0, DefenseDown = 1, ManaCostUp = 2, ItemDebuff = 3 }
    [Serializable]
    public enum EquipementSlot { MainHand = 0, OffHand = 1, Helmet = 2, BodyArmor = 3, Legs = 4, Boots = 5 }
    [Serializable]
    public enum DamageType { Unmidigatable = 0, Physical = 1, Magical = 2, Psychological = 3 }
    [Serializable]
    public enum ItemRarity { Normal = 0, Rare = 1, Legendary = 2, Unique = 3 }
    [Serializable]
    public enum StatType { Attack = 0, Defense = 1, Ressource = 2, Sneak = 3, HP = 4}
    [Serializable]
    public enum SpaceTaker { Nothing = 0, Object = 1, Player = 2, Enemy = 3, Allies = 4 }
    [Serializable]
    public enum Direction { North=0, South=1, West=2, East=3}
}
