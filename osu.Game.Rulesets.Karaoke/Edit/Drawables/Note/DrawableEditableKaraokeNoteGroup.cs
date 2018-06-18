using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Note
{
    public class DrawableEditableKaraokeNoteGroup : DrawableKaraokeNoteGroup<DrawableEditableLyricNote>
    {
        public DrawableEditableKaraokeNoteGroup(BaseLyric hitObject)
            : base(hitObject)
        {
            //add background
            AddInternal(new Box()
            {
                RelativeSizeAxes = Axes.Both,
                Colour = new Color4(0, 0, 0, 200),
                Depth = 1,
            });
        }
    }
}
