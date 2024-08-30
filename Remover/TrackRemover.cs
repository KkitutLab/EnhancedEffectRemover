using ADOFAI;
using System;
using System.Collections.Generic;

namespace EnhancedEffectRemover
{
    public static class TrackRemover
    {
        public static void RemoveTrackAnimations(List<LevelEventType> events, LevelData __instance, Settings Settings)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.AnimateTrack
            });

            if (Settings.SetTrackAnimationToDefault)
            {
                __instance.trackSettings["trackAppearAnimation"] = TrackAnimationType.Fade;
                __instance.trackSettings["trackDisappearAnimation"] = TrackAnimationType.Fade;
                __instance.trackSettings["beatsAhead"] = 8.0f;
                __instance.trackSettings["beatsBehind"] = 0.0f;
            }
        }

        public static void RemoveTrackPositions(List<LevelEventType> events)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.PositionTrack
            });
        }

        public static void RemoveTrackMoves(List<LevelEventType> events)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.MoveTrack
            });
        }

        public static void RemoveTrackColors(List<LevelEventType> events, LevelData __instance, Settings Settings)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.ColorTrack,
                LevelEventType.RecolorTrack
            });

            if (Settings.SetTrackColorToDefault)
            {
                __instance.trackSettings["trackStyle"] = TrackStyle.Standard;
                __instance.trackSettings["trackColor"] = "debb7bff";
                __instance.trackSettings["trackColorType"] = TrackColorType.Single;
            }
        }
    }
}
