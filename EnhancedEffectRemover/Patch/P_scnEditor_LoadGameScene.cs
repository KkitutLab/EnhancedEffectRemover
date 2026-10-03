using EnhancedEffectRemover.Config;

namespace EnhancedEffectRemover.Patch;

internal static class P_scnEditor_LoadGameScene {
    public static void Postfix(scnEditor __instance) {
        SaveToggle.Apply(__instance, Settings.Instance.EnableSave);
        Settings.Instance.Save();
    }
}
