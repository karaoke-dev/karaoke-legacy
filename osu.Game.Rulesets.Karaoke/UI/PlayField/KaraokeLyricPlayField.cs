using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Karaoke.UI.PlayField
{
    /// <summary>
    /// use to manage karaoke lyric's position arrangement
    /// 1. 
    /// |                   |
    /// |                   |
    /// |   karaoke         |
    /// |           karaoke |
    /// 
    /// 2.
    /// |                       |
    /// |      <!--scrolling--> |
    /// |  karaoke   karaoke    |
    /// |                       |
    /// 
    /// 3.
    /// |            ^  |
    /// |   karaoke  |  |
    /// |   karaoke  |  |
    /// |   karaoke  |  |
    /// 
    /// 4. more
    /// 
    /// 2. 3. 4. will be implement until release
    /// </summary>
    public class KaraokeLyricPlayField : Playfield
    {

    }
}
