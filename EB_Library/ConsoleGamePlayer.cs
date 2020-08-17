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
        public static Config config = new Config();
        public static CharacterTemplate player = new CharacterTemplate();
        public void Setup(CharacterTemplate p_player, Config p_config)
        {
            var options = new Dictionary<ConsoleKey, int>
            {
                [ConsoleKey.Escape] = 0
            };
            p_player.LearnAction("Cure 1", 2, 0);
            p_player.LearnAction("Attack 1", 2, 1);
            p_player.LearnAction("Berserker Rage 1", 2, 3);
            p_player.ApplyBuff(new BuffsTemplate { name = Buffs.DefenseUp, timer = 2 });
            p_player.ApplyBuff(new BuffsTemplate { name = Buffs.DamageUp, timer = 2 });
            p_player.ApplyBuff(new BuffsTemplate { name = Buffs.DefenseUp, timer = 2 });
            p_player.ApplyDebuff(new DebuffsTemplate { name = Debuffs.DefenseDown, timer = 2 });
            p_player.RemoveBuff(Buffs.DamageUp);
            player = p_player;
            config = p_config;
            while (MainFunc());
        }
        public bool MainFunc()
        {
            Clear();
            return !new Controllers().MainMenu();           
        }
    }
}
