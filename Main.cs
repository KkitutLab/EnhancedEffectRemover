using HarmonyLib;
using UnityModManagerNet;
using System.Reflection;
using System.IO;

namespace EnhancedEffectRemover
{
    public class Main
    {
        public static UnityModManager.ModEntry.ModLogger Logger;
        public static Harmony harmony;

        public static Settings settings = new();
        public static void StartUp(UnityModManager.ModEntry modEntry)
        {
            Logger = modEntry.Logger;

            settings.setiingFilePath = Path.Combine(modEntry.Path, "Settings.json");
            settings.Load();

            modEntry.OnToggle = OnToggle;
            modEntry.OnGUI = OnGUI;
            modEntry.OnSaveGUI = OnSaveGUI;
        }
        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool isToggled)
        {
            if (isToggled)
            {
                Harmony harmony = new(modEntry.Info.Id);
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            else
            {
                harmony.UnpatchAll(modEntry.Info.Id);
            }

            return true;
        }
        private static void OnGUI(UnityModManager.ModEntry modEntry) => settings.LoadGUI();
        private static void OnSaveGUI(UnityModManager.ModEntry modEntry) => settings.Save();
    }
}