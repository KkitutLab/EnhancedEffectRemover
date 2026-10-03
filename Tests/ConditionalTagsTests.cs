using EnhancedEffectRemover.Features;
using Xunit;

namespace EnhancedEffectRemover.Tests;

public class ConditionalTagsTests {
    [Theory]
    [InlineData("NONE")]
    [InlineData("无")]
    [InlineData("無")]
    [InlineData("AUCUN")]
    [InlineData("ŽÁDNÝ")]
    [InlineData("NICHTS")]
    [InlineData("없음")]
    public void IsNoneTag_NoneValue_ReturnsTrue(string tag) {
        Assert.True(ConditionalTags.IsNoneTag(tag));
    }

    [Theory]
    [InlineData("a")]
    [InlineData("boss")]
    [InlineData("hit")]
    [InlineData("유다희")]
    [InlineData("none")]
    [InlineData("None")]
    [InlineData(" NONE")]
    public void IsNoneTag_RealTag_ReturnsFalse(string tag) {
        Assert.False(ConditionalTags.IsNoneTag(tag));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void IsNoneTag_NullOrWhitespace_ReturnsTrue(string? tag) {
        Assert.True(ConditionalTags.IsNoneTag(tag));
    }

    [Fact]
    public void Keys_ContainsAllRuntimeKeys() {
        string[] expected = [
            "perfectTag",
            "hitTag",
            "earlyPerfectTag",
            "latePerfectTag",
            "barelyTag",
            "veryEarlyTag",
            "veryLateTag",
            "missTag",
            "tooEarlyTag",
            "tooLateTag",
            "lossTag",
            "onCheckpointTag"
        ];
        Assert.Equal(expected.OrderBy(k => k), ConditionalTags.Keys.OrderBy(k => k));
    }

    [Fact]
    public void NoneTags_ContainsAllKnownNoneValues() {
        string[] expected = ["NONE", "无", "無", "AUCUN", "ŽÁDNÝ", "NICHTS", "없음"];
        Assert.Equal(expected.OrderBy(v => v), ConditionalTags.NoneTags.OrderBy(v => v));
    }
}
