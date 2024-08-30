using ADOFAI;
using System;
using System.Collections.Generic;

namespace EnhancedEffectRemover
{
    public static class CamRemover
    {
        public static void Remove(List<LevelEventType> events, LevelData levelData, Settings Settings)
        {
            events.AddRange(new List<LevelEventType> 
            {
                LevelEventType.MoveCamera
            });

            if (Settings.SetCameraZoomScale)
            {
                float zoom = Settings.CameraZoomScale;

                levelData.cameraSettings = new LevelEvent(0, LevelEventType.CameraSettings, GCS.settingsInfo["CameraSettings"]);
                levelData.cameraSettings["zoom"] = zoom;
            }
        }
    }
}
