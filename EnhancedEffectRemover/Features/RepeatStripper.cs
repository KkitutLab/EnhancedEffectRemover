using ADOFAI;

namespace EnhancedEffectRemover.Features;

public static class RepeatStripper {
    public static void Strip(List<LevelEventType> doomed) {
        doomed.Add(LevelEventType.RepeatEvents);
    }
}
