﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces;
using osu.Game.Rulesets.Karaoke.Objects;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog
{
    /// <summary>
    /// show list singer
    /// </summary>
    public class ListSingerDialog : DialogContainer
    {
    }

    public class ListSingerScrollContainer : TableView<KaraokeSinger, SingerCell>
    {
        public ListSingerScrollContainer()
        {
        }
    }

    public class SingerCell : KaraokeBaseTableViewCell<KaraokeSinger>
    {
        public SingerCell()
        {
        }
    }
}
