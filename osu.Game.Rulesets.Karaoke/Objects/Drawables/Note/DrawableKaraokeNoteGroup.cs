using System;
using System.Collections.Generic;
using System.Text;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Judgements;
using osu.Game.Rulesets.Karaoke.UI.Layers.Note;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Note
{
    /// <summary>
    /// list of DrawableLyricNote
    /// </summary>
    public class DrawableKaraokeNoteGroup : DrawableBaseNote<BaseLyric>
    {

        private FillFlowContainer<DrawableLyricNote> listNote;

        /// <summary>
        /// Whether the hold note has been released too early and shouldn't give full score for the release.
        /// </summary>
        private bool hasBroken;

        public DrawableKaraokeNoteGroup(BaseLyric hitObject)
            : base(hitObject)
        {
            RelativeSizeAxes = Axes.Y;

            InternalChildren = new Drawable[]
            {
                listNote = new FillFlowContainer<DrawableLyricNote>
                {
                    Name = "Background",
                    Direction = FillDirection.Horizontal,
                    RelativeSizeAxes = Axes.Both,
                },
            };

            for (int i = 0; i < 10; i++)
            {
                var note = new DrawableLyricNote(KaraokeStage.COLUMN_HEIGHT * i, hitObject);
                note.Width = 100;
                //note.Height = KaraokeStage.COLUMN_HEIGHT;
                //note.Y = KaraokeStage.COLUMN_HEIGHT * i;
                listNote.Add(note);
            }
        }

        protected override void UpdateState(ArmedState state)
        {
            switch (state)
            {
                //case ArmedState.Hit:
                // Good enough for now, we just want them to have a lifetime end
                //    this.Delay(2000).Expire();
                //    break;
            }
        }

        protected override void CheckForJudgements(bool userTriggered, double timeOffset)
        {
            AddJudgement(new KaraokeJudgement { Result = HitResult.Perfect });
        }
    }
}
