using ADOFAI;
using System.Collections.Generic;

namespace EnhancedEffectRemover.Remover
{
    public static class ParticleRemover
    {
        public static void Remove(List<LevelEventType> events)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.EmitParticle,
                LevelEventType.SetParticle
            });
        }
    }
}
