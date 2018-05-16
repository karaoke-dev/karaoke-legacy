using System;
using System.Collections.Generic;
using System.Text;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects.Drawables;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Note
{
    public class DrawableNoteJudgement : DrawableJudgement
    {
        public DrawableNoteJudgement(Judgement judgement, DrawableHitObject judgedObject)
            : base(judgement, judgedObject)
        {
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            if (JudgementText != null)
                JudgementText.TextSize = 25;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            this.FadeInFromZero(50, Easing.OutQuint);

            if (Judgement.IsHit)
            {
                this.ScaleTo(0.8f);
                this.ScaleTo(1, 250, Easing.OutElastic);

                this.Delay(50).FadeOut(200).ScaleTo(0.75f, 250);
            }

            Expire();
        }
    }
}
