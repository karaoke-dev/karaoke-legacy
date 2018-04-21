// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Input.Handlers;
using osu.Game.Rulesets.Configuration;
using osu.Game.Rulesets.Karaoke.Beatmaps;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;
using osu.Game.Rulesets.Karaoke.Replays;
using osu.Game.Rulesets.Karaoke.Scoring;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.UI
{
    public class KaraokeRulesetContainer : RulesetContainer<Lyric>
    {
        protected KaraokeConfigManager ConfigManager;

        public KaraokeRulesetContainer(Ruleset ruleset, WorkingBeatmap beatmap, bool isForCurrentRuleset)
            : base(ruleset, beatmap, isForCurrentRuleset)
        {
            //TODO : add "autoPlay" to Mods to control play speed
        }

        public override ScoreProcessor CreateScoreProcessor() => new KaraokeScoreProcessor(this);

        protected override BeatmapConverter<Lyric> CreateBeatmapConverter() => new KaraokeBeatmapConverter();

        protected override BeatmapProcessor<Lyric> CreateBeatmapProcessor() => new KaraokeBeatmapProcessor();

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

        public override PassThroughInputManager CreateInputManager() => new KaraokeInputManager(Ruleset.RulesetInfo);

        protected override DrawableHitObject<Lyric> GetVisualRepresentation(Lyric h)
        {
            if (h is Lyric karaokeObject)
            {
                return new DrawableLyric(karaokeObject);
            }
            return null;
        }

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new KaraokeReplayInputHandler(replay);

        protected override Vector2 GetAspectAdjustedSize() => new Vector2(0.75f);

        protected override IRulesetConfigManager CreateConfig(Ruleset ruleset, SettingsStore settings)
        {
            ConfigManager = new KaraokeConfigManager(settings, Ruleset.RulesetInfo, Variant);
            return ConfigManager;
        }
    }
}
