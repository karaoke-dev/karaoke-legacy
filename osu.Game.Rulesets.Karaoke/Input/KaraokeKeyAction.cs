// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.ComponentModel;

namespace osu.Game.Rulesets.Karaoke.Input
{
    public enum KaraokeKeyAction
    {
        //KaraokeGamePlay
        [Description("First BaseLyric")] FirstLyric, // 1 ,number_1

        [Description("Previous BaseLyric")] PreviousLyric, // left

        [Description("Next BaseLyric")] NextLyric, //right

        [Description("PlayAndPause")] PlayAndPause, //space

        [Description("Increase Speed")] IncreaseSpeed, //Q

        [Description("Decrease Speed")] DecreaseSpeed, //A

        [Description("Reset Speed")] ResetSpeed, //Z

        [Description("Increase Tone")] IncreaseTone, //W

        [Description("Decrease Tone")] DecreaseTone, //S

        [Description("Reset Tone")] ResetTone, //X

        [Description("Increase BaseLyric Appear Time")] IncreaseLyricAppearTime, //E

        [Description("Decrease BaseLyric Appear Time")] DecreaseLyricAppearTime, //D

        [Description("Reset BaseLyric Appear Time")] ResetLyricAppearTime, //C

        [Description("Panel key")] OpenPanel, //P
    }
}
