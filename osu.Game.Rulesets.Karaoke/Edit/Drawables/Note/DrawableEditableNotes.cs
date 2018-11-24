// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Note
{
    public class DrawableEditableNotes : DrawableNotes<DrawableEditableSingleNote>
    {
        public DrawableEditableNotes(Objects.Lyric hitObject)
            : base(hitObject)
        {
            //add background
            AddInternal(new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = new Color4(0, 0, 0, 200),
                Depth = 1
            });
        }
    }
}
