using System;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Judgements;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Note
{
    public class DrawableLyricNoteGroup : DrawableLyricNoteGroup<DrawableLyricNote>
    {
        public DrawableLyricNoteGroup(BaseLyric hitObject)
            : base(hitObject)
        {
        }
    }

    /// <summary>
    ///     list of DrawableLyricNote
    /// </summary>
    public class DrawableLyricNoteGroup<T> : DrawableBaseNote<BaseLyric> where T : DrawableLyricNote, new()
    {
        public BindableDouble NoteSpeed = new BindableDouble();

        public override Color4 AccentColour
        {
            set
            {
                foreach (var single in ListNote)
                    single.AccentColour = value;
            }
        }

        protected FillFlowContainer<T> ListNote;
        private float _lastWidth;

        public DrawableLyricNoteGroup(BaseLyric hitObject)
            : base(hitObject)
        {
            RelativeSizeAxes = Axes.Y;

            InternalChildren = new Drawable[]
            {
                ListNote = new FillFlowContainer<T>
                {
                    Name = "Background",
                    Direction = FillDirection.Horizontal,
                    RelativeSizeAxes = Axes.Both
                }
            };

            //initial note
            InitialNote();
        }

        protected virtual void InitialNote()
        {
            foreach (var timeline in HitObject.TimeLines)
            {
                var note = new T
                {
                    HitObject = HitObject,
                    TimeLine = timeline
                };
                ListNote.Add(note);
            }
        }

        protected override void Update()
        {
            base.Update();

            //means width changed
            if (Math.Abs(_lastWidth - DrawWidth) > 0)
            {
                _lastWidth = DrawWidth;
                foreach (var note in ListNote)
                {
                    var precentage = note.Duration / HitObject.Duration;
                    note.Width = (float)(_lastWidth * precentage);
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
    }
}
