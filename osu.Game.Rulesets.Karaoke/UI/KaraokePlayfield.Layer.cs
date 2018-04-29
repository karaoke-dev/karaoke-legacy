// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Framework.Timing;
using osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel;
using osu.Game.Rulesets.Karaoke.UI.Layers.Effect;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input;
using osu.Game.Rulesets.Karaoke.UI.Layers.Lyric;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.UI
{
    public partial class KaraokePlayfield
    {
        private ControlPanelLayer _karaokePanelOverlay;
        private InputLayer _inputLayer;
        private EffectLayer _effectLayer;

        /// <summary>
        /// Frontend
        /// </summary>
        public override void InitialFrontendLayer()
        {
            //panel
            KaraokeRulesetContainer.Add(_karaokePanelOverlay = new ControlPanelLayer(this)
            {
                Clock = new FramedClock(new StopwatchClock(true)),
                RelativeSizeAxes = Axes.X,
                Origin = Anchor.BottomCentre,
                Anchor = Anchor.BottomCentre,
                Scale = new Vector2(1.0f),
                Depth = 10f,
            });

            //Input layer
            KaraokeRulesetContainer.Add(_inputLayer = new InputLayer
            {
                RelativeSizeAxes = Axes.Both,
                Depth = -2,
                Clock = new FramedClock(new StopwatchClock(true)),
            });

            Layers.Add(_karaokePanelOverlay);
            Layers.Add(_inputLayer);
        }

        /// <summary>
        /// Ruleset
        /// </summary>
        public override void InitialRulesetLayer()
        {
            base.InitialRulesetLayer();

            //layer
            Add(KaraokeLyricPlayField = new KaraokeLyricPlayField()
            {
                KaraokeRulesetContainer = KaraokeRulesetContainer
            });

            Layers.Add(KaraokeLyricPlayField);
        }

        /// <summary>
        /// Backend
        /// </summary>
        public override void InitialBackendLayer()
        {
            Add(_effectLayer = new EffectLayer()
            {
            });
            Layers.Add(_effectLayer);
        }
    }
}
