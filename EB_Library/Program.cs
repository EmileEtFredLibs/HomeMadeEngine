using System;
using System.Numerics;
using ConsoleGamePlayer.ConsoleInterface;
using HomeMadeEngine;
using HomeMadeEngine.Character;

namespace ConsoleGamePlayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Save().LoadData();
            try
            {
                Save.Config.Version = "v0.0.0.5b";
                new ConsoleGamePlayer().Setup();
            }
            finally
            {
                new Save().SaveData();
            }
        }
    }
}
