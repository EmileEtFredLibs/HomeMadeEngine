using ConsoleGamePlayer.ConsoleInterface;
using ConsoleGamePlayer.Serialization.Json;
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
        // FILENAME
        //------------------------------------------------------------------------------------------------------------
        public const string SaveConfig = "Config";
        public const string SavePlayer = "Player";

        // EXTENTION
        //------------------------------------------------------------------------------------------------------------
        public const string Bin = ".txt";
        public const string Json = ".json";
        public const string Xml = ".xml";

        //------------------------------------------------------------------------------------------------------------
        // STATIC FIELDS
        //____________________________________________________________________________________________________________
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public static CharacterTemplate Player;
        public static Config Config;
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public enum SaveType { None=0, All=1, Bin=2, Json=3, XML=4, SQL=5 }

        //------------------------------------------------------------------------------------------------------------
        // MAIN FUNCTIONS
        //____________________________________________________________________________________________________________
        public void Saving(SaveType p_type)
        {
            int nbSave = 0;
            try
            {
                switch (p_type)
                {
                    case SaveType.None: break;
                    case SaveType.All:
                        __SaveBin__(ref nbSave);
                        __SaveJson__(ref nbSave);
                        break;
                    case SaveType.Bin: __SaveBin__(ref nbSave); break;
                    case SaveType.Json: __SaveJson__(ref nbSave); break;
                    case SaveType.XML: break;
                    case SaveType.SQL: break;
                    default:
                        __SaveBin__(ref nbSave);
                        __SaveJson__(ref nbSave);
                        break;
                }
            }
            finally
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
        public void Loading(SaveType p_type)
        {
            try
            {
                switch (p_type)
                {
                    case SaveType.None: break;
                    case SaveType.Bin: __LoadBin__(); break;
                    case SaveType.Json: __LoadJson__(); break;
                    case SaveType.XML: break;
                    case SaveType.SQL: break;
                    default:
                        __LoadBin__();
                        __LoadJson__();
                        break;
                }
            }
            finally
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                if (Player == null)
                {
                    Player = new CharacterTemplate(50, 90, 12, 2, HomeMadeEngine.RessourceTypes.Rage, 12, 100, false, 4, 5, 1, 0, 0, 0);
                    Console.WriteLine("LOAD PLAYER FAILED");
                }
                if (Config == null)
                {
                    Config = new Config(InterfaceEnum.MainMenu);
                    Console.WriteLine("LOAD CONFIG FAILED");
                }
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        //------------------------------------------------------------------------------------------------------------
        // SAVE FUNCTIONS
        //____________________________________________________________________________________________________________
        private void __SaveBin__(ref int p_nb)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(SavePlayer + Bin, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, Player);
                Console.WriteLine("SAVE PLAYER SUCCESSFULL");
                p_nb++;
            }
            using (FileStream stream = new FileStream(SaveConfig + Bin, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, Config);
                Console.WriteLine("SAVE CONFIG SUCCESSFULL");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        private void __SaveJson__(ref int p_nb)
        {
            string jsonString = JsonConvert.SerializeObject(Player);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new CharacterTemplateConverterJson());
            serializer.NullValueHandling = NullValueHandling.Include;
            using (StreamWriter stream = new StreamWriter(SavePlayer+Json))
            {
                using (JsonWriter writer = new JsonTextWriter(stream))
                {
                    serializer.Serialize(writer, jsonString);
                }
                p_nb++;
            }
            
        }

        //------------------------------------------------------------------------------------------------------------
        // LOAD FUNCTIONS
        //____________________________________________________________________________________________________________
        private void __LoadBin__()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            using (FileStream stream = new FileStream(SavePlayer+Bin, FileMode.Open, FileAccess.Read))
            {
                Player = (CharacterTemplate)formatter.Deserialize(stream);
                Console.WriteLine("LOAD PLAYER SUCCESFULL");
            }
            using (FileStream stream = new FileStream(SaveConfig+Bin, FileMode.Open, FileAccess.Read))
            {
                Config = (Config)formatter.Deserialize(stream);
                Console.WriteLine("LOAD CONFIG SUCCESFULL");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        private void __LoadJson__()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            string jsonString = File.ReadAllText(SavePlayer+Json);
            Player = JsonConvert.DeserializeObject<CharacterTemplate>(jsonString);
            Console.WriteLine("LOAD PLAYER SUCCESFULL");
                
            //jsonString = File.ReadAllText(SaveConfig+Json);
            //Config = JsonConvert.DeserializeObject<Config>(jsonString);
            //Console.WriteLine("LOAD CONFIG SUCCESFULL");
            Config = new Config();

            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
