using ADOFAI;
using System.Collections.Generic;

namespace EnhancedEffectRemover.Remover
{
    public class DecoRemover
    {
        private static readonly string[] ConditionalTagKeys =
        {
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
            "lossTag"
        };

        public static void Remove(List<LevelEventType> events, LevelData __instance, Settings settings)
        {
            if (settings.RemoveAllDecorations)
            {
                __instance.decorations.Clear();
                __instance.decorationSettings = new LevelEvent(0, LevelEventType.DecorationSettings, GCS.settingsInfo["DecorationSettings"]);

                events.AddRange(new List<LevelEventType> {
                    LevelEventType.DecorationSettings,
                    LevelEventType.AddDecoration,
                    LevelEventType.AddText,
                    LevelEventType.SetText,
                    LevelEventType.SetDefaultText,
                    LevelEventType.MoveDecorations,
                    LevelEventType.AddObject,
                    LevelEventType.SetObject
                });

                return;
            }

            HashSet<string> conditionalEventTags = GetConditionalEventTags(__instance);
            HashSet<string> preservedDecorationTags = GetPreservedDecorationTags(__instance, conditionalEventTags);

            __instance.decorations.RemoveAll(data => IsDecorationData(data) && !ShouldPreserve(data, conditionalEventTags, preservedDecorationTags));
            __instance.levelEvents.RemoveAll(data => IsDecorationData(data) && !ShouldPreserve(data, conditionalEventTags, preservedDecorationTags));
        }

        private static HashSet<string> GetConditionalEventTags(LevelData levelData)
        {
            HashSet<string> tags = new();

            foreach (var eventData in levelData.levelEvents)
            {
                if (eventData.eventType != LevelEventType.SetConditionalEvents) continue;

                foreach (string key in ConditionalTagKeys)
                {
                    if (!eventData.ContainsKey(key)) continue;

                    string tag = eventData.GetString(key);
                    if (!string.IsNullOrWhiteSpace(tag) && tag != "없음")
                    {
                        tags.Add(tag);
                    }
                }
            }

            return tags;
        }

        private static HashSet<string> GetPreservedDecorationTags(LevelData levelData, HashSet<string> conditionalEventTags)
        {
            HashSet<string> tags = new();

            foreach (var eventData in levelData.levelEvents)
            {
                if (!IsDecorationData(eventData) || !HasAnyEventTag(eventData, conditionalEventTags)) continue;

                foreach (string tag in GetTags(eventData, "tag"))
                {
                    tags.Add(tag);
                }
            }

            return tags;
        }

        private static bool ShouldPreserve(LevelEvent eventData, HashSet<string> conditionalEventTags, HashSet<string> preservedDecorationTags)
        {
            return HasAnyEventTag(eventData, conditionalEventTags) || HasAnyTag(eventData, preservedDecorationTags);
        }

        private static bool IsDecorationData(LevelEvent eventData)
        {
            return eventData.eventType == LevelEventType.DecorationSettings
                || eventData.eventType == LevelEventType.AddDecoration
                || eventData.eventType == LevelEventType.AddText
                || eventData.eventType == LevelEventType.SetText
                || eventData.eventType == LevelEventType.SetDefaultText
                || eventData.eventType == LevelEventType.MoveDecorations
                || eventData.eventType == LevelEventType.AddObject
                || eventData.eventType == LevelEventType.SetObject;
        }

        private static bool HasAnyEventTag(LevelEvent eventData, HashSet<string> tags)
        {
            foreach (string eventTag in GetTags(eventData, "eventTag"))
            {
                if (tags.Contains(eventTag)) return true;
            }

            return false;
        }

        private static bool HasAnyTag(LevelEvent eventData, HashSet<string> tags)
        {
            foreach (string tag in GetTags(eventData, "tag"))
            {
                if (tags.Contains(tag)) return true;
            }

            return false;
        }

        private static IEnumerable<string> GetTags(LevelEvent eventData, string key)
        {
            if (!eventData.ContainsKey(key)) yield break;

            string tags = eventData.GetString(key);
            if (string.IsNullOrWhiteSpace(tags)) yield break;

            foreach (string tag in tags.Split(' '))
            {
                if (!string.IsNullOrWhiteSpace(tag)) yield return tag;
            }
        }
    }
}
