using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;

namespace EnhancedEffectRemover
{
    [Serializable]
    public class Settings
    {
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
                    _cameraZoomScale = 1000.0f;
                else
                    _cameraZoomScale = value;
            }
        }
        public bool CheckPoints { get; set; }
        public bool Filters { get; set; }
        public bool Decorations { get; set; }
        public bool Backgrounds { get; set; }
        public bool Cameras { get; set; }
        public bool RepeatEvents { get; set; }
        public bool FrameRate { get; set; }
        public bool HitSounds { get; set; }
        public bool PlanetOrbit { get; set; }
        public bool PlanetScale { get; set; }
        public bool PlanetRadius { get; set; }
        public bool TrackAnimations { get; set; }
        public bool TrackPos { get; set; }
        public bool TrackMove { get; set; }
        public bool TrackColors { get; set; }
        public bool HoldSounds { get; set; }
        public bool HideIcons { get; set; }
        public bool TrackPanel { get; set; }
        public bool PlanetPanel { get; set; }

        public void LoadGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("Non-DLC Settings");
            GUILayout.EndHorizontal();

            Filters = GUILayout.Toggle(Filters, "  Filter");
            Decorations = GUILayout.Toggle(Decorations, "  Decoration");
            Backgrounds = GUILayout.Toggle(Backgrounds, "  Background");
            Cameras = GUILayout.Toggle(Cameras, "  Camera");
            RepeatEvents = GUILayout.Toggle(RepeatEvents, "  Repeat Event");
            FrameRate = GUILayout.Toggle(FrameRate, "  Framerate");
            HitSounds = GUILayout.Toggle(HitSounds, "  HitSound");

            int planetSettingCount = (PlanetOrbit ? 1 : 0) + (PlanetScale ? 1 : 0) + (PlanetRadius ? 1 : 0);
            PlanetPanel = GUILayout.Toggle(PlanetPanel, "  " + planetSettingCount + " Planet Events");

            if (PlanetPanel)
            {
                GUILayout.Space(5);
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                if (GUILayout.Button("Toggle All", GUILayout.Width(100), GUILayout.Height(30)))
                {
                    if (planetSettingCount != 0)
                    {
                        PlanetOrbit = false;
                        PlanetScale = false;
                        PlanetRadius = false;
                    }
                    else
                    {
                        PlanetOrbit = true;
                        PlanetScale = true;
                        PlanetRadius = true;
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                PlanetOrbit = GUILayout.Toggle(PlanetOrbit, "  Planet Orbit");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                PlanetScale = GUILayout.Toggle(PlanetScale, "  Planet Scale");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                PlanetRadius = GUILayout.Toggle(PlanetRadius, "  Planet Radius");
                GUILayout.EndHorizontal();
            }

            int trackSettingCount = (TrackAnimations ? 1 : 0) + (TrackPos ? 1 : 0) + (TrackMove ? 1 : 0) + (TrackColors ? 1 : 0);
            TrackPanel = GUILayout.Toggle(TrackPanel, "  " + trackSettingCount + " Track Events");

            if (TrackPanel)
            {
                GUILayout.Space(5);
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                if (GUILayout.Button("Toggle All", GUILayout.Width(100), GUILayout.Height(30)))
                {
                    if (trackSettingCount != 0)
                    {
                        TrackAnimations = false;
                        TrackMove = false;
                        TrackPos = false;
                        TrackColors = false;
                    }
                    else
                    {
                        TrackAnimations = true;
                        TrackMove = true;
                        TrackPos = true;
                        TrackColors = true;
                    }
                }
                GUILayout.EndHorizontal();

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
                TrackColors = GUILayout.Toggle(TrackColors, "  Track Color");
                GUILayout.EndHorizontal();
            }

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("DLC Settings");
            GUILayout.EndHorizontal();

            HoldSounds = GUILayout.Toggle(HoldSounds, "  HoldSound");
            HideIcons = GUILayout.Toggle(HideIcons, "  HideIcon");

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("Tweaks");
            GUILayout.EndHorizontal();

            CheckPoints = GUILayout.Toggle(CheckPoints, "  Remove CheckPoints");
            if (Cameras)
            {
                SetCameraZoomScale = GUILayout.Toggle(SetCameraZoomScale, "  Set Camera Zoom (100 ~ 1000) ");
                if (SetCameraZoomScale)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(10);
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
            if (TrackAnimations) SetTrackAnimationToDefault = GUILayout.Toggle(SetTrackAnimationToDefault, "  Set Track Animation to Default");
            if (TrackColors) SetTrackColorToDefault = GUILayout.Toggle(SetTrackColorToDefault, "  Set Track Color to Default");

            GUILayout.Space(10);
            GUILayout.EndVertical();
        }

        public void Load()
        {
            if (File.Exists(Main.filePath))
            {
                try
                {
                    JsonConvert.PopulateObject(File.ReadAllText(Main.filePath), this);
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
                File.WriteAllText(Main.filePath, JsonConvert.SerializeObject(this, Formatting.Indented));
            } catch (Exception e)
            {
                Main.Logger.Error(e.Message);
            }
        }
    }
}