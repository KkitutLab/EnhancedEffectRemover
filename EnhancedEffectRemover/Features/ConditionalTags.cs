namespace EnhancedEffectRemover.Features;

public static class ConditionalTags {
    public static readonly string[] Keys = [
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

    public static readonly HashSet<string> NoneTags = [
        "NONE", "无", "無", "AUCUN", "ŽÁDNÝ", "NICHTS", "없음"
    ];

    public static bool IsNoneTag(string? tag) {
        return string.IsNullOrWhiteSpace(tag) || NoneTags.Contains(tag);
    }
}
