using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Replays.Legacy;
using osu.Game.Rulesets.Replays.Types;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Replays
{
    public class KaraokeReplayFrame : ReplayFrame, IConvertibleReplayFrame
    {
        public Vector2 Position;
        public List<KaraokeAction> Actions = new List<KaraokeAction>();

        public KaraokeReplayFrame()
        {
        }

        public KaraokeReplayFrame(double time, Vector2 position, params KaraokeAction[] actions)
            : base(time)
        {
            Position = position;
            Actions.AddRange(actions);
        }

        public void ConvertFrom(LegacyReplayFrame legacyFrame, Beatmap beatmap)
        {
            Position = legacyFrame.Position;
            //if (legacyFrame.MouseLeft) Actions.Add(KaraokeAction.LeftButton);
            //if (legacyFrame.MouseRight) Actions.Add(KaraokeAction.RightButton);
        }
    }
}
