using ADOFAI;
using System;
using System.Collections.Generic;

namespace EnhancedEffectRemover.Remover
{
    public static class FilterRemover
    {
        public static void RemoveFilter(List<LevelEventType> events)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.Flash,
                LevelEventType.SetFilter,
                LevelEventType.HallOfMirrors,
                LevelEventType.ShakeScreen,
                LevelEventType.Bloom,
                LevelEventType.ScreenTile,
                LevelEventType.ScreenScroll
            });
        }

        public static void RemoveAdvFilter(List<LevelEventType> events)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.SetFilterAdvanced
            });
        }
    }
}

