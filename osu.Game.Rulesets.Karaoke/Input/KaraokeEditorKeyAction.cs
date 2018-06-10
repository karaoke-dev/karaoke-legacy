// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.ComponentModel;

namespace osu.Game.Rulesets.Karaoke.Input
{
    public enum KaraokeEditorKeyAction
    {
        //open Dialog Hotkey
        [Description("Template Dialog")] TemplateDialog, //T

        [Description("Lyrics Dialog")] LyricsDialog, //L

        [Description("Translate Dialog")] TranslateDialog, //R

        [Description("Singer Dialog")] SingerDialog //P
    }
}
