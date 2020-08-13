using ConsoleGamePlayer.ConsoleInterface;
using HomeMadeEngine.Character;
using System.IO;
using System.Text.Json;

namespace ConsoleGamePlayer
{
    //banana
    public class SaveAndLoad
    {
        public const string SaveConfig = "Config.json";
        public const string SavePlayer = "Player.json";
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
        public void Load(out CharacterTemplate p_player, out Config p_config)
        {
            p_config = new Config(InterfaceEnum.Testing);
            p_player = new CharacterTemplate();
        }
    }
}
