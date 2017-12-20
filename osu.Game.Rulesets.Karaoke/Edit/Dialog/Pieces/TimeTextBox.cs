using osu.Game.Graphics.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Input;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces
{
    /// <summary>
    /// set the rule to change the input time format to --:--.--
    /// osu.Game.Screens.Edit.Components : TimeInfoContainer.cs
    /// </summary>
    public class TimeTextBox : OsuTextBox
    {
        public double _timeValue;
        public double TimeValue
        {
            get => _timeValue;
            set
            {
                if (value != _timeValue)
                {
                    _timeValue = value;
                    this.Text = DoubleToString(_timeValue);
                }
            }
        }

        public TimeTextBox()
        {
            LengthLimit = 9;
            PlaceholderText = "00:00:000";
            Placeholder.FixedWidth = true;
            Current.ValueChanged += (newString) =>
              {
                  if (Text.Length == 2 || Text.Length == 5)
                  {
                      var text = Text + ":";
                      Text = text;
                  }
              };
        }

        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;

                /*
                
                    

                /*
                if (base.Text.EndsWith(".") || base.Text.EndsWith(":"))
                    return;

                */
                //convert double to value
                //if(Text.Length>5)
                //    TimeValue = StringToDouble(base.Text);
            }
        }

        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            return base.OnKeyDown(state, args);
        }


        protected  bool HandlePendingText(InputState state)
        {
            return base.HandlePendingText(state);
        }

        protected string DoubleToString(double time)
        {
            return TimeSpan.FromMilliseconds(time).ToString(@"mm\:ss\:fff");
        }

        protected double StringToDouble(string stringTime)
        {
            TimeSpan time;
            TimeSpan.TryParse(stringTime,out time);
            double milliSecond = time.TotalMilliseconds/60;
            return milliSecond;
        }
    }
}
