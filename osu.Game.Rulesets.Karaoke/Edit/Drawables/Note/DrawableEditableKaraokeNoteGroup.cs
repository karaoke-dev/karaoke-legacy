using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Note
{
    public class DrawableEditableKaraokeNoteGroup : DrawableKaraokeNoteGroup<DrawableEditableLyricNote>
    {
        public DrawableEditableKaraokeNoteGroup(BaseLyric hitObject)
            : base(hitObject)
        {
        }
    }
}
