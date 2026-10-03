using EnhancedEffectRemover.Config;

namespace EnhancedEffectRemover.Patch;

internal static class P_SaveLevelEditorAction_Execute {
    public static bool Prefix() {
        return !Settings.Instance.Enabled || Settings.Instance.EnableSave;
    }
}
