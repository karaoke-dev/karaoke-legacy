// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Objects.Translate
{
    /// <summary>
    ///     to record the string ,and which language
    /// </summary>
    public class LyricTranslate : IHasText
    {
        /// <summary>
        /// Text
        /// </summary>
        public string Text { get; set; }

        public LyricTranslate()
        {

        }

        public LyricTranslate(string translateText)
        {
            Text = translateText;
        }
    }
}
