using ConsoleGamePlayer.Serialization;
using HomeMadeEngine;
using HomeMadeEngine.Templates;
using System;
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
            //__CombatMiddlePart__(Save.Player);
            if (Save.Config.Menu == InterfaceEnum.CombatMenu)
                __SelectionMenu__(new string[] { "Actions", "Defend", "Inventory", "Leave"});
            if (Save.Config.Menu == InterfaceEnum.CombatActionMenu)
                __CombatActionBar__();
        }

        // STATUS BAR
        //------------------------------------------------------------------------------------------------------------
        private void __CombatStatusBar__()
        {
            WriteLine("--------------------------------------------------------------"); // 1,90,1
            Console.BackgroundColor = ConsoleColor.Red;
            WriteLine(" {0}/{1} Health", Save.Player.CurrentHp, Save.Player.MaxHp);
            switch (Save.Player.Spellcost)
            {
                case SpellCost.Mana: Console.BackgroundColor = ConsoleColor.Blue; break;
                case SpellCost.Rage: Console.BackgroundColor = ConsoleColor.DarkRed; break;
                case SpellCost.Energy: Console.BackgroundColor = ConsoleColor.DarkYellow; break;
                default: Console.BackgroundColor = ConsoleColor.Black; break;
            }
            if ((int)Save.Player.Spellcost > 1)
                WriteLine(" {0}/{1} {2}", Save.Player.CurrentRessource, Save.Player.MaxRessource, Save.Player.Spellcost);
            else
                WriteLine("");
            Console.BackgroundColor = ConsoleColor.Black;
            WriteLine("");
            if (Save.Player.Buffs.Count > 0)
                WriteLine(__CombatBuffsBar__(true));
            else
                WriteLine("");
            if (Save.Player.Debuffs.Count > 0)
                WriteLine(__CombatBuffsBar__(false));
            else
                WriteLine("");
            WriteLine("--------------------------------------------------------------");
        }
        private string __CombatBuffsBar__(bool p_buff)
        {
            StringBuilder outty = new StringBuilder();
            if (p_buff)
            {
                for(int i=0; i< Save.Player.Buffs.Count;i++)
                {
                    if (Save.Player.Buffs.Count <= i)
                        outty.Append(" " + Save.Player.Buffs[i].Name.ToString() + ": " + Save.Player.Buffs[i].Timer + " turn" + ", ");
                    else
                        outty.Append(" " + Save.Player.Buffs[i].Name.ToString() + ": " + Save.Player.Buffs[i].Timer + " turn");
                }
            }
            else
            {
                for (int i = 0; i < Save.Player.Debuffs.Count; i++)
                {
                    if (Save.Player.Debuffs.Count <= i)
                        outty.Append(" " + Save.Player.Debuffs[i].Name.ToString() + ": " + Save.Player.Debuffs[i].Timer + " turn" + ", ");
                    else
                        outty.Append(" " + Save.Player.Debuffs[i].Name.ToString() + ": " + Save.Player.Debuffs[i].Timer + " turn");
                }
            }
            return outty.ToString();
        }

        // MIDDLE PART
        //------------------------------------------------------------------------------------------------------------
        private void __CombatMiddlePart__()
        {
            for (int i = 0; i < MiddleLines; i++)
            {
                StringBuilder str = new StringBuilder();
                if (Save.Player.Position.Y == i) {
                    for (int j = 0; j < Save.Player.Position.X; j++)
                    {
                        if (j < Save.Player.Position.X-1)
                            str.Append("  ");
                        else
                            str.Append("X");
                    }
                }
                else
                {
                    str.Append("");
                }
                WriteLine(str.ToString());
            }
            WriteLine("--------------------------------------------------------------");
        }
        private void __Position__()
        {
            
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
                else if (Save.Player.Spellcost > 0)
                {
                    WriteLine("{0,0}{1,-30}{2,-10}: {3,-20}", cur, Save.Player.Actions[i].Name, Save.Player.Spellcost, Save.Player.CostReturner(i));
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
