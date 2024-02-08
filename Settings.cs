using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;


namespace EnhancedEffectRemover
{
    public class Settings
    {
        public bool setTrackAnimationtoDefault;
        public bool setCameratoDefault;
        public bool setTrackColortoDefault;

        public bool removeCameras;
        public bool removeDecos;
        public bool removeFilters;
        public bool removeBackgrounds;
        public bool removeTracks;
        public bool removeTrackAnimations;
        public bool removeTrackColors;

        public float zoomScale = 250;

        [JsonIgnore]
        public string zoomString = "250";  

        public void LoadGUI()
        {
            GUILayout.BeginVertical();

            GUILayout.Space(10);
            GUILayout.Label("<color=#FF2222><size=20> Level save will <b>NOT</b> work !!</size></color>");
            GUILayout.Space(10);

            removeFilters = GUILayout.Toggle(removeFilters, " Remove Filters");
            removeDecos = GUILayout.Toggle(removeDecos, " Remove Decorations");
            removeBackgrounds = GUILayout.Toggle(removeBackgrounds, " Remove Backgrounds");
            removeCameras = GUILayout.Toggle(removeCameras, " Remove Cameras");
            removeTracks = GUILayout.Toggle(removeTracks, " Remove Tracks");

            GUILayout.EndHorizontal();

            if (removeTracks)
            {
                GUILayout.BeginVertical();

                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                removeTrackAnimations = GUILayout.Toggle(removeTrackAnimations, " Remove Track Animations");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                removeTrackColors = GUILayout.Toggle(removeTrackColors, " Remove Track Colors");
                GUILayout.EndHorizontal();

                GUILayout.EndVertical();
            }

            if (removeCameras || (removeTrackAnimations && removeTracks) || (removeTrackColors && removeTracks))
            {
                GUILayout.BeginVertical();

                GUILayout.Space(20);
                GUILayout.BeginHorizontal();

                if (removeCameras)
                {
                    setCameratoDefault = GUILayout.Toggle(setCameratoDefault, " Set Camera Zoom (100 ~ 1000) ");
                    if (setCameratoDefault)
                    {
                        GUILayout.BeginHorizontal();

                        zoomString = GUILayout.TextField(zoomString, GUILayout.Width(100));
                        if (float.TryParse(zoomString, out zoomScale))
                        {
                            if (zoomScale < 100 || zoomScale > 1000)
                            {
                                GUILayout.Label("<color=#ff0000> Out of Range !</color>");
                                zoomScale = 250;
                            }
                        }
                        else zoomString = "250";

                        GUILayout.FlexibleSpace();  
                        GUILayout.EndHorizontal();
                    }
                }
                GUILayout.EndHorizontal();

                if (removeTrackAnimations) setTrackAnimationtoDefault = GUILayout.Toggle(setTrackAnimationtoDefault, " Set Track Animation to Default");
                if (removeTrackColors) setTrackColortoDefault = GUILayout.Toggle(setTrackColortoDefault, " Set Track Color to Default");

                GUILayout.EndVertical();
            }
        }

        public void Load()
        {
            if (File.Exists(Main.settingsPath))
            {
                try
                {
                    JsonConvert.PopulateObject(File.ReadAllText(Main.settingsPath), this);

                    zoomString = zoomScale.ToString();
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