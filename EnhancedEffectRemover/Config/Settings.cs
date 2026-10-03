using MelonLoader.Utils;
using Newtonsoft.Json;

namespace EnhancedEffectRemover.Config;

public sealed class Settings {
    private static Settings? _instance;
    public static Settings Instance => _instance ??= new();

    private Settings() { }

    public static string FilePath => Path.Combine(MelonEnvironment.UserDataDirectory, Info.Name, "Settings.json");

    public const float DefaultCameraZoom = 250f;
    public const string DefaultHotkey = "Alt+Quote";

    private float _cameraZoomScale = DefaultCameraZoom;
    public float CameraZoomScale {
        get => _cameraZoomScale;
        set => _cameraZoomScale = value < 0 ? DefaultCameraZoom : value;
    }

    public bool ResetTrackAnimation { get; set; } = true;
    public bool ResetTrackColor { get; set; } = true;
    public bool RemoveAllDecorations { get; set; } = true;
    public bool ResetTrackOpacity { get; set; } = true;
    public bool SetCameraZoomScale { get; set; } = true;
    public bool CheckPoints { get; set; } = true;
    public bool Filters { get; set; } = true;
    public bool AdvFilters { get; set; } = true;
    public bool Particles { get; set; } = true;
    public bool Decorations { get; set; } = true;
    public bool Backgrounds { get; set; } = true;
    public bool Cameras { get; set; } = true;
    public bool RepeatEvents { get; set; } = true;
    public bool FrameRate { get; set; } = true;
    public bool HitSounds { get; set; } = true;
    public bool PlanetOrbit { get; set; } = true;
    public bool PlanetScale { get; set; } = true;
    public bool PlanetRadius { get; set; } = true;
    public bool TrackAnimations { get; set; } = true;
    public bool TrackPos { get; set; } = true;
    public bool TrackMove { get; set; } = true;
    public bool TrackColors { get; set; } = true;
    public bool HoldSounds { get; set; } = true;
    public bool HideIcons { get; set; } = true;
    public bool DefaultTexts { get; set; } = true;
    public bool TrackPanel { get; set; } = true;
    public bool PlanetPanel { get; set; } = true;
    public bool EnableSave { get; set; } = true;
    public bool Enabled { get; set; } = true;
    public bool BlockDuringPlay { get; set; } = true;
    public string Hotkey { get; set; } = DefaultHotkey;

    public void Load() {
        try {
            if (!File.Exists(FilePath)) {
                Save();
                return;
            }

            var serializerSettings = new JsonSerializerSettings {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

            JsonConvert.PopulateObject(File.ReadAllText(FilePath), this, serializerSettings);
        } catch (Exception e) {
            Log.Warn($"Settings load failed ({e.Message})");
        }
    }

    public void Save() {
        try {
            string? dir = Path.GetDirectoryName(FilePath);
            if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);

            File.WriteAllText(FilePath, JsonConvert.SerializeObject(this, Formatting.Indented));
        } catch (Exception e) {
            Log.Warn($"Settings save failed ({e.Message})");
        }
    }
}
