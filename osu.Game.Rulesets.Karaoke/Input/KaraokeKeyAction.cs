using System.ComponentModel;

namespace osu.Game.Rulesets.Karaoke.Input
{
    public enum KaraokeKeyAction
    {
        //KaraokeGamePlay
        [Description("First Lyric")] FirstLyric, // 1 ,number_1

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

        //open Dialog Hotkey
        [Description("Template Dialog")] TemplateDialog, //T

        [Description("Lyrics Dialog")] LyricsDialog, //L

        [Description("Translate Dialog")] TranslateDialog, //R

        [Description("Singer Dialog")] SingerDialog, //P
    }
}
