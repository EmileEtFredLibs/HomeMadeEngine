using ConsoleGamePlayer.ConsoleInterface;
using HomeMadeEngine.Templates;
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
        public enum SaveType { None=0, Bin=1, Json=2, XML=3 }

        //------------------------------------------------------------------------------------------------------------
        // MAIN FUNCTIONS
        //____________________________________________________________________________________________________________
        public void Saving(SaveType p_type)
        {
            switch (p_type)
            {
                case SaveType.None: break;
                case SaveType.Bin: __SaveBin__(); break;
                case SaveType.Json: __SaveJson__(); break;
                case SaveType.XML: break;
                default:                    
                    __SaveBin__();                   
                    __SaveJson__(); 
                    break;
            }
        }
        public void Loading(SaveType p_type)
        {
            switch (p_type)
            {
                case SaveType.None: break;
                case SaveType.Bin: __LoadBin__(); break;
                case SaveType.Json: __LoadJson__(); break;
                case SaveType.XML: break;
                default:
                    __LoadBin__();
                    __LoadJson__();
                    break;
            }
        }

        //------------------------------------------------------------------------------------------------------------
        // SAVE FUNCTIONS
        //____________________________________________________________________________________________________________
        private void __SaveBin__()
        {
            int nbSave = 0;
            try 
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(SavePlayerBin, FileMode.Create, FileAccess.Write))
                {
                    formatter.Serialize(stream, Player);
                    Console.WriteLine("SAVE PLAYER SUCCESSFULL");
                    nbSave++;
                }
                using (FileStream stream = new FileStream(SaveConfigBin, FileMode.Create, FileAccess.Write))
                {
                    formatter.Serialize(stream, Config);
                    Console.WriteLine("SAVE CONFIG SUCCESSFULL");
                }
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
        private void __SaveJson__()
        {
            int nbSave = 0;
            try
            {
                string jsonString = JsonConvert.SerializeObject(Player);
                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new CharacterTemplateConverterJson());
                serializer.NullValueHandling = NullValueHandling.Include;
                using (StreamWriter stream = new StreamWriter(SavePlayerJson))
                {
                    using (JsonWriter writer = new JsonTextWriter(stream))
                    {
                        serializer.Serialize(writer, jsonString);
                    }
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
        private void __LoadBin__()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                using (FileStream stream = new FileStream(SavePlayerBin, FileMode.Open, FileAccess.Read))
                {
                    Player = (CharacterTemplate)formatter.Deserialize(stream);
                    Console.WriteLine("LOAD PLAYER SUCCESFULL");
                }
                using (FileStream stream = new FileStream(SaveConfigBin, FileMode.Open, FileAccess.Read))
                {
                    Config = (Config)formatter.Deserialize(stream);
                    Console.WriteLine("LOAD CONFIG SUCCESFULL");
                }
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
        private void __LoadJson__()
        {
            try
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                string jsonString = File.ReadAllText(SavePlayerJson);
                Player = JsonConvert.DeserializeObject<CharacterTemplate>(jsonString);
                Console.WriteLine("LOAD PLAYER SUCCESFULL");
                
                //jsonString = File.ReadAllText(SaveConfigJson);
                //Config = JsonConvert.DeserializeObject<Config>(jsonString);
                //Console.WriteLine("LOAD CONFIG SUCCESFULL");
                Config = new Config();

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
