using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMadeEngine
{
    public enum SpellCost { Nothing=0, Health=1, Mana=2, Rage=3, Energy=4 }
    public enum Buffs { DefenseUp=0, DamageUp=1 }
    public enum Debuffs { Unhealable=0, DefenseDown=1 }
    public enum EquipementSlot { MainHand=0, OffHand=1, Helmet=2, BodyArmor=3, Legs=4, Boots=5 }
    public enum DamageType { Unmidigatable=0, Physical=1, Magical=2, Psychological=3}
    public enum ItemRarity { Normal=0, Rare=1, Legendary=2, Unique=3};
}
