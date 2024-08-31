using Newtonsoft.Json;
using System;
using System.IO;

namespace EnhancedEffectRemover
{
    [Serializable]
    public class Settings
    {
        private static Settings _instance;

        private Settings() { }
        public static Settings Instance => _instance ??= new Settings();

        private float _cameraZoomScale = 250.0f;
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
        public bool ResetTrackAnimation { get; set; }
        public bool ResetTrackColor { get; set; }
        public bool ResetTrackOpacity { get; set; }
        public bool SetCameraZoomScale { get; set; }
        public bool CheckPoints { get; set; }
        public bool Filters { get; set; }
        public bool AdvFilters { get; set; }
        public bool Particles { get; set; }
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
        public bool EnableSave { get; set; }
        public void Load()
        {
            if (File.Exists(Main.filePath))
            {
                try
                {
                    var settings = new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    };

                    JsonConvert.PopulateObject(File.ReadAllText(Main.filePath), this, settings);
                }
                catch (Exception e)
                {
                    Main.Logger.Error(e.Message);
                }
            }
            else
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