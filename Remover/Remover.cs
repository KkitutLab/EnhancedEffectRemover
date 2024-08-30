using ADOFAI;
using System.Collections.Generic;

namespace EnhancedEffectRemover
{
    public class Remover
    {
        private static Settings Settings => Settings.Instance;
        public static void Remove(LevelData levelData)
        {
            List<LevelEventType> events = new();

            if (Settings.Decorations) DecoRemover.Remove(events, levelData);
            if (Settings.Filters) FilterRemover.Remove(events);
            if (Settings.Backgrounds) BGRemover.Remove(events, levelData);
            if (Settings.Cameras) CamRemover.Remove(events, levelData, Settings);
            if (Settings.PlanetOrbit) PlanetEventRemover.RemovePlanetOrbit(events);
            if (Settings.PlanetScale) PlanetEventRemover.RemovePlanetScale(events);
            if (Settings.PlanetRadius) PlanetEventRemover.RemovePlanetRadius(events);
            if (Settings.RepeatEvents) RepeatRemover.Remove(events);
            if (Settings.FrameRate) FrameRateRemover.Remove(events);
            if (Settings.HitSounds) SoundRemover.RemoveHitSounds(events);
            if (Settings.HoldSounds) SoundRemover.RemoveHoldSounds(events);
            if (Settings.TrackAnimations) TrackRemover.RemoveTrackAnimations(events, levelData, Settings);
            if (Settings.TrackPos) TrackRemover.RemoveTrackPositions(events);
            if (Settings.TrackMove) TrackRemover.RemoveTrackMoves(events);
            if (Settings.TrackColors) TrackRemover.RemoveTrackColors(events);
            if (Settings.HideIcons) HideRemover.Remove(events);
            if (Settings.CheckPoints) Misc.RemoveCheckPoints(events);

            HashSet<LevelEventType> eventSet = new(events);
            levelData.levelEvents.RemoveAll(data => eventSet.Contains(data.eventType));
        }
    }
}
