using ADOFAI;

namespace EnhancedEffectRemover.Features;

public static class HideStripper {
    public static void Strip(List<LevelEventType> doomed) {
        doomed.Add(LevelEventType.Hide);
    }
}
