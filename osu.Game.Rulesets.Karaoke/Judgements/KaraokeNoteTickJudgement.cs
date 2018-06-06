using System;
using System.Collections.Generic;
using System.Text;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Karaoke.Judgements
{
    public class KaraokeNoteTickJudgement : KaraokeJudgement
    {
        public override bool AffectsCombo => false;

        protected override int NumericResultFor(HitResult result) => 20;
    }
}
