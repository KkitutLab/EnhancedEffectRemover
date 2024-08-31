using ADOFAI;
using System;
using System.Collections.Generic;

namespace EnhancedEffectRemover.Remover
{
    public static class TrackRemover
    {
        public static void RemoveTrackAnimations(List<LevelEventType> events, LevelData __instance, Settings Settings)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.AnimateTrack
            });

            if (Settings.ResetTrackAnimation)
            {
                Misc.ResetTrackAnimations(__instance);
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

            if (Settings.ResetTrackColor)
            {
                Misc.ResetTrackColor(__instance);
            }
        }
    }
}
