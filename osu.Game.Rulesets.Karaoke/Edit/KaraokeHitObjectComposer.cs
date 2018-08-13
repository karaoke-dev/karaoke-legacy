// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Edit.Tools;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Lyric;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Note;
using osu.Game.Rulesets.Karaoke.Edit.Layers.Selection.Overlays;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Karaoke.Edit
{
    public class KaraokeHitObjectComposer : HitObjectComposer
    {
        protected override IReadOnlyList<ICompositionTool> CompositionTools => new ICompositionTool[]
        {
            new HitObjectCompositionTool<BaseLyric>("Lyric")
            //new HitObjectCompositionTool<FormattedText>(),//add subtext to karaoke Object
            //new HitObjectCompositionTool<LyricTranslate>(),//add translate to BaseLyric
        };

        public KaraokeHitObjectComposer(Ruleset ruleset)
            : base(ruleset)
        {
        }

        public override HitObjectMask CreateMaskFor(DrawableHitObject hitObject)
        {
            switch (hitObject)
            {
                case DrawableEditableKaraokeObject lyric:
                    return new LyricMask(lyric);
                case DrawableEditableKaraokeNoteGroup note:
                    return new NoteMask(note);
            }

            return base.CreateMaskFor(hitObject);
        }

        protected override RulesetContainer CreateRulesetContainer(Ruleset ruleset, WorkingBeatmap beatmap)
        {
            return new KaraokeEditRulesetContainer(ruleset, beatmap);
        }
    }
}
