using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;

namespace EnhancedEffectRemover
{ 
    public class Settings
    {
        public string setiingFilePath;
        private float _cameraZoomScale = 250.0f;
        public bool SetTrackAnimationToDefault { get; set; }
        public bool SetTrackColorToDefault { get; set; }
        public bool SetCameraZoomScale { get; set; }
        public float CameraZoomScale
        {
            get { return _cameraZoomScale; }
            set
            {
                if (value < 0)
                    _cameraZoomScale = 250.0f;
                else if (value > 1000)
                    _cameraZoomScale = 250.0f;
                else
                    _cameraZoomScale = value;
            }
        }

        public bool Filters { get; set; }
        public bool Decorations { get; set; }
        public bool Backgrounds { get; set; }
        public bool PlanetEvents { get; set; }
        public bool Cameras { get; set; }
        public bool RepeatEvents { get; set; }
        public bool FrameRate { get; set; }
        public bool TrackAnimations { get; set; }
        public bool TrackPos { get; set; }
        public bool TrackMove { get; set; }
        public bool TrackColors { get; set; }
        public bool HoldSounds { get; set; }
        public bool HideIcons { get; set; }

        public bool TrackPanel { get; set; }
        public bool TweaksPanel { get; set; }

        public void LoadGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            GUILayout.Label("Non-DLC Settings");    
            Filters = GUILayout.Toggle(Filters, "  Filters");
            Decorations = GUILayout.Toggle(Decorations, "  Decorations");
            Backgrounds = GUILayout.Toggle(Backgrounds, "  Backgrounds");
            Cameras = GUILayout.Toggle(Cameras, "  Cameras");
            PlanetEvents = GUILayout.Toggle(PlanetEvents, "  Planet Events");
            RepeatEvents = GUILayout.Toggle(RepeatEvents, "  Repeat Events");
            TrackPanel = GUILayout.Toggle(TrackPanel, "  Tracks");
            if (TrackPanel)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                TrackAnimations = GUILayout.Toggle(TrackAnimations, "  Animate Track");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                TrackMove = GUILayout.Toggle(TrackMove, "  Move Track");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                TrackPos = GUILayout.Toggle(TrackPos, "  Position Track");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                TrackColors = GUILayout.Toggle(TrackColors, "  Track Colors");
                GUILayout.EndHorizontal();
            }

            GUILayout.Space(10);
            GUILayout.Label("DLC Settings");
            HoldSounds = GUILayout.Toggle(HoldSounds, "  HoldSounds");
            HideIcons = GUILayout.Toggle(HideIcons, "  HideIcons");

            if (Cameras || TrackAnimations)
            {
                GUILayout.Space(10);
                GUILayout.Label("Tweaks");

                if (Cameras)
                {
                    SetCameraZoomScale = GUILayout.Toggle(SetCameraZoomScale, " Set Camera Zoom (100 ~ 1000) ");
                    if (SetCameraZoomScale)
                    {
                        GUILayout.BeginHorizontal();
                        CameraZoomScale = GUILayout.HorizontalSlider(CameraZoomScale, 100.0f, 1000.0f, GUILayout.Width(300));

                        string inputZoom = GUILayout.TextField(CameraZoomScale.ToString("F2"));
                        if (float.TryParse(inputZoom, out float parsedZoomScale))
                        {
                            CameraZoomScale = parsedZoomScale;
                        }

                        GUILayout.FlexibleSpace();
                        GUILayout.EndHorizontal();
                    }
                }

                if (TrackAnimations) SetTrackAnimationToDefault = GUILayout.Toggle(SetTrackAnimationToDefault, " Set Track Animation to Default");
                if (TrackColors) SetTrackColorToDefault = GUILayout.Toggle(SetTrackColorToDefault, " Set Track Color to Default");
            }

            GUILayout.EndVertical();
        }
        public void Load()
        {
            if (File.Exists(setiingFilePath))
            {
                try
                {
                    JsonConvert.PopulateObject(File.ReadAllText(setiingFilePath), this);
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
                File.WriteAllText(setiingFilePath, JsonConvert.SerializeObject(this, Formatting.Indented));
            } catch (Exception e)
            {
                Main.Logger.Error(e.Message);
            }
        }
    }
}