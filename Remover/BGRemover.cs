using ADOFAI;
using System;
using System.Collections.Generic;

namespace EnhancedEffectRemover
{
    public static class BGRemover
    {
        public static void Remove(List<LevelEventType> events, LevelData levelData)
        {
            events.AddRange(new List<LevelEventType>
            {
                LevelEventType.CustomBackground
            });

            levelData.backgroundSettings = new LevelEvent(0, LevelEventType.BackgroundSettings, GCS.settingsInfo["BackgroundSettings"]);
            levelData.miscSettings["bgVideo"] = "";
        }
    }
}
