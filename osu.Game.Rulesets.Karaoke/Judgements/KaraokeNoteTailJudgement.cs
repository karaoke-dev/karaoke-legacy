using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Karaoke.Judgements
{
    public class KaraokeNoteTailJudgement : KaraokeJudgement
    {
        /// <summary>
        ///     Whether the hold note has been released too early and shouldn't give full score for the release.
        /// </summary>
        public bool HasBroken;

        protected override int NumericResultFor(HitResult result)
        {
            switch (result)
            {
                default:
                    return base.NumericResultFor(result);
                case HitResult.Great:
                case HitResult.Perfect:
                    return base.NumericResultFor(HasBroken ? HitResult.Good : result);
            }
        }
    }
}
