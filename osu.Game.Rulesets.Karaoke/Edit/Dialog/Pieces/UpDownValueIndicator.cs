using OpenTK;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.UI.Panel.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Graphics;
using osu.Game.Graphics.Sprites;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces
{
    /// <summary>
    /// will show : 
    /// label / (+)button / (-)button
    /// </summary>
    public class UpDownValueIndicator : Container
    {
        public Action<float> OnValueChanged;

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

        public float PrefixText { get; set; }
        public float PostfixText { get; set; }

        public UpDownValueIndicator()
        {
            Add(FillFlowContainer = new FillFlowContainer<Drawable>()
            {
                Children = new Drawable[]
                {
                    OsuSpriteText=new OsuSpriteText()
                    {
                        Origin = Anchor.CentreRight,
                        Anchor = Anchor.CentreLeft,
                        Width=70,
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
                            UpdateText();
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
                            UpdateText();
                            OnValueChanged?.Invoke(Value);
                        }
                    }
                }
            });
            

            UpdateText();
        }

        void UpdateText()
        {
            OsuSpriteText.Text = PrefixText + Value.ToString() + PostfixText;
        }
    }
}
