// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Common.Pieces;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Pieces
{
    public class EditableMainKaraokeText : MainKaraokeText
    {
        public int? HoverIndex { get; set; }
        public int? StartSelectIndex { get; set; }
        public int? EndSelectIndex { get; set; }

        public Color4 HoverColor { get; set; } = Color4.Red;
        public Color4 SelectColor { get; set; } = Color4.Purple;

        public EditableMainKaraokeText(FormattedText textObject, Dictionary<int, TextComponent> mainText)
            : base(textObject, mainText)
        {
        }

        protected override void Update()
        {
            base.Update();
            foreach (var single in Children)
            {
                single.Colour = new Color4(0f, 0f, 0f, 0f);
                single.Alpha = 0.001f;
            }

            try
            {
                if (HoverIndex != null)
                {
                    Children[HoverIndex.Value].Colour = HoverColor;
                    Children[HoverIndex.Value].Alpha = 1;
                }

                if (StartSelectIndex != null)
                {
                    if (EndSelectIndex != null)
                    {
                        if (StartSelectIndex <= EndSelectIndex)
                        {
                            for (int i = StartSelectIndex.Value; i <= EndSelectIndex; i++)
                            {
                                Children[i].Colour = SelectColor;
                                Children[i].Alpha = 1;
                            }
                        }
                        else
                        {
                            for (int i = EndSelectIndex.Value; i <= StartSelectIndex; i++)
                            {
                                Children[i].Colour = SelectColor;
                                Children[i].Alpha = 1;
                            }
                        }
                    }
                    else
                    {
                        Children[StartSelectIndex.Value].Colour = SelectColor;
                        Children[StartSelectIndex.Value].Alpha = 1;
                    }
                }
            }
            catch
            {
            }
        }
    }
}
