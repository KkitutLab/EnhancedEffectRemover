using MelonLoader;

namespace EnhancedEffectRemover;

internal static class Log {
    public static void Msg(string message) {
        MelonLogger.Msg(message);
    }

    public static void Warn(string message) {
        MelonLogger.Warning(message);
    }
}
