// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Lines;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces
{
    /// <summary>
    /// use to show the size and spacing of the template / single Karaoke object
    /// </summary>
    public class DrawableKaraokeTemplate : DrawableLyric
    {
        //single part
        protected UpDownValueIndicator SubTextSegmentedControl;

        protected UpDownValueIndicator SubTextToMainTextSegmentedControl;
        protected UpDownValueIndicator MainTextSegmentedControl;
        protected UpDownValueIndicator MainTextToTranslateTextSegmentedControl;
        protected UpDownValueIndicator TranslateTextSegmentedControl;

        //line
        protected Path SubTextPath;

        protected Path SubTextToMainTextPath;
        protected Path MainTextPath;
        protected Path MainTextToTranslateTextPath;
        protected Path TranslateTextPath;

        //Scale
        protected UpDownValueIndicator ScaleSegmentedControl;

        protected Container SegmentedControlContainer;

        //don't update by time
        public override bool ProgressUpdateByTime => false;

        public DrawableKaraokeTemplate(Lyric hitObject)
            : base(hitObject)
        {
            var templateValue = Template.Value;
            SegmentedControlContainer = new Container()
            {
                Children = new Drawable[]
                {
                    SubTextPath = new Path() { PathWidth = 1 },
                    SubTextToMainTextPath = new Path() { PathWidth = 1 },
                    MainTextPath = new Path() { PathWidth = 1 },
                    MainTextToTranslateTextPath = new Path() { PathWidth = 1 },
                    TranslateTextPath = new Path() { PathWidth = 1 },

                    SubTextSegmentedControl = new UpDownValueIndicator()
                    {
                        Origin = Anchor.BottomLeft,
                        Value = templateValue.TopText.FontSize ?? 0,
                        PostfixText = "px",
                        OnValueChanged = (newValue) =>
                        {
                            templateValue.TopText.FontSize = (int)newValue;
                            UpdateDrawable();
                            Template.TriggerChange();
                        },
                    },
                    SubTextToMainTextSegmentedControl = new UpDownValueIndicator()
                    {
                        Origin = Anchor.BottomRight,
                        Value = templateValue.TopText.Position.Y,
                        PostfixText = "px",
                        OnValueChanged = (newValue) =>
                        {
                            templateValue.TopText.Position = new Vector2(0, newValue);
                            UpdateDrawable();
                            Template.TriggerChange();
                        },
                    },
                    MainTextSegmentedControl = new UpDownValueIndicator()
                    {
                        Origin = Anchor.BottomLeft,
                        Value = templateValue.MainText.FontSize ?? 0,
                        PostfixText = "px",
                        OnValueChanged = (newValue) =>
                        {
                            templateValue.MainText.FontSize = (int)newValue;
                            UpdateDrawable();
                            Template.TriggerChange();
                        },
                    },
                    MainTextToTranslateTextSegmentedControl = new UpDownValueIndicator()
                    {
                        Origin = Anchor.BottomRight,
                        Value = templateValue.TranslateText.Position.Y,
                        PostfixText = "px",
                        OnValueChanged = (newValue) =>
                        {
                            templateValue.TranslateText.Position = new Vector2(0, newValue);
                            UpdateDrawable();
                            Template.TriggerChange();
                        },
                    },
                    TranslateTextSegmentedControl = new UpDownValueIndicator()
                    {
                        Origin = Anchor.BottomLeft,
                        Value = templateValue.TranslateText.FontSize ?? 0,
                        PostfixText = "px",
                        OnValueChanged = (newValue) =>
                        {
                            templateValue.TranslateText.FontSize = (int)newValue;
                            UpdateDrawable();
                            Template.TriggerChange();
                        },
                    },
                    ScaleSegmentedControl = new UpDownValueIndicator()
                    {
                        Origin = Anchor.BottomCentre,
                        Step = 0.1f,
                        Position = new Vector2(50, 150),
                        Value = templateValue.Scale,
                        PostfixText = "x",
                        OnValueChanged = (newValue) =>
                        {
                            templateValue.Scale = newValue;
                            UpdateDrawable();
                            Template.TriggerChange();
                        },
                    },
                }
            };

            AddInternal(SegmentedControlContainer);
        }

        protected override void UpdateValue()
        {
            base.UpdateValue();

            if (SubTextSegmentedControl == null)
                return;

            var templateValue = Template.Value;

            //1. Get all start Position
            Vector2 subTextSegmentedControlStartPosition = new Vector2(TextsAndMaskPiece.SubTexts.Last().Position.X + 20, TextsAndMaskPiece.SubText.Position.Y);
            Vector2 mainTextSegmentedControlStartPosition = new Vector2(TextsAndMaskPiece.MainText.TotalWidth, TextsAndMaskPiece.MainText.Position.Y);
            Vector2 translateTextSegmentedControlStartPosition = new Vector2(TranslateText.Width, TranslateText.Position.Y);
            Vector2 subTextToMainTextSegmentedControlStartPosition = new Vector2(-10, (subTextSegmentedControlStartPosition.Y + mainTextSegmentedControlStartPosition.Y) / 2);
            Vector2 mainTextToTranslateTextSegmentedControlStartPosition = new Vector2(-10, (mainTextSegmentedControlStartPosition.Y + translateTextSegmentedControlStartPosition.Y) / 2);

            //2. get all end position (s mainText and subText and translate text position)
            Vector2 subTextSegmentedControlEndPosition = subTextSegmentedControlStartPosition + new Vector2(100, -50) / templateValue.Scale;
            Vector2 subTextToMainTextSegmentedControlEndPosition = subTextToMainTextSegmentedControlStartPosition + new Vector2(-50, -50) / templateValue.Scale;
            Vector2 mainTextSegmentedControlEndPosition = mainTextSegmentedControlStartPosition + new Vector2(100, -10) / templateValue.Scale;
            Vector2 mainTextToTranslateTextSegmentedControlEndPosition = mainTextToTranslateTextSegmentedControlStartPosition + new Vector2(-50, 10) / templateValue.Scale;
            Vector2 translateTextSegmentedControlEndPosition = translateTextSegmentedControlStartPosition + new Vector2(100, 30) / templateValue.Scale;

            //3. update position
            SubTextSegmentedControl.Position = subTextSegmentedControlEndPosition;
            SubTextToMainTextSegmentedControl.Position = subTextToMainTextSegmentedControlEndPosition;
            MainTextSegmentedControl.Position = mainTextSegmentedControlEndPosition;
            MainTextToTranslateTextSegmentedControl.Position = mainTextToTranslateTextSegmentedControlEndPosition;
            TranslateTextSegmentedControl.Position = translateTextSegmentedControlEndPosition;

            //4.update path positions
            drawLine(SubTextPath, subTextSegmentedControlStartPosition, subTextSegmentedControlEndPosition);
            drawLine(SubTextToMainTextPath, subTextToMainTextSegmentedControlStartPosition, subTextToMainTextSegmentedControlEndPosition);
            drawLine(MainTextPath, mainTextSegmentedControlStartPosition, mainTextSegmentedControlEndPosition);
            drawLine(MainTextToTranslateTextPath, mainTextToTranslateTextSegmentedControlStartPosition, mainTextToTranslateTextSegmentedControlEndPosition);
            drawLine(TranslateTextPath, translateTextSegmentedControlStartPosition, translateTextSegmentedControlEndPosition);

            void drawLine(Path line, Vector2 path1, Vector2 path2)
            {
                var x = Math.Max(0, path1.X - path2.X);
                var y = Math.Max(0, path1.Y - path2.Y);
                line.Position = path1 - new Vector2(x, y);
                line.Positions = new List<Vector2>()
                {
                    new Vector2(),
                    path2 - path1,
                };
            }
        }
    }
}
