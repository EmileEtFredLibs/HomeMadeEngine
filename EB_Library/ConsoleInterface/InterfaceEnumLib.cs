﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ConsoleGamePlayer.ConsoleInterface
{
    [Serializable]
    public enum InterfaceEnum { Testing=0, MainMenu=1, CombatMenu=2, CombatActionMenu=3, InventoryMenu=4 }
}
