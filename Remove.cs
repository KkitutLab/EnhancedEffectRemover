using System;
using ADOFAI;


namespace EnhancedEffectRemover
{
    public class Remover
    {
        public static void Remove(LevelData __instance)
        {
            if (Settings.removeDecos)
            {
                __instance.decorations.Clear();
                __instance.decorationSettings.data.Clear();
            }
            if (Settings.removeFilters)
            {
                if (Settings.removeDecos)
                {
                    LevelEventType[] typesToRemove =
                    {
                        LevelEventType.Flash,
                        LevelEventType.SetFilter,
                        LevelEventType.HallOfMirrors,
                        LevelEventType.ShakeScreen,
                        LevelEventType.Bloom,
                        LevelEventType.ScreenTile,
                        LevelEventType.ScreenScroll,
                        LevelEventType.DecorationSettings,
                        LevelEventType.AddDecoration,
                        LevelEventType.MoveDecorations
                    };

                    __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);
                }
                else
                {
                    LevelEventType[] typesToRemove =
                    {
                        LevelEventType.Flash,
                        LevelEventType.SetFilter,
                        LevelEventType.HallOfMirrors,
                        LevelEventType.ShakeScreen,
                        LevelEventType.Bloom,
                        LevelEventType.ScreenTile,
                        LevelEventType.ScreenScroll,
                        LevelEventType.ShakeScreen
                    };

                    __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);
                }
            }
            if (Settings.removeBackgrounds)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.CustomBackground
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);
                __instance.backgroundSettings = new LevelEvent(0, LevelEventType.BackgroundSettings, GCS.settingsInfo["BackgroundSettings"]);
                __instance.miscSettings["bgVideo"] = "";
            }
            if (Settings.removeCameras)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.MoveCamera
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);

                if (Settings.setCameratoDefault)
                {
                    float zoom = 275;

                    __instance.cameraSettings = new LevelEvent(0, LevelEventType.CameraSettings, GCS.settingsInfo["CameraSettings"]);
                    __instance.cameraSettings["zoom"] = zoom;
                }
            }
            if (Settings.removeTracks && Settings.removeTrackColors && Settings.removeTrackAnimations)
            {
                __instance.trackSettings = new LevelEvent(0, LevelEventType.TrackSettings, GCS.settingsInfo["TrackSettings"]);
            }
            if (Settings.removeTracks && Settings.removeTrackAnimations)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.AnimateTrack,
                    LevelEventType.PositionTrack,
                    LevelEventType.MoveTrack,
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);

                if (Settings.setTrackAnimationtoDefault)
                {
                    __instance.trackSettings["trackAppearAnimation"] = TrackAnimationType.Fade;
                    __instance.trackSettings["trackDisappearAnimation"] = TrackAnimationType.Fade;
                    __instance.trackSettings["beatsAhead"] = (float)8;
                    __instance.trackSettings["beatsBehind"] = (float)0;
                }
            }
            if (Settings.removeTracks && Settings.removeTrackColors)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.ColorTrack,
                    LevelEventType.RecolorTrack
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);

                if (Settings.setTrackColortoDefault)
                {
                    __instance.trackSettings["trackStyle"] = TrackStyle.Standard;
                    __instance.trackSettings["trackColor"] = "debb7bff";
                    __instance.trackSettings["trackColorType"] = TrackColorType.Single;
                }
            }
        }
    }
}
