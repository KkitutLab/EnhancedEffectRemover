using ADOFAI;
using System.Collections.Generic;

namespace EnhancedEffectRemover.Remover
{
    public static class SoundRemover
    {
        public static void RemoveHitSounds(List<LevelEventType> events)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.PlaySound,
                LevelEventType.SetHitsound
            });
        }
        public static void RemoveHoldSounds(List<LevelEventType> events)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.SetHoldSound
            });
        }
    }
}
