using ADOFAI;

namespace EnhancedEffectRemover.Features;

public static class BackgroundStripper {
    public static void Strip(List<LevelEventType> doomed, LevelData level) {
        doomed.Add(LevelEventType.CustomBackground);

        level.backgroundSettings = new(0, LevelEventType.BackgroundSettings, GCS.settingsInfo["BackgroundSettings"]);
        level.miscSettings["bgVideo"] = "";
    }
}
