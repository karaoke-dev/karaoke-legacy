// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Game.Graphics.UserInterface;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces
{
    public class EnumDropdown<T> : OsuEnumDropdown<T>
    {
        public DropdownMenu DropdownContainer => Menu;
    }
}
