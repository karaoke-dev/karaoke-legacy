// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Input.StateChanges;
using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.Karaoke.Replays
{
    public class KaraokeReplayInputHandler : FramedReplayInputHandler<KaraokeReplayFrame>
    {
        public KaraokeReplayInputHandler(Replay replay)
            : base(replay)
        {
        }

        public override List<IInput> GetPendingInputs()
        {
            return new List<IInput>
            {
                new ReplayState<KaraokeKeyAction>()
            };
        }
    }
}
