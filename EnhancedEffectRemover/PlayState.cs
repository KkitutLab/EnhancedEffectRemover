namespace EnhancedEffectRemover;

internal static class PlayState {
    public static bool IsPlaying() {
        try {
            var conductor = scrConductor.instance;
            var controller = scrController.instance;
            if (conductor == null || !conductor.isGameWorld)
                return false;
            if (controller == null)
                return false;
            if (!controller.paused)
                return true;

            var editor = scnEditor.instance;
            return editor != null && editor.pausedInPlayMode;
        } catch {
            return false;
        }
    }
}
