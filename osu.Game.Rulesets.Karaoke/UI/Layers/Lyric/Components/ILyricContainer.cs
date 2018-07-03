using System;
using System.Collections.Generic;
using System.Text;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Lyric.Components
{
    public interface ILyricContainer
    {
        void Add(DrawableLyric drawable);

        List<DrawableLyric> Lyrics { get; }
    }
}
