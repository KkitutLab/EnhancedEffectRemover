using UnityEngine;

namespace EnhancedEffectRemover
{
    public class GUI : MonoBehaviour
    {
        private static Settings Settings => Settings.Instance;
        public static void LoadGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("<color=#" + (Settings.EnableSave ? "00ff00" : "ff0000") +  ">Save is " + (Settings.EnableSave ? "On" : "Off") + "</color>");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("Toggle Save", GUILayout.Width(150), GUILayout.Height(50)))
            {
               Settings.EnableSave = !Settings.EnableSave;
               Patcher.ToggleSave(scnEditor.instance, Settings.EnableSave);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("Non-DLC Events");
            GUILayout.EndHorizontal();

            Settings.Filters = GUILayout.Toggle(Settings.Filters, "  Filter");
            Settings.AdvFilters = GUILayout.Toggle(Settings.AdvFilters, "  Advanced Filter");
            Settings.Particles = GUILayout.Toggle(Settings.Particles, "  Particles");
            Settings.Decorations = GUILayout.Toggle(Settings.Decorations, "  Decoration");
            Settings.Backgrounds = GUILayout.Toggle(Settings.Backgrounds, "  Background");
            Settings.Cameras = GUILayout.Toggle(Settings.Cameras, "  Camera");
            Settings.RepeatEvents = GUILayout.Toggle(Settings.RepeatEvents, "  Repeat Event");
            Settings.FrameRate = GUILayout.Toggle(Settings.FrameRate, "  Frame Rate");
            Settings.HitSounds = GUILayout.Toggle(Settings.HitSounds, "  HitSound");
            Settings.CheckPoints = GUILayout.Toggle(Settings.CheckPoints, "  CheckPoints");

            int planetSettingCount = (Settings.PlanetOrbit ? 1 : 0) + (Settings.PlanetScale ? 1 : 0) + (Settings.PlanetRadius ? 1 : 0);
            Settings.PlanetPanel = GUILayout.Toggle(Settings.PlanetPanel, "  " + planetSettingCount + " Planet Events");

            if (Settings.PlanetPanel)
            {
                GUILayout.Space(5);
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                if (GUILayout.Button("Toggle All", GUILayout.Width(100), GUILayout.Height(30)))
                {
                    if (planetSettingCount != 0)
                    {
                        Settings.PlanetOrbit = false;
                        Settings.PlanetScale = false;
                        Settings.PlanetRadius = false;
                    }
                    else
                    {
                        Settings.PlanetOrbit = true;
                        Settings.PlanetScale = true;
                        Settings.PlanetRadius = true;
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                Settings.PlanetOrbit = GUILayout.Toggle(Settings.PlanetOrbit, "  Planet Orbit");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                Settings.PlanetScale = GUILayout.Toggle(Settings.PlanetScale, "  Planet Scale");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                Settings.PlanetRadius = GUILayout.Toggle(Settings.PlanetRadius, "  Planet Radius");
                GUILayout.EndHorizontal();

                GUILayout.Space(5);
            }

            int trackSettingCount = (Settings.TrackAnimations ? 1 : 0) + (Settings.TrackPos ? 1 : 0) + (Settings.TrackMove ? 1 : 0) + (Settings.TrackColors ? 1 : 0);
            Settings.TrackPanel = GUILayout.Toggle(Settings.TrackPanel, "  " + trackSettingCount + " Track Events");

            if (Settings.TrackPanel)
            {
                GUILayout.Space(5);
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                if (GUILayout.Button("Toggle All", GUILayout.Width(100), GUILayout.Height(30)))
                {
                    if (trackSettingCount != 0)
                    {
                        Settings.TrackAnimations = false;
                        Settings.TrackMove = false;
                        Settings.TrackPos = false;
                        Settings.TrackColors = false;
                    }
                    else
                    {
                        Settings.TrackAnimations = true;
                        Settings.TrackMove = true;
                        Settings.TrackPos = true;
                        Settings.TrackColors = true;
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                Settings.TrackAnimations = GUILayout.Toggle(Settings.TrackAnimations, "  Animate Track");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                Settings.TrackMove = GUILayout.Toggle(Settings.TrackMove, "  Move Track");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                Settings.TrackPos = GUILayout.Toggle(Settings.TrackPos, "  Position Track");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                Settings.TrackColors = GUILayout.Toggle(Settings.TrackColors, "  Track Color");
                GUILayout.EndHorizontal();
            }

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("DLC Events");
            GUILayout.EndHorizontal();

            Settings.HoldSounds = GUILayout.Toggle(Settings.HoldSounds, "  HoldSound");
            Settings.HideIcons = GUILayout.Toggle(Settings.HideIcons, "  HideIcon & Judgements");

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("Miscs");
            GUILayout.EndHorizontal();

            Settings.ResetTrackOpacity = GUILayout.Toggle(Settings.ResetTrackOpacity, "  Reset all 'Track Opacity' value to 100%");
            if (Settings.Cameras)
            {
                Settings.SetCameraZoomScale = GUILayout.Toggle(Settings.SetCameraZoomScale, "  Set Camera Zoom (100 ~ 1000) ");
                if (Settings.SetCameraZoomScale)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                    Settings.CameraZoomScale = GUILayout.HorizontalSlider(Settings.CameraZoomScale, 100.0f, 1000.0f, GUILayout.Width(300));

                    string inputZoom = GUILayout.TextField(Settings.CameraZoomScale.ToString("F2"));
                    if (float.TryParse(inputZoom, out float parsedZoomScale))
                    {
                        Settings.CameraZoomScale = parsedZoomScale;
                    }

                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                }
            }
            if (Settings.TrackAnimations) Settings.SetTrackAnimationToDefault = GUILayout.Toggle(Settings.SetTrackAnimationToDefault, "  Set Track Animation to Default");
            if (Settings.TrackColors) Settings.SetTrackColorToDefault = GUILayout.Toggle(Settings.SetTrackColorToDefault, "  Set Track Color to Default");
            
            GUILayout.Space(10);
            GUILayout.EndVertical();
        }
    }
}
