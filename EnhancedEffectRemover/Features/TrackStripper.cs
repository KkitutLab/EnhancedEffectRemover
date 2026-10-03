using ADOFAI;
using EnhancedEffectRemover.Config;

namespace EnhancedEffectRemover.Features;

public static class TrackStripper {
    public static void StripAnimations(List<LevelEventType> doomed, LevelData level, Settings settings) {
        doomed.Add(LevelEventType.AnimateTrack);

        if (settings.ResetTrackAnimation) ResetAnimations(level);
    }

    public static void StripPositions(List<LevelEventType> doomed) {
        doomed.Add(LevelEventType.PositionTrack);
    }

    public static void StripMoves(List<LevelEventType> doomed) {
        doomed.Add(LevelEventType.MoveTrack);
    }

    public static void StripColors(List<LevelEventType> doomed, LevelData level, Settings settings) {
        doomed.Add(LevelEventType.ColorTrack);
        doomed.Add(LevelEventType.RecolorTrack);

        if (settings.ResetTrackColor) ResetColors(level);
    }

    public static void ResetOpacity(LevelData level) {
        foreach (var e in level.levelEvents) {
            if (e.eventType is LevelEventType.MoveTrack or LevelEventType.PositionTrack && e.ContainsKey("opacity")) {
                e["opacity"] = 100.0f;
            }
        }
    }

    private static void ResetAnimations(LevelData level) {
        level.trackSettings["trackAppearAnimation"] = TrackAnimationType.Fade;
        level.trackSettings["trackDisappearAnimation"] = TrackAnimationType.Fade;
        level.trackSettings["beatsAhead"] = 8.0f;
        level.trackSettings["beatsBehind"] = 0.0f;
    }

    private static void ResetColors(LevelData level) {
        level.trackSettings["trackStyle"] = TrackStyle.Standard;
        level.trackSettings["trackColor"] = "debb7bff";
        level.trackSettings["trackColorType"] = TrackColorType.Single;
    }
}
