using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    public class Text : IHasText
    {
        /// <summary>
        /// text
        /// </summary>
        public virtual string Text { get; set; }
    }
}
