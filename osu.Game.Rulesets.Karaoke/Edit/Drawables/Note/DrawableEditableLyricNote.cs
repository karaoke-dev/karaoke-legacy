using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.UserInterface;
using osu.Game.Graphics.UserInterface;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Note
{
    /// <summary>
    /// Eeditable note
    /// TODO : make it editable
    /// </summary>
    public class DrawableEditableLyricNote : DrawableLyricNote, IHasContextMenu
    {
        public MenuItem[] ContextMenuItems => new MenuItem[]
        {
            new OsuMenuItem(@"Delete", MenuItemType.Highlighted)
        };
    }
}
