using ADOFAI;

namespace EnhancedEffectRemover.Features;

public static class CheckpointStripper {
    public static void Strip(List<LevelEventType> doomed) {
        doomed.Add(LevelEventType.Checkpoint);
    }
}
