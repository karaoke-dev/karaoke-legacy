// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Objects;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog
{
    /// <summary>
    /// right click karaoke and select add/Edit subText object, will pop-up this dialog
    /// show a textbox and Add/Edit Button
    /// </summary>
    public class EditKaraokeSubTextDialog : DialogContainer
    {
        public override string Title => "TopText";

        protected Lyric Lyric { get; set; }

        public EditKaraokeSubTextDialog(Lyric lyric)
        {
            Lyric = lyric;
            Width = 200;
            Height = 100;
        }
    }
}
