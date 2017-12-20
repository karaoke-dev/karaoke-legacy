using osu.Game.Graphics.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces
{
    /// <summary>
    /// set the rule to change the input time format to --:--.--
    /// </summary>
    public class TimeTextBox : FocusedTextBox
    {
        public double _timeValue;
        public double TimeValue
        {
            get => _timeValue;
            set
            {
                _timeValue = value;
                this.Text = DoubleToString(_timeValue);
            }
        }

        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;

            }
        }

        protected string DoubleToString(double time)
        {
            return TimeSpan.FromMilliseconds(time).ToString(@"mm\:ss\:fff");
        }

        protected double StringToDouble(string stringTime)
        {
            TimeSpan time;
            TimeSpan.TryParse(stringTime,out time);
            double milliSecond= time.Milliseconds
            return milliSecond;
        }
    }
}
