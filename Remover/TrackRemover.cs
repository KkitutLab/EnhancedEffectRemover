using ADOFAI;
using System;
using System.Collections.Generic;

namespace EnhancedEffectRemover
{
    public static class TrackRemover
    {
        public static void RemoveTrackAnimations(List<LevelEventType> events, LevelData levelData, Settings Settings)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.AnimateTrack
            });

            if (Settings.SetTrackAnimationToDefault)
            {
                levelData.trackSettings = new LevelEvent(0, LevelEventType.TrackSettings, GCS.settingsInfo["TrackSettings"]);
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

        public static void RemoveTrackColors(List<LevelEventType> events)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.ColorTrack,
                LevelEventType.RecolorTrack
            });
        }
    }
}
