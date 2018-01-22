// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Graphics.UserInterface;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces
{
    public class EnumDropdown<T> : OsuEnumDropdown<T>
    {
        public DropdownMenu DropdownContainer => Menu;
    }
}
