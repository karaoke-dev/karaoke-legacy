// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Karaoke.UI.Panel.Pieces;

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

        public float Value { get; set; }
        public float Step { get; set; } = 1;

        public string PrefixText { get; set; }
        public string PostfixText { get; set; }

        public UpDownValueIndicator()
        {
            Add(FillFlowContainer = new FillFlowContainer<Drawable>()
            {
                Children = new Drawable[]
                {
                    OsuSpriteText = new OsuSpriteText()
                    {
                        Origin = Anchor.CentreRight,
                        Anchor = Anchor.CentreLeft,
                        Width = 70,
                    },
                    DecreaseButton = new KaraokeButton()
                    {
                        //Position = new Vector2(-10, 0),
                        Origin = Anchor.CentreRight,
                        Anchor = Anchor.CentreLeft,
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
                        Origin = Anchor.CentreLeft,
                        Anchor = Anchor.CentreRight,
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
