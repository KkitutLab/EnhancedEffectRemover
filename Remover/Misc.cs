using ADOFAI;
using System.Collections.Generic;

namespace EnhancedEffectRemover.Remover
{
    public class Misc
    {
        public static void RemoveCheckPoints(List<LevelEventType> events)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.Checkpoint
            });
        }

        public static void ResetTrackOpacity(LevelData __instance)
        {
            foreach (var eventData in __instance.levelEvents)
            {
                if (eventData.eventType == LevelEventType.MoveTrack || eventData.eventType == LevelEventType.PositionTrack)
                {
                    if (eventData.data.ContainsKey("opacity"))
                    {
                        eventData.data["opacity"] = 100.0f;
                    }
                }
            }
        }

        public static void ResetTrackAnimations(LevelData __instance)
        { 
            __instance.trackSettings["trackAppearAnimation"] = TrackAnimationType.Fade;
            __instance.trackSettings["trackDisappearAnimation"] = TrackAnimationType.Fade;
            __instance.trackSettings["beatsAhead"] = 8.0f;
            __instance.trackSettings["beatsBehind"] = 0.0f;
        }

        public static void ResetTrackColor(LevelData __instance)
        {
            __instance.trackSettings["trackStyle"] = TrackStyle.Standard;
            __instance.trackSettings["trackColor"] = "debb7bff";
            __instance.trackSettings["trackColorType"] = TrackColorType.Single;
        }
    }
}
