using osu.Game.Database;
using osu.Game.Rulesets.Objects.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Objects.Types
{
    /// <summary>
    /// define the basic class of karaoke object
    /// </summary>
    public interface IHasKaraokeComponent 
    {
        TextObject MainText { get; set; }

        List<SubTextObject> ListSubTextObject { get; set; }

        ListProgressPoint ListProgressPoint { get; set; }

        ListKaraokeTranslateString ListTranslate { get; set; }
    }
}
