using HarmonyLib;
using System.Reflection;
using System.IO;
using UnityModManagerNet;


namespace EnhancedEffectRemover
{
    public class Main
    {
        public static UnityModManager.ModEntry.ModLogger Logger;
        public static Harmony harmony;

        public static string settingsPath;
        public static void StartUp(UnityModManager.ModEntry modEntry)
        {
            Logger = modEntry.Logger;

            modEntry.OnToggle = OnToggle;
            modEntry.OnGUI = OnGUI;

            settingsPath = Path.Combine(modEntry.Path, "Settings.json");

            Settings.Load();
        }

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool isToggled)
        {
            if (isToggled)
            {
                harmony = new Harmony(modEntry.Info.Id);
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            else
            {
                harmony.UnpatchAll(modEntry.Info.Id);
            }

            return true;
        }

        private static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            Settings.LoadGUI();
        }
    }
}