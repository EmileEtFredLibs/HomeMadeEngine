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
        // TEST FUNCTION
        //____________________________________________________________________________________________________________
        private void __TestPlayer__()
        {
            
        }
        private void __TestMenu__()
        {
            new Interface().MainMenu();
        }

        //------------------------------------------------------------------------------------------------------------
        // MAIN MENU
        //____________________________________________________________________________________________________________
        public bool MainMenu()
        {
            __InterfaceCenter__();
            return __MenuChoice__();
        }
        private bool __MenuChoice__()
        {
            switch (ReadKey().Key)
            {
                //case ConsoleKey.Escape: return true;
                case ConsoleKey.UpArrow: ConsoleGamePlayer.config.Down(); break;
                case ConsoleKey.DownArrow: ConsoleGamePlayer.config.Up(); break;
                case ConsoleKey.Enter: return __MenuCenter__(); 
                // case ConsoleKey.__KEY__: __FUNCTION__(); break;
            }
            return false;
        }
        private void __InterfaceCenter__()
        {
            switch (ConsoleGamePlayer.config.Menu)
            {
                case InterfaceEnum.Testing: __TestMenu__(); break;
                case InterfaceEnum.MainMenu: new Interface().MainMenu(); break;
                case InterfaceEnum.CombatMenu: new Interface().CombatMenu(); break;
                case InterfaceEnum.CombatActionMenu: new Interface().CombatMenu(); break;
            }
        }
        private bool __MenuCenter__()
        {
            switch (ConsoleGamePlayer.config.Menu)
            {
                case InterfaceEnum.Testing: __TestMenu__(); break;
                case InterfaceEnum.MainMenu: return __MainMenuSwaper__(); 
                case InterfaceEnum.CombatMenu: __CombatMainMenu__(); break;
                case InterfaceEnum.CombatActionMenu: __CombatActionMenu__(); break;
                default: throw new ArgumentException("MenuCenter choice not handled");
            }
            return false;
        }

        //------------------------------------------------------------------------------------------------------------
        // COMBAT CONTROLLER
        //____________________________________________________________________________________________________________
        private void __CombatMainMenu__()
        {
            switch (ConsoleGamePlayer.config.Position)
            {
                case 0: ConsoleGamePlayer.config.MenuChanging(InterfaceEnum.CombatActionMenu); break;
                case 1: ConsoleGamePlayer.player.Defend(); break;
                case 2: break;
                case 3: ConsoleGamePlayer.config.MenuChanging(InterfaceEnum.MainMenu); break;
                default: throw new ArgumentException("CombatMainMenu choice not handled");
            }
            new Interface().CombatMenu();
        }
        private void __CombatActionMenu__()
        {
            if (ConsoleGamePlayer.player.Actions.Count >= ConsoleGamePlayer.config.Position)
            {
                ConsoleGamePlayer.player.UseAction(ConsoleGamePlayer.config.Position, new CharacterTemplate[] { ConsoleGamePlayer.player });
                ConsoleGamePlayer.config.MenuChanging(InterfaceEnum.CombatMenu);
            }
            else
            {
                throw new ArgumentException("CombatActionMenu action not handled");
            }
            ConsoleGamePlayer.config.MenuChanging(InterfaceEnum.CombatMenu);
        }

        //------------------------------------------------------------------------------------------------------------
        // MAIN MENU CONTROLLER
        //____________________________________________________________________________________________________________
        private bool __MainMenuSwaper__()
        {
            switch (ConsoleGamePlayer.config.Position)
            {
                case 0: ConsoleGamePlayer.config.MenuChanging(InterfaceEnum.CombatMenu); break;
                case 1: ConsoleGamePlayer.config.MenuChanging(InterfaceEnum.CombatMenu); break;
                case 2: return true;
                default: throw new ArgumentException("CombatMainMenu choice not handled");
            }
            new Interface().CombatMenu();
            return false;
        }
    }
}
