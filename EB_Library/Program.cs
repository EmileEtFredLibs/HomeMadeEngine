using System;
using System.Numerics;
using ConsoleGamePlayer.ConsoleInterface;
using HomeMadeEngine;
using HomeMadeEngine.Character;
using ConsoleGamePlayer.Serialization;

namespace ConsoleGamePlayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Save().LoadBin();
            //new Save().LoadJson();
            try
            {
                Save.Config.Version = "v0.0.0.6a";
                new ConsoleGamePlayer().Setup();
            }
            finally
            {
                new Save().SaveBin();
                //new Save().SaveJson();
            }
        }
    }
}
