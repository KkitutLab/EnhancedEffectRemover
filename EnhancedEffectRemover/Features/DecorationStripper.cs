using ADOFAI;
using EnhancedEffectRemover.Config;

namespace EnhancedEffectRemover.Features;

public static class DecorationStripper {
    public static void Strip(List<LevelEventType> doomed, LevelData level, Settings settings) {
        if (settings.RemoveAllDecorations) {
            level.decorations.Clear();
            level.decorationSettings = new(0, LevelEventType.DecorationSettings, GCS.settingsInfo["DecorationSettings"]);

            doomed.Add(LevelEventType.DecorationSettings);
            doomed.Add(LevelEventType.AddDecoration);
            doomed.Add(LevelEventType.AddText);
            doomed.Add(LevelEventType.SetText);
            doomed.Add(LevelEventType.MoveDecorations);
            doomed.Add(LevelEventType.AddObject);
            doomed.Add(LevelEventType.SetObject);
            return;
        }

        HashSet<string> conditionalTags = GetConditionalEventTags(level);
        HashSet<string> preservedTags = GetPreservedDecorationTags(level, conditionalTags);

        level.decorations.RemoveAll(e => IsDecorationData(e) && !ShouldPreserve(e, conditionalTags, preservedTags));
        level.levelEvents.RemoveAll(e => IsDecorationData(e) && !ShouldPreserve(e, conditionalTags, preservedTags));
    }

    private static HashSet<string> GetConditionalEventTags(LevelData level) {
        HashSet<string> tags = [];

        foreach (var e in level.levelEvents) {
            if (e.eventType != LevelEventType.SetConditionalEvents) continue;

            foreach (string key in ConditionalTags.Keys) {
                if (!e.ContainsKey(key)) continue;

                string? tag = e.GetString(key);
                if (!ConditionalTags.IsNoneTag(tag)) {
                    tags.Add(tag!);
                }
            }
        }

        return tags;
    }

    private static HashSet<string> GetPreservedDecorationTags(LevelData level, HashSet<string> conditionalTags) {
        HashSet<string> tags = [];

        foreach (var e in level.levelEvents) {
            if (!IsDecorationData(e) || !HasAnyEventTag(e, conditionalTags)) continue;

            foreach (string tag in GetTags(e, "tag")) {
                tags.Add(tag);
            }
        }

        return tags;
    }

    private static bool ShouldPreserve(LevelEvent e, HashSet<string> conditionalTags, HashSet<string> preservedTags) {
        return HasAnyEventTag(e, conditionalTags) || HasAnyTag(e, preservedTags);
    }

    private static bool IsDecorationData(LevelEvent e) {
        return e.eventType is LevelEventType.DecorationSettings
            or LevelEventType.AddDecoration
            or LevelEventType.AddText
            or LevelEventType.SetText
            or LevelEventType.MoveDecorations
            or LevelEventType.AddObject
            or LevelEventType.SetObject;
    }

    private static bool HasAnyEventTag(LevelEvent e, HashSet<string> tags) {
        foreach (string eventTag in GetTags(e, "eventTag")) {
            if (tags.Contains(eventTag)) return true;
        }

        return false;
    }

    private static bool HasAnyTag(LevelEvent e, HashSet<string> tags) {
        foreach (string tag in GetTags(e, "tag")) {
            if (tags.Contains(tag)) return true;
        }

        return false;
    }

    private static IEnumerable<string> GetTags(LevelEvent e, string key) {
        if (!e.ContainsKey(key)) yield break;

        string? tags = e.GetString(key);
        if (string.IsNullOrWhiteSpace(tags)) yield break;

        foreach (string tag in tags.Split(' ')) {
            if (!string.IsNullOrWhiteSpace(tag)) yield return tag;
        }
    }
}
