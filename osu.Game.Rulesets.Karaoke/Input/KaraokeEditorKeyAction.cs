using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Input
{
    public enum KaraokeEditorKeyAction
    {
        //open Dialog Hotkey
        [Description("Template Dialog")] TemplateDialog, //T

        [Description("Lyrics Dialog")] LyricsDialog, //L

        [Description("Translate Dialog")] TranslateDialog, //R

        [Description("Singer Dialog")] SingerDialog, //P
    }
}
