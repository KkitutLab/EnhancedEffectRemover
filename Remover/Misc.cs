using ADOFAI;
using System;
using System.Collections.Generic;

namespace EnhancedEffectRemover
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
                if (eventData.eventType == LevelEventType.MoveTrack)
                {
                    if (eventData.data.ContainsKey("opacity"))
                    {
                        if (Convert.ToSingle(eventData.data["opacity"]) > 100.0f)
                        {
                            eventData.data["opacity"] = 100.0f;
                        }
                    }
                }

                if (eventData.eventType == LevelEventType.PositionTrack)
                {
                    if (eventData.data.ContainsKey("opacity"))
                    {
                        if (Convert.ToSingle(eventData.data["opacity"]) > 100.0f)
                        {
                            eventData.data["opacity"] = 100.0f;
                        }
                    }
                }
            }
        }
    }
}
