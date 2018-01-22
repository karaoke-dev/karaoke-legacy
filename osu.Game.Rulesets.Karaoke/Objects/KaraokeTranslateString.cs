// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// to record the string ,and which language
    /// </summary>
    public class KaraokeTranslateString : TextObject
    {
        public KaraokeTranslateString()
        {
        }

        public KaraokeTranslateString(string langCode, string translateText)
        {
            LangCode = langCode;
            Text = translateText;
        }

        /// <summary>
        /// Lang code
        /// </summary>
        public string LangCode { get; set; }
    }

    /// <summary>
    /// list Progress point
    /// </summary>
    public class ListKaraokeTranslateString : List<KaraokeTranslateString>
    {
        /// <summary>
        /// if add ,check this lang code is added already ?
        /// </summary>
        /// <param name="translateString"></param>
        public new void Add(KaraokeTranslateString translateString)
        {
            if (this.Any(x => x.LangCode == translateString.LangCode))
                FindLast(x => x.LangCode == translateString.LangCode).Text = translateString.Text;
            else
                base.Add(translateString);
        }
    }
}
