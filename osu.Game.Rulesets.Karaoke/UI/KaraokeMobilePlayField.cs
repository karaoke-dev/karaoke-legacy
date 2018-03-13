// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Beatmaps;

namespace osu.Game.Rulesets.Karaoke.UI
{
    /// <summary>
    /// Add Mobile PlayField support
    /// </summary>
    public class KaraokeMobilePlayField : KaraokePlayfield
    {
        public KaraokeMobilePlayField(Ruleset ruleset, WorkingBeatmap beatmap, KaraokeRulesetContainer container)
            : base(ruleset, beatmap, container)
        {
        }
    }
}
