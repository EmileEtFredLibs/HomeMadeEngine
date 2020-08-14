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
            new SaveAndLoad().Load(out CharacterTemplate player);
            Config config = new Config(InterfaceEnum.MainMenu);
            try
            {
                config.Version = "v0.0.0.4a";
                new ConsoleGamePlayer().Setup(player, config);
            }
            finally
            {
                //new SaveAndLoad().Save(player);
            }
        }
    }
}
