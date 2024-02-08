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

        [HarmonyPatch(typeof(SaveLevelEditorAction), "Execute")]
        class BlockSave
        {
            static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(scnEditor), "LockPathEditing")]
        class BlockPathEdit
        {
            static bool Prefix()
            {
                return false;
            }
          }

        [HarmonyPatch(typeof(scnEditor), "LoadGameScene")]

        class SaveOnLoad
        {
            static void Postfix()
            {
                Main.settings.Save();
            }
        }
    }
}