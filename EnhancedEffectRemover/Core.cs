using ADOFAI;
using ADOFAI.Editor.Actions;
using EnhancedEffectRemover.Config;
using EnhancedEffectRemover.Patch;
using EnhancedEffectRemover.UI;
using MelonLoader;
using System.Reflection;

[assembly: MelonInfo(typeof(EnhancedEffectRemover.Core), EnhancedEffectRemover.Info.Name, EnhancedEffectRemover.Info.Version, EnhancedEffectRemover.Info.Author, EnhancedEffectRemover.Info.DownloadLink)]
[assembly: MelonGame("7th Beat Games", "A Dance of Fire and Ice")]

namespace EnhancedEffectRemover;

public sealed class Core : MelonMod {
    public static Core? Instance { get; private set; }

    public override void OnInitializeMelon() {
        Instance = this;
        Settings.Instance.Load();
        ApplyPatches();
        SettingsWindow.Build();
        Log.Msg("Loaded. Alt+' opens settings.");
    }

    public override void OnDeinitializeMelon() {
        SettingsWindow.Dispose();
        Settings.Instance.Save();
        HarmonyInstance.UnpatchSelf();
        Instance = null;
    }

    public override void OnUpdate() {
        SettingsWindow.Tick();
    }

    private void ApplyPatches() {
        SafePatch.Apply(HarmonyInstance,
            typeof(LevelData).GetMethod(nameof(LevelData.Decode)),
            null,
            SafePatch.Method(typeof(P_LevelData_Decode), nameof(P_LevelData_Decode.Postfix)),
            $"{nameof(LevelData)}.{nameof(LevelData.Decode)}");
        SafePatch.Apply(HarmonyInstance,
            typeof(SaveLevelEditorAction).GetMethod(nameof(SaveLevelEditorAction.Execute)),
            SafePatch.Method(typeof(P_SaveLevelEditorAction_Execute), nameof(P_SaveLevelEditorAction_Execute.Prefix)),
            null,
            $"{nameof(SaveLevelEditorAction)}.{nameof(SaveLevelEditorAction.Execute)}");
        SafePatch.Apply(HarmonyInstance,
            typeof(scnEditor).GetMethod(nameof(scnEditor.SaveLevel)),
            SafePatch.Method(typeof(P_scnEditor_SaveLevel), nameof(P_scnEditor_SaveLevel.Prefix)),
            null,
            $"{nameof(scnEditor)}.{nameof(scnEditor.SaveLevel)}");
        SafePatch.Apply(HarmonyInstance,
            typeof(scnEditor).GetMethod("LoadGameScene", BindingFlags.Instance | BindingFlags.NonPublic),
            null,
            SafePatch.Method(typeof(P_scnEditor_LoadGameScene), nameof(P_scnEditor_LoadGameScene.Postfix)),
            $"{nameof(scnEditor)}.LoadGameScene");
    }
}
