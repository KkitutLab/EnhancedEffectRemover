using System;
using ADOFAI;


namespace EnhancedEffectRemover
{
    public class Remover
    {
        public static void Remove(LevelData __instance)
        {
            if (!Main.isEnabled) return;

            if (Setting.removeDecos)
            {
                __instance.decorations.Clear();
                __instance.decorationSettings.data.Clear();
            }
            if (Setting.removeFilters)
            {
                if (Setting.removeDecos)
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
            if (Setting.removeBackgrounds)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.CustomBackground
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);
                __instance.backgroundSettings = new LevelEvent(0, LevelEventType.BackgroundSettings, GCS.settingsInfo["BackgroundSettings"]);
                __instance.miscSettings["bgVideo"] = "";
            }
            if (Setting.removeCameras)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.MoveCamera
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);

                if (Setting.setCameratoDefault)
                {
                    float zoom = 275;

                    __instance.cameraSettings = new LevelEvent(0, LevelEventType.CameraSettings, GCS.settingsInfo["CameraSettings"]);
                    __instance.cameraSettings["zoom"] = zoom;
                }
            }
            if (Setting.removeTracks && Setting.removeTrackColors && Setting.removeTrackAnimations)
            {
                __instance.trackSettings = new LevelEvent(0, LevelEventType.TrackSettings, GCS.settingsInfo["TrackSettings"]);
            }
            if (Setting.removeTracks && Setting.removeTrackAnimations)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.AnimateTrack,
                    LevelEventType.PositionTrack,
                    LevelEventType.MoveTrack,
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);

                if (Setting.setTrackAnimationtoDefault)
                {
                    __instance.trackSettings["trackAppearAnimation"] = TrackAnimationType.Fade;
                    __instance.trackSettings["trackDisappearAnimation"] = TrackAnimationType.Fade;
                    __instance.trackSettings["beatsAhead"] = (float)8;
                    __instance.trackSettings["beatsBehind"] = (float)0;
                }
            }
            if (Setting.removeTracks && Setting.removeTrackColors)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.ColorTrack,
                    LevelEventType.RecolorTrack
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);

                if (Setting.setTrackColortoDefault)
                {
                    __instance.trackSettings["trackStyle"] = TrackStyle.Standard;
                    __instance.trackSettings["trackColor"] = "debb7bff";
                    __instance.trackSettings["trackColorType"] = TrackColorType.Single;
                }
            }
        }
    }
}
