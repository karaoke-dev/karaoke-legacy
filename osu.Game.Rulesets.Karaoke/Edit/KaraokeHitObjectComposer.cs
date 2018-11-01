// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Edit.Tools;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Lyric;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Note;
using osu.Game.Rulesets.Karaoke.Edit.Masks.LyricMasks;
using osu.Game.Rulesets.Karaoke.Edit.Masks.NoteMasks;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.UI;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Karaoke.Edit
{
    public class KaraokeHitObjectComposer : HitObjectComposer<Lyric>
    {
        public KaraokeHitObjectComposer(Ruleset ruleset)
            : base(ruleset)
        {
        }

        protected override RulesetContainer<Lyric> CreateRulesetContainer(Ruleset ruleset, WorkingBeatmap beatmap)
            => new KaraokeEditRulesetContainer(ruleset, beatmap);

        protected override IReadOnlyList<HitObjectCompositionTool> CompositionTools => new[]
        {
            new LyricCompositionTool(),
        };

        protected override Container CreateLayerContainer() => new PlayfieldAdjustmentContainer { RelativeSizeAxes = Axes.Both };

        public override SelectionMask CreateMaskFor(DrawableHitObject hitObject)
        {
            switch (hitObject)
            {
                case DrawableEditableKaraokeObject lyric:
                    return new LyricSelectionMask(lyric);
                case DrawableEditableNotes note:
                    return new NoteSelectionMask(note);
            }

            return base.CreateMaskFor(hitObject);
        }
    }
}
