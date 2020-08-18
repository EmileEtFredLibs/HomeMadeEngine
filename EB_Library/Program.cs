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
            new Save().Loading(Save.SaveType.Bin);
            //new Save().LoadJson();
            try
            {
                Save.Config.Version = "v0.0.0.7";
                new ConsoleGamePlayer().Setup();
            }
            finally
            {
                new Save().Saving(Save.SaveType.Bin);
                //new Save().SaveJson();
            }
        }
    }
}
