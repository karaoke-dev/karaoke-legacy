// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.UI.Layers.Type;
using osu.Game.Rulesets.UI.Scrolling;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Note
{
    /// <summary>
    /// use to show karaoke tone Playfield
    /// like : 
    /// ---------------------------#####
    /// --------------#####----####-----
    /// ---------#####-----####---------
    /// ---######-----------------------
    /// --------------------------------
    /// </summary>
    public class KaraokeTonePlayfield : ScrollingPlayfield , ILayer
    {
        public KaraokeTonePlayfield()
            : base(ScrollingDirection.Right)
        {

        }
    }
}
