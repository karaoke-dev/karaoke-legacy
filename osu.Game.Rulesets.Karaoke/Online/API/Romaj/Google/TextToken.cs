// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace osu.Game.Rulesets.Karaoke.Online.API.Romaj.Google
{
    public class TextToken
    {
        public TokenType Type { get; private set; }
        public string Text { get; set; }
        public string Prefix { get; set; }

        public Dictionary<string, string> PunctuationMap { get; } = new Dictionary<string, string>()
        {
            { "、", ", " },
            { "“", "\"" },
            { "”", "\"" }
        };

        public TextToken(TokenType type, string text = "", string prefix = "")
        {
            Type = type;
            Text = text;
            Prefix = prefix;
        }

        // 1. Latin - Don't translate
        // 2. Katakana - Translate to output language
        // 3. Hiragana / Kanji - Translate to phonetic
        public string Translate(string url, List<string> maps = null,
                                List<string> particles = null,
                                string languagePair = GoogleRomajiTranslator.LanguagePair)
        {
            string translation = "";

            switch (Type)
            {
                case TokenType.HiraganaKanji:
                {
                    // Get phoentic text
                    HtmlDocument doc = new HtmlWeb().Load(url);
                    string phoneticText = WebUtility.HtmlDecode(doc.GetElementbyId("src-translit").InnerText);
                    translation = FormatTranslation(phoneticText, maps, particles);
                    break;
                }

                case TokenType.Katakana:
                {
                    // Get translated text
                    HtmlDocument doc = new HtmlWeb().Load(url);
                    string translatedText = WebUtility.HtmlDecode(doc.GetElementbyId("result_box").InnerText);
                    translation = FormatTranslation(translatedText, maps, particles);
                    break;
                }

                case TokenType.Latin:
                default:
                {
                    translation = FormatTranslation(Text, maps, particles);
                    break;
                }
            }

            return translation;
        }

        private string FormatTranslation(string translatedText,
                                         List<string> maps = null,
                                         List<string> particles = null)
        {
            // Add prefixes, trim whitespace, and capitalise words, etc.
            string outText = "";
            switch (Type)
            {
                case TokenType.HiraganaKanji:
                    translatedText = FixYouon(translatedText);
                    goto case TokenType.Katakana;

                case TokenType.Katakana:
                    // Maps
                    translatedText = MapPhrases(translatedText, maps);

                    // Capitalise
                    translatedText = new CultureInfo("en").TextInfo.ToTitleCase(translatedText);

                    // Decapitalise particles
                    translatedText = LowercaseParticles(translatedText, particles);

                    // Attach suffixes
                    translatedText = AttachSuffixes(translatedText);

                    // Trim and join
                    outText = Prefix + translatedText.Trim();
                    break;

                case TokenType.Latin:
                default:
                    // Replace japanese punctuation
                    foreach (string s in PunctuationMap.Keys)
                    {
                        string sVal;
                        if (PunctuationMap.TryGetValue(s, out sVal))
                        {
                            translatedText = translatedText.Replace(s, sVal);
                        }
                    }

                    // Join
                    outText = Prefix + translatedText;
                    break;
            }

            return outText;
        }

        #region Function

        private static char MapSplitChar = ':';

        private List<string> Suffixes = new List<string>()
        {
            "Iru"
        };

        public string MapPhrases(string text, List<string> maps)
        {
            if (maps == null) return text;

            foreach (string map in maps)
            {
                string[] mapStrings = map.Split(MapSplitChar);

                // Make sure mapping is valid
                if (map.IndexOf(MapSplitChar) == 0 || mapStrings.Length != 1 && mapStrings.Length != 2) continue;

                text = Regex.Replace(text,
                    mapStrings[0],
                    mapStrings[1],
                    RegexOptions.IgnoreCase);
            }

            return text;
        }

        public string LowercaseParticles(string text, List<string> particles)
        {
            if (particles == null) return text;

            foreach (string particle in particles)
            {
                text = Regex.Replace(text,
                    @"\b" + particle + @"\b",
                    particle,
                    RegexOptions.IgnoreCase);
            }

            return text;
        }

        public bool IsTranslated(string text)
        {
            return !text.Any(c => Unicode.IsJapanese(c.ToString()));
        }

        public string AttachSuffixes(string text)
        {
            foreach (string suffix in Suffixes)
            {
                text = Regex.Replace(text,
                    @"\s" + suffix + @"\b",
                    suffix.ToLower());
            }

            return text;
        }

        public string FixYouon(string text)
        {
            // Shi/chi: shi ~yu -> shu, etc
            text = Regex.Replace(text, @"((?i)(?:s|c)(?-i)h)i ?~y([aou])", "$1$2");
            // shi ~e -> she, etc
            text = Regex.Replace(text, @"((?i)(?:s|c)(?-i)h)i ?~([e])", "$1$2");
            // Non-shi/chi: ji ~yu -> jyu, etc
            text = Regex.Replace(text, @"((?!(?i)(?:s|c)(?-i)h))i ?~(y[aou])", "$1$2");
            // Non-shi/chi unconventional: vu ~yu -> vyu, etc
            text = Regex.Replace(text, @"((?!(?i)(?:s|c)(?-i)h))u ?~(y[aou])", "$1$2");

            // ri ~i -> ryi, etc
            text = Regex.Replace(text, @"((?i)[knhmrgjbp](?-i))i ?~([ei])", "$1y$2");

            // All others, e.g. vu ~o -> vo
            text = Regex.Replace(text, @"[iu] ?~([aeiou])", "$1");

            return text;
        }

        #endregion
    }

    public enum TokenType
    {
        Latin,
        HiraganaKanji,
        Katakana
    }
}
