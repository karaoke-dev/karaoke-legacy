// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Edit.Tools;
using osu.Game.Rulesets.Karaoke.Edit.Layers.Selection.Overlays;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;
using osu.Game.Rulesets.Karaoke.UI;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Karaoke.Edit
{
    public class KaraokeHitObjectComposer : HitObjectComposer
    {
        public KaraokeHitObjectComposer(Ruleset ruleset)
            : base(ruleset)
        {
        }

        protected override RulesetContainer CreateRulesetContainer(Ruleset ruleset, WorkingBeatmap beatmap) => new KaraokeEditRulesetContainer(ruleset, beatmap);

        protected override IReadOnlyList<ICompositionTool> CompositionTools => new ICompositionTool[]
        {
            new HitObjectCompositionTool<BaseLyric>() //karaoke object
            //new HitObjectCompositionTool<FormattedText>(),//add subtext to karaoke Object
            //new HitObjectCompositionTool<LyricTranslate>(),//add translate to BaseLyric
        };

        public override HitObjectMask CreateMaskFor(DrawableHitObject hitObject)
        {
            switch (hitObject)
            {
                case DrawableLyric lyric:
                    return new LyricMask(lyric);
            }

            return base.CreateMaskFor(hitObject);
        }

        protected override ScalableContainer CreateLayerContainer()
        {
            return new ScalableContainer(KaraokeBasePlayfield.BASE_SIZE.X) { RelativeSizeAxes = Axes.Both };
        }
    }
}
