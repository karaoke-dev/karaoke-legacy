using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Configuration;
using osu.Game.Rulesets.Karaoke.Configuration;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric.Types
{
    /// <summary>
    /// All the bindable things
    /// </summary>
    public interface IDrawableLyricBindable
    {
        /// <summary>
        /// Style
        /// </summary>
        BindableObject<KaraokeLyricConfig> Style { get; set; }

        /// <summary>
        /// Template
        /// </summary>
        BindableObject<LyricTemplate> Template { get; set; }

        /// <summary>
        /// SingerTemplate
        /// </summary>
        BindableObject<SingerTemplate> SingerTemplate { get; set; }

        /// <summary>
        /// TranslateCode
        /// </summary>
        Bindable<TranslateCode> TranslateCode { get; set; }
    }
}
