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
        public static string filePath;
        public static void StartUp(UnityModManager.ModEntry modEntry)
        {
            Logger = modEntry.Logger;

            filePath = Path.Combine(modEntry.Path, "Settings.json");
            Settings.Instance.Load();

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
                    SceneManager.LoadScene("scnEditor");
                }
            }

            return true;
        }
        private static void OnGUI(UnityModManager.ModEntry modEntry) => GUI.LoadGUI();
        private static void OnSaveGUI(UnityModManager.ModEntry modEntry) => Settings.Instance.Save();
    }
}