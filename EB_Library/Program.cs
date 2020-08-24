using System;
using System.Numerics;
using ConsoleGamePlayer.ConsoleInterface;
using HomeMadeEngine;
using HomeMadeEngine.Templates;
using ConsoleGamePlayer.Serialization;

namespace ConsoleGamePlayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Save().Loading(Save.SaveType.None);
            try
            {
                new ConsoleGamePlayer().Setup();
            }
            finally
            {
                new Save().Saving(Save.SaveType.All);
            }
        }
    }
}
