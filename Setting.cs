using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityModManagerNet;
using System;


namespace EnhancedEffectRemover
{
    public class Setting
    {
        public static readonly string filePath = UnityModManager.modsPath + "\\Enhanced Effect Remover\\Settings.json";

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
           if (Main.isEnabled)
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

                    GUILayout.Space(30);
                    if (removeCameras) setCameratoDefault = GUILayout.Toggle(setCameratoDefault, " Set Camera to Default");
                    if (removeTrackAnimations) setTrackAnimationtoDefault = GUILayout.Toggle(setTrackAnimationtoDefault, " Set Track Animation to Default");
                    if (removeTrackColors) setTrackColortoDefault = GUILayout.Toggle(setTrackColortoDefault, " Set Track Color to Default");

                    GUILayout.EndVertical();
                }
            }
        }

        public static void Load()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                SettingData data = JsonConvert.DeserializeObject<SettingData>(json);

                setTrackAnimationtoDefault = data.setTrackAnimationtoDefault;
                setCameratoDefault = data.setCameratoDefault;
                setTrackColortoDefault = data.setTrackColortoDefault;

                removeCameras = data.removeCameras;
                removeDecos = data.removeDecos;
                removeFilters = data.removeFilters;
                removeBackgrounds = data.removeBackgrounds;
                removeTracks = data.removeTracks;
                removeTrackAnimations = data.removeTrackAnimations;
                removeTrackColors = data.removeTrackColors;
            } else
            {
                Save();
            }
        }
        public static void Save()
        {
            SettingData data = new SettingData
            {
                setTrackAnimationtoDefault = setTrackAnimationtoDefault,
                setCameratoDefault = setCameratoDefault,
                setTrackColortoDefault = setTrackColortoDefault,
                removeCameras = removeCameras,
                removeDecos = removeDecos,
                removeFilters = removeFilters,
                removeBackgrounds = removeBackgrounds,
                removeTracks = removeTracks,
                removeTrackAnimations = removeTrackAnimations,
                removeTrackColors = removeTrackColors
            };

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }

    [System.Serializable]
    public class SettingData
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
    }
}