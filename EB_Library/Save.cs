using ConsoleGamePlayer.ConsoleInterface;
using HomeMadeEngine.Character;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace ConsoleGamePlayer
{
    public class Save
    {
        public const string SaveConfig = "Config.json";
        public static CharacterTemplate Player = new CharacterTemplate();
        public static Config Config = new Config(InterfaceEnum.MainMenu);
        public const string SavePlayer = "Player.txt";
        public async void SaveAsync(CharacterTemplate p_player, Config p_config)
        {
            string jsonConfig = JsonSerializer.Serialize(p_config);
            File.WriteAllText(SaveConfig, jsonConfig);
            using (FileStream fs = File.Create(SaveConfig))
            {
                await JsonSerializer.SerializeAsync(fs, jsonConfig);

            }
            string jsonPlayer = JsonSerializer.Serialize(p_player);
            File.WriteAllText(SavePlayer, jsonPlayer);
            using (FileStream fs = File.Create(SavePlayer))
            {
                await JsonSerializer.SerializeAsync(fs, jsonPlayer);
            }
            return;
        }
        public void SaveData()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(SavePlayer, FileMode.Create, FileAccess.Write))
                formatter.Serialize(stream, Player);
        }
        public void Load()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                using (FileStream stream = new FileStream(SavePlayer, FileMode.Open, FileAccess.Read))
                    Player = (CharacterTemplate)formatter.Deserialize(stream);
            }
            catch
            {
                Player = new CharacterTemplate(50, 90, 12, 2, HomeMadeEngine.SpellCost.Energy, 12, 100, false, 4, 5, 0, 0, 0, 0);
            }
            
        }
    }
}
