// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Input.Handlers;
using osu.Game.Replays;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;
using osu.Game.Rulesets.Karaoke.Replays;
using osu.Game.Rulesets.Karaoke.Scoring;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.UI.Scrolling;

namespace osu.Game.Rulesets.Karaoke.UI
{
    public class KaraokeRulesetContainer : ScrollingRulesetContainer<KaraokeBasePlayfield, Lyric>
    {
        protected new KaraokeConfigManager Config => (KaraokeConfigManager)base.Config;

        private readonly Bindable<KaroakeNoteScrollDirection> configDirection = new Bindable<KaroakeNoteScrollDirection>();

        public KaraokeRulesetContainer(Ruleset ruleset, WorkingBeatmap beatmap)
            : base(ruleset, beatmap)
        {
        }

        public override ScoreProcessor CreateScoreProcessor()
        {
            return new KaraokeScoreProcessor(this);
        }

        public override PassThroughInputManager CreateInputManager()
        {
            return new KaraokeInputManager(Ruleset.RulesetInfo);
        }

        protected override Playfield CreatePlayfield()
        {
            return new KaraokePlayfield(Ruleset, WorkingBeatmap, this);

            /*
            //Desktop version
            if (ConfigManager?.Get<PlatformType>(KaraokeSetting.Device) == PlatformType.Desktop)
                return new KaraokeDesktopPlayfield(Ruleset, WorkingBeatmap, this);

            //Mobile version
            return new KaraokeMobilePlayField(Ruleset, WorkingBeatmap, this);
            */
        }

        public override DrawableHitObject<Lyric> GetVisualRepresentation(Lyric h)
        {
            if (h is Lyric karaokeObject)
                return new DrawableLyric(karaokeObject);

            return null;
        }

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay)
        {
            return new KaraokeReplayInputHandler(replay);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            //BarLines.ForEach(Playfield.Add);

            Config.BindWith(KaraokeSetting.NoteScrollDirection, configDirection);
            configDirection.BindValueChanged(v => Direction.Value = (ScrollingDirection)v, true);

            Config.BindWith(KaraokeSetting.NoteTime, TimeRange);
        }
    }
}
