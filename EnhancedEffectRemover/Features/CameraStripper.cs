using ADOFAI;
using EnhancedEffectRemover.Config;
using UnityEngine;

namespace EnhancedEffectRemover.Features;

public static class CameraStripper {
    public static void Strip(List<LevelEventType> doomed, LevelData level, Settings settings) {
        doomed.Add(LevelEventType.MoveCamera);

        if (settings.SetCameraZoomScale) {
            level.cameraSettings = new(0, LevelEventType.CameraSettings, GCS.settingsInfo["CameraSettings"]);
            level.cameraSettings["zoom"] = settings.CameraZoomScale;
        }

        level.cameraSettings["relativeTo"] = CamMovementType.Player;
        level.cameraSettings["position"] = Vector2.zero;
    }
}
