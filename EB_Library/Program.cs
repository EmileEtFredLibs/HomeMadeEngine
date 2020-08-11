﻿using System;
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
            new SaveAndLoad().Load(out CharacterTemplate player, out Config config);
            try
            {
                new ConsoleGamePlayer().Setup(player, config);
            }
            finally
            {
                new SaveAndLoad().SaveAsync(player, config);
            }
        }
    }
}