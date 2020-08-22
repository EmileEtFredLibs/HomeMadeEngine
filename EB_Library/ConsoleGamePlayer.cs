using System;
using System.Collections.Generic;
using System.Text;
using ConsoleGamePlayer.ConsoleInterface;
using ConsoleGamePlayer.Serialization;
using HomeMadeEngine;
using HomeMadeEngine.Templates;
using HomeMadeEngine.Math;
using HomeMadeEngine.Actions;
using static System.Console;

namespace ConsoleGamePlayer
{
    public class ConsoleGamePlayer
    {
        public static HmGrid MainGrid = new HmGrid(10, 10, new SpaceTakersTemplate(SpaceTaker.Nothing));
        public static Dictionary<ConsoleKey, int> Options = new Dictionary<ConsoleKey, int>
        {
            [ConsoleKey.Escape] = 0
        };
        public void Setup()
        {
            // CONFIGS
            Save.Config.Version = "v0.0.0.8";
            Save.Config.MenuChanging(InterfaceEnum.CombatMenu);
            Save.Config.ResetPos();

            // PLAYERS
            Save.Player.LearnAction("Cure 1", 2, 0);
            Save.Player.LearnAction("Attack 1", 0, 1);
            Save.Player.LearnAction("Berserker Rage 1", 5, 3);
            Save.Player.LearnAction("Cure 2", 5, 4);
            while (MainFunc());
        }
        public bool MainFunc()
        {
            Clear();
            return !new Controllers().MainMenu();           
        }
    }
}
