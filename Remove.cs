using System;
using ADOFAI;


namespace EnhancedEffectRemover
{
    public class Remover
    {
        public static void Remove(LevelData __instance)
        {
            if (Main.settings.removeDecos)
            {
                __instance.decorations.Clear();
                __instance.decorationSettings.data.Clear();
            }
            if (Main.settings.removeFilters)
            {
                if (Main.settings.removeDecos)
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
            if (Main.settings.removeBackgrounds)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.CustomBackground
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);
                __instance.backgroundSettings = new LevelEvent(0, LevelEventType.BackgroundSettings, GCS.settingsInfo["BackgroundSettings"]);
                __instance.miscSettings["bgVideo"] = "";
            }
            if (Main.settings.removeCameras)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.MoveCamera
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);

                if (Main.settings.setCameratoDefault)
                {
                    float zoom = Main.settings.zoomScale;

                    __instance.cameraSettings = new LevelEvent(0, LevelEventType.CameraSettings, GCS.settingsInfo["CameraSettings"]);
                    __instance.cameraSettings["zoom"] = zoom;
                }
            }
            if (Main.settings.removeTracks && Main.settings.removeTrackColors && Main.settings.removeTrackAnimations)
            {
                __instance.trackSettings = new LevelEvent(0, LevelEventType.TrackSettings, GCS.settingsInfo["TrackSettings"]);
            }
            if (Main.settings.removeTracks && Main.settings.removeTrackAnimations)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.AnimateTrack,
                    LevelEventType.PositionTrack,
                    LevelEventType.MoveTrack,
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);

                if (Main.settings.setTrackAnimationtoDefault)
                {
                    __instance.trackSettings["trackAppearAnimation"] = TrackAnimationType.Fade;
                    __instance.trackSettings["trackDisappearAnimation"] = TrackAnimationType.Fade;
                    __instance.trackSettings["beatsAhead"] = (float)8;
                    __instance.trackSettings["beatsBehind"] = (float)0;
                }
            }
            if (Main.settings.removeTracks && Main.settings.removeTrackColors)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.ColorTrack,
                    LevelEventType.RecolorTrack
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);

                foreach (var eventData in __instance.levelEvents)
                {
                    if (eventData.eventType == LevelEventType.MoveTrack)
                    {
                        if (eventData.data.ContainsKey("opacity"))
                        {
                            if (Convert.ToSingle(eventData.data["opacity"]) > 100.0f)
                            {
                                eventData.data["opacity"] = 100.0f;
                            }
                        }
                    }
                }

                if (Main.settings.setTrackColortoDefault)
                {
                    __instance.trackSettings["trackStyle"] = TrackStyle.Standard;
                    __instance.trackSettings["trackColor"] = "debb7bff";
                    __instance.trackSettings["trackColorType"] = TrackColorType.Single;
                }
            }
        }
    }
} 