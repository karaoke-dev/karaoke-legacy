// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.UI.Interface;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.UI
{
    /// <summary>
    /// Karaoke base panel
    /// the design should be like that : 
    /// |                   |                       |   Kaeakoe Mobile 
    /// |                   |  [playable Playfield] |   Karaoke Ipad
    /// |                   |                       |   Karaoke Desktop
    /// |   base Playfield  |-----------------------|-------------
    /// |                   |  [editor playField]    
    /// |                   |                       
    /// </summary>
    public partial class KaraokeBasePlayfield : Playfield, IAmKaraokeField
    {
        public Ruleset Ruleset { get; set; }
        public WorkingBeatmap WorkingBeatmap { get; set; }
        public KaraokeRulesetContainer KaraokeRulesetContainer { get; set; }

        public KaraokeConfigManager KaraokeConfigManager { get; set; }

        public static readonly Vector2 BASE_SIZE = new Vector2(512, 384);

        /// <summary>
        /// Size
        /// </summary>
        public override Vector2 Size
        {
            get
            {
                var parentSize = Parent.DrawSize;
                var aspectSize = parentSize.X * 0.75f < parentSize.Y ? new Vector2(parentSize.X, parentSize.X * 0.75f) : new Vector2(parentSize.Y * 4f / 3f, parentSize.Y);

                return new Vector2(aspectSize.X / parentSize.X, aspectSize.Y / parentSize.Y) * base.Size;
            }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="ruleset"></param>
        /// <param name="beatmap"></param>
        /// <param name="container"></param>
        public KaraokeBasePlayfield(Ruleset ruleset, WorkingBeatmap beatmap, KaraokeRulesetContainer container)
            : base(BASE_SIZE.X)
        {
            Ruleset = ruleset;
            WorkingBeatmap = beatmap;
            KaraokeRulesetContainer = container;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="manager"></param>
        [BackgroundDependencyLoader(true)]
        private void load(KaraokeConfigManager manager)
        {
            KaraokeConfigManager = manager;

            //Dialog
            InitialDialogLayer();
            //Frontend
            InitialFrontendLayer();
            //Ruleset
            InitialRulesetLayer();
            //Backend
            InitialBackendLayer();

            //post process
            PostProcessLayer(manager);
        }

        /*
        protected override void LoadComplete()
        {
            base.LoadComplete();
        }
        */

        /// <summary>
        /// Add HitObject
        /// </summary>
        /// <param name="h"></param>
        public override void Add(DrawableHitObject h)
        {
            KaraokeLyricPlayField.Add(h);

            //import
            if (KaraokeTonePlayfield != null)
            {

            }
        }

        //post process
        public override void PostProcess()
        {
            base.PostProcess();
        }

        #region Input

        /*
        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            foreach (var single in state.Keyboard.Keys)
            {
                if (single == Key.S)
                {
                    OpenLoadSaveDialog();
                }
            }
            return base.OnKeyDown(state, args);
        }
        */

        #endregion
    }
}
