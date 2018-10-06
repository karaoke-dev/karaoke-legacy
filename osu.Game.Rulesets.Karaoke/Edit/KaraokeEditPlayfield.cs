// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Input.Events;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Lyric;
using osu.Game.Rulesets.Karaoke.UI;
using OpenTK.Input;

namespace osu.Game.Rulesets.Karaoke.Edit
{
    public partial class KaraokeEditPlayfield : KaraokeBasePlayfield
    {
        /// <summary>
        ///     Selected karaoke Object
        /// </summary>
        public DrawableEditableKaraokeObject NowSelectedKaraokeObject { get; set; }


        public KaraokeEditPlayfield(Ruleset ruleset, WorkingBeatmap beatmap, KaraokeEditRulesetContainer container)
            : base(ruleset, beatmap, container)
        {
        }

        /// <summary>
        ///     using hotkay to open dialog
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override bool OnKeyDown(KeyDownEvent e)
        {
            foreach (var single in e.PressedKeys)
                if (single == Key.L)
                {
                    //Open Lyrics dialog
                    OpenListKaraokeLyricsDialog();
                    break;
                }
                else if (single == Key.T)
                {
                    //Open Translate dialog
                    OpenListKaraokeTranslateDialog();
                    break;
                }

            return base.OnKeyDown(e);
        }
    }
}
