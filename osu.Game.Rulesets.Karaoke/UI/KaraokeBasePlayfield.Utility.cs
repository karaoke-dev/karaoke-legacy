// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.UI.Tool;

namespace osu.Game.Rulesets.Karaoke.UI
{
    public partial class KaraokeBasePlayfield
    {
        public KaraokeTool KaraokeFieldTool { get; } = new KaraokeTool();
    }
}
