using HomeMadeEngine;
using HomeMadeEngine.Character;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace ConsoleGamePlayer.ConsoleInterface
{
    public class Controllers
    {
        //------------------------------------------------------------------------------------------------------------
        // MAIN MENU
        //____________________________________________________________________________________________________________
        private void __TestPlayer__(CharacterTemplate p_player)
        {
            p_player.Hurt(1);
            WriteLine(p_player.CurrentHp);
            p_player.ApplyDebuff(new DebuffsTemplate { name = Debuffs.Unhealable, timer = 2 });
            for (int i = 0; i < 7; i++)
            {
                p_player.UseAction(0, p_player);
                if (p_player.Debuffs.Count > 0)
                    WriteLine("Unhealable Debuff: {0}", p_player.Debuffs[0].timer);
                p_player.UpdateTimers();
            }
        }
        public bool TestingInterface(CharacterTemplate p_player, Config p_config)
        {
            WriteLine(p_config.Position);
            new Interface().CombatMenu(p_player, p_config);
            return __Choice__(p_player, p_config, __MenuCenter__);
        }
        public bool MainMenu(CharacterTemplate p_player, Config p_config)
        {
            return __Choice__(p_player, p_config, __MenuCenter__);
        }
        private bool __Choice__(CharacterTemplate p_player, Config p_config, Action<InterfaceEnum> p_action)
        {
            p_config.ResetMax();
            switch (ReadKey().Key)
            {
                case ConsoleKey.Escape: return true;
                case ConsoleKey.UpArrow: p_config.Down(); break;
                case ConsoleKey.DownArrow: p_config.Up(); break;
                case ConsoleKey.Enter: p_action(p_config.Menu); break;
                // case ConsoleKey.__KEY__: __FUNCTION__(); break;
            }
            return false;
        }
        private void __MenuCenter__(InterfaceEnum p_interf)
        {
            // SWITCH CASE WHEN ENUM DONE
        }

        //------------------------------------------------------------------------------------------------------------
        // COMBAT CONTROLLER
        //____________________________________________________________________________________________________________

    }
}
