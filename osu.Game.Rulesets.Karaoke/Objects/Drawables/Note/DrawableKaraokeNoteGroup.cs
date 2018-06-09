using System;
using System.Collections.Generic;
using System.Text;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Judgements;
using osu.Game.Rulesets.Karaoke.UI;
using osu.Game.Rulesets.Karaoke.UI.Layers.Note;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Note
{
    /// <summary>
    /// list of DrawableLyricNote
    /// </summary>
    public class DrawableKaraokeNoteGroup : DrawableBaseNote<BaseLyric>
    {

        private FillFlowContainer<DrawableLyricNote> listNote;
        private float _lastWidth;

        public BindableDouble NoteSpeed = new BindableDouble();


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

            foreach (var timeline in hitObject.TimeLines)
            {
                var noteHeight = (timeline.Value.Tone ?? 0) * KaraokeStage.COLUMN_HEIGHT;
                var note = new DrawableLyricNote(noteHeight, hitObject);

                listNote.Add(note);
            }

        }

        
        protected override void Update()
        {
            base.Update();

            //means width channged
            if (Math.Abs(_lastWidth - DrawWidth) > 0)
            {
                _lastWidth = DrawWidth;
                foreach (var note in listNote)
                {
                    note.Width = (float)(NoteSpeed.Value * note.Duration / 1000);
                }
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

        public override Color4 AccentColour
        {
            set
            {
                foreach (var single in listNote)
                {
                    single.AccentColour = value;
                }
            }
        }
    }
}
