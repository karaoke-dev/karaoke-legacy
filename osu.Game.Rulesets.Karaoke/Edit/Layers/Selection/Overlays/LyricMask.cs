using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;

namespace osu.Game.Rulesets.Karaoke.Edit.Layers.Selection.Overlays
{
    /// <summary>
    /// Drawable Lyric Mask
    /// </summary>
    public class LyricMask : HitObjectMask
    {
        public LyricMask(DrawableLyric drawableLyric)
            : base(drawableLyric)
        {

        }
    }
}
