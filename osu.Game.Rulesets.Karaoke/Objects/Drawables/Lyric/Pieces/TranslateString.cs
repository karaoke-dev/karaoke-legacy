// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Objects.Drawables.Common.Pieces;
using osu.Game.Rulesets.Karaoke.Objects.Text;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric.Pieces
{
    /// <summary>
    /// show the translate string 
    /// just define the format of string ?
    /// the text will assign from DrawableObject?
    /// </summary>
    public class TranslateString : KaraokeText
    {
        public TranslateString(FormattedText textObject)
            : base(textObject)
        {
        }
    }
}
