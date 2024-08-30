using ADOFAI;
using System.Collections.Generic;

namespace EnhancedEffectRemover
{
    public static class HideRemover
    {
        public static void Remove(List<LevelEventType> events)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.Hide
            });
        }
    }
}
