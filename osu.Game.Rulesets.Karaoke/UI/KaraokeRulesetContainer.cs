// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Input;
using osu.Framework.Lists;
using osu.Game.Beatmaps;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Configuration;
using osu.Game.Input.Handlers;
using osu.Game.Rulesets.Configuration;
using osu.Game.Rulesets.Karaoke.Beatmaps;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;
using osu.Game.Rulesets.Karaoke.Replays;
using osu.Game.Rulesets.Karaoke.Scoring;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Timing;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.UI.Scrolling;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.UI
{
    public class KaraokeRulesetContainer : BaseRulesetContainer<BaseLyric>
    {
        protected KaraokeConfigManager ConfigManager;

        public KaraokeRulesetContainer(Ruleset ruleset, WorkingBeatmap beatmap, bool isForCurrentRuleset)
            : base(ruleset, beatmap, isForCurrentRuleset)
        {
            //TODO : add "autoPlay" to Mods to control play speed
        }

        public override ScoreProcessor CreateScoreProcessor()
        {
            return new KaraokeScoreProcessor(this);
        }

        public override PassThroughInputManager CreateInputManager()
        {
            return new KaraokeInputManager(Ruleset.RulesetInfo);
        }

        protected override BeatmapConverter<BaseLyric> CreateBeatmapConverter()
        {
            return new KaraokeBeatmapConverter();
        }

        protected override BeatmapProcessor<BaseLyric> CreateBeatmapProcessor()
        {
            return new KaraokeBeatmapProcessor();
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

        protected override DrawableHitObject<BaseLyric> GetVisualRepresentation(BaseLyric h)
        {
            if (h is BaseLyric karaokeObject)
                return new DrawableLyric(karaokeObject);

            return null;
        }

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay)
        {
            return new KaraokeReplayInputHandler(replay);
        }

        protected override Vector2 GetAspectAdjustedSize()
        {
            const float default_relative_height = KaraokeBasePlayfield.DEFAULT_HEIGHT / 768;
            const float default_aspect = 16f / 9f;

            float aspectAdjust = MathHelper.Clamp(DrawWidth / DrawHeight, 0.4f, 4) / default_aspect;

            return new Vector2(1, default_relative_height * aspectAdjust);
        }

        protected override Vector2 PlayfieldArea => Vector2.One;

        protected override IRulesetConfigManager CreateConfig(Ruleset ruleset, SettingsStore settings)
        {
            ConfigManager = new KaraokeConfigManager(settings, Ruleset.RulesetInfo, Variant);
            return ConfigManager;
        }
    }
}
