﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Karaoke.Beatmaps;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Karaoke.KaraokeDifficulty
{
    public class KaraokeDifficultyCalculator : DifficultyCalculator<KaraokeObject>
    {
        private const int section_length = 400;
        private const double difficulty_multiplier = 0.0675;

        public KaraokeDifficultyCalculator(Beatmap beatmap, Mod[] mods = null)
            : base(beatmap)
        {
        }

        public KaraokeDifficultyCalculator(Beatmap beatmap)
          : base(beatmap)
        {
        }

        protected override void PreprocessHitObjects()
        {
           
        }

        public override double Calculate(Dictionary<string, double> categoryDifficulty = null)
        {
            //TODO : implement
            return 1.5;
        }

        protected override BeatmapConverter<KaraokeObject> CreateBeatmapConverter(Beatmap beatmap) => new KaraokeBeatmapConverter();
    }
}
