// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace osu.Game.Rulesets.Karaoke.Tools.Translator
{
    /// <summary>
    /// translate language code
    /// google : 
    /// https://cloud.google.com/translate/docs/languages
    /// </summary>
    public enum TranslateCode
    {
        [Description("Default")] //use default
        Default,

        [Description("Afrikaans")]
        Afrikaans, //   af

        [Description("Albanian")]
        Albanian, //    sq

        [Description("Amharic")] //=========================
        Amharic, // am

        [Description("Arabic")]
        Arabic, //  ar

        [Description("Armenian")]
        Armenian, //    hy

        [Description("Azeerbaijani")]
        Azeerbaijani, //    az

        [Description("Basque")]
        Basque, //  eu

        [Description("Belarusian")]
        Belarusian, //  be

        [Description("Bengali")]
        Bengali, // bn

        [Description("Bosnian")]
        Bosnian, // bs

        [Description("Bulgarian")]
        Bulgarian, //   bg

        [Description("Catalan")]
        Catalan, // ca

        [Description("Cebuano")]
        Cebuano, // ceb (ISO-639-2)

        [Description("Chinese_Simplified")]
        Chinese_Simplified, // (Simplified)	zh-CN (BCP-47)

        [Description("Chinese_Traditional")]
        Chinese_Traditional, // (Traditional)	zh-TW (BCP-47)

        [Description("Corsican")]
        Corsican, //    co

        [Description("Croatian")]
        Croatian, //    hr

        [Description("Czech")]
        Czech, //   cs

        [Description("Danish")]
        Danish, //  da

        [Description("Dutch")]
        Dutch, //   nl

        [Description("English")]
        English, // en

        [Description("Esperanto")]
        Esperanto, //   eo

        [Description("Estonian")]
        Estonian, //    et

        [Description("Finnish")]
        Finnish, // fi

        [Description("French")]
        French, //  fr

        [Description("Frisian")]
        Frisian, // fy

        [Description("Galician")]
        Galician, //    gl

        [Description("Georgian")]
        Georgian, //    ka

        [Description("German")]
        German, //  de

        [Description("Greek")]
        Greek, //   el

        [Description("Gujarati")]
        Gujarati, //    gu

        [Description("Haitian")]
        Haitian, // Creole  ht

        [Description("Hausa")]
        Hausa, //   ha

        [Description("Hawaiian")]
        Hawaiian, //    haw (ISO-639-2)

        [Description("Hebrew")]
        Hebrew, //  iw

        [Description("Hindi")]
        Hindi, //   hi

        [Description("Hmong")]
        Hmong, //   hmn (ISO-639-2)

        [Description("Hungarian")]
        Hungarian, //   hu

        [Description("Icelandic")]
        Icelandic, //	is

        [Description("Igbo")]
        Igbo, //    ig

        [Description("Indonesian")]
        Indonesian, //  id

        [Description("Irish")]
        Irish, //   ga

        [Description("Italian")]
        Italian, // it

        [Description("Japanese")]
        Japanese, //    ja

        [Description("Javanese")]
        Javanese, //    jw

        [Description("Kannada")]
        Kannada, // kn

        [Description("Kazakh")]
        Kazakh, //  kk

        [Description("Khmer")]
        Khmer, //   km

        [Description("Korean")]
        Korean, //  ko

        [Description("Kurdish")]
        Kurdish, // ku

        [Description("Kyrgyz")]
        Kyrgyz, //  ky

        [Description("Lao")]
        Lao, // lo

        [Description("Latin")]
        Latin, //   la

        [Description("Latvian")]
        Latvian, // lv

        [Description("Lithuanian")]
        Lithuanian, //  lt

        [Description("Luxembourgish")]
        Luxembourgish, //   lb

        [Description("Macedonian")]
        Macedonian, //  mk

        [Description("Malagasy")]
        Malagasy, //    mg

        [Description("Malay")]
        Malay, //   ms

        [Description("Malayalam")]
        Malayalam, //   ml

        [Description("Maltese")]
        Maltese, // mt

        [Description("Maori")]
        Maori, //   mi

        [Description("Marathi")]
        Marathi, // mr

        [Description("Mongolian")]
        Mongolian, //   mn

        [Description("Myanmar")]
        Myanmar, // (Burmese)	my

        [Description("Nepali")]
        Nepali, //  ne

        [Description("Norwegian")]
        Norwegian, //   no

        [Description("Nyanja")]
        Nyanja, // (Chichewa)	ny

        [Description("Pashto")]
        Pashto, //  ps

        [Description("Persian")]
        Persian, // fa

        [Description("Polish")]
        Polish, //  pl

        [Description("Portuguese")]
        Portuguese, // (Portugal, Brazil)	pt

        [Description("Punjabi")]
        Punjabi, // pa

        [Description("Romanian")]
        Romanian, //    ro

        [Description("Russian")]
        Russian, // ru

        [Description("Samoan")]
        Samoan, //  sm

        [Description("Scots")]
        Scots, // Gaelic    gd

        [Description("Serbian")]
        Serbian, // sr

        [Description("Sesotho")]
        Sesotho, // st

        [Description("Shona")]
        Shona, //   sn

        [Description("Sindhi")]
        Sindhi, // sd

        [Description("Sinhala")]
        Sinhala, // (Sinhalese)	si

        [Description("Slovak")]
        Slovak, // sk

        [Description("Slovenian")]
        Slovenian, // sl

        [Description("Somali")]
        Somali, // so

        [Description("Spanish")]
        Spanish, // es

        [Description("Sundanese")]
        Sundanese, // su

        [Description("Swahili")]
        Swahili, // sw

        [Description("Swedish")]
        Swedish, // sv

        [Description("Tagalog")]
        Tagalog, // (Filipino)	tl

        [Description("Tajik")]
        Tajik, // tg

        [Description("Tamil")]
        Tamil, // ta

        [Description("Telugu")]
        Telugu, // te

        [Description("Thai")]
        Thai, // th

        [Description("Turkish")]
        Turkish, // tr

        [Description("Ukrainian")]
        Ukrainian, // uk

        [Description("Urdu")]
        Urdu, // ur

        [Description("Uzbek")]
        Uzbek, // uz

        [Description("Vietnamese")]
        Vietnamese, // vi

        [Description("Welsh")]
        Welsh, // cy

        [Description("Xhosa")]
        Xhosa, // xh

        [Description("Yiddish")]
        Yiddish, // yi

        [Description("Yoruba")]
        Yoruba, // yo

        [Description("Zulu")]
        Zulu, //  zu
    }

    public static class LangTagConvertor
    {
        public static string GetCode(TranslateCode code)
        {
            string value = "";
            ListTranslateCode.TryGetValue(code, out value);
            return value;
        }

        public static TranslateCode GetEnum(string code)
        {
            return ListTranslateCode.Where(x => x.Value == code).FirstOrDefault().Key;
        }

        public static Dictionary<TranslateCode, string> ListTranslateCode = new Dictionary<TranslateCode, string>()
        {
            { TranslateCode.Default, "" },
            { TranslateCode.Afrikaans, "af" },
            { TranslateCode.Albanian, "sq" },
            { TranslateCode.Amharic, "am" },
            { TranslateCode.Arabic, "ar" },
            { TranslateCode.Armenian, "hy" },
            { TranslateCode.Azeerbaijani, "az" },
            { TranslateCode.Basque, "eu" },
            { TranslateCode.Belarusian, "be" },
            { TranslateCode.Bengali, "bn" },
            { TranslateCode.Bosnian, "bs" },
            { TranslateCode.Catalan, "ca" },
            { TranslateCode.Cebuano, "ceb" },
            { TranslateCode.Chinese_Simplified, "zh-CN" },
            { TranslateCode.Chinese_Traditional, "zh-TW" },
            { TranslateCode.Corsican, "co" },
            { TranslateCode.Croatian, "hr" },
            { TranslateCode.Czech, "cs" },
            { TranslateCode.Danish, "da" },
            { TranslateCode.Dutch, "nl" },
            { TranslateCode.English, "en" },
            { TranslateCode.Esperanto, "eo" },
            { TranslateCode.Estonian, "et" },
            { TranslateCode.Finnish, "fi" },
            { TranslateCode.French, "fr" },
            { TranslateCode.Frisian, "fy" },
            { TranslateCode.Galician, "gl" },
            { TranslateCode.Georgian, "ka" },
            { TranslateCode.German, "de" },
            { TranslateCode.Greek, "el" },
            { TranslateCode.Gujarati, "gu" },
            { TranslateCode.Haitian, "Creole" },
            { TranslateCode.Hausa, "ha" },
            { TranslateCode.Hawaiian, "haw" },
            { TranslateCode.Hebrew, "iw" },
            { TranslateCode.Hindi, "hi" },
            { TranslateCode.Hmong, "hmn" },
            { TranslateCode.Hungarian, "hu" },
            { TranslateCode.Icelandic, "is" },
            { TranslateCode.Igbo, "ig" },
            { TranslateCode.Indonesian, "id" },
            { TranslateCode.Irish, "ga" },
            { TranslateCode.Italian, "it" },
            { TranslateCode.Japanese, "ja" },
            { TranslateCode.Javanese, "jw" },
            { TranslateCode.Kannada, "kn" },
            { TranslateCode.Kazakh, "kk" },
            { TranslateCode.Khmer, "km" },
            { TranslateCode.Korean, "ko" },
            { TranslateCode.Kurdish, "ku" },
            { TranslateCode.Kyrgyz, "ky" },
            { TranslateCode.Lao, "lo" },
            { TranslateCode.Latin, "la" },
            { TranslateCode.Latvian, "lv" },
            { TranslateCode.Lithuanian, "lt" },
            { TranslateCode.Luxembourgish, "lb" },
            { TranslateCode.Macedonian, "mk" },
            { TranslateCode.Malagasy, "mg" },
            { TranslateCode.Malay, "ms" },
            { TranslateCode.Malayalam, "ml" },
            { TranslateCode.Maltese, "mt" },
            { TranslateCode.Maori, "mi" },
            { TranslateCode.Marathi, "mr" },
            { TranslateCode.Mongolian, "mn" },
            { TranslateCode.Myanmar, "my" },
            { TranslateCode.Nepali, "ne" },
            { TranslateCode.Norwegian, "no" },
            { TranslateCode.Nyanja, "ny" },
            { TranslateCode.Pashto, "ps" },
            { TranslateCode.Persian, "fa" },
            { TranslateCode.Polish, "pl" },
            { TranslateCode.Portuguese, "pt" },
            { TranslateCode.Punjabi, "pa" },
            { TranslateCode.Romanian, "ro" },
            { TranslateCode.Russian, "ru" },
            { TranslateCode.Samoan, "sm" },
            { TranslateCode.Scots, "gd" },
            { TranslateCode.Serbian, "sr" },
            { TranslateCode.Sesotho, "st" },
            { TranslateCode.Shona, "sn" },
            { TranslateCode.Sindhi, "sd" },
            { TranslateCode.Sinhala, "si" },
            { TranslateCode.Slovak, "sk" },
            { TranslateCode.Slovenian, "sl" },
            { TranslateCode.Somali, "so" },
            { TranslateCode.Spanish, "es" },
            { TranslateCode.Sundanese, "su" },
            { TranslateCode.Swahili, "sw" },
            { TranslateCode.Swedish, "sv" },
            { TranslateCode.Tagalog, "tl" },
            { TranslateCode.Tajik, "tg" },
            { TranslateCode.Tamil, "ta" },
            { TranslateCode.Telugu, "te" },
            { TranslateCode.Thai, "th" },
            { TranslateCode.Turkish, "tr" },
            { TranslateCode.Ukrainian, "uk" },
            { TranslateCode.Urdu, "ur" },
            { TranslateCode.Uzbek, "uz" },
            { TranslateCode.Vietnamese, "vi" },
            { TranslateCode.Welsh, "cy" },
            { TranslateCode.Xhosa, "xh" },
            { TranslateCode.Yiddish, "yi" },
            { TranslateCode.Yoruba, "yo" },
            { TranslateCode.Zulu, "zu" },
        };
    }
}
