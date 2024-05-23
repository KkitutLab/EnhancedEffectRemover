using HarmonyLib;
using UnityModManagerNet;
using System.Reflection;
using System.IO;
using UnityEngine.SceneManagement;

namespace EnhancedEffectRemover
{
    public class Main
    {
        public static UnityModManager.ModEntry.ModLogger Logger;

        public static Settings settings = new();
        public static string filePath;
        public static void StartUp(UnityModManager.ModEntry modEntry)
        {
            Logger = modEntry.Logger;

            filePath = Path.Combine(modEntry.Path, "Settings.json");
            settings.Load();

            modEntry.OnToggle = OnToggle;
            modEntry.OnGUI = OnGUI;
            modEntry.OnSaveGUI = OnSaveGUI;
        }
        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool isToggled)
        {
            Harmony harmony = new(modEntry.Info.Id);

            if (isToggled)
            {
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            else
            {
                harmony.UnpatchAll(modEntry.Info.Id);

                if (SceneManager.GetActiveScene().name == "scnEditor")
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }

            return true;
        }
        private static void OnGUI(UnityModManager.ModEntry modEntry) => settings.LoadGUI();
        private static void OnSaveGUI(UnityModManager.ModEntry modEntry) => settings.Save();
    }
}