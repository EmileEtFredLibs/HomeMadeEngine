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
    public class Interface
    {
        //------------------------------------------------------------------------------------------------------------
        // CONSTANTS
        //____________________________________________________________________________________________________________
        public const int MiddleLines = 10;
        public const int BottomMenu = 3;
        public const int MainMiddleMax = 3;

        //------------------------------------------------------------------------------------------------------------
        // MAIN MENU
        //____________________________________________________________________________________________________________
        public void MainMenu()
        {
            Clear();
            __MainUpBar__();
            string[] menu;
            if (Save.Player != null)
                menu = new string[] { "Continue", "Restart", "Quit" };
            else
                menu = new string[] { "Restart", "Quit" };
            __SelectionMenu__(menu);
        }
        private void __MainUpBar__()
        {
            WriteLine("--------------------------------------------------------------");
            WriteLine("ConsoleGame Engine {0}", Save.Config.Version);
            WriteLine("--------------------------------------------------------------");
        }

        //------------------------------------------------------------------------------------------------------------
        // COMBAT MENU
        //____________________________________________________________________________________________________________
        public void CombatMenu()
        {
            Clear();
            __CombatStatusBar__();
            __CombatMiddlePart__();
            if (Save.Config.Menu == InterfaceEnum.CombatMenu)
                __SelectionMenu__(new string[] { "Actions", "Defend", "Move", "Inventory", "Leave"});
            if (Save.Config.Menu == InterfaceEnum.CombatActionMenu)
                __CombatActionBar__();
        }

        // STATUS BAR
        //------------------------------------------------------------------------------------------------------------
        private void __CombatStatusBar__()
        {
            WriteLine("--------------------------------------------------------------"); // 1,90,1
            __RessWriter__();
            WriteLine("");
            __CombatBuffsBar__(PassiveType.Buff);
            __CombatBuffsBar__(PassiveType.Debuff);
            WriteLine("X:{0} Y:{1} Z:{2}", Save.Player.Position.X, Save.Player.Position.Y, Save.Player.Position.Z);
            WriteLine("--------------------------------------------------------------");
        }
        private void __RessWriter__()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            WriteLine(" {0}/{1} Health", Save.Player.CurrentHp, Save.Player.MaxHp);
            switch (Save.Player.RessourceType)
            {
                case RessourceTypes.Mana: Console.BackgroundColor = ConsoleColor.Blue; break;
                case RessourceTypes.Rage: Console.BackgroundColor = ConsoleColor.DarkRed; break;
                case RessourceTypes.Energy: Console.BackgroundColor = ConsoleColor.DarkYellow; break;
                default: Console.BackgroundColor = ConsoleColor.Black; break;
            }
            if ((int)Save.Player.RessourceType > 1)
                WriteLine(" {0}/{1} {2}", Save.Player.CurrentRessource, Save.Player.MaxRessource, Save.Player.RessourceType);
            else
                WriteLine("");
            Console.BackgroundColor = ConsoleColor.Black;
        }
        private void __CombatBuffsBar__(PassiveType p_passiveType)
        {
            StringBuilder outty = new StringBuilder();
            int numb = 0;
            for (int i = 0; i < Save.Player.Passives.Count; i++)
            {
                if (Save.Player.Passives[i].Type == p_passiveType)
                {
                    numb++;
                    int buffTime = Save.Player.Passives[i].FindHighestTimer();
                    if (Save.Player.Passives.Count <= i)
                        outty.Append(" " + Save.Player.Passives[i].Name.ToString() + ": " + buffTime + " turn" + ", ");
                    else
                        outty.Append(" " + Save.Player.Passives[i].Name.ToString() + ": " + buffTime + " turn");
                
                }
            }
            if (numb == 0)
                WriteLine("");
            else
                WriteLine(outty.ToString());
        }

        // MIDDLE PART
        //------------------------------------------------------------------------------------------------------------
        private void __CombatMiddlePart__()
        {
            ConsoleGamePlayer.MainGrid.ResetCharacter();
            var path = ConsoleGamePlayer.MainGrid.Pathfinder(Save.Player.Position, new HmVector(10, 1, 1));
            if (path.Count != 0)
                foreach(var vector in path)
                    ConsoleGamePlayer.MainGrid.ChangeSpot(vector, SpaceTaker.Enemy);
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
            WriteLine("--------------------------------------------------------------");
        }

        // BOTTOM PART
        //------------------------------------------------------------------------------------------------------------
        private void __SelectionMenu__(string[] Menus)
        {
            string cur;
            Save.Config.ChangingMax(Menus.Length - 1);
            for (int i = 0; i < Menus.Length; i++)
            {
                if (i == Save.Config.Position)
                {
                    cur = "> ";
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                else
                {
                    cur = "  ";
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                WriteLine("{0,0}{1,-30}", cur, Menus[i]);
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        private void __CombatActionBar__()
        {
            Save.Config.ChangingMax(Save.Player.Actions.Count);
            string cur;
            for (int i = 0; i <= Save.Player.Actions.Count; i++)
            {
                if (i == Save.Config.Position)
                {
                    cur = "> ";
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                else
                {
                    cur = "  ";
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                if (i == Save.Player.Actions.Count)
                {
                    WriteLine("{0}Return",cur);
                }
                else if (Save.Player.RessourceType > 0)
                {
                    WriteLine("{0,0}{1,-30}{2,-10}: {3,-20}", cur, Save.Player.Actions[i].Name, Save.Player.RessourceType, Save.Player.CostReturner(i));
                }
                else 
                {
                    WriteLine("{0,0}{1,-30}{2,-10}: {3,-20}", cur, Save.Player.Actions[i].Name, " ", " ");
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
