using ConsoleGamePlayer.ConsoleInterface;
using HomeMadeEngine.Character;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleGamePlayer.Serialization
{
    public class Save
    {
        //------------------------------------------------------------------------------------------------------------
        // CONST FIELDS
        //____________________________________________________________________________________________________________
        public const string SaveConfigBin = "Config.txt";
        public const string SaveConfigJson = "Config.json";
        public const string SavePlayerBin = "Player.txt";
        public const string SavePlayerJson = "Player.json";

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
                FileStream stream = new FileStream(SavePlayerBin, FileMode.Create, FileAccess.Write);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, Player);
                Console.WriteLine("SAVE PLAYER SUCCESSFULL");
                nbSave++;
                stream.Close();
                stream = new FileStream(SaveConfigBin, FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, Config);
                Console.WriteLine("SAVE CONFIG SUCCESSFULL");
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
        public void SaveJson()
        {
            int nbSave = 0;
            try
            {
                JsonConvert.DefaultSettings = () =>
                {
                    var settings = new JsonSerializerSettings();
                    settings.Converters.Add(new CharacterTemplateConverterJson());
                    return settings;
                };
                string jsonString = JsonConvert.SerializeObject(Player);
                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new CharacterTemplateConverterJson());
                serializer.NullValueHandling = NullValueHandling.Include;
                using (StreamWriter stream = new StreamWriter(SavePlayerJson))
                using (JsonWriter writer = new JsonTextWriter(stream))
                {
                    serializer.Serialize(writer, jsonString);
                }

            }
            catch
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                if (nbSave < 2)
                    Console.WriteLine("SAVE CONFIG FAILED");
                else if (nbSave < 1)
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
                FileStream stream = new FileStream(SavePlayerBin, FileMode.Open, FileAccess.Read);
                Player = (CharacterTemplate)formatter.Deserialize(stream);
                Console.WriteLine("LOAD PLAYER SUCCESFULL");
                stream.Close();
                stream = new FileStream(SaveConfigBin, FileMode.Open, FileAccess.Read);
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
        public void LoadJson()
        {
            try
            {
                string jsonString = File.ReadAllText(SavePlayerJson);
                Player = JsonConvert.DeserializeObject<CharacterTemplate>(jsonString);
                //jsonString = File.ReadAllText(SaveConfigJson);
                //Config = JsonSerializer.Deserialize<Config>(jsonString);
                Config = new Config();
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
