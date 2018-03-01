// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// use to record romaji
    /// </summary>
    public class RomajiText : TextComponent, IHasEndIndex
    {
        public RomajiText()
        {
        }

        public RomajiText(string str)
        {
            Text = str;
        }

        /// <summary>
        /// relativa to textIndex
        /// </summary>
        public int? Length { get; set; }
    }
}
