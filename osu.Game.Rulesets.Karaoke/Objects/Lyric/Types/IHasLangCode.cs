using System;
using System.Collections.Generic;
using System.Text;
using osu.Game.Rulesets.Karaoke.Configuration;

namespace osu.Game.Rulesets.Karaoke.Objects.Lyric.Types
{
    public interface IHasLangCode
    {
        /// <summary>
        /// translate code
        /// </summary>
        TranslateCode Lang { get; set; }
    }
}
