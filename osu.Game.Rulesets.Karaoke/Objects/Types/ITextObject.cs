using osu.Game.Rulesets.Objects.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Objects.Types
{
    /// <summary>
    /// ITextObject
    /// </summary>
    public interface ITextObject : IHasPosition
    {
        string Text { get; set; }

        int? FontSize { get; set; }
    }
}
