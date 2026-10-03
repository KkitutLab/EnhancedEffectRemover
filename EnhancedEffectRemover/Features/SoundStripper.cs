using ADOFAI;

namespace EnhancedEffectRemover.Features;

public static class SoundStripper {
    public static void StripHitSounds(List<LevelEventType> doomed) {
        doomed.Add(LevelEventType.PlaySound);
        doomed.Add(LevelEventType.SetHitsound);
    }

    public static void StripHoldSounds(List<LevelEventType> doomed) {
        doomed.Add(LevelEventType.SetHoldSound);
    }
}
