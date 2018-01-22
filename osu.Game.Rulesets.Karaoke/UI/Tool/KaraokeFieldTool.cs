// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Tools.Translator;

namespace osu.Game.Rulesets.Karaoke.UI.Tool
{
    /// <summary>
    /// some useful tools will be defined in here
    /// </summary>
    public class KaraokeFieldTool
    {
        /// <summary>
        /// translate tool
        /// </summary>
        public TranslateorBase Translateor = new GoogleTranslator();
    }
}
