using ADOFAI;
using System;
using System.Collections.Generic;

namespace EnhancedEffectRemover.Remover
{
    public static class RepeatRemover
    {
        public static void Remove(List<LevelEventType> events)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.RepeatEvents
            });
        }
    }
}
