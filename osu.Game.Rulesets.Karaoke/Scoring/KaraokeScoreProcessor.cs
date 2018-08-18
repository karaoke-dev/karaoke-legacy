// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Extensions;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Karaoke.Scoring
{
    /// <summary>
    ///     Karaoke does not have score i think
    /// </summary>
    internal class KaraokeScoreProcessor : ScoreProcessor<BaseLyric>
    {
        private readonly Dictionary<HitResult, int> scoreResultCounts = new Dictionary<HitResult, int>();

        private float hpDrainRate;

        public KaraokeScoreProcessor(RulesetContainer<BaseLyric> rulesetContainer)
            : base(rulesetContainer)
        {
        }

        public override void PopulateScore(Score score)
        {
            base.PopulateScore(score);

            score.Statistics[HitResult.Great] = scoreResultCounts.GetOrDefault(HitResult.Great);
            score.Statistics[HitResult.Good] = scoreResultCounts.GetOrDefault(HitResult.Good);
            score.Statistics[HitResult.Meh] = scoreResultCounts.GetOrDefault(HitResult.Meh);
            score.Statistics[HitResult.Miss] = scoreResultCounts.GetOrDefault(HitResult.Miss);
        }

        protected override void SimulateAutoplay(Beatmap<BaseLyric> beatmap)
        {
            while (true)
            {
                base.SimulateAutoplay(beatmap);

                if (!HasFailed)
                    break;

                Reset(false);
            }
        }

        protected override void Reset(bool storeResults)
        {
            base.Reset(storeResults);

            scoreResultCounts.Clear();
        }

        protected override void ApplyResult(JudgementResult result)
        {
            base.ApplyResult(result);

            if (result.Type != HitResult.None)
                scoreResultCounts[result.Type] = scoreResultCounts.GetOrDefault(result.Type) + 1;

            switch (result.Type)
            {
                case HitResult.Great:
                    Health.Value += (10.2 - hpDrainRate) * 0.02;
                    break;

                case HitResult.Good:
                    Health.Value += (8 - hpDrainRate) * 0.02;
                    break;

                case HitResult.Meh:
                    Health.Value += (4 - hpDrainRate) * 0.02;
                    break;

                /*case HitResult.SliderTick:
                    Health.Value += Math.Max(7 - hpDrainRate, 0) * 0.01;
                    break;*/

                case HitResult.Miss:
                    Health.Value -= hpDrainRate * 0.04;
                    break;
            }

            //In Karaoke mode ,HP will always 1 
            Health.Value = 1;
        }
    }
}
