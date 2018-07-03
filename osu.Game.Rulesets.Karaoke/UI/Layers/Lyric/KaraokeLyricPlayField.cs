// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric.Types;
using osu.Game.Rulesets.Karaoke.UI.Layers.Lyric.Components;
using osu.Game.Rulesets.Karaoke.UI.Layers.Type;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Lyric
{
    /// <summary>
    ///     use to manage karaoke lyric's position arrangement
    ///     1.
    ///     |                   |
    ///     |                   |
    ///     |   karaoke         |
    ///     |           karaoke |
    ///     <see cref="LagacyKaraokeLyricContainer"/>
    /// 
    ///     2.
    ///     |                       |
    ///     |      <!--scrolling--> |
    ///     |  karaoke   karaoke    |
    ///     |                       |
    ///     <see cref="KaraokeLyricScrollContainer"/>
    /// 
    ///     3.
    ///     |            ^  |
    ///     |   karaoke  |  |
    ///     |   karaoke  |  |
    ///     |   karaoke  |  |
    ///     
    /// 
    ///     4. more
    ///     2. 3. 4. will be implement until release
    /// </summary>
    public class KaraokeLyricPlayField : Playfield, IDrawableLyricBindable, ILayer
    {
        public KaraokeRulesetContainer KaraokeRulesetContainer { get; set; }

        //bindable
        public BindableObject<KaraokeLyricConfig> Style { get; set; } = new BindableObject<KaraokeLyricConfig>(new KaraokeLyricConfig());
        public BindableObject<LyricTemplate> Template { get; set; } = new BindableObject<LyricTemplate>(new LyricTemplate());
        public BindableObject<SingerTemplate> SingerTemplate { get; set; } = new BindableObject<SingerTemplate>(new SingerTemplate());
        public Bindable<TranslateCode> TranslateCode { get; set; } = new Bindable<TranslateCode>();

        private readonly ILyricContainer _lyricContainer;
        public List<IDrawableLyricParameter> ListDrawableKaraokeObject => _lyricContainer.Lyrics.ToList<IDrawableLyricParameter>();

        /// <summary>
        /// Ctor
        /// </summary>
        public KaraokeLyricPlayField()
        {
            //Lagacy
            _lyricContainer = new LagacyKaraokeLyricContainer()
            {

            };

            Children = new Drawable[]
            {
                (Drawable)_lyricContainer
            };
        }

        public override void Add(DrawableHitObject h)
        {
            h.Depth = (float)h.HitObject.StartTime;

            if (h is DrawableLyric drawableLyric)
            {
                //binding
                //drawableLyric.Style.BindTo(Style);
                //drawableLyric.Template.BindTo(Template);
                //drawableLyric.SingerTemplate.BindTo(SingerTemplate);
                //drawableLyric.TranslateCode.BindTo(TranslateCode);

                //add to container
                _lyricContainer.Add(drawableLyric);
            }
        }

        
    }
}
