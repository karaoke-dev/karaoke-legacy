// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Framework.Graphics;
using osu.Game.Graphics.UserInterface;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Desktop.Pieces
{
    /// <summary>
    /// it's a slider with up and down button
    /// </summary>
    public class WithUpAndDownButtonSlider : OsuSliderBar<double>
    {
        public EventHandler<double> OnValueChanged;

        protected int ButtonZixe = 25;

        /// <summary>
        /// Decrease Button
        /// </summary>
        public KaraokeButton DecreaseButton;

        /// <summary>
        /// Increase button
        /// </summary>
        public KaraokeButton IncreaseButton;

        /// <summary>
        /// max value
        /// </summary>
        public double MinValue
        {
            get => CurrentNumber.MinValue;
            set { CurrentNumber.MinValue = value; }
        }

        /// <summary>
        /// min value
        /// </summary>
        public double MaxValue
        {
            get => CurrentNumber.MaxValue;
            set { CurrentNumber.MaxValue = value; }
        }

        /// <summary>
        /// now value
        /// </summary>
        public double Value
        {
            get => CurrentNumber.Value;
            set { CurrentNumber.Value = value; }
        }

        /// <summary>
        /// default value
        /// </summary>
        public float DefauleValue { get; set; }

        /// <summary>
        /// reset to defaule value
        /// </summary>
        /// <returns></returns>
        public void ResetToDefauleValue()
        {
            Value = DefauleValue;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public WithUpAndDownButtonSlider()
        {
            // Origin = Anchor.Centre;
            // Anchor = Anchor.Centre;

            CurrentNumber.MinValue = 0;
            CurrentNumber.MaxValue = 1;
            //RelativeSizeAxes = Axes.X;
            KeyboardStep = 0.1f;

            //TODO : tp add button in here ?

            Add(DecreaseButton = new KaraokeButton()
            {
                Position = new Vector2(-10, 0),
                Origin = Anchor.CentreRight,
                Anchor = Anchor.CentreLeft,
                Width = ButtonZixe,
                Height = ButtonZixe,
                Text = "-",
                TooltipText = "Decrease",
                Action = () =>
                {
                    double newValue = Value - KeyboardStep;
                    Value = newValue;
                }
            });

            Add(IncreaseButton = new KaraokeButton()
            {
                Position = new Vector2(10, 0),
                Origin = Anchor.CentreLeft,
                Anchor = Anchor.CentreRight,
                Width = ButtonZixe,
                Height = ButtonZixe,
                Text = "+",
                TooltipText = "Increase",
                Action = () =>
                {
                    double newValue = Value + KeyboardStep;
                    Value = newValue;
                }
            });
        }

        public override string TooltipText
        {
            get { return Current.Value.ToString(@"0.##"); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected override void UpdateValue(float value)
        {
            base.UpdateValue(value);

            if (OnValueChanged != null)
                OnValueChanged(this, Value);
        }
    }
}
