using ADOFAI;
using System;
using System.Collections.Generic;

namespace EnhancedEffectRemover
{
    public static class PlanetEventRemover
    {
        public static void RemovePlanetOrbit(List<LevelEventType> events)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.SetPlanetRotation
            });
        }

        public static void RemovePlanetScale(List<LevelEventType> events)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.ScalePlanets
            });
        }

        public static void RemovePlanetRadius(List<LevelEventType> events)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.ScaleRadius
            });
        }
    }
}
