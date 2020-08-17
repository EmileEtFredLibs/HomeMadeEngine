﻿using ConsoleGamePlayer.ConsoleInterface;
using HomeMadeEngine.Character;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace ConsoleGamePlayer
{
    public class Save
    {
        //------------------------------------------------------------------------------------------------------------
        // CONST FIELDS
        //____________________________________________________________________________________________________________
        public const string SaveConfig = "Config.txt";
        public const string SavePlayer = "Player.txt";

        //------------------------------------------------------------------------------------------------------------
        // STATIC FIELDS
        //____________________________________________________________________________________________________________
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public static CharacterTemplate Player;
        public static Config Config;
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        //------------------------------------------------------------------------------------------------------------
        // SAVE FUNCTIONS
        //____________________________________________________________________________________________________________
        public void SaveBin()
        {
            int nbSave = 0;
            try 
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                FileStream stream = new FileStream(SavePlayer, FileMode.Create, FileAccess.Write);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, Player);
                Console.WriteLine("SAVE PLAYER SUCCESFUL");
                nbSave++;
                stream.Close();
                stream = new FileStream(SaveConfig, FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, Config);
                Console.WriteLine("SAVE CONFIG SUCCESFUL");
                stream.Close();
                Console.BackgroundColor = ConsoleColor.Black;
            }
            catch
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                if (nbSave<2)
                    Console.WriteLine("SAVE CONFIG FAILED");
                else if (nbSave<1)
                    Console.WriteLine("SAVE PLAYER FAILED");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ReadKey();
            }
        }

        //------------------------------------------------------------------------------------------------------------
        // LOAD FUNCTIONS
        //____________________________________________________________________________________________________________
        public void LoadBin()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                FileStream stream = new FileStream(SavePlayer, FileMode.Open, FileAccess.Read);
                Player = (CharacterTemplate)formatter.Deserialize(stream);
                Console.WriteLine("LOAD PLAYER SUCCESFULL");
                stream.Close();
                stream = new FileStream(SaveConfig, FileMode.Open, FileAccess.Read);
                Config = (Config)formatter.Deserialize(stream);
                stream.Close();
                Console.WriteLine("LOAD CONFIG SUCCESFULL");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ReadKey();
            }
            catch
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                if (Player == null)
                {
                    Player = new CharacterTemplate(50, 90, 12, 2, HomeMadeEngine.SpellCost.Energy, 12, 100, false, 4, 5, 0, 0, 0, 0);
                    Console.WriteLine("LOAD PLAYER FAILED");
                }
                if (Config == null)
                {
                    Config = new Config(InterfaceEnum.MainMenu);
                    Console.WriteLine("LOAD CONFIG FAILED");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ReadKey();
            }
            
        }
    }
}
