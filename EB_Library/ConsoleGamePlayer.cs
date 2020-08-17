using System;
using System.Collections.Generic;
using System.Text;
using ConsoleGamePlayer.ConsoleInterface;
using HomeMadeEngine;
using HomeMadeEngine.Character;
using HomeMadeEngine.Math;
using static System.Console;

namespace ConsoleGamePlayer
{
    public class ConsoleGamePlayer
    {
        public void Setup()
        {
            var options = new Dictionary<ConsoleKey, int>
            {
                [ConsoleKey.Escape] = 0
            };
            Save.Player.LearnAction("Cure 1", 2, 0);
            Save.Player.LearnAction("Attack 1", 0, 1);
            Save.Player.LearnAction("Berserker Rage 1", 5, 3);
            Save.Player.ApplyBuff(new BuffsTemplate { name = Buffs.DefenseUp, timer = 2 });
            Save.Player.ApplyBuff(new BuffsTemplate { name = Buffs.DamageUp, timer = 2 });
            Save.Player.ApplyBuff(new BuffsTemplate { name = Buffs.DefenseUp, timer = 2 });
            Save.Player.ApplyDebuff(new DebuffsTemplate { name = Debuffs.DefenseDown, timer = 2 });
            Save.Player.RemoveBuff(Buffs.DamageUp);
            Save.Config.ResetPos();
            while (MainFunc());
        }
        public bool MainFunc()
        {
            Clear();
            return !new Controllers().MainMenu();           
        }
    }
}
