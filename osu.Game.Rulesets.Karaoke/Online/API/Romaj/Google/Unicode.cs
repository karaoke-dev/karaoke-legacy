// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Linq;

namespace osu.Game.Rulesets.Karaoke.Online.API.Romaj.Google
{
    public static class Unicode
    {
        // Unicode ranges for each set
        public const int ROMAJI_MIN = 0x0020;

        public const int ROMAJI_MAX = 0x007E;
        public const int HIRAGANA_MIN = 0x3040;
        public const int HIRAGANA_MAX = 0x309F;
        public const int KATAKANA_MIN = 0x30A0;

        public const int KATAKANA_MAX = 0x30FF;

        // ー character present in both hiragana and katakana
        public const int HIRAKATA_PROLONGED_CHAR = 0x30FC;

        public const int KANJI_MIN = 0x4E00;

        public const int KANJI_MAX = 0x9FBF;

        // Covers Basic Latin, Latin-1 Supplement, Extended A, Extended B
        public const int LATIN_MIN = 0x0000;

        public const int LATIN_MAX = 0x024F;

        public static bool IsLatin(string text)
        {
            return text.Count(c => c >= LATIN_MIN && c <= LATIN_MAX) == text.Length;
        }

        public static bool IsJapanese(string text)
        {
            return text.Count(c => IsHiragana(c.ToString()) ||
                                   IsKatakana(c.ToString()) ||
                                   IsKanji(c.ToString())) == text.Length;
        }

        public static bool IsHiragana(string text)
        {
            return text.Count(c => c >= HIRAGANA_MIN && c <= HIRAGANA_MAX ||
                                   c == HIRAKATA_PROLONGED_CHAR) == text.Length;
        }

        public static bool IsKatakana(string text)
        {
            return text.Count(c => c >= KATAKANA_MIN && c <= KATAKANA_MAX ||
                                   c == HIRAKATA_PROLONGED_CHAR) == text.Length;
        }

        public static bool IsKanji(string text)
        {
            return text.Count(c => c >= KANJI_MIN && c <= KANJI_MAX) == text.Length;
        }

        public static bool IsProlongedChar(char c)
        {
            return c == HIRAKATA_PROLONGED_CHAR;
        }
    }
}
