using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
