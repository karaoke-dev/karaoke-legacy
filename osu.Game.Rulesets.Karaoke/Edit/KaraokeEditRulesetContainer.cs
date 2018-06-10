// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Karaoke.Edit.Drawables;
using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.UI;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Karaoke.Edit
{
    public class KaraokeEditRulesetContainer : KaraokeRulesetContainer
    {
        public KaraokeEditRulesetContainer(Ruleset ruleset, WorkingBeatmap beatmap)
            : base(ruleset, beatmap)
        {
            Name = "KaraokeEditRulesetContainer";
        }

        public override PassThroughInputManager CreateInputManager()
        {
            return new KaraokeEditorInputManager(Ruleset.RulesetInfo);
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
