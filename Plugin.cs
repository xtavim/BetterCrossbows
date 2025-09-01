using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using ServerSync;
using BepInEx.Logging;

namespace BetterCrossbows
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public new static readonly ManualLogSource Logger =
            BepInEx.Logging.Logger.CreateLogSource(PluginInfo.PLUGIN_NAME);

        public static ConfigEntry<bool> crossbowEnabled;
        public static ConfigEntry<int> crossbowLoadingTime;
        public static ConfigEntry<bool> reloadWhileMoving;
        public static ConfigEntry<bool> enableReloadStaminaDrain;

        private static ConfigSync configSync = new(PluginInfo.PLUGIN_GUID)
        {
            DisplayName = PluginInfo.PLUGIN_NAME,
            CurrentVersion = PluginInfo.PLUGIN_VERSION,
            MinimumRequiredVersion = PluginInfo.PLUGIN_VERSION
        };

        private void Awake()
        {
            InitializeConfig();
            InitializeHarmonyPatches();
        }

        private ConfigEntry<T> ConfigSync<T>(string group, string name, T value, ConfigDescription description,
            bool synchronizedSetting = true)
        {
            ConfigDescription configDescription = new ConfigDescription(
                description.Description + (synchronizedSetting ? " [Synced with Server]" : " [Not Synced with Server]"),
                description.AcceptableValues, description.Tags);
            ConfigEntry<T> configEntry = Config.Bind<T>(group, name, value, configDescription);
            configSync.AddConfigEntry<T>(configEntry).SynchronizedConfig = synchronizedSetting;
            return configEntry;
        }

        private void InitializeConfig()
        {
            Config.SaveOnConfigSet = false;

            // Server lock configuration
            var serverConfigLocked = ConfigSync("1 - ServerSync", "Lock Configuration", true,
                new ConfigDescription(
                    "If enabled, the configuration is locked and can be changed by server admins only."));
            configSync.AddLockingConfigEntry(serverConfigLocked);

            //Configs
            crossbowEnabled = ConfigSync("Crossbows", "Crossbow Enabled", true,
                new ConfigDescription("If enabled, the crossbow will be tweaked."));

            crossbowLoadingTime = ConfigSync("Crossbows", "Crossbow Loading Time", 4,
                new ConfigDescription("Crossbow Loading Time in seconds (vanilla is 4 seconds)",
                    new AcceptableValueRange<int>(1, 10)));

            reloadWhileMoving = ConfigSync("Crossbows", "Reload While Moving", true,
                new ConfigDescription("If enabled, the crossbow will be able to reload while moving, jumping, etc."));
                
            enableReloadStaminaDrain = ConfigSync("Crossbows", "Reload Stamina Drain", true,
                new ConfigDescription("If enabled, the crossbow will drain stamina when reloading."));

            Config.SaveOnConfigSet = true;
            Config.Save();
        }

        private static void InitializeHarmonyPatches()
        {
            var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
        }
    }
}