using ADOFAI;
using System.Collections.Generic;

namespace EnhancedEffectRemover
{
    public class DecoRemover
    {
        public static void Remove(List<LevelEventType> events, LevelData __instance)
        {
            __instance.decorations.Clear();
            __instance.decorationSettings.data.Clear();

            events.AddRange(new List<LevelEventType> {
                LevelEventType.DecorationSettings,
                LevelEventType.AddDecoration,
                LevelEventType.MoveDecorations,
                LevelEventType.AddObject,
                LevelEventType.SetObject
            });
        }
    }
}
