// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Mobile.Pieces
{
    /// <summary>
    /// use to show value 
    /// Format : (+/-) [Value] (unit)
    /// </summary>
    public class ValueInfo : Info
    {
        /// <summary>
        /// Format
        /// </summary>
        public Format Format { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Unit
        /// </summary>
        public string Unit { get; set; }
    }

    /// <summary>
    /// Format
    /// </summary>
    public enum Format
    {
        Number,
        Time, //HH:MM:SS
    }
}
