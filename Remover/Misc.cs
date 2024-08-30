using ADOFAI;
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

        public static void ResetTrackOpacity()
        {

        }
    }
}
