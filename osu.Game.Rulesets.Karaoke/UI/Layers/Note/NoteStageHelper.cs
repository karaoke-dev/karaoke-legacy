// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Objects.Note;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Note
{
    public class NoteStageHelper
    {
        public static float GetPositionByTone(Tone tone)
        {
            var noteHeight = (float)(tone.Scale + (tone.Helf ? 0.5 : 0)) * (KaraokeStage.COLUMN_HEIGHT + KaraokeStage.COLUMN_SPACING);

            //large tone is upper
            return -noteHeight;
        }
    }
}
