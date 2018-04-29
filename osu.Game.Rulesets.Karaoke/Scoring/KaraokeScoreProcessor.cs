// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Extensions;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Karaoke.Judgements;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Karaoke.Scoring
{
    /// <summary>
    /// Karaoke does not have score i think
    /// </summary>
    internal class KaraokeScoreProcessor : ScoreProcessor<BaseLyric>
    {
        public KaraokeScoreProcessor(RulesetContainer<BaseLyric> rulesetContainer)
            : base(rulesetContainer)
        {
        }

        private float hpDrainRate;

        private readonly Dictionary<HitResult, int> scoreResultCounts = new Dictionary<HitResult, int>();

        protected override void SimulateAutoplay(Beatmap<BaseLyric> beatmap)
        {
            hpDrainRate = beatmap.BeatmapInfo.BaseDifficulty.DrainRate;

            foreach (var obj in beatmap.HitObjects)
            {
                var slider = obj;
                if (slider != null)
                {
                    // Head
                    AddJudgement(new KaraokeJudgement { Result = HitResult.Great });
                }

                AddJudgement(new KaraokeJudgement { Result = HitResult.Great });
            }
        }

        protected override void Reset(bool storeResults)
        {
            base.Reset(storeResults);

            scoreResultCounts.Clear();
        }

        public override void PopulateScore(Score score)
        {
            base.PopulateScore(score);

            score.Statistics[HitResult.Great] = scoreResultCounts.GetOrDefault(HitResult.Great);
            score.Statistics[HitResult.Good] = scoreResultCounts.GetOrDefault(HitResult.Good);
            score.Statistics[HitResult.Meh] = scoreResultCounts.GetOrDefault(HitResult.Meh);
            score.Statistics[HitResult.Miss] = scoreResultCounts.GetOrDefault(HitResult.Miss);
        }

        protected override void OnNewJudgement(Judgement judgement)
        {
            base.OnNewJudgement(judgement);

            var osuJudgement = (KaraokeJudgement)judgement;

            if (judgement.Result != HitResult.None)
            {
                scoreResultCounts[judgement.Result] = scoreResultCounts.GetOrDefault(judgement.Result) + 1;
            }

            switch (judgement.Result)
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
