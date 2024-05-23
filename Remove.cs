using System;
using ADOFAI;

namespace EnhancedEffectRemover
{
    public class Remover
    {
        public static void Remove(LevelData __instance)
        {
            if (Main.settings.Decorations)
            {
                __instance.decorations.Clear();
                __instance.decorationSettings.data.Clear();
            }
            if (Main.settings.Filters)
            {
                if (Main.settings.Decorations)
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
                        LevelEventType.MoveDecorations,
                        LevelEventType.AddObject,
                        LevelEventType.SetObject,
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
            if (Main.settings.Backgrounds)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.CustomBackground
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);
                __instance.backgroundSettings = new LevelEvent(0, LevelEventType.BackgroundSettings, GCS.settingsInfo["BackgroundSettings"]);
                __instance.miscSettings["bgVideo"] = "";
            }
            if (Main.settings.Cameras)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.MoveCamera
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);

                if (Main.settings.SetCameraZoomScale)
                {
                    float zoom = Main.settings.CameraZoomScale;

                    __instance.cameraSettings = new LevelEvent(0, LevelEventType.CameraSettings, GCS.settingsInfo["CameraSettings"]);
                    __instance.cameraSettings["zoom"] = zoom;
                }
            }
            if (Main.settings.PlanetOrbit)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.SetPlanetRotation,
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);
            }
            if (Main.settings.PlanetScale)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.ScalePlanets,
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);
            }
            if (Main.settings.PlanetRadius)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.ScaleRadius
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);
            }
            if (Main.settings.RepeatEvents)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.RepeatEvents
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);
            }
            if (Main.settings.FrameRate)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.SetFrameRate
                };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);
            }
            if (Main.settings.HitSounds)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.PlaySound,
                    LevelEventType.SetHitsound,
                };
                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);
            }
            if (Main.settings.TrackAnimations && Main.settings.TrackPos && Main.settings.TrackMove && Main.settings.TrackColors)
            {
                __instance.trackSettings = new LevelEvent(0, LevelEventType.TrackSettings, GCS.settingsInfo["TrackSettings"]);
            }
            if (Main.settings.TrackAnimations)
            {
                LevelEventType[] typesToRemove =
                {
                        LevelEventType.AnimateTrack,
                    };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);

                if (Main.settings.SetTrackAnimationToDefault)
                {
                    __instance.trackSettings["trackAppearAnimation"] = TrackAnimationType.Fade;
                    __instance.trackSettings["trackDisappearAnimation"] = TrackAnimationType.Fade;
                    __instance.trackSettings["beatsAhead"] = 8.0f;
                    __instance.trackSettings["beatsBehind"] = 0.0f;
                }
            }
            if (Main.settings.TrackPos)
            {
                LevelEventType[] typesToRemove =
                {
                        LevelEventType.PositionTrack,
                    };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);
            }
            if (Main.settings.TrackMove)
            {
                LevelEventType[] typesToRemove =
                {
                        LevelEventType.MoveTrack,
                    };

                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);
            }
            if (Main.settings.TrackColors)
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

                    if (eventData.eventType == LevelEventType.PositionTrack)
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


                if (Main.settings.SetTrackColorToDefault)
                {
                    __instance.trackSettings["trackStyle"] = TrackStyle.Standard;
                    __instance.trackSettings["trackColor"] = "debb7bff";
                    __instance.trackSettings["trackColorType"] = TrackColorType.Single;
                }
            }
            if (Main.settings.HoldSounds)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.SetHoldSound
                };
                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);
            }
            if (Main.settings.HideIcons)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.Hide
                };
                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);
            }
            if (Main.settings.CheckPoints)
            {
                LevelEventType[] typesToRemove =
                {
                    LevelEventType.Checkpoint
                };
                __instance.levelEvents.RemoveAll(data => Array.IndexOf(typesToRemove, data.eventType) != -1);
            }
        }
    }
} 