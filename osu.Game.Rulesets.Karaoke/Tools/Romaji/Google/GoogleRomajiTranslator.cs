﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using osu.Game.Rulesets.Karaoke.Tools.Romaji.Google;

namespace osu.Game.Rulesets.Karaoke.Tools.Romaji
{
    /// <summary>
    /// google romaji translator
    /// </summary>
    public class GoogleRomajiTranslator : RomajiTranslatorBase
    {
        public const string LanguagePair = "ja|en";

        private const string TranslatorUrl = "https://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}";

        private static char MapSplitChar = ':';

        private static List<string> Suffixes = new List<string>() {
            "Iru"
        };

        public static string GetTranslatorUrl(string text, string languagePair = LanguagePair)
        {
            return string.Format(TranslatorUrl, text, languagePair);
        }

        public static string Translate(string inText, string languagePair = LanguagePair)
        {
            // Check if already translated / romanized
            // TODO check japanese punctuation too
            // if (IsTranslated(inText)) return inText;

            // Normalize to convert full-width characters
            inText = inText.Normalize(NormalizationForm.FormKC);

            // Split the text into separate sequential tokens and translate each token
            List<TextToken> textTokens = TextToken.GetTextTokens(inText);

            // Load maps and particles lists once
            string hirakanjiMapPath = Path.Combine(Maps.DirectoryPath, Maps.HirakanjiLatn);
            List<string> hirakanjiMaps = new List<string>(File.ReadAllLines(hirakanjiMapPath));

            string hirakanjiParticlesPath = Path.Combine(Particles.DirectoryPath, Particles.HirakanjiLatn);
            List<string> hirakanjiParticles = new List<string>(File.ReadAllLines(hirakanjiParticlesPath));

            string kataMapPath = Path.Combine(Maps.DirectoryPath, Maps.KataEn);
            List<string> kataMaps = new List<string>(File.ReadAllLines(kataMapPath));

            string kataParticlesPath = Path.Combine(Particles.DirectoryPath, Particles.KataEn);
            List<string> kataParticles = new List<string>(File.ReadAllLines(kataParticlesPath));

            // Translate each token and join them back together
            string outText = "";
            foreach (TextToken textToken in textTokens)
            {
                switch (textToken.Type)
                {
                    case TokenType.HiraganaKanji:
                        outText += textToken.Translate(hirakanjiMaps, hirakanjiParticles);
                        break;

                    case TokenType.Katakana:
                        outText += textToken.Translate(kataMaps, kataParticles);
                        break;

                    case TokenType.Latin:
                    default:
                        outText += textToken.Translate();
                        break;
                }
            }

            // Normalize
            outText = outText.Normalize(NormalizationForm.FormKC);

            return outText;
        }

        public static string MapPhrases(string text, List<string> maps)
        {
            if (maps == null) return text;

            foreach (string map in maps)
            {
                string[] mapStrings = map.Split(MapSplitChar);

                // Make sure mapping is valid
                if (map.IndexOf(MapSplitChar) == 0 || (mapStrings.Length != 1 && mapStrings.Length != 2)) continue;

                text = Regex.Replace(text,
                    mapStrings[0],
                    mapStrings[1],
                    RegexOptions.IgnoreCase);
            }

            return text;
        }

        public static string LowercaseParticles(string text, List<string> particles)
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

        public static bool IsTranslated(string text)
        {
            return !text.Any(c => Unicode.IsJapanese(c.ToString()));
        }

        public static string AttachSuffixes(string text)
        {
            foreach (string suffix in Suffixes)
            {
                text = Regex.Replace(text,
                    @"\s" + suffix + @"\b",
                    suffix.ToLower());
            }

            return text;
        }

        public static string FixYouon(string text)
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
    }
}
