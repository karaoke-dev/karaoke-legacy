// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.ComponentModel;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Karaoke.Input
{
    public class KaraokeInputManager : RulesetInputManager<KaraokeKeyAction>
    {
        public KaraokeInputManager(RulesetInfo ruleset)
            : base(ruleset, 0, SimultaneousBindingMode.Unique)
        {

        }
    }

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

        [Description("Panel key")] OpenPanel //P
    }

    /// <summary>
    ///     X or Y-anix can be use as...
    /// </summary>
    public enum KaraokeScrollAction
    {
        /// <summary>
        ///     Time
        /// </summary>
        Time,

        /// <summary>
        ///     Volumn
        /// </summary>
        Volumn,

        /// <summary>
        ///     Dim
        /// </summary>
        BackgroundDim,

        /// <summary>
        ///     Tone
        /// </summary>
        Tome,

        /// <summary>
        ///     Speed
        /// </summary>
        Speed
    }

    /// <summary>
    ///     action
    /// </summary>
    public enum KaraokeTapAction
    {
        /// <summary>
        ///     Tap to pause
        /// </summary>
        Pause,

        /// <summary>
        ///     Show panel
        /// </summary>
        ShowPanel
    }
}
