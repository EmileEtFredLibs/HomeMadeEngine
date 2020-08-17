using HomeMadeEngine;
using HomeMadeEngine.Character;
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
            if (ConsoleGamePlayer.player != null)
                menu = new string[] { "Continue", "Restart", "Quit" };
            else
                menu = new string[] { "Restart", "Quit" };
            __SelectionMenu__(menu);

        }
        private void __MainUpBar__()
        {
            WriteLine("--------------------------------------------------------------");
            WriteLine("ConsoleGame Engine {0}", ConsoleGamePlayer.config.Version);
            WriteLine("--------------------------------------------------------------");
        }

        //------------------------------------------------------------------------------------------------------------
        // COMBAT MENU
        //____________________________________________________________________________________________________________
        public void CombatMenu()
        {
            Clear();
            __CombatStatusBar__();
            //__CombatMiddlePart__(ConsoleGamePlayer.player);
            if (ConsoleGamePlayer.config.Menu == InterfaceEnum.CombatMenu)
                __SelectionMenu__(new string[] { "Actions", "Defend", "Inventory", "Leave"});
            if (ConsoleGamePlayer.config.Menu == InterfaceEnum.CombatActionMenu)
                __CombatActionBar__();
        }

        // STATUS BAR
        //------------------------------------------------------------------------------------------------------------
        private void __CombatStatusBar__()
        {
            WriteLine("--------------------------------------------------------------"); // 1,90,1
            Console.BackgroundColor = ConsoleColor.Red;
            WriteLine(" {0}/{1} Health", ConsoleGamePlayer.player.CurrentHp, ConsoleGamePlayer.player.MaxHp);
            switch (ConsoleGamePlayer.player.Spellcost)
            {
                case SpellCost.Mana: Console.BackgroundColor = ConsoleColor.Blue; break;
                case SpellCost.Rage: Console.BackgroundColor = ConsoleColor.DarkRed; break;
                case SpellCost.Energy: Console.BackgroundColor = ConsoleColor.DarkYellow; break;
                default: Console.BackgroundColor = ConsoleColor.Black; break;
            }
            if ((int)ConsoleGamePlayer.player.Spellcost > 1)
                WriteLine(" {0}/{1} {2}", ConsoleGamePlayer.player.CurrentRessource, ConsoleGamePlayer.player.MaxRessource, ConsoleGamePlayer.player.Spellcost);
            else
                WriteLine("");
            Console.BackgroundColor = ConsoleColor.Black;
            WriteLine("");
            if (ConsoleGamePlayer.player.Buffs.Count > 0)
                WriteLine(__CombatBuffsBar__(true));
            else
                WriteLine("");
            if (ConsoleGamePlayer.player.Debuffs.Count > 0)
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
                for(int i=0; i< ConsoleGamePlayer.player.Buffs.Count;i++)
                {
                    if (ConsoleGamePlayer.player.Buffs.Count <= i)
                        outty.Append(" " + ConsoleGamePlayer.player.Buffs[i].name.ToString() + ": " + ConsoleGamePlayer.player.Buffs[i].timer + " turn" + ", ");
                    else
                        outty.Append(" " + ConsoleGamePlayer.player.Buffs[i].name.ToString() + ": " + ConsoleGamePlayer.player.Buffs[i].timer + " turn");
                }
            }
            else
            {
                for (int i = 0; i < ConsoleGamePlayer.player.Debuffs.Count; i++)
                {
                    if (ConsoleGamePlayer.player.Debuffs.Count <= i)
                        outty.Append(" " + ConsoleGamePlayer.player.Debuffs[i].name.ToString() + ": " + ConsoleGamePlayer.player.Debuffs[i].timer + " turn" + ", ");
                    else
                        outty.Append(" " + ConsoleGamePlayer.player.Debuffs[i].name.ToString() + ": " + ConsoleGamePlayer.player.Debuffs[i].timer + " turn");
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
                if (ConsoleGamePlayer.player.Position.Y == i) {
                    for (int j = 0; j < ConsoleGamePlayer.player.Position.X; j++)
                    {
                        if (j < ConsoleGamePlayer.player.Position.X-1)
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
            ConsoleGamePlayer.config.ChangingMax(Menus.Length - 1);
            for (int i = 0; i < Menus.Length; i++)
            {
                if (i == ConsoleGamePlayer.config.Position)
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
            ConsoleGamePlayer.config.ChangingMax(ConsoleGamePlayer.player.Actions.Count - 1);
            string cur;
            for (int i = 0; i < ConsoleGamePlayer.player.Actions.Count; i++)
            {
                if (i == ConsoleGamePlayer.config.Position)
                {
                    cur = "> ";
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                else
                {
                    cur = "  ";
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                if (ConsoleGamePlayer.player.Spellcost > 0)
                {
                    WriteLine("{0,0}{1,-30}{2,-10}: {3,-20}", cur, ConsoleGamePlayer.player.Actions[i].name, ConsoleGamePlayer.player.Spellcost, ConsoleGamePlayer.player.Actions[i].cost);
                }
                else
                {
                    WriteLine("{0,0}{1,-30}{2,-10}: {3,-20}", cur, ConsoleGamePlayer.player.Actions[i].name, " ", " ");
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
