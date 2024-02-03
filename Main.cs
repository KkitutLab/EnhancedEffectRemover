using HarmonyLib;
using System.Reflection;
using UnityModManagerNet;


namespace EnhancedEffectRemover
{
    public class Main
    {
        public static UnityModManager.ModEntry.ModLogger Logger;
        public static Harmony harmony;

        public static bool isEnabled = false;

        public static void StartUp(UnityModManager.ModEntry modEntry)
        {
            Logger = modEntry.Logger;

            modEntry.OnToggle = OnToggle;
            modEntry.OnGUI = OnGUI;

            Setting.Load();
        }

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool isToggled)
        {
            isEnabled = isToggled;

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
            Setting.LoadGUI();
        }
    }
}