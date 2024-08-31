using ADOFAI;
using System;
using System.Collections.Generic;

namespace EnhancedEffectRemover.Remover
{
    public static class BGRemover
    {
        public static void Remove(List<LevelEventType> events, LevelData __instance)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.CustomBackground
            });

            __instance.backgroundSettings = new LevelEvent(0, LevelEventType.BackgroundSettings, GCS.settingsInfo["BackgroundSettings"]);
            __instance.miscSettings["bgVideo"] = "";
        }
    }
}
