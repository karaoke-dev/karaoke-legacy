﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

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
        }

        /// <summary>
        /// create editable HitObject
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        protected override DrawableHitObject<KaraokeObject> GetVisualRepresentation(KaraokeObject h)
        {
            if (h is KaraokeObject karaokeObject)
            {
                return new DrawableEditableKaraokeObject(karaokeObject);
            }
            return null;
        }

        protected override Playfield CreatePlayfield() => new KaraokeEditPlayfield(Ruleset, WorkingBeatmap, this);
    }
}
