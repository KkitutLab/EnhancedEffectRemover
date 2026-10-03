using ADOFAI;
using EnhancedEffectRemover.Config;

namespace EnhancedEffectRemover.Features;

public static class EffectStripper {
    public static void Strip(LevelData level) {
        Settings settings = Settings.Instance;
        List<LevelEventType> doomed = [];

        if (settings.Decorations) DecorationStripper.Strip(doomed, level, settings);
        if (settings.Filters) FilterStripper.Strip(doomed);
        if (settings.AdvFilters) FilterStripper.StripAdvanced(doomed);
        if (settings.Particles) ParticleStripper.Strip(doomed);
        if (settings.Backgrounds) BackgroundStripper.Strip(doomed, level);
        if (settings.Cameras) CameraStripper.Strip(doomed, level, settings);
        if (settings.PlanetOrbit) PlanetStripper.StripOrbit(doomed);
        if (settings.PlanetScale) PlanetStripper.StripScale(doomed);
        if (settings.PlanetRadius) PlanetStripper.StripRadius(doomed);
        if (settings.RepeatEvents) RepeatStripper.Strip(doomed);
        if (settings.FrameRate) FrameRateStripper.Strip(doomed);
        if (settings.HitSounds) SoundStripper.StripHitSounds(doomed);
        if (settings.HoldSounds) SoundStripper.StripHoldSounds(doomed);
        if (settings.TrackAnimations) TrackStripper.StripAnimations(doomed, level, settings);
        if (settings.TrackPos) TrackStripper.StripPositions(doomed);
        if (settings.TrackMove) TrackStripper.StripMoves(doomed);
        if (settings.TrackColors) TrackStripper.StripColors(doomed, level, settings);
        if (settings.HideIcons) HideStripper.Strip(doomed);
        if (settings.DefaultTexts) DefaultTextStripper.Strip(doomed);
        if (settings.CheckPoints) CheckpointStripper.Strip(doomed);
        if (settings.ResetTrackOpacity) TrackStripper.ResetOpacity(level);

        HashSet<LevelEventType> doomedSet = [..doomed];
        level.levelEvents.RemoveAll(e => doomedSet.Contains(e.eventType));
    }
}
