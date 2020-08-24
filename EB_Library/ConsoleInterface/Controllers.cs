using ConsoleGamePlayer.Serialization;
using HomeMadeEngine;
using HomeMadeEngine.Math;
using HomeMadeEngine.Templates;
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
        private void __TestMenu__()
        {
            ConsoleGamePlayer.MainGrid.ResetCharacter();
            List<HmVector> walls = new List<HmVector>
            {
                new HmVector(2,2,1),
                new HmVector(3,2,1),
                new HmVector(4,2,1),
                new HmVector(4,3,1),
                new HmVector(4,4,1),
                new HmVector(4,5,1),
                new HmVector(5,5,1),
                new HmVector(5,6,1),
                new HmVector(5,7,1)
            };
            var path = ConsoleGamePlayer.MainGrid.Pathfinder(Save.Player.Position, new HmVector(6, 9, 1));
            if (path.Count != 0)
                foreach (var vector in path)
                    ConsoleGamePlayer.MainGrid.ChangeSpot(vector, SpaceTaker.Enemy);
            //foreach(var wall in walls)
            //    ConsoleGamePlayer.MainGrid.ChangeSpot(wall, SpaceTaker.Object);
            ConsoleGamePlayer.MainGrid.ChangeSpot(Save.Player.Position, SpaceTaker.Player);
            for (int z = 0; ConsoleGamePlayer.MainGrid.Z > z; z++)
            {
                for (int y = 0; ConsoleGamePlayer.MainGrid.Y > y; y++)
                {
                    for (int x = 0; ConsoleGamePlayer.MainGrid.X > x; x++)
                    {
                        switch (ConsoleGamePlayer.MainGrid.Space[x][y][z].Type)
                        {
                            case SpaceTaker.Nothing: Write("[ ]"); break;
                            case SpaceTaker.Player: Write(" O "); break;
                            case SpaceTaker.Enemy: Write(" X "); break;
                            case SpaceTaker.Allies: Write(" + "); break;
                            case SpaceTaker.Object: Write(" = "); break;
                        }

                    }
                    WriteLine();
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------
        // COMMUN MENU
        //____________________________________________________________________________________________________________
        public bool MainMenu()
        {
            __InterfaceCenter__();
            return __MainChoice__();
        }
        private bool __MainChoice__()
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
                case 2: Save.Config.MenuChanging(InterfaceEnum.InventoryMenu); break;
                case 3: break;
                case 4: Save.Config.MenuChanging(InterfaceEnum.MainMenu); break;
                default: throw new ArgumentException("CombatMainMenu choice not handled");
            }
            Save.Config.ResetPos();
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
            Save.Config.ResetPos();
            Save.Config.MenuChanging(InterfaceEnum.CombatMenu);
        }
        //------------------------------------------------------------------------------------------------------------
        // INVENTORY CONTROLLER
        //____________________________________________________________________________________________________________

        private void __InventoryMenu__()
        {
            if (Save.Player.Inventory.Count > Save.Config.Position)
            {
                
            }
        }

        //------------------------------------------------------------------------------------------------------------
        // MAIN MENU CONTROLLER
        //____________________________________________________________________________________________________________
        private bool __MainMenuSwaper__()
        {
            switch (Save.Config.Position)
            {
                case 0: Save.Config.MenuChanging(InterfaceEnum.CombatMenu); new Interface().CombatMenu(); break;
                case 1: Save.Config.MenuChanging(InterfaceEnum.CombatMenu); new Interface().CombatMenu(); break;
                case 2: return true;
                default: throw new ArgumentException("CombatMainMenu choice not handled");
            }
            Save.Config.ResetPos();
            return false;
        }
    }
}
