// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
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
            //foreach (OsuHitObject h in Objects)
            //    (h as Slider)?.Curve?.Calculate();
        }

        public override double Calculate(Dictionary<string, string> categoryDifficulty = null)
        {
            //TODO : implement
            return 1.5;

            /*
            OsuDifficultyBeatmap beatmap = new OsuDifficultyBeatmap(Objects);
            Skill[] skills =
            {
                new Aim(),
                new Speed()
            };

            double sectionEnd = section_length / TimeRate;
            foreach (OsuDifficultyHitObject h in beatmap)
            {
                while (h.BaseObject.StartTime > sectionEnd)
                {
                    foreach (Skill s in skills)
                    {
                        s.SaveCurrentPeak();
                        s.StartNewSectionFrom(sectionEnd);
                    }

                    sectionEnd += section_length;
                }

                foreach (Skill s in skills)
                    s.Process(h);
            }

            double aimRating = Math.Sqrt(skills[0].DifficultyValue()) * difficulty_multiplier;
            double speedRating = Math.Sqrt(skills[1].DifficultyValue()) * difficulty_multiplier;

            double starRating = aimRating + speedRating + Math.Abs(aimRating - speedRating) / 2;

            if (categoryDifficulty != null)
            {
                categoryDifficulty.Add("Aim", aimRating.ToString("0.00"));
                categoryDifficulty.Add("Speed", speedRating.ToString("0.00"));
            }

            return starRating;
            */
        }

        protected override BeatmapConverter<KaraokeObject> CreateBeatmapConverter(Beatmap beatmap) => new KaraokeBeatmapConverter();
    }
}
