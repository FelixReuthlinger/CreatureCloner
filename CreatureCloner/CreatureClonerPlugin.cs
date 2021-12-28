using BepInEx;
using Jotunn;
using Jotunn.Managers;

namespace CreatureCloner {
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency(Main.ModGuid)]
    internal class CreatureClonerPlugin : BaseUnityPlugin {
        public const string PluginGuid = "org.bepinex.plugins.creature.cloner";
        public const string PluginName = "CreatureCloner";
        public const string PluginVersion = "0.1.0";

        private void Awake() {
            PrefabManager.OnPrefabsRegistered += CloneCreatures;
            CommandManager.Instance.AddConsoleCommand(new CreatureWriterController());
        }

        private static void CloneCreatures() {
            CreatureReader.LoadConfig();
            PrefabManager.OnPrefabsRegistered -= CloneCreatures;
        }
    }
}