using ADOFAI;
using System;
using System.Collections.Generic;

namespace EnhancedEffectRemover
{
    public static class FilterRemover
    {
        public static void Remove(List<LevelEventType> events)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.Flash,
                LevelEventType.SetFilter,
                LevelEventType.HallOfMirrors,
                LevelEventType.ShakeScreen,
                LevelEventType.Bloom,
                LevelEventType.ScreenTile,
                LevelEventType.ScreenScroll,
            });
        }
    }
}

