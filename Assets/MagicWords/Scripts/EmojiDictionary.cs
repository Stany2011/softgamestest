using System.Collections.Generic;

namespace MagicWords
{
    public static class EmojiDictionary
    {
        private static readonly Dictionary<string, string> emojiMap = new()
        {
            { "laughing", "😆" },
            { "neutral", "😐" },
            { "satisfied", "😊" },
            { "intrigued", "🤔" },
            { "affirmative", "👍" },
            { "win", "🏆" }
        };

        public static string ReplacePlaceholders(string input)
        {
            foreach (var pair in emojiMap)
            {
                input = input.Replace($"{{{pair.Key}}}", pair.Value);
            }
            return input;
        }
    }
}
