using ADOFAI;
using ADOFAI.Editor.Actions;
using HarmonyLib;
using UnityEngine.SceneManagement;

namespace EnhancedEffectRemover
{
    public class Patcher
    {
        [HarmonyPatch(typeof(LevelData), "Decode")]
        class LevelDecodePatch
        {
            static void Postfix(LevelData __instance)
            {
                Remover.Remove(__instance);
                Settings.Instance.Save();
            }
        }

        [HarmonyPatch(typeof(SaveLevelEditorAction), "Execute")]
        class BlockSave
        {
            static bool Prefix()
            {
                return Settings.Instance.EnableSave;
            }
        }

        [HarmonyPatch(typeof(scnEditor), "LoadGameScene")]
        class SaveOnLoad
        {
            static void Postfix(scnEditor __instance)
            {
                ToggleSave(__instance, Settings.Instance.EnableSave);
                Settings.Instance.Save();
            }
        }

        public static void ToggleSave(scnEditor __instance, bool isSaveEnabled)
        {
            if (SceneManager.GetActiveScene().name != "scnEditor") return;

            __instance.popupUnsavedChangesSave.interactable = isSaveEnabled;
            __instance.buttonSave.interactable = isSaveEnabled;
        }
    }
}