// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Replays.Legacy;
using osu.Game.Rulesets.Replays.Types;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Replays
{
    public class KaraokeReplayFrame : ReplayFrame, IConvertibleReplayFrame
    {
        public Vector2 Position;
        public List<KaraokeKeyAction> Actions = new List<KaraokeKeyAction>();

        public KaraokeReplayFrame()
        {
        }

        public KaraokeReplayFrame(double time, Vector2 position, params KaraokeKeyAction[] actions)
            : base(time)
        {
            Position = position;
            Actions.AddRange(actions);
        }

        public void ConvertFrom(LegacyReplayFrame legacyFrame, IBeatmap beatmap)
        {
            Position = legacyFrame.Position;
            //if (legacyFrame.MouseLeft) Actions.Add(KaraokeAction.LeftButton);
            //if (legacyFrame.MouseRight) Actions.Add(KaraokeAction.RightButton);
        }
    }
}
