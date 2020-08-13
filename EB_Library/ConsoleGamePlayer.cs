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
        public void Setup(CharacterTemplate p_player, Config p_config)
        {
            var options = new Dictionary<ConsoleKey, int>
            {
                [ConsoleKey.Escape] = 0
            };
            p_player.LearnAction("Cure 1", 2, ActionsLib.Cure1);
            p_player.LearnAction("Attack 1", 2, ActionsLib.AttackStandart);
            p_player.ApplyBuff(new BuffsTemplate { name = Buffs.DefenseUp, timer = 2 });
            p_player.ApplyDebuff(new DebuffsTemplate { name = Debuffs.DefenseDown, timer = 2 });
            while (MainFunc(p_player, p_config));
        }
        public bool MainFunc(CharacterTemplate p_player, Config p_config)
        {
            Clear();
            return !new Controllers().MainMenu(p_player, p_config);           
        }
    }
}
