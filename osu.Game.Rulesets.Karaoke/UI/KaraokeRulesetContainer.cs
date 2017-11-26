﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Karaoke.Beatmaps;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Replays;
using osu.Game.Rulesets.Karaoke.Scoring;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;
using OpenTK;
using osu.Game.Rulesets.Karaoke.Objects.Drawables;

namespace osu.Game.Rulesets.Karaoke.UI
{
    public class KaraokeRulesetContainer : RulesetContainer<KaraokeObject>
    {
        public KaraokeRulesetContainer(Ruleset ruleset, WorkingBeatmap beatmap, bool isForCurrentRuleset)
            : base(ruleset, beatmap, isForCurrentRuleset)
        {
            //TODO : add "autoPlay" to Mods to control play speed
        }

        public override ScoreProcessor CreateScoreProcessor() => new KaraokeScoreProcessor(this);

        protected override BeatmapConverter<KaraokeObject> CreateBeatmapConverter() => new KaraokeBeatmapConverter();

        protected override BeatmapProcessor<KaraokeObject> CreateBeatmapProcessor() => new KaraokeBeatmapProcessor();

        protected override Playfield CreatePlayfield() => new KaraokePlayfield(Ruleset, WorkingBeatmap ,this);

        public override PassThroughInputManager CreateInputManager() => new KaraokeInputManager(Ruleset.RulesetInfo);

        protected override DrawableHitObject<KaraokeObject> GetVisualRepresentation(KaraokeObject h)
        {
            if (h is KaraokeObject karaokeObject)
            {
                return new DrawableKaraokeObject(karaokeObject);
            }
            return null;
        }

        protected override FramedReplayInputHandler CreateReplayInputHandler(Replay replay) => new KaraokeReplayInputHandler(replay);

        protected override Vector2 GetPlayfieldAspectAdjust() => new Vector2(0.75f);
    }
}
