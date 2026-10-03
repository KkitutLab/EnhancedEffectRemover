using System.Reflection;
using MelonLoader;

namespace EnhancedEffectRemover.Patch;

internal static class SafePatch {
    public static bool Apply(HarmonyLib.Harmony harmony, MethodBase? target, HarmonyLib.HarmonyMethod? prefix, HarmonyLib.HarmonyMethod? postfix, string name) {
        if (target == null) {
            Log.Warn($"Skipped {name} (target missing)");
            return false;
        }

        if (prefix == null && postfix == null) {
            Log.Warn($"Skipped {name} (no patch method)");
            return false;
        }

        try {
            harmony.Patch(target, prefix, postfix);
            Log.Msg($"Patched {name}");
            return true;
        } catch (Exception e) {
            Log.Warn($"Skipped {name} ({e.Message})");
            return false;
        }
    }

    public static HarmonyLib.HarmonyMethod? Method(Type type, string name) {
        var method = type.GetMethod(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (method == null) {
            Log.Warn($"Skipped {name} (method missing)");
        }

        return method == null ? null : new HarmonyLib.HarmonyMethod(method);
    }
}
