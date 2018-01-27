using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.UI.Scrolling;

namespace osu.Game.Rulesets.Karaoke.UI.PlayField
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
    public class KaraokeTonePlayfield : ScrollingPlayfield
    {

        public KaraokeTonePlayfield()
            : base(ScrollingDirection.Right)
        {
        }
    }
}
