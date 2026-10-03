using UnityEngine.SceneManagement;

namespace EnhancedEffectRemover.Patch;

public static class SaveToggle {
    public static void Apply(scnEditor? editor, bool enabled) {
        if (editor == null) return;
        if (SceneManager.GetActiveScene().name != "scnEditor") return;

        editor.popupUnsavedChangesSave.interactable = enabled;
        editor.buttonSave.interactable = enabled;
    }
}
