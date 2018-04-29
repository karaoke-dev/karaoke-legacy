// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Desktop.Pieces;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces
{
    /// <summary>
    /// will show : 
    /// label / (+)button / (-)button
    /// </summary>
    public class UpDownValueIndicator : Container
    {
        public Action<float> OnValueChanged { get; set; }

        /// <summary>
        /// Decrease Button
        /// </summary>
        public KaraokeButton DecreaseButton;

        /// <summary>
        /// Increase button
        /// </summary>
        public KaraokeButton IncreaseButton;

        /// <summary>
        /// label
        /// </summary>
        public OsuSpriteText OsuSpriteText;

        protected FillFlowContainer<Drawable> FillFlowContainer;

        protected int ButtonZixe = 25;

        private float _value;

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                updateText();
            }
        }

        public float Step { get; set; } = 1;

        private string _prefixText;

        public string PrefixText
        {
            get => _prefixText;
            set
            {
                _prefixText = value;
                updateText();
            }
        }

        private string _postText;

        public string PostfixText
        {
            get => _postText;
            set
            {
                _postText = value;
                updateText();
            }
        }

        public UpDownValueIndicator()
        {
            Width = 100;
            Height = 30;

            Add(new Box()
            {
                RelativeSizeAxes = Axes.Both,
                Colour = new Color4(0.0f, 0.0f, 0.0f, 0.5f)
            });


            Add(FillFlowContainer = new FillFlowContainer<Drawable>()
            {
                Direction = FillDirection.Horizontal,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    OsuSpriteText = new OsuSpriteText()
                    {
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                        //Width = 70,
                    },
                    DecreaseButton = new KaraokeButton()
                    {
                        //Position = new Vector2(-10, 0),
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                        Width = ButtonZixe,
                        Height = ButtonZixe,
                        Text = "-",
                        TooltipText = "Decrease",
                        Action = () =>
                        {
                            Value = Value - Step;
                            updateText();
                            OnValueChanged?.Invoke(Value);
                        }
                    },
                    IncreaseButton = new KaraokeButton()
                    {
                        //Position = new Vector2(10, 0),
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                        Width = ButtonZixe,
                        Height = ButtonZixe,
                        Text = "+",
                        TooltipText = "Increase",
                        Action = () =>
                        {
                            Value = Value + Step;
                            updateText();
                            OnValueChanged?.Invoke(Value);
                        }
                    }
                }
            });

            updateText();
        }

        private void updateText()
        {
            OsuSpriteText.Text = PrefixText + Value.ToString() + PostfixText;
        }
    }
}
