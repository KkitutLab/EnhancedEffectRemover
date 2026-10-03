using ADOFAI;

namespace EnhancedEffectRemover.Features;

public static class PlanetStripper {
    public static void StripOrbit(List<LevelEventType> doomed) {
        doomed.Add(LevelEventType.SetPlanetRotation);
    }

    public static void StripScale(List<LevelEventType> doomed) {
        doomed.Add(LevelEventType.ScalePlanets);
    }

    public static void StripRadius(List<LevelEventType> doomed) {
        doomed.Add(LevelEventType.ScaleRadius);
    }
}
