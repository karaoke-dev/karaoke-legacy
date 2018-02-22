using System.Collections.Generic;
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

        private string TranslatorUrl = "https://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}";

        
        public string GetTranslatorUrl(string text, string languagePair = LanguagePair)
        {
            return string.Format(TranslatorUrl, text, languagePair);
        }

        public string Translate(string inText, string languagePair = LanguagePair)
        {
            // Check if already translated / romanized
            // TODO check japanese punctuation too
            // if (IsTranslated(inText)) return inText;

            // Normalize to convert full-width characters
            inText = inText.Normalize(NormalizationForm.FormKC);

            // Split the text into separate sequential tokens and translate each token
            List<TextToken> textTokens = GetTextTokens(inText);

            // Load maps and particles lists once
            List<string> hirakanjiMaps = new List<string>()
            {
                " ̄ : ", "tsud:tsu"
            };

            List<string> hirakanjiParticles = new List<string>()
            {
                "ba","de","e","ga","ka","mo","na", "ne",  "ni", "no",
                "o", "te",  "to", "wa",    "wo",   "ya",   "yo", "sa", "ze", "zo",
            };

            List<string> kataMaps = new List<string>()
            {
                " ̄ : ","eye:ai","lung:rune"
            };

            List<string> kataParticles = new List<string>()
            {
                ""
            };

            

            // Translate each token and join them back together
            string outText = "";
            foreach (TextToken textToken in textTokens)
            {
                string url = GetTranslatorUrl(textToken.Text, languagePair);

                switch (textToken.Type)
                {
                    case TokenType.HiraganaKanji:
                        outText += textToken.Translate(url,hirakanjiMaps, hirakanjiParticles);
                        break;

                    case TokenType.Katakana:
                        outText += textToken.Translate(url,kataMaps, kataParticles);
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
            List<TextToken> textTokens = new List<TextToken>();

            // Start with arbitrary token type
            TokenType prevCharTokenType = TokenType.Latin;
            TokenType currCharTokenType = prevCharTokenType;

            TextToken currToken = new TextToken(currCharTokenType);

            foreach (char c in inText)
            {
                string cs = c.ToString();

                if (Unicode.IsProlongedChar(c))
                {
                    // Special condition for prolonged sound character
                    currCharTokenType = prevCharTokenType;
                }
                else if (Unicode.IsHiragana(cs) || Unicode.IsKanji(cs))
                {
                    // Hiragana / Kanji
                    currCharTokenType = TokenType.HiraganaKanji;
                }
                else if (Unicode.IsKatakana(cs))
                {
                    // Katakana
                    currCharTokenType = TokenType.Katakana;
                }
                else
                {
                    // Latin or other
                    currCharTokenType = TokenType.Latin;
                }

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
                    string tokenPrefix = "";

                    if (!string.IsNullOrEmpty(currToken.Text))
                    {
                        // Add token to token list if there is text in it
                        textTokens.Add(currToken);

                        // Get token prefix for new token if previous token was not empty
                        if (textTokens.Count > 0)
                        {
                            char prevLastChar = textTokens.Last().Text.Last();
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
            {
                textTokens.Add(currToken);
            }

            return textTokens;
        }

        public string GetTokenPrefix(TokenType prevType, TokenType currType,
                                            char prevLastChar, char currFirstChar)
        {
            string prefix = "";

            if (HasPrefix(prevType, currType, prevLastChar, currFirstChar))
            {
                prefix = " ";
            }

            return prefix;
        }

        public bool HasPrefix(TokenType prevType, TokenType currType, char prevLastChar, char currFirstChar)
        {
            bool hasPrefix = char.IsPunctuation(currFirstChar);

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
                    {
                        hasPrefix = true;
                    }

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
                            {
                                hasPrefix = true;
                            }

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
                            {
                                hasPrefix = true;
                            }

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
