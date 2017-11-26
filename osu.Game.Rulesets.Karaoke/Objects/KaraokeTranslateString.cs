// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

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

        /// <summary>
        /// Lang code
        /// </summary>
        public string LangCode { get; set; }

    }
}
