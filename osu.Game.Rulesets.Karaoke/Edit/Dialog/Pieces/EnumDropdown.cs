using osu.Game.Graphics.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces
{
    public class EnumDropdown<T> : OsuEnumDropdown<T>
    {
        public DropdownMenu DropdownContainer => Menu;
    }
}
