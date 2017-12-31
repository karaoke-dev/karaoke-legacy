// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Framework.Graphics.Primitives;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces
{
    /// <summary>
    /// use to show the size and spacing of the template / single Karaoke object
    /// </summary>
    public class DrawableKaraokeTemplate : DrawableKaraokeObject
    {
        protected UpDownValueIndicator SubTextSegmentedControl;
        protected UpDownValueIndicator SubTextToMainTextSegmentedControl;
        protected UpDownValueIndicator MainTextSegmentedControl;
        protected UpDownValueIndicator MainTextToTranslateTextSegmentedControl;
        protected UpDownValueIndicator TranslateTextSegmentedControl;


        //don't update by time
        public override bool ProgressUpdateByTime => false;

        public DrawableKaraokeTemplate(KaraokeObject hitObject, KaraokeTemplate template)
            : base(hitObject)
        {
            Template = template;
            SubTextSegmentedControl = new UpDownValueIndicator()
            {
                PostfixText = "px",
                OnValueChanged = (newValue) =>
                {
                    UpdateDrawable();
                },
            };
            SubTextToMainTextSegmentedControl = new UpDownValueIndicator()
            {
                PostfixText = "px",
                OnValueChanged = (newValue) =>
                {
                    UpdateDrawable();
                },
            };
            MainTextSegmentedControl = new UpDownValueIndicator()
            {
                PostfixText = "px",
                OnValueChanged = (newValue) =>
                {
                    UpdateDrawable();
                },
            };
            MainTextToTranslateTextSegmentedControl = new UpDownValueIndicator()
            {
                PostfixText = "px",
                OnValueChanged = (newValue) =>
                {
                    UpdateDrawable();
                },
            };
            TranslateTextSegmentedControl = new UpDownValueIndicator()
            {
                PostfixText = "px",
                OnValueChanged = (newValue) =>
                {
                    UpdateDrawable();
                },
            };
        }

        /// <summary>
        /// update drawable
        /// </summary>
        protected override void UpdateDrawable()
        {
            base.UpdateDrawable();
            //Get all start Position
            Vector2 subTextSegmentedControlStartPosition = TextsAndMaskPiece.SubKaraokeText.Position;
            Vector2 subTextToMainTextSegmentedControlStartPosition = TextsAndMaskPiece.SubKaraokeText.Position;
            Vector2 mainTextSegmentedControlStartPosition = TextsAndMaskPiece.SubKaraokeText.Position;
            Vector2 mainTextToTranslateTextSegmentedControlStartPosition = TextsAndMaskPiece.SubKaraokeText.Position;
            Vector2 translateTextSegmentedControlStartPosition = TextsAndMaskPiece.SubKaraokeText.Position;
            //1. get position (mainText and subText and translate text position)
            Vector2 subTextSegmentedControlEndPosition = TextsAndMaskPiece.SubKaraokeText.Position + new Vector2(100, -50);
            Vector2 subTextToMainTextSegmentedControlEndPosition = TextsAndMaskPiece.SubKaraokeText.Position + new Vector2(100, -50);
            Vector2 mainTextSegmentedControlEndPosition = TextsAndMaskPiece.SubKaraokeText.Position + new Vector2(100, -50);
            Vector2 mainTextToTranslateTextSegmentedControlEndPosition = TextsAndMaskPiece.SubKaraokeText.Position + new Vector2(100, -50);
            Vector2 translateTextSegmentedControlEndPosition = TextsAndMaskPiece.SubKaraokeText.Position + new Vector2(100, -50);
            //2. update position
            SubTextSegmentedControl.Position = subTextSegmentedControlEndPosition;
            SubTextToMainTextSegmentedControl.Position = subTextToMainTextSegmentedControlEndPosition;
            MainTextSegmentedControl.Position = mainTextSegmentedControlEndPosition;
            MainTextToTranslateTextSegmentedControl.Position = mainTextToTranslateTextSegmentedControlEndPosition;
            TranslateTextSegmentedControl.Position = translateTextSegmentedControlEndPosition;
            //3. draw line (Zero position,)
            Add(new Line());
            //4. add drawable
            Add(SubTextSegmentedControl);
            Add(SubTextToMainTextSegmentedControl);
            Add(MainTextSegmentedControl);
            Add(MainTextToTranslateTextSegmentedControl);
            Add(TranslateTextSegmentedControl);
        }
    }
}
