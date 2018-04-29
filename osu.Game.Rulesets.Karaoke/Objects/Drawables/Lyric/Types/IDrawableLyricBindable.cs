// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

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
        /// Lang
        /// </summary>
        Bindable<TranslateCode> TranslateCode { get; set; }
    }
}
