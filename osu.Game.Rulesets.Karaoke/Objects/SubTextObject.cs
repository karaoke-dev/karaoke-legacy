using osu.Game.Rulesets.Karaoke.Objects.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    public class SubTextObject : TextObject , IHasCharIndex
    {
        /// <summary>
        /// relativa to textIndex
        /// </summary>
        public int CharIndex { get; set; }
    }
}
