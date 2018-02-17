// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Beatmaps;
using osu.Game.Rulesets.Karaoke.Edit.Drawables;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.UI;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Karaoke.Edit
{
    public class KaraokeEditRulesetContainer : KaraokeRulesetContainer
    {
        public KaraokeEditRulesetContainer(Ruleset ruleset, WorkingBeatmap beatmap, bool isForCurrentRuleset)
            : base(ruleset, beatmap, isForCurrentRuleset)
        {
            Name = "KaraokeEditRulesetContainer";
        }

        /// <summary>
        /// create editable HitObject
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        protected override DrawableHitObject<Lyric> GetVisualRepresentation(Lyric h)
        {
            if (h is Lyric karaokeObject)
            {
                return new DrawableEditableKaraokeObject(karaokeObject);
            }
            return null;
        }

        protected override Playfield CreatePlayfield() => new KaraokeEditPlayfield(Ruleset, WorkingBeatmap, this);
    }
}
