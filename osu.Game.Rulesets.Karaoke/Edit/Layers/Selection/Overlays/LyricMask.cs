// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;

namespace osu.Game.Rulesets.Karaoke.Edit.Layers.Selection.Overlays
{
    /// <summary>
    ///     Drawable BaseLyric Mask
    /// </summary>
    public class LyricMask : HitObjectMask
    {
        public LyricMask(DrawableLyric drawableLyric)
            : base(drawableLyric)
        {
        }
    }
}
