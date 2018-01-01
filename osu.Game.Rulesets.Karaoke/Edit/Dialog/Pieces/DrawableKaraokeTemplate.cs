// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Lines;
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

        protected Container SegmentedControlContainer = new Container();
        //don't update by time
        public override bool ProgressUpdateByTime => false;

        public DrawableKaraokeTemplate(KaraokeObject hitObject, KaraokeTemplate template)
            : base(hitObject)
        {
            Template = template;

            Add(SegmentedControlContainer);
        }

        /// <summary>
        /// update drawable
        /// </summary>
        protected override void UpdateDrawable()
        {
            base.UpdateDrawable();

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

            SegmentedControlContainer.Clear();

            //Get all start Position
            Vector2 subTextSegmentedControlStartPosition =  TextsAndMaskPiece.SubKaraokeText.Position;
            Vector2 mainTextSegmentedControlStartPosition = TextsAndMaskPiece.MainKaraokeText.Position;
            Vector2 translateTextSegmentedControlStartPosition = TranslateText.Position;
            Vector2 subTextToMainTextSegmentedControlStartPosition = (subTextSegmentedControlStartPosition + mainTextSegmentedControlStartPosition) /2;
            Vector2 mainTextToTranslateTextSegmentedControlStartPosition = (mainTextSegmentedControlStartPosition + translateTextSegmentedControlStartPosition/2);
            
            //1. get position (mainText and subText and translate text position)
            Vector2 subTextSegmentedControlEndPosition = subTextSegmentedControlStartPosition + new Vector2(100, -50);
            Vector2 subTextToMainTextSegmentedControlEndPosition = subTextToMainTextSegmentedControlStartPosition + new Vector2(100, -50);
            Vector2 mainTextSegmentedControlEndPosition = mainTextSegmentedControlStartPosition + new Vector2(100, -50);
            Vector2 mainTextToTranslateTextSegmentedControlEndPosition = mainTextToTranslateTextSegmentedControlStartPosition + new Vector2(100, -50);
            Vector2 translateTextSegmentedControlEndPosition = translateTextSegmentedControlStartPosition + new Vector2(100, -50);
            //2. update position
            SubTextSegmentedControl.Position = subTextSegmentedControlEndPosition;
            SubTextToMainTextSegmentedControl.Position = subTextToMainTextSegmentedControlEndPosition;
            MainTextSegmentedControl.Position = mainTextSegmentedControlEndPosition;
            MainTextToTranslateTextSegmentedControl.Position = mainTextToTranslateTextSegmentedControlEndPosition;
            TranslateTextSegmentedControl.Position = translateTextSegmentedControlEndPosition;
            //3. draw line (Zero position,)
            SegmentedControlContainer.Add(new Path()
            {
                PathWidth =1,
                Positions = new List<Vector2>()
                {
                    subTextSegmentedControlStartPosition, subTextSegmentedControlEndPosition
                }
            });
            SegmentedControlContainer.Add(new Path()
            {
                PathWidth = 1,
                Positions = new List<Vector2>()
                {
                    subTextToMainTextSegmentedControlStartPosition, subTextToMainTextSegmentedControlEndPosition
                }
            });
            SegmentedControlContainer.Add(new Path()
            {
                PathWidth = 1,
                Positions = new List<Vector2>()
                {
                    mainTextSegmentedControlStartPosition, mainTextSegmentedControlEndPosition
                }
            });
            SegmentedControlContainer.Add(new Path()
            {
                PathWidth = 1,
                Positions = new List<Vector2>()
                {
                    mainTextToTranslateTextSegmentedControlStartPosition, mainTextToTranslateTextSegmentedControlEndPosition
                }
            });
            SegmentedControlContainer.Add(new Path()
            {
                PathWidth = 1,
                Positions = new List<Vector2>()
                {
                    translateTextSegmentedControlStartPosition, translateTextSegmentedControlEndPosition
                }
            });
            //4. add drawable
            SegmentedControlContainer.Add(SubTextSegmentedControl);
            SegmentedControlContainer.Add(SubTextToMainTextSegmentedControl);
            SegmentedControlContainer.Add(MainTextSegmentedControl);
            SegmentedControlContainer.Add(MainTextToTranslateTextSegmentedControl);
            SegmentedControlContainer.Add(TranslateTextSegmentedControl);
        }
    }
}
