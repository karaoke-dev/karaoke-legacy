// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Lyric;
using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.UI;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Edit
{
    public class KaraokeEditRulesetContainer : KaraokeRulesetContainer
    {
        public KaraokeEditRulesetContainer(Ruleset ruleset, WorkingBeatmap beatmap, bool isForCurrentRuleset)
            : base(ruleset, beatmap, isForCurrentRuleset)
        {
            Name = "KaraokeEditRulesetContainer";
        }

        public override PassThroughInputManager CreateInputManager()
        {
            return new KaraokeEditorInputManager(Ruleset.RulesetInfo);
        }

        protected override Vector2 GetAspectAdjustedSize()
        {
            const float default_relative_height = KaraokeBasePlayfield.DEFAULT_HEIGHT / 512;
            const float default_aspect = 16f / 9f;

            var aspectAdjust = MathHelper.Clamp(DrawWidth / DrawHeight, 0.4f, 4) / default_aspect;

            return new Vector2(1, default_relative_height * aspectAdjust);
        }

        /// <summary>
        ///     create editable HitObject
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        protected override DrawableHitObject<BaseLyric> GetVisualRepresentation(BaseLyric h)
        {
            if (h is BaseLyric karaokeObject)
                return new DrawableEditableKaraokeObject(karaokeObject);

            return null;
        }

        protected override Playfield CreatePlayfield()
        {
            return new KaraokeEditPlayfield(Ruleset, WorkingBeatmap, this);
        }
    }
}
