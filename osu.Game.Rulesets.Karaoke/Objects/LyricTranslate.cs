// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// to record the string ,and which language
    /// </summary>
    public class LyricTranslate : FormattedText
    {
        public LyricTranslate()
        {
        }

        public LyricTranslate(string langCode, string translateText)
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
    public class ListKaraokeTranslateString : List<LyricTranslate>
    {
        /// <summary>
        /// if add ,check this lang code is added already ?
        /// </summary>
        /// <param name="translate"></param>
        public new void Add(LyricTranslate translate)
        {
            if (this.Any(x => x.LangCode == translate.LangCode))
                FindLast(x => x.LangCode == translate.LangCode).Text = translate.Text;
            else
                base.Add(translate);
        }
    }
}
