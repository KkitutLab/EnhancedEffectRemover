using ADOFAI;

namespace EnhancedEffectRemover.Features;

public static class ParticleStripper {
    public static void Strip(List<LevelEventType> doomed) {
        doomed.Add(LevelEventType.EmitParticle);
        doomed.Add(LevelEventType.SetParticle);
    }
}
