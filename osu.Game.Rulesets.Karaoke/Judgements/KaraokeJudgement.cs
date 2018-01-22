// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Scoring;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Judgements
{
    public class KaraokeJudgement : Judgement
    {
        public override HitResult MaxResult => HitResult.Great;

        /// <summary>
        /// The positional hit offset.
        /// </summary>
        public Vector2 PositionOffset;

        protected override int NumericResultFor(HitResult result)
        {
            switch (result)
            {
                default:
                    return 0;
                case HitResult.Meh:
                    return 50;
                case HitResult.Good:
                    return 100;
                case HitResult.Great:
                    return 300;
            }
        }
    }
}
