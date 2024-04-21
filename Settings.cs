using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;


namespace EnhancedEffectRemover
{
    public class Settings
    {
        public bool setTrackAnimationtoDefault;
        public bool setCamera;
        public bool setTrackColortoDefault;

        public bool removeFilters;
        public bool removeDecos;
        public bool removeBackgrounds;
        public bool removePlanetEvents;
        public bool removeCameras;
        public bool removeRepeatEvents;

        public bool removeTracks;
        public bool removeTrackEvents;
        public bool removeTrackAnimations;
        public bool removeTrackPos;
        public bool removeTrackMove;
        public bool removeTrackColors;

        public bool removeHoldSounds;
        public bool removeHideIcons;

        public float zoomScale = 250;

        public void LoadGUI()
        {
            GUILayout.BeginVertical();

            GUILayout.Space(10);
            GUILayout.Label("<color=#FF2222><size=20> Level save will <b>NOT</b> work !!</size></color>");
            GUILayout.Space(10);

            GUILayout.Label("Non-DLC Settings");
            removeFilters = GUILayout.Toggle(removeFilters, " Remove Filters");
            removeDecos = GUILayout.Toggle(removeDecos, " Remove Decorations");
            removeBackgrounds = GUILayout.Toggle(removeBackgrounds, " Remove Backgrounds");
            removeCameras = GUILayout.Toggle(removeCameras, " Remove Cameras");
            removePlanetEvents = GUILayout.Toggle(removePlanetEvents, " Remove Planet Events");
            removeRepeatEvents = GUILayout.Toggle(removeRepeatEvents, " Remove Repeat Events");
            removeTracks = GUILayout.Toggle(removeTracks, " Remove Tracks");

            if (removeTracks)
            {
                GUILayout.BeginVertical();

                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                removeTrackEvents = GUILayout.Toggle(removeTrackEvents, " Remove Track Events");
                GUILayout.EndHorizontal();

                if (removeTrackEvents)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(60);
                    removeTrackAnimations = GUILayout.Toggle(removeTrackAnimations, " Remove Animate Track");
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(60);
                    removeTrackMove = GUILayout.Toggle(removeTrackMove, " Remove Move Track");
                    GUILayout.EndHorizontal();
                    
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(60);
                    removeTrackPos = GUILayout.Toggle(removeTrackPos, " Remove Position Track");
                    GUILayout.EndHorizontal();
                }

                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                removeTrackColors = GUILayout.Toggle(removeTrackColors, " Remove Track Colors");
                GUILayout.EndHorizontal();

                GUILayout.EndVertical();
            }

            if (removeCameras || (removeTracks && removeTrackEvents && removeTrackAnimations) || (removeTrackColors && removeTracks))
            {
                GUILayout.BeginVertical();
                GUILayout.Space(20);

                if (removeCameras)
                {
                    setCamera = GUILayout.Toggle(setCamera, " Set Camera Zoom (100 ~ 1000) ");
                    if (setCamera)
                    {
                        GUILayout.BeginHorizontal();

                        zoomScale = GUILayout.HorizontalSlider(zoomScale, 100.0f, 1000.0f, GUILayout.Width(200));

                        string inputZoom = GUILayout.TextField(zoomScale.ToString("F2"));
                        if (float.TryParse(inputZoom, out float parsedZoomScale))
                        {
                            if (parsedZoomScale > 1000 || parsedZoomScale < 100)
                            {
                                zoomScale = 250;
                            }
                            else
                            {
                                zoomScale = parsedZoomScale;
                            }
                        }

                        GUILayout.FlexibleSpace();
                        GUILayout.EndHorizontal();
                    }
                }

                if (removeTracks && removeTrackEvents && removeTrackAnimations) setTrackAnimationtoDefault = GUILayout.Toggle(setTrackAnimationtoDefault, " Set Track Animation to Default");
                if (removeTrackColors) setTrackColortoDefault = GUILayout.Toggle(setTrackColortoDefault, " Set Track Color to Default");

                GUILayout.EndVertical();
            }

            GUILayout.Space(10);
            GUILayout.Label("DLC Settings");

            removeHoldSounds = GUILayout.Toggle(removeHoldSounds, " Remove HoldSounds");
            removeHideIcons = GUILayout.Toggle(removeHideIcons, " Remove HideIcons");

            GUILayout.EndVertical();
        }
        public void Load()
        {
            if (File.Exists(Main.settingsPath))
            {
                try
                {
                    JsonConvert.PopulateObject(File.ReadAllText(Main.settingsPath), this);
                } catch (Exception e)
                {
                    Main.Logger.Error(e.Message);
                }
            } else
            {
                Save();
            }
        }
        public void Save()
        {
            try
            {
                File.WriteAllText(Main.settingsPath, JsonConvert.SerializeObject(this, Formatting.Indented));
            } catch (Exception e)
            {
                Main.Logger.Error(e.Message);
            }
        }
    }
}