// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric.Types;
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
    ///     2.
    ///     |                       |
    ///     |      <!--scrolling--> |
    ///     |  karaoke   karaoke    |
    ///     |                       |
    ///     3.
    ///     |            ^  |
    ///     |   karaoke  |  |
    ///     |   karaoke  |  |
    ///     |   karaoke  |  |
    ///     4. more
    ///     2. 3. 4. will be implement until release
    /// </summary>
    public class KaraokeLyricPlayField : Playfield, IDrawableLyricBindable, ILayer
    {
        public KaraokeRulesetContainer KaraokeRulesetContainer { get; set; }
        public List<IDrawableLyricParameter> ListDrawableKaraokeObject { get; set; } = new List<IDrawableLyricParameter>();

        //bindable
        public BindableObject<KaraokeLyricConfig> Style { get; set; } = new BindableObject<KaraokeLyricConfig>(new KaraokeLyricConfig());

        public BindableObject<LyricTemplate> Template { get; set; } = new BindableObject<LyricTemplate>(new LyricTemplate());
        public BindableObject<SingerTemplate> SingerTemplate { get; set; } = new BindableObject<SingerTemplate>(new SingerTemplate());
        public Bindable<TranslateCode> TranslateCode { get; set; } = new Bindable<TranslateCode>();

        public KaraokeLyricPlayField()
        {
            RelativeSizeAxes = Axes.Both;
            Margin = new MarginPadding{Top = 350};
        }

        protected override HitObjectContainer CreateHitObjectContainer() => new LyricPlayFieldContainer();

        private class LyricPlayFieldContainer : HitObjectContainer
        {

            public readonly LyricLagacyLayoutContainer LyricLagacyLayoutContainer;
            public LyricPlayFieldContainer()
            {
                InternalChild = LyricLagacyLayoutContainer = new LyricLagacyLayoutContainer()
                {
                    RelativeSizeAxes = Axes.Both
                };
            }

            public override IEnumerable<DrawableHitObject> Objects => LyricLagacyLayoutContainer.Children.Cast<DrawableHitObject>().OrderBy(h => h.HitObject.StartTime);
            public override IEnumerable<DrawableHitObject> AliveObjects => LyricLagacyLayoutContainer.FlowingChildren.Cast<DrawableHitObject>().OrderBy(h => h.HitObject.StartTime);

            public override void Add(DrawableHitObject hitObject) => LyricLagacyLayoutContainer.Add(hitObject as DrawableLyric);
            public override bool Remove(DrawableHitObject hitObject) => LyricLagacyLayoutContainer.Remove(hitObject as DrawableLyric);
        }
    }
}
