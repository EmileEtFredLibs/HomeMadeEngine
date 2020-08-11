using HomeMadeEngine;
using HomeMadeEngine.Character;
using System;
using System.Text;
using static System.Console;

namespace ConsoleGamePlayer.ConsoleInterface
{
    public class Interface
    {
        public void CombatMenu(CharacterTemplate p_player, Config p_config)
        {
            Clear();
            __StatusBar__(p_player);
            __ActionBar__(p_player, p_config);
        }
        private void __StatusBar__(CharacterTemplate p_player)
        {
            WriteLine("--------------------------------------------------------------"); // 1,90,1
            Console.BackgroundColor = ConsoleColor.Red;
            WriteLine(" {0}/{1} Health", p_player.CurrentHp, p_player.MaxHp);
            switch (p_player.Spellcost)
            {
                case SpellCost.Mana: Console.BackgroundColor = ConsoleColor.Blue; break;
                case SpellCost.Rage: Console.BackgroundColor = ConsoleColor.DarkRed; break;
                case SpellCost.Energy: Console.BackgroundColor = ConsoleColor.Yellow; break;
                default: Console.BackgroundColor = ConsoleColor.Black; break;
            }
            if ((int)p_player.Spellcost > 1)
                WriteLine(" {0}/{1} {2}", p_player.CurrentRessource, p_player.MaxRessource, p_player.Spellcost);
            else
                WriteLine("");
            Console.BackgroundColor = ConsoleColor.Black;
            WriteLine("");
            if (p_player.Buffs.Count > 0)
                WriteLine(__BuffsBar__(p_player, true));
            else
                WriteLine("");
            if (p_player.Debuffs.Count > 0)
                WriteLine(__BuffsBar__(p_player, false));
            else
                WriteLine("");
            WriteLine("--------------------------------------------------------------");
        }
        private string __BuffsBar__(CharacterTemplate p_player, bool p_buff)
        {
            StringBuilder outty = new StringBuilder();
            if (p_buff)
            {
                for(int i=0; i< p_player.Buffs.Count;i++)
                {
                    if (p_player.Buffs.Count <= i)
                        outty.Append(" " + p_player.Buffs[i].name.ToString() + ": " + p_player.Buffs[i].timer + " turn" + ", ");
                    else
                        outty.Append(" " + p_player.Buffs[i].name.ToString() + ": " + p_player.Buffs[i].timer + " turn");
                }
            }
            else
            {
                for (int i = 0; i < p_player.Debuffs.Count; i++)
                {
                    if (p_player.Debuffs.Count <= i)
                        outty.Append(" " + p_player.Debuffs[i].name.ToString() + ": " + p_player.Debuffs[i].timer + " turn" + ", ");
                    else
                        outty.Append(" " + p_player.Debuffs[i].name.ToString() + ": " + p_player.Debuffs[i].timer + " turn");
                }
            }
            return outty.ToString();
        }
        private void __ActionBar__(CharacterTemplate p_player, Config p_config)
        {
            p_config.ChangingMax(p_player.Actions.Count-1);
            string cur;
            for (int i = 0; i < p_player.Actions.Count; i++)
            {
                if (i == p_config.Position)
                {
                    cur = "> ";
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                else
                {
                    cur = "  ";
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                WriteLine("{0,0}{1,-30}{2,-10}: {3,-20}", cur, p_player.Actions[i].name, p_player.Spellcost, p_player.Actions[i].cost);
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
