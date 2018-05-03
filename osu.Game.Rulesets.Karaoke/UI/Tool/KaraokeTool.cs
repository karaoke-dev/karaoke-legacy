// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Tools.Translator;

namespace osu.Game.Rulesets.Karaoke.UI.Tool
{
    /// <summary>
    /// some useful tools will be defined in here
    /// the tool is just use to modified objects
    /// not drawable
    /// </summary>
    public class KaraokeTool
    {
        /// <summary>
        /// translate tool
        /// </summary>
        public ITranslator Translateor = new GoogleTranslator();
    }
}
