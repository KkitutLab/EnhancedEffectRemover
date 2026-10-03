using ADOFAI;
using EnhancedEffectRemover.Config;
using EnhancedEffectRemover.Features;

namespace EnhancedEffectRemover.Patch;

internal static class P_LevelData_Decode {
    public static void Postfix(LevelData __instance) {
        if (!Settings.Instance.Enabled)
            return;

        EffectStripper.Strip(__instance);
        Settings.Instance.Save();
    }
}
