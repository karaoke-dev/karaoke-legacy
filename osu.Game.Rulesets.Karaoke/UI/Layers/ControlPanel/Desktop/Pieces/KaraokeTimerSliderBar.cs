// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Framework.Graphics;
using osu.Framework.Input;
using osu.Game.Graphics.Sprites;
using osu.Game.Graphics.UserInterface;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Desktop.Pieces
{
    public class KaraokeTimerSliderBar : OsuSliderBar<double>
    {
        public EventHandler<double> OnValueChanged;

        /// <summary>
        /// Now time label
        /// </summary>
        public OsuSpriteText NowTimeSpriteText;

        /// <summary>
        /// total time label
        /// </summary>
        public OsuSpriteText TotalTimeSpriteText;

        /// <summary>
        /// constructor
        /// </summary>
        public KaraokeTimerSliderBar()
        {
            CurrentNumber.MinValue = 0;
            CurrentNumber.MaxValue = 1;
            //RelativeSizeAxes = Axes.X;
            KeyboardStep = 1000f;

            //now time
            Add(NowTimeSpriteText = new OsuSpriteText
            {
                //Position = new Vector2(startXPositin + 240, oneLayerYPosition),
                Position = new Vector2(-10, -2),
                Text = "--:--",
                UseFullGlyphHeight = false,
                Origin = Anchor.CentreRight,
                Anchor = Anchor.CentreLeft,
                TextSize = 15,
                Alpha = 1,
                //ShadowColour = _textColor,
                //BorderColour = _textColor,
            });

            //end time
            Add(TotalTimeSpriteText = new OsuSpriteText
            {
                //Position = new Vector2(startXPositin + 240, oneLayerYPosition),
                //Position = new Vector2(startXPositin + 600, oneLayerYPosition),
                Position = new Vector2(35, -2),
                Text = "--:--",
                UseFullGlyphHeight = false,
                Origin = Anchor.CentreRight,
                Anchor = Anchor.CentreRight,
                TextSize = 15,
                Alpha = 1,
                //ShadowColour = _textColor,
                //BorderColour = _textColor,
            });
        }

        public double StartTime
        {
            set { CurrentNumber.MinValue = value; }
        }

        public double EndTime
        {
            set
            {
                CurrentNumber.MaxValue = value;
                TotalTimeSpriteText.Text = GetTimeFormat(((int)CurrentNumber.MaxValue - (int)CurrentNumber.MinValue) / 1000);
            }
        }

        public double CurrentTime
        {
            get => CurrentNumber.Value;
            set
            {
                CurrentNumber.Value = value;
                NowTimeSpriteText.Text = GetTimeFormat(((int)CurrentNumber.Value - (int)CurrentNumber.MinValue) / 1000);
            }
        }

        public override string TooltipText
        {
            get { return GetTimeFormat((int)CurrentNumber.Value / 1000); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected override void UpdateValue(float value)
        {
            base.UpdateValue(value);
        }

        protected override void OnUserChange()
        {
            base.OnUserChange();

            //just if user trigger can update value
            if (OnValueChanged != null)
                OnValueChanged(this, Current.Value);
        }

        protected string GetTimeFormat(int second)
        {
            return (second / 60).ToString("D2") + ":" + (second % 60).ToString("D2");
        }

        protected override bool OnWheel(InputState state)
        {
            if (state.Mouse.WheelDelta != 0)
            {
                CurrentTime = CurrentTime + state.Mouse.WheelDelta * 500;
            }

            //eat this event and not pass to next
            return true;
            //return base.OnWheel(state);
        }
    }
}
