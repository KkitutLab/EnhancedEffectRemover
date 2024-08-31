using ADOFAI;
using System;
using System.Collections.Generic;

namespace EnhancedEffectRemover.Remover
{
    public static class CamRemover
    {
        public static void Remove(List<LevelEventType> events, LevelData __instance, Settings Settings)
        {
            events.AddRange(new List<LevelEventType> 
            {
                LevelEventType.MoveCamera
            });

            if (Settings.SetCameraZoomScale)
            {
                float zoom = Settings.CameraZoomScale;

                __instance.cameraSettings = new LevelEvent(0, LevelEventType.CameraSettings, GCS.settingsInfo["CameraSettings"]);
                __instance.cameraSettings["zoom"] = zoom;
            }
        }
    }
}
