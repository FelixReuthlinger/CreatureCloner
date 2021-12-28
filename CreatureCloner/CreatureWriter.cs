using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BepInEx;
using CreatureCloner.Model;
using Jotunn.Entities;
using Jotunn.Managers;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Logger = Jotunn.Logger;

namespace CreatureCloner {
    public static class CreatureWriter {
        private static readonly string DefaultFileName = $"{CreatureClonerPlugin.PluginGuid}.defaults.yaml";
        private static readonly string DefaultFile = Path.Combine(Paths.ConfigPath, DefaultFileName);
        private const string InvalidObjectRegex = @"\([0-9]+\)";
        private const string CloneString = "(Clone)";
        private const string PlayerString = "Player";

        public static void Run() {
            WriteAll(GetAllSpawners(), DefaultFile);
        }

        private static Dictionary<string, CreatureModel> GetAllSpawners() {
            return PrefabManager.Cache
                .GetPrefabs(typeof(Humanoid))
                .Where(kv => !kv.Key.Contains(PlayerString))
                .Where(kv => !kv.Key.Contains(CloneString))
                .Where(kv => !Regex.IsMatch(kv.Key, InvalidObjectRegex))
                .ToDictionary(
                    pair => pair.Key,
                    pair => {
                        Logger.LogInfo($"serializing creature '{pair.Key}'");
                        GameObject creaturePrefab = PrefabManager.Instance.GetPrefab(pair.Key);
                        return CreatureModel.ReadFromGameObject(creaturePrefab);
                    });
        }

        private static void WriteAll(Dictionary<string, CreatureModel> spawners, string file) {
            Logger.LogInfo($"Writing YAML default contents to file '{file}'");
            var yamlContent = new SerializerBuilder()
                .DisableAliases()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build()
                .Serialize(spawners);
            File.WriteAllText(file, yamlContent);
        }
    }

    public class CreatureWriterController : ConsoleCommand {
        public override void Run(string[] args) {
            CreatureWriter.Run();
        }

        public override string Name => "creature_cloner_write_defaults_to_file";
        public override string Help => "Write all creature information to a YAML file inside the BepInEx config folder.";
    }
}