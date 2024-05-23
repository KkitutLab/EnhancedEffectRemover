using ADOFAI;
using ADOFAI.Editor.Actions;
using HarmonyLib;

namespace EnhancedEffectRemover
{
    public class RemoveEffects
    {
        [HarmonyPatch(typeof(LevelData), "Decode")]
        class LevelDecodePatch
        {
            static void Postfix(LevelData __instance)
            {
                Remover.Remove(__instance);
            }
        }

        [HarmonyPatch(typeof(SaveLevelEditorAction), "Execute")]
        class BlockSave
        {
            static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(scnEditor), "LoadGameScene")]
        class SaveOnLoad
        {
            static void Postfix(scnEditor __instance)
            {
                __instance.popupUnsavedChangesSave.interactable = false;
                __instance.buttonSave.interactable = false;

                Main.settings.Save();
            }
        }
    }
}
