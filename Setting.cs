using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;
using UnityModManagerNet;


namespace EnhancedEffectRemover
{
    public class Settings
    {

        public static bool setTrackAnimationtoDefault;
        public static bool setCameratoDefault;
        public static bool setTrackColortoDefault;

        public static bool removeCameras;
        public static bool removeDecos;
        public static bool removeFilters;
        public static bool removeBackgrounds;
        public static bool removeTracks;
        public static bool removeTrackAnimations;
        public static bool removeTrackColors;

        public static void LoadGUI()
        {
            GUILayout.Label(Main.settingsPath);
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

                GUILayout.Space(30);
                if (removeCameras) setCameratoDefault = GUILayout.Toggle(setCameratoDefault, " Set Camera to Default");
                if (removeTrackAnimations) setTrackAnimationtoDefault = GUILayout.Toggle(setTrackAnimationtoDefault, " Set Track Animation to Default");
                if (removeTrackColors) setTrackColortoDefault = GUILayout.Toggle(setTrackColortoDefault, " Set Track Color to Default");

                GUILayout.EndVertical();
            }
        }

        public static void Load()
        {
            if (File.Exists(Main.settingsPath))
            {
                try
                {
                    Settings settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Main.settingsPath));
                }
                catch (Exception e)
                {
                    Main.Logger.Error(e.Message);
                }
            } else
            {
                Save();
            }
        }
        public static void Save()
        {
            try
            {
                File.WriteAllText(Main.settingsPath, JsonConvert.SerializeObject(new Settings(), Formatting.Indented));
            } catch (Exception e)
            {
                Main.Logger.Error(e.Message);
            }
        }
    }
}