using ConsoleGamePlayer.Serialization;
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
                case ConsoleKey.Escape: Save.Config.SetPos(Save.Config.Max); return __MenuCenter__();
                case ConsoleKey.UpArrow: Save.Config.Up(); break;
                case ConsoleKey.DownArrow: Save.Config.Down(); break;
                case ConsoleKey.Enter: return __MenuCenter__(); 
                // case ConsoleKey.__KEY__: __FUNCTION__(); break;
            }
            return false;
        }
        private void __InterfaceCenter__()
        {
            switch (Save.Config.Menu)
            {
                case InterfaceEnum.Testing: __TestMenu__(); break;
                case InterfaceEnum.MainMenu: new Interface().MainMenu(); break;
                case InterfaceEnum.CombatMenu: new Interface().CombatMenu(); break;
                case InterfaceEnum.CombatActionMenu: new Interface().CombatMenu(); break;
            }
        }
        private bool __MenuCenter__()
        {
            switch (Save.Config.Menu)
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
            switch (Save.Config.Position)
            {
                case 0: Save.Config.MenuChanging(InterfaceEnum.CombatActionMenu); break;
                case 1: Save.Player.Defend(); break;
                case 2: break;
                case 3: Save.Config.MenuChanging(InterfaceEnum.MainMenu); break;
                default: throw new ArgumentException("CombatMainMenu choice not handled");
            }
            new Interface().CombatMenu();
        }
        private void __CombatActionMenu__()
        {
            if (Save.Player.Actions.Count > Save.Config.Position)
            {
                Save.Player.UseAction(Save.Config.Position, new CharacterTemplate[] { Save.Player });
                Save.Config.MenuChanging(InterfaceEnum.CombatMenu);
            }
            else if (Save.Config.Position == Save.Player.Actions.Count)
            {
                Save.Config.MenuChanging(InterfaceEnum.CombatMenu);
            }
            else
            {
                throw new ArgumentException("CombatActionMenu action not handled");
            }
            Save.Config.MenuChanging(InterfaceEnum.CombatMenu);
        }

        //------------------------------------------------------------------------------------------------------------
        // MAIN MENU CONTROLLER
        //____________________________________________________________________________________________________________
        private bool __MainMenuSwaper__()
        {
            switch (Save.Config.Position)
            {
                case 0: Save.Config.MenuChanging(InterfaceEnum.CombatMenu); break;
                case 1: Save.Config.MenuChanging(InterfaceEnum.CombatMenu); break;
                case 2: return true;
                default: throw new ArgumentException("CombatMainMenu choice not handled");
            }
            new Interface().CombatMenu();
            return false;
        }
    }
}
