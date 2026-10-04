using EnhancedEffectRemover.Config;

namespace EnhancedEffectRemover.Patch;

internal static class P_scnEditor_SaveLevel {
    public static bool Prefix() {
        return !Settings.Instance.Enabled || Settings.Instance.EnableSave;
    }
}
