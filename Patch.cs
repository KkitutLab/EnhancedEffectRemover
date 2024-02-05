using ADOFAI;
using ADOFAI.Editor.Actions;
using HarmonyLib;
using UnityEngine.UI;


namespace EnhancedEffectRemover {

    public class RemoveEffects
    {
        [HarmonyPatch(typeof(LevelData), "Decode")]
        class LevelDecodePatch
        {
            static void Postfix(LevelData __instance, ref LoadResult status)
            {
                Remover.Remove(__instance);
                Settings.Save();

                status = LoadResult.Successful;
            }
        }


        [HarmonyPatch(typeof(scnEditor), "OpenLevelCo")]
        class ForceLockOn
        {
            static void Postfix(scnEditor __instance)
            {
                __instance.lockPathEditing = true;
                __instance.lockBackground.color = __instance.shortcutsLockColor;
                __instance.lockIcon.color = __instance.shortcutsLockIconColor;
                __instance.lockIcon.sprite = __instance.lockSpriteOn;
                __instance.floorButtonContainer.SetActive(false);
                __instance.buttonSave.interactable = false;
            }
        }


        [HarmonyPatch(typeof(SaveLevelEditorAction))]
        [HarmonyPatch("Execute")]
        class SaveLevelEditorAction_Execute_Patch
        {
            static bool Prefix()
            {
                return false;
            }
        }


        [HarmonyPatch(typeof(scnEditor), "LockPathEditing")]
        class BlockPathEditToggle
        {
            static bool Prefix()
            {
                return false;
            }
          }
    }
}