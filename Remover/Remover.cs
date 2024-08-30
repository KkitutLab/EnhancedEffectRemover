using ADOFAI;
using System.Collections.Generic;

namespace EnhancedEffectRemover
{
    public class Remover
    {
        private static Settings Settings => Settings.Instance;
        public static void Remove(LevelData __instance)
        {
            List<LevelEventType> events = new();

            if (Settings.Decorations) DecoRemover.Remove(events, __instance);
            if (Settings.Filters) FilterRemover.RemoveFilter(events);
            if (Settings.AdvFilters) FilterRemover.RemoveAdvFilter(events);
            if (Settings.Particles) ParticleRemover.Remove(events);
            if (Settings.Backgrounds) BGRemover.Remove(events, __instance);
            if (Settings.Cameras) CamRemover.Remove(events, __instance, Settings);
            if (Settings.PlanetOrbit) PlanetEventRemover.RemovePlanetOrbit(events);
            if (Settings.PlanetScale) PlanetEventRemover.RemovePlanetScale(events);
            if (Settings.PlanetRadius) PlanetEventRemover.RemovePlanetRadius(events);
            if (Settings.RepeatEvents) RepeatRemover.Remove(events);
            if (Settings.FrameRate) FrameRateRemover.Remove(events);
            if (Settings.HitSounds) SoundRemover.RemoveHitSounds(events);
            if (Settings.HoldSounds) SoundRemover.RemoveHoldSounds(events);
            if (Settings.TrackAnimations) TrackRemover.RemoveTrackAnimations(events, __instance, Settings);
            if (Settings.TrackPos) TrackRemover.RemoveTrackPositions(events);
            if (Settings.TrackMove) TrackRemover.RemoveTrackMoves(events);
            if (Settings.TrackColors) TrackRemover.RemoveTrackColors(events, __instance, Settings);
            if (Settings.HideIcons) HideRemover.Remove(events);
            if (Settings.CheckPoints) Misc.RemoveCheckPoints(events);
            if (Settings.ResetTrackOpacity) Misc.ResetTrackOpacity(__instance);

            HashSet<LevelEventType> eventSet = new(events);
            __instance.levelEvents.RemoveAll(data => eventSet.Contains(data.eventType));
        }
    }
}
