using System.Reflection;
using EnhancedEffectRemover.Config;
using Xunit;

namespace EnhancedEffectRemover.Tests;

public class SettingsTests {
    [Fact]
    public void NewSettings_HaveExpectedDefaults() {
        Assert.Equal(Settings.DefaultCameraZoom, Settings.Instance.CameraZoomScale);
        Assert.Equal(Settings.DefaultHotkey, Settings.Instance.Hotkey);
        foreach (var property in typeof(Settings).GetProperties().Where(p => p.PropertyType == typeof(bool))) {
            Assert.True((bool)property.GetValue(Settings.Instance)!);
        }
    }

    [Theory]
    [InlineData(-1000f, Settings.DefaultCameraZoom)]
    [InlineData(-0.01f, Settings.DefaultCameraZoom)]
    [InlineData(0f, 0f)]
    [InlineData(100f, 100f)]
    [InlineData(250f, Settings.DefaultCameraZoom)]
    [InlineData(1000f, 1000f)]
    [InlineData(1001f, 1001f)]
    [InlineData(5000f, 5000f)]
    public void CameraZoomScale_ClampsToValidRange(float input, float expected) {
        float previous = Settings.Instance.CameraZoomScale;
        try {
            Settings.Instance.CameraZoomScale = input;
            Assert.Equal(expected, Settings.Instance.CameraZoomScale);
        } finally {
            Settings.Instance.CameraZoomScale = previous;
        }
    }
}
