// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.ComponentModel;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Karaoke.Input
{
    /// <summary>
    /// Karaoke's editor input manager
    /// <see cref="KaraokeKeyAction"/> cannot be work in editor
    /// </summary>
    public class KaraokeEditorInputManager : RulesetInputManager<KaraokeEditorKeyAction>
    {
        public KaraokeEditorInputManager(RulesetInfo ruleset)
            : base(ruleset, 1, SimultaneousBindingMode.Unique)
        {
        }
    }

    public enum KaraokeEditorKeyAction
    {
        //open Dialog Hotkey
        [Description("Template Dialog")] TemplateDialog, //T

        [Description("Lyrics Dialog")] LyricsDialog, //L

        [Description("Translate Dialog")] TranslateDialog, //R

        [Description("Singer Dialog")] SingerDialog //P
    }
}
