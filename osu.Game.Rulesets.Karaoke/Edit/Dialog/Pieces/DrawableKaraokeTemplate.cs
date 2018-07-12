// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Lines;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces
{
    /// <summary>
    ///     use to show the size and spacing of the template / single Karaoke object
    /// </summary>
    public class DrawableKaraokeTemplate : DrawableLyric
    {
        //don't update by time
        public override bool ProgressUpdateByTime => false;

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

        public DrawableKaraokeTemplate(BaseLyric hitObject)
            : base(hitObject)
        {
            var templateValue = Template.Value?.Clone() as LyricTemplate;
            SegmentedControlContainer = new Container
            {
                Children = new Drawable[]
                {
                    SubTextPath = new Path { PathWidth = 1 },
                    SubTextToMainTextPath = new Path { PathWidth = 1 },
                    MainTextPath = new Path { PathWidth = 1 },
                    MainTextToTranslateTextPath = new Path { PathWidth = 1 },
                    TranslateTextPath = new Path { PathWidth = 1 },

                    SubTextSegmentedControl = new UpDownValueIndicator
                    {
                        Origin = Anchor.BottomLeft,
                        Value = templateValue.TopText.FontSize ?? 0,
                        PostfixText = "px",
                        OnValueChanged = newValue =>
                        {
                            var value = Template.Value?.Clone() as LyricTemplate;
                            value.TopText.FontSize = (int)newValue;
                            Template.Value = value;
                        }
                    },
                    SubTextToMainTextSegmentedControl = new UpDownValueIndicator
                    {
                        Origin = Anchor.BottomRight,
                        Value = templateValue.TopText.Position.Y,
                        PostfixText = "px",
                        OnValueChanged = newValue =>
                        {
                            var value = Template.Value?.Clone() as LyricTemplate;
                            value.TopText.Position = new Vector2(0, newValue);
                            Template.Value = value;
                        }
                    },
                    MainTextSegmentedControl = new UpDownValueIndicator
                    {
                        Origin = Anchor.BottomLeft,
                        Value = templateValue.MainText.FontSize ?? 0,
                        PostfixText = "px",
                        OnValueChanged = newValue =>
                        {
                            var value = Template.Value?.Clone() as LyricTemplate;
                            value.MainText.FontSize = (int)newValue;
                            Template.Value = value;
                        }
                    },
                    MainTextToTranslateTextSegmentedControl = new UpDownValueIndicator
                    {
                        Origin = Anchor.BottomRight,
                        Value = templateValue.TranslateText.Position.Y,
                        PostfixText = "px",
                        OnValueChanged = newValue =>
                        {
                            var value = Template.Value?.Clone() as LyricTemplate;
                            value.TranslateText.Position = new Vector2(0, newValue);
                            UpdateDrawable();
                            Template.Value = value;
                        }
                    },
                    TranslateTextSegmentedControl = new UpDownValueIndicator
                    {
                        Origin = Anchor.BottomLeft,
                        Value = templateValue.TranslateText.FontSize ?? 0,
                        PostfixText = "px",
                        OnValueChanged = newValue =>
                        {
                            var value = Template.Value?.Clone() as LyricTemplate;
                            value.TranslateText.FontSize = (int)newValue;
                            Template.Value = value;
                        }
                    },
                    ScaleSegmentedControl = new UpDownValueIndicator
                    {
                        Origin = Anchor.BottomCentre,
                        Step = 0.1f,
                        Position = new Vector2(50, 150),
                        Value = templateValue.Scale,
                        PostfixText = "x",
                        OnValueChanged = newValue =>
                        {
                            var value = Template.Value?.Clone() as LyricTemplate;
                            value.Scale = newValue;
                            Template.Value = value;
                        }
                    }
                }
            };

            /*
            Template.ValueChanged += (b) =>
            {
                UpdateDrawable();
            };
            */

            AddInternal(SegmentedControlContainer);
        }

        protected override void UpdateValue()
        {
            base.UpdateValue();

            //TODO
            /*
            if (SubTextSegmentedControl == null)
                return;

            var templateValue = Template.Value;

            //Lyric
            var mainText = RightSideText.LyricText;
            var subTexts = RightSideText.ListDrawableSubText;
            var subText = subTexts?.FirstOrDefault();

            if (subText != null)
            {
                //1. Get all start Position
                var subTextSegmentedControlStartPosition = new Vector2(subTexts.Last().Position.X + 20, subText.Position.Y);
                var mainTextSegmentedControlStartPosition = new Vector2(mainText.TotalWidth, mainText.Position.Y);
                var translateTextSegmentedControlStartPosition = new Vector2(TranslateText.Width, TranslateText.Position.Y);
                var subTextToMainTextSegmentedControlStartPosition = new Vector2(-10, (subTextSegmentedControlStartPosition.Y + mainTextSegmentedControlStartPosition.Y) / 2);
                var mainTextToTranslateTextSegmentedControlStartPosition = new Vector2(-10, (mainTextSegmentedControlStartPosition.Y + translateTextSegmentedControlStartPosition.Y) / 2);

                //2. get all end position (s mainText and subText and translate text position)
                var subTextSegmentedControlEndPosition = subTextSegmentedControlStartPosition + new Vector2(100, -50) / templateValue.Scale;
                var subTextToMainTextSegmentedControlEndPosition = subTextToMainTextSegmentedControlStartPosition + new Vector2(-50, -50) / templateValue.Scale;
                var mainTextSegmentedControlEndPosition = mainTextSegmentedControlStartPosition + new Vector2(100, -10) / templateValue.Scale;
                var mainTextToTranslateTextSegmentedControlEndPosition = mainTextToTranslateTextSegmentedControlStartPosition + new Vector2(-50, 10) / templateValue.Scale;
                var translateTextSegmentedControlEndPosition = translateTextSegmentedControlStartPosition + new Vector2(100, 30) / templateValue.Scale;

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
                    line.Positions = new List<Vector2>
                    {
                        new Vector2(),
                        path2 - path1
                    };
                }
            }
            */
        }
    }
}
