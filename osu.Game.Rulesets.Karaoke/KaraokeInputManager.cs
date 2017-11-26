// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.ComponentModel;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Karaoke
{
    public class KaraokeInputManager : RulesetInputManager<KaraokeAction>
    {
        public KaraokeInputManager(RulesetInfo ruleset)
            : base(ruleset, 0, SimultaneousBindingMode.Unique)
        {
        }
    }

    public enum KaraokeAction
    {
        //TODO : remove it from osu!
        [Description("Left Button")] LeftButton,
        [Description("Right Button")] RightButton,

        //Karaoke
        [Description("First Lyric")] FirstLyric, // 1 button
        [Description("Previous Lyric")] PreviousLyric, // left
        [Description("Next Lyric")] NextLyric, //right
        [Description("PlayAndPause")] PlayAndPause, //space

        [Description("Increase Speed")] IncreaseSpeed, //Q
        [Description("Decrease Speed")] DecreaseSpeed, //A
        [Description("Reset Speed")] ResetSpeed, //Z

        [Description("Increase Tone")] IncreaseTone, //W
        [Description("Decrease Tone")] DecreaseTone, //S
        [Description("Reset Tone")] ResetTone, //X

        [Description("Increase Lyric Appear Time")] IncreaseLyricAppearTime, //E
        [Description("Decrease Lyric Appear Time")] DecreaseLyricAppearTime, //D
        [Description("Reset Lyric Appear Time")] ResetLyricAppearTime, //C

        [Description("Panel key")] OpenPanel, //P
    }
}
