using ADOFAI;

namespace EnhancedEffectRemover.Features;

public static class FilterStripper {
    public static void Strip(List<LevelEventType> doomed) {
        doomed.Add(LevelEventType.Flash);
        doomed.Add(LevelEventType.SetFilter);
        doomed.Add(LevelEventType.HallOfMirrors);
        doomed.Add(LevelEventType.ShakeScreen);
        doomed.Add(LevelEventType.Bloom);
        doomed.Add(LevelEventType.ScreenTile);
        doomed.Add(LevelEventType.ScreenScroll);
    }

    public static void StripAdvanced(List<LevelEventType> doomed) {
        doomed.Add(LevelEventType.SetFilterAdvanced);
    }
}
