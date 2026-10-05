using EnhancedEffectRemover.Config;
using EnhancedEffectRemover.Patch;
using O5Kit.Control;
using O5Kit.Core;
using O5Kit.Factory;
using O5Kit.Input;
using UnityEngine;

namespace EnhancedEffectRemover.UI;

public static class SettingsWindow {
    private const string GroupNonDlc = "nondlc";
    private const string GroupPlanet = "planet";
    private const string GroupTrack = "track";
    private const string GroupDlc = "dlc";
    private const string GroupMisc = "misc";

    private static Settings Settings => Settings.Instance;

    private static O5Context? _ctx;
    private static O5WindowManager? _manager;
    private static O5Window? _window;
    private static TMPro.TextMeshProUGUI? _enableLabel;
    private static TMPro.TextMeshProUGUI? _saveLabel;
    private static O5Button? _hotkeyButton;
    private static bool _capturing;
    private static readonly HashSet<KeyCode> _captureHeld = new();
    private static bool _captureArmed;
    private static KeyCode? _captureMain;
    private static bool _captureAlt;
    private static bool _captureCtrl;
    private static bool _captureShift;
    private static readonly KeyCode[] _allKeys = (KeyCode[])Enum.GetValues(typeof(KeyCode));
    private static KeyCode _hotkeyMain = KeyCode.Quote;
    private static bool _hotkeyAlt = true;
    private static bool _hotkeyCtrl;
    private static bool _hotkeyShift;

    private static readonly Dictionary<string, List<O5Toggle>> _toggles = new();

    private static RectTransform? _removeAllRow;
    private static RectTransform? _setZoomRow;
    private static O5Slider? _zoomSlider;
    private static RectTransform? _zoomRow;
    private static RectTransform? _resetAnimRow;
    private static RectTransform? _resetColorRow;

    public static void Build() {
        if (_window != null)
            return;

        EnsureEventSystem();

        var root = new GameObject("EER_UI");
        UnityEngine.Object.DontDestroyOnLoad(root);

        _ctx = new();
        _ctx.SetTheme(O5Kit.Core.O5Theme.Dark with {
            PanelBG = new Color32(0x2E, 0x30, 0x28, 255),
            TopBar = new Color32(0x48, 0x4D, 0x3C, 255),
            MenuBG = new Color32(0x68, 0x72, 0x52, 255),
            ObjectBG = new Color32(0x3C, 0x40, 0x32, 255),
            ObjectButton = new Color32(0xD9, 0x8A, 0x52, 255),
            ObjectActive = new Color32(0xF0, 0xA1, 0x4D, 255),
            ObjectActiveBright = new Color32(0xFF, 0xCF, 0x91, 255),
            InactiveAlpha = 102f / 255f,
            MenuHover = new Color32(0xC1, 0xB0, 0x78, 102),
            CardHeader = new Color32(0x55, 0x5A, 0x46, 255),
            CardPanel = new Color32(0x35, 0x38, 0x2D, 255),
            SoftRed = new Color32(0xD8, 0x6C, 0x55, 255),
            ButtonHover = new Color32(0xEE, 0xA2, 0x68, 255),
            ButtonPressed = new Color32(0xFF, 0xD6, 0x9E, 255),
            Text = new Color32(0xFF, 0xF7, 0xE8, 255),
            TextDimAlpha = 153f / 255f,
            TextFaintAlpha = 51f / 255f,
            OverlayScrim = new Color32(0x18, 0x1A, 0x14, 148),
            Outline = new Color32(0xE8, 0xDC, 0xC5, 255),
            ControlOutlineIdle = new Color32(255, 255, 255, 0),
            MathOk = new Color32(0x96, 0xD9, 0x7A, 255),
            MathWarn = new Color32(0xF2, 0xD0, 0x72, 255),
            MathErr = new Color32(0xF0, 0x87, 0x78, 255),
            EditorGuide = new Color32(0x79, 0xC8, 0x5A, 255),
            EditorGuideShadow = new Color32(0x00, 0x00, 0x00, 230),
            WorkspaceAlpha = 20f / 255f,
            ChannelR = new Color32(0xF0, 0x75, 0x6B, 255),
            ChannelG = new Color32(0x7E, 0xCD, 0x72, 255),
            ChannelB = new Color32(0x91, 0xA8, 0xD8, 255),
            ChannelA = new Color32(0x73, 0x73, 0x68, 255),
            ChannelS = new Color32(0x65, 0xC7, 0xD4, 255),
            ChannelV = new Color32(0xF2, 0xC2, 0x59, 255),
            CornerRadius = 12f,
            OutlineWidth = 2f,
            ControlHeight = 50f,
            FontSizeBody = 24f,
            FontSizeH1 = 32f
        });
        _manager = O5WindowManager.Create(_ctx, root.transform);

        _window = _manager.Create(new() {
            Title = "EnhancedEffectRemover",
            Size = new(560, 800),
            MinSize = new(400, 300),
            Resizable = true
        });
        _window.CloseRequested += _ => SetVisible(false);

        var (_, content, _) = O5Factory.ScrollView(_ctx, _window.Content);

        _enableLabel = O5Factory.ControlText(_ctx, content, _ctx.Theme.FontSizeBody);
        RefreshEnableLabel();
        O5Factory.Button(_ctx, content, ToggleMod, "Toggle Mod", nameof(ToggleMod));

        _saveLabel = O5Factory.ControlText(_ctx, content, _ctx.Theme.FontSizeBody);
        RefreshSaveLabel();
        O5Factory.Button(_ctx, content, ToggleSave, "Toggle Save", nameof(ToggleSave));

        RefreshHotkey();
        _hotkeyButton = O5Factory.Button(_ctx, content, StartCapture, $"Hotkey: {HotkeyText()}", nameof(StartCapture));
        O5Factory.Toggle(_ctx, content, true, Settings.BlockDuringPlay, v => {
            Settings.BlockDuringPlay = v;
            Settings.Save();
        }, "Block during play", nameof(Settings.BlockDuringPlay));

        Header(content, "Non-DLC Events");
        O5Factory.Button(_ctx, content, () => ToggleAll(GroupNonDlc), "Toggle All", nameof(ToggleAll) + GroupNonDlc);
        AddToggle(content, GroupNonDlc, "Filter", nameof(Settings.Filters), () => Settings.Filters, v => Settings.Filters = v);
        AddToggle(content, GroupNonDlc, "Advanced Filter", nameof(Settings.AdvFilters), () => Settings.AdvFilters, v => Settings.AdvFilters = v);
        AddToggle(content, GroupNonDlc, "Particles", nameof(Settings.Particles), () => Settings.Particles, v => Settings.Particles = v);
        AddToggle(content, GroupNonDlc, "Decoration", nameof(Settings.Decorations), () => Settings.Decorations, v => {
            Settings.Decorations = v;
            _removeAllRow?.gameObject.SetActive(v);
        });
        AddToggle(content, GroupNonDlc, "Background", nameof(Settings.Backgrounds), () => Settings.Backgrounds, v => Settings.Backgrounds = v);
        AddToggle(content, GroupNonDlc, "Camera", nameof(Settings.Cameras), () => Settings.Cameras, v => {
            Settings.Cameras = v;
            _setZoomRow?.gameObject.SetActive(v);
            _zoomRow?.gameObject.SetActive(v);
        });
        AddToggle(content, GroupNonDlc, "Repeat Event", nameof(Settings.RepeatEvents), () => Settings.RepeatEvents, v => Settings.RepeatEvents = v);
        AddToggle(content, GroupNonDlc, "Frame Rate", nameof(Settings.FrameRate), () => Settings.FrameRate, v => Settings.FrameRate = v);
        AddToggle(content, GroupNonDlc, "HitSound", nameof(Settings.HitSounds), () => Settings.HitSounds, v => Settings.HitSounds = v);
        AddToggle(content, GroupNonDlc, "CheckPoint", nameof(Settings.CheckPoints), () => Settings.CheckPoints, v => Settings.CheckPoints = v);
        AddToggle(content, GroupNonDlc, "Default Text (Title)", nameof(Settings.DefaultTexts), () => Settings.DefaultTexts, v => Settings.DefaultTexts = v);

        Header(content, "Planet Events");
        O5Factory.Button(_ctx, content, () => ToggleAll(GroupPlanet), "Toggle All", nameof(ToggleAll) + GroupPlanet);
        AddToggle(content, GroupPlanet, "Planet Orbit", nameof(Settings.PlanetOrbit), () => Settings.PlanetOrbit, v => Settings.PlanetOrbit = v);
        AddToggle(content, GroupPlanet, "Planet Scale", nameof(Settings.PlanetScale), () => Settings.PlanetScale, v => Settings.PlanetScale = v);
        AddToggle(content, GroupPlanet, "Planet Radius", nameof(Settings.PlanetRadius), () => Settings.PlanetRadius, v => Settings.PlanetRadius = v);

        Header(content, "Track Events");
        O5Factory.Button(_ctx, content, () => ToggleAll(GroupTrack), "Toggle All", nameof(ToggleAll) + GroupTrack);
        AddToggle(content, GroupTrack, "Animate Track", nameof(Settings.TrackAnimations), () => Settings.TrackAnimations, v => {
            Settings.TrackAnimations = v;
            _resetAnimRow?.gameObject.SetActive(v);
        });
        AddToggle(content, GroupTrack, "Move Track", nameof(Settings.TrackMove), () => Settings.TrackMove, v => Settings.TrackMove = v);
        AddToggle(content, GroupTrack, "Position Track", nameof(Settings.TrackPos), () => Settings.TrackPos, v => Settings.TrackPos = v);
        AddToggle(content, GroupTrack, "Track Color", nameof(Settings.TrackColors), () => Settings.TrackColors, v => {
            Settings.TrackColors = v;
            _resetColorRow?.gameObject.SetActive(v);
        });

        Header(content, "DLC Events");
        O5Factory.Button(_ctx, content, () => ToggleAll(GroupDlc), "Toggle All", nameof(ToggleAll) + GroupDlc);
        AddToggle(content, GroupDlc, "HoldSound", nameof(Settings.HoldSounds), () => Settings.HoldSounds, v => Settings.HoldSounds = v);
        AddToggle(content, GroupDlc, "HideIcon & Judgements", nameof(Settings.HideIcons), () => Settings.HideIcons, v => Settings.HideIcons = v);

        Header(content, "Miscs");
        O5Factory.Button(_ctx, content, () => ToggleAll(GroupMisc), "Toggle All", nameof(ToggleAll) + GroupMisc);
        _removeAllRow = AddToggle(content, GroupMisc, "Remove all decorations", nameof(Settings.RemoveAllDecorations),
            () => Settings.RemoveAllDecorations, v => Settings.RemoveAllDecorations = v).Rect;
        AddToggle(content, GroupMisc, "Reset all 'Track Opacity' value to 100%", nameof(Settings.ResetTrackOpacity),
            () => Settings.ResetTrackOpacity, v => Settings.ResetTrackOpacity = v);
        _setZoomRow = AddToggle(content, GroupMisc, "Set Camera Zoom", nameof(Settings.SetCameraZoomScale),
            () => Settings.SetCameraZoomScale, v => {
                Settings.SetCameraZoomScale = v;
                _ctx?.NotifyEnabledChanged(v);
            }).Rect;
        var zoomSlider = O5Factory.Slider(_ctx, content, Settings.DefaultCameraZoom, 100f, 1000f, Settings.CameraZoomScale, "F2",
            ClampMode.Slider, null, null, v => {
                Settings.CameraZoomScale = (float)v;
                Settings.Save();
            }, null, "Camera Zoom", nameof(Settings.CameraZoomScale));
        zoomSlider.EnabledWhen = () => Settings.SetCameraZoomScale;
        _zoomSlider = zoomSlider;
        _zoomRow = zoomSlider.Rect;
        _resetAnimRow = AddToggle(content, GroupMisc, "Set Track Animation to Default", nameof(Settings.ResetTrackAnimation),
            () => Settings.ResetTrackAnimation, v => Settings.ResetTrackAnimation = v).Rect;
        _resetColorRow = AddToggle(content, GroupMisc, "Set Track Color to Default", nameof(Settings.ResetTrackColor),
            () => Settings.ResetTrackColor, v => Settings.ResetTrackColor = v).Rect;

        RefreshConditionalRows();
        RefreshTitle();
        SetVisible(false);
    }

    public static void Tick() {
        O5Object.TickAll();

        if (Settings.BlockDuringPlay && PlayState.IsPlaying()) {
            if (_window?.Rect.gameObject.activeSelf == true)
                SetVisible(false);
            return;
        }

        if (_capturing) {
            CaptureTick();
            return;
        }

        bool alt = !_hotkeyAlt || O5Input.GetKey(KeyCode.LeftAlt) || O5Input.GetKey(KeyCode.RightAlt);
        bool ctrl = !_hotkeyCtrl || O5Input.GetKey(KeyCode.LeftControl) || O5Input.GetKey(KeyCode.RightControl);
        bool shift = !_hotkeyShift || O5Input.GetKey(KeyCode.LeftShift) || O5Input.GetKey(KeyCode.RightShift);
        if (alt && ctrl && shift && !O5InputBlocker.IsEditing && O5Input.GetKeyDown(_hotkeyMain)) {
            Toggle();
        }
    }

    public static void Dispose() {
        try {
            _manager?.Dispose();
            _ctx?.Dispose();
        } catch (Exception e) {
            Log.Warn($"UI dispose failed ({e.Message})");
        } finally {
            _manager = null;
            _window = null;
            _ctx = null;
            _toggles.Clear();
            _enableLabel = null;
            _saveLabel = null;
            _hotkeyButton = null;
            _capturing = false;
            _captureHeld.Clear();
            _removeAllRow = null;
            _setZoomRow = null;
            _zoomSlider = null;
            _zoomRow = null;
            _resetAnimRow = null;
            _resetColorRow = null;
        }
    }

    private static void Toggle() {
        SetVisible(!(_window?.Rect.gameObject.activeSelf ?? false));
    }

    private static void SetVisible(bool visible) {
        if (_window == null)
            return;

        _window.Rect.gameObject.SetActive(visible);
        if (visible)
            _window.BringToFront();
    }

    private static void Header(Transform parent, string text) {
        O5Factory.ControlTextH1(_ctx!, parent).text = text;
    }

    private static O5Toggle AddToggle(Transform parent, string group, string text, string id, Func<bool> get, Action<bool> set) {
        var toggle = O5Factory.Toggle(_ctx!, parent, true, get(), v => {
            set(v);
            Settings.Save();
        }, text, id);

        if (!_toggles.TryGetValue(group, out var list)) {
            list = [];
            _toggles[group] = list;
        }
        list.Add(toggle);

        return toggle;
    }

    private static void ToggleAll(string group) {
        if (!_toggles.TryGetValue(group, out var list))
            return;

        bool target = list.Exists(t => t.Value) ? false : true;
        foreach (var toggle in list)
            toggle.Set(target);

        RefreshConditionalRows();
        Settings.Save();
    }

    private static void ToggleMod() {
        Settings.Enabled = !Settings.Enabled;
        Settings.Save();
        SaveToggle.Apply(scnEditor.instance, !Settings.Enabled || Settings.EnableSave);
        RefreshEnableLabel();
        RefreshSaveLabel();
        Log.Msg(Settings.Enabled ? "Mod enabled" : "Mod disabled");
    }

    private static void RefreshEnableLabel() {
        if (_enableLabel == null)
            return;

        _enableLabel.text = Settings.Enabled
            ? "<color=#78B855>Mod is On</color>"
            : "<color=#C86A55>Mod is Off</color>";
    }

    private static void ToggleSave() {
        Settings.EnableSave = !Settings.EnableSave;
        SaveToggle.Apply(scnEditor.instance, !Settings.Enabled || Settings.EnableSave);
        Settings.Save();
        RefreshSaveLabel();
    }

    private static void RefreshSaveLabel() {
        if (_saveLabel == null)
            return;

        _saveLabel.text = Settings.EnableSave
            ? "<color=#78B855>Save is On</color>"
            : "<color=#C86A55>Save is Off</color>";
    }

    private static void RefreshConditionalRows() {
        _removeAllRow?.gameObject.SetActive(Settings.Decorations);
        _setZoomRow?.gameObject.SetActive(Settings.Cameras);
        _zoomRow?.gameObject.SetActive(Settings.Cameras);
        _resetAnimRow?.gameObject.SetActive(Settings.TrackAnimations);
        _resetColorRow?.gameObject.SetActive(Settings.TrackColors);
    }

    private static void StartCapture() {
        _capturing = true;
        _captureArmed = false;
        _captureHeld.Clear();
        _captureMain = null;
        _captureAlt = _captureCtrl = _captureShift = false;
        RefreshHotkeyButton();
    }

    private static void EndCapture() {
        _capturing = false;
        _captureHeld.Clear();
        RefreshHotkeyButton();
    }

    private static void CaptureTick() {
        if (O5Input.GetKeyDown(KeyCode.Escape)) {
            Settings.Hotkey = Settings.DefaultHotkey;
            Settings.Save();
            RefreshHotkey();
            RefreshTitle();
            EndCapture();
            return;
        }

        foreach (var key in _allKeys) {
            if (key is KeyCode.None or KeyCode.Escape)
                continue;
            if (O5Input.GetKey(key)) {
                if (_captureHeld.Add(key) && !IsModifier(key))
                    _captureMain = key;
            } else {
                _captureHeld.Remove(key);
            }
        }

        if (!_captureArmed) {
            if (_captureHeld.Count == 0) {
                _captureArmed = true;
                _captureMain = null;
                _captureAlt = _captureCtrl = _captureShift = false;
            }
            return;
        }

        foreach (var key in _captureHeld) {
            if (key is KeyCode.LeftAlt or KeyCode.RightAlt)
                _captureAlt = true;
            else if (key is KeyCode.LeftControl or KeyCode.RightControl or KeyCode.LeftCommand or KeyCode.RightCommand)
                _captureCtrl = true;
            else if (key is KeyCode.LeftShift or KeyCode.RightShift)
                _captureShift = true;
            else
                _captureMain = key;
        }

        if (_captureHeld.Count == 0 && _captureMain.HasValue) {
            Settings.Hotkey = (_captureAlt ? "Alt+" : "") + (_captureCtrl ? "Control+" : "") + (_captureShift ? "Shift+" : "") + _captureMain.Value;
            Settings.Save();
            RefreshHotkey();
            RefreshTitle();
            EndCapture();
        }
    }

    private static bool IsModifier(KeyCode key) {
        return key is KeyCode.LeftAlt or KeyCode.RightAlt
            or KeyCode.LeftControl or KeyCode.RightControl
            or KeyCode.LeftShift or KeyCode.RightShift
            or KeyCode.LeftCommand or KeyCode.RightCommand;
    }

    private static void RefreshHotkey() {
        _hotkeyMain = KeyCode.Quote;
        _hotkeyAlt = false;
        _hotkeyCtrl = false;
        _hotkeyShift = false;

        bool found = false;
        foreach (var raw in (Settings.Hotkey ?? Settings.DefaultHotkey).Split('+')) {
            string token = raw.Trim();
            if (token.Length == 0)
                continue;
            if (token.Equals("Alt", StringComparison.OrdinalIgnoreCase))
                _hotkeyAlt = true;
            else if (token.Equals("Control", StringComparison.OrdinalIgnoreCase) || token.Equals("Ctrl", StringComparison.OrdinalIgnoreCase))
                _hotkeyCtrl = true;
            else if (token.Equals("Shift", StringComparison.OrdinalIgnoreCase))
                _hotkeyShift = true;
            else if (Enum.TryParse<KeyCode>(token, true, out var key) && key != KeyCode.None && key != KeyCode.Escape && !IsModifier(key)) {
                _hotkeyMain = key;
                found = true;
            }
        }

        if (!found) {
            _hotkeyMain = KeyCode.Quote;
            _hotkeyAlt = true;
            _hotkeyCtrl = false;
            _hotkeyShift = false;
        }
    }

    private static string HotkeyText() {
        string text = "";
        if (_hotkeyAlt)
            text += "Alt+";
        if (_hotkeyCtrl)
            text += "Ctrl+";
        if (_hotkeyShift)
            text += "Shift+";
        return text + (_hotkeyMain == KeyCode.Quote ? "'" : _hotkeyMain.ToString());
    }

    private static void RefreshTitle() {
        _window?.SetTitle($"EnhancedEffectRemover {HotkeyText()}");
    }

    private static void RefreshHotkeyButton() {
        if (_hotkeyButton?.Label == null)
            return;

        _hotkeyButton.Label.text = _capturing ? "Press keys... (Esc resets)" : $"Hotkey: {HotkeyText()}";
    }

    private static void EnsureEventSystem() {
        // Never create our own EventSystem. scnEditor gates wheel zoom on
        // `EventSystem.current.currentInputModule is CustomStandaloneInputModule`,
        // so a second EventSystem with the stock StandaloneInputModule steals
        // `EventSystem.current` and kills the editor mouse wheel.
        // Our overlay canvas works through the game's own EventSystem (same as Overlayer).
    }
}
