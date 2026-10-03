using ADOFAI;

namespace EnhancedEffectRemover.Features;

public static class FrameRateStripper {
    public static void Strip(List<LevelEventType> doomed) {
        doomed.Add(LevelEventType.SetFrameRate);
    }
}
