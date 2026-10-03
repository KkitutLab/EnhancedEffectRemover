using ADOFAI;

namespace EnhancedEffectRemover.Features;

public static class DefaultTextStripper {
    public static void Strip(List<LevelEventType> doomed) {
        doomed.Add(LevelEventType.SetDefaultText);
    }
}
