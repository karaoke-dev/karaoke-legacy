// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace osu.Game.Rulesets.Karaoke.Online.API.Romaj.Google
{
    /// <summary>
    ///     google romaji translator
    /// </summary>
    public class GoogleRomajiApi
    {
        public const string LANGUAGE_PAIR = "ja|en";

        private readonly string TranslatorUrl = "https://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}";


        public string GetTranslatorUrl(string text, string languagePair = LANGUAGE_PAIR)
        {
            return string.Format(TranslatorUrl, text, languagePair);
        }

        public string Translate(string inText, string languagePair = LANGUAGE_PAIR)
        {
            // Check if already translated / romanized
            // TODO check japanese punctuation too
            // if (IsTranslated(inText)) return inText;

            // Normalize to convert full-width characters
            inText = inText.Normalize(NormalizationForm.FormKC);

            // Split the text into separate sequential tokens and translate each token
            var textTokens = GetTextTokens(inText);

            // Load maps and particles lists once
            var hirakanjiMaps = new List<string>
            {
                " ̄ : ",
                "tsud:tsu"
            };

            var hirakanjiParticles = new List<string>
            {
                "ba",
                "de",
                "e",
                "ga",
                "ka",
                "mo",
                "na",
                "ne",
                "ni",
                "no",
                "o",
                "te",
                "to",
                "wa",
                "wo",
                "ya",
                "yo",
                "sa",
                "ze",
                "zo"
            };

            var kataMaps = new List<string>
            {
                " ̄ : ",
                "eye:ai",
                "lung:rune"
            };

            var kataParticles = new List<string>
            {
                ""
            };


            // Translate each token and join them back together
            var outText = "";
            foreach (var textToken in textTokens)
            {
                var url = GetTranslatorUrl(textToken.Text, languagePair);

                switch (textToken.Type)
                {
                    case TokenType.HiraganaKanji:
                        outText += textToken.Translate(url, hirakanjiMaps, hirakanjiParticles);
                        break;

                    case TokenType.Katakana:
                        outText += textToken.Translate(url, kataMaps, kataParticles);
                        break;

                    case TokenType.Latin:
                    default:
                        outText += textToken.Translate(url);
                        break;
                }
            }

            // Normalize
            outText = outText.Normalize(NormalizationForm.FormKC);

            return outText;
        }

        #region Function

        // Loop through characters in a string and split them into sequential tokens
        // eg. "Cake 01. ヴァンパイア雪降る夜"
        // => ["Cake 01. ", "ヴァンパイア", "雪降る夜"]
        public List<TextToken> GetTextTokens(string inText)
        {
            var textTokens = new List<TextToken>();

            // Start with arbitrary token type
            var prevCharTokenType = TokenType.Latin;
            var currCharTokenType = prevCharTokenType;

            var currToken = new TextToken(currCharTokenType);

            foreach (var c in inText)
            {
                var cs = c.ToString();

                if (Unicode.IsProlongedChar(c))
                    currCharTokenType = prevCharTokenType;
                else if (Unicode.IsHiragana(cs) || Unicode.IsKanji(cs))
                    currCharTokenType = TokenType.HiraganaKanji;
                else if (Unicode.IsKatakana(cs))
                    currCharTokenType = TokenType.Katakana;
                else
                    currCharTokenType = TokenType.Latin;

                // Check if there is a new token
                if (prevCharTokenType == currCharTokenType)
                {
                    // Same token
                    currToken.Text += cs;
                }
                else
                {
                    // New token

                    // Modifies the prefix of the token depending on prev/curr tokens
                    // eg. Add space before curr token
                    var tokenPrefix = "";

                    if (!string.IsNullOrEmpty(currToken.Text))
                    {
                        // Add token to token list if there is text in it
                        textTokens.Add(currToken);

                        // Get token prefix for new token if previous token was not empty
                        if (textTokens.Count > 0)
                        {
                            var prevLastChar = textTokens.Last().Text.Last();
                            tokenPrefix = GetTokenPrefix(prevCharTokenType,
                                currCharTokenType,
                                prevLastChar, c);
                        }
                    }

                    // Create new token
                    currToken = new TextToken(currCharTokenType, cs, tokenPrefix);

                    prevCharTokenType = currCharTokenType;
                }
            }

            // Add last token to the list
            if (!string.IsNullOrEmpty(currToken.Text))
                textTokens.Add(currToken);

            return textTokens;
        }

        public string GetTokenPrefix(TokenType prevType, TokenType currType,
                                     char prevLastChar, char currFirstChar)
        {
            var prefix = "";

            if (HasPrefix(prevType, currType, prevLastChar, currFirstChar))
                prefix = " ";

            return prefix;
        }

        public bool HasPrefix(TokenType prevType, TokenType currType, char prevLastChar, char currFirstChar)
        {
            var hasPrefix = char.IsPunctuation(currFirstChar);

            switch (currType)
            {
                // =========================================================================
                // Current: Latin
                // =========================================================================
                case TokenType.Latin:
                    // ==============================
                    // Previous: HiraganaKanji / Katakana
                    // ==============================
                    if (!char.IsWhiteSpace(currFirstChar) && !char.IsPunctuation(currFirstChar))
                        hasPrefix = true;

                    // Some other characters which override above
                    switch (currFirstChar)
                    {
                        case '(':
                        case '&':
                            hasPrefix = true;
                            break;

                        case '、':
                        case ',':
                        case '“':
                        case '”':
                        case '"':
                        case ')':
                        case '」':
                        case '~':
                        case '-':
                        case '!':
                        case '?':
                            hasPrefix = false;
                            break;
                    }

                    break;

                // =========================================================================
                // Current: HiraganaKanji
                // =========================================================================
                case TokenType.HiraganaKanji:
                    switch (prevType)
                    {
                        // ==============================
                        // Previous: Latin
                        // ==============================
                        case TokenType.Latin:
                            if (!char.IsWhiteSpace(prevLastChar))
                                hasPrefix = true;

                            // Some other characters which override above
                            switch (prevLastChar)
                            {
                                case '(':
                                case '「':
                                case '"':
                                case '“':
                                case '~':
                                case '-':
                                    hasPrefix = false;
                                    break;
                            }

                            break;

                        // ==============================
                        // Previous: Katakana
                        // ==============================
                        case TokenType.Katakana:
                            hasPrefix = true;
                            break;
                    }

                    break;

                // =========================================================================
                // Current: Katakana
                // =========================================================================
                case TokenType.Katakana:
                    switch (prevType)
                    {
                        // ==============================
                        // Previous: Latin
                        // ==============================
                        case TokenType.Latin:
                            if (!char.IsWhiteSpace(prevLastChar))
                                hasPrefix = true;

                            // Some other characters which override above
                            switch (prevLastChar)
                            {
                                case '(':
                                case '「':
                                case '"':
                                case '“':
                                case '~':
                                case '-':
                                    hasPrefix = false;
                                    break;
                            }

                            break;

                        // ==============================
                        // Previous: HirganaKanji
                        // ==============================
                        case TokenType.HiraganaKanji:
                            hasPrefix = true;
                            break;
                    }

                    break;
            }

            return hasPrefix;
        }

        #endregion
    }
}
