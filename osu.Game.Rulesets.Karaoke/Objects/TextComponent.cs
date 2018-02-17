using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    public abstract class TextComponent : IHasText
    {
        /// <summary>
        /// text
        /// </summary>
        public virtual string Text { get; set; }
    }
}
