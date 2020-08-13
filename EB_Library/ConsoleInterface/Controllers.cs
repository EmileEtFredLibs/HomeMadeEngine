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
            
        }
        public bool TestingInterface(CharacterTemplate p_player, Config p_config)
        {
            WriteLine(p_config.Position);
            new Interface().CombatMenu(p_player, p_config);
            return __Choice__(p_player, p_config);
        }
        public bool MainMenu(CharacterTemplate p_player, Config p_config)
        {
            return __Choice__(p_player, p_config);
        }
        private bool __Choice__(CharacterTemplate p_player, Config p_config)
        {
            p_config.ResetMax();
            switch (ReadKey().Key)
            {
                case ConsoleKey.Escape: return true;
                case ConsoleKey.UpArrow: p_config.Down(); break;
                case ConsoleKey.DownArrow: p_config.Up(); break;
                case ConsoleKey.Enter: __MenuCenter__(p_player, p_config); break;
                // case ConsoleKey.__KEY__: __FUNCTION__(); break;
            }
            return false;
        }
        private void __MenuCenter__(CharacterTemplate p_player, Config p_config)
        {
            switch (p_config.Menu)
            {
                case InterfaceEnum.Testing: break;
                case InterfaceEnum.MainMenu: break;
                case InterfaceEnum.CombatMenu: __CombatMainMenu__(p_config); break;
                case InterfaceEnum.CombatActionMenu: __CombatActionMenu__(p_player, p_config); break;
                default: throw new ArgumentException("MenuCenter choice not handled");
            }
        }

        //------------------------------------------------------------------------------------------------------------
        // COMBAT CONTROLLER
        //____________________________________________________________________________________________________________
        private void __CombatMainMenu__(Config p_config)
        {
            switch (p_config.Position)
            {
                case 0: p_config.MenuChanging(InterfaceEnum.CombatActionMenu); break;
                case 1: p_config.MenuChanging(InterfaceEnum.CombatActionMenu); break;
                case 2: p_config.MenuChanging(InterfaceEnum.MainMenu); break;
                default: throw new ArgumentException("CombatMainMenu choice not handled");
            }
        }
        private void __CombatActionMenu__(CharacterTemplate p_player, Config p_config)
        {
            if (p_player.Actions.Count >= p_config.Position)
            {
                p_player.Actions[p_config.Position].action(p_player, new CharacterTemplate[] { p_player });
            }
            else
            {
                throw new ArgumentException("CombatActionMenu action not handled");
            }
        }

    }
}
