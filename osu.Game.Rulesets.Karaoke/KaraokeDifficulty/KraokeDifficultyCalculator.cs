// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Beatmaps;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Karaoke.Difficulty;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Karaoke.KaraokeDifficulty
{
    public class KaraokeDifficultyCalculator : DifficultyCalculator
    {
        private const int section_length = 400;
        private const double difficulty_multiplier = 0.0675;

        public KaraokeDifficultyCalculator(Ruleset ruleset, WorkingBeatmap beatmap)
            : base(ruleset, beatmap)
        {
        }

        protected override DifficultyAttributes Calculate(IBeatmap beatmap, Mod[] mods, double timeRate)
        {
            //TODO : implement
            return new KaraokeDifficultyAttributes(mods, timeRate)
            {
            };
        }

        /*
        protected override BeatmapConverter<BaseLyric> CreateBeatmapConverter(Beatmap beatmap) => new KaraokeBeatmapConverter();
        */
    }
}
