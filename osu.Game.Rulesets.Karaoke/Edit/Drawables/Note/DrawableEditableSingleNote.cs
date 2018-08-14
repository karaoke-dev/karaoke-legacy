// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.UserInterface;
using osu.Game.Graphics.UserInterface;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Note
{
    /// <summary>
    ///     Eeditable note
    ///     TODO : make it editable
    /// </summary>
    public class DrawableEditableSingleNote : DrawableSingleNote, IHasContextMenu
    {
        public MenuItem[] ContextMenuItems => new MenuItem[]
        {
            new OsuMenuItem(@"Delete", MenuItemType.Highlighted)
        };
    }
}
