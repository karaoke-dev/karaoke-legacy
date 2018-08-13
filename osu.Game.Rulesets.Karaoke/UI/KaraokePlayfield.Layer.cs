// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Timing;
using osu.Game.Rulesets.Karaoke.Beatmaps;
using osu.Game.Rulesets.Karaoke.Objects.Note;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input;
using osu.Game.Rulesets.Karaoke.UI.Layers.Lyric;
using osu.Game.Rulesets.Karaoke.UI.Layers.Note;

namespace osu.Game.Rulesets.Karaoke.UI
{
    public partial class KaraokePlayfield
    {
        private InputLayer _inputLayer;

        /// <summary>
        ///     Frontend
        /// </summary>
        public override void InitialFrontendLayer()
        {
            //Input layer
            Add(_inputLayer = new InputLayer
            {
                RelativeSizeAxes = Axes.Both,
                Depth = -2,
                Clock = new FramedClock(new StopwatchClock(true))
            });
        }

        /// <summary>
        ///     Ruleset
        /// </summary>
        public override void InitialRulesetLayer()
        {
            base.InitialRulesetLayer();

            //layer
            Add(KaraokeLyricPlayField = new KaraokeLyricPlayField
            {
                KaraokeRulesetContainer = KaraokeRulesetContainer,
                Margin = new MarginPadding
                {
                    Left = 100,
                    Right = 100,
                    Top = 350,
                    Bottom = 40,
                }
            });

            //layer
            Add(KaraokeTonePlayfield = new KaraokeTonePlayfield(new List<KaraokeStageDefinition>
            {
                new KaraokeStageDefinition
                {
                    Columns = 11,
                    DefaultTone = new Tone(0, true)
                }
            })
            {
                KaraokeRulesetContainer = KaraokeRulesetContainer
            });

            AddNested(KaraokeLyricPlayField);
            AddNested(KaraokeTonePlayfield);
        }
    }
}
