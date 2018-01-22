// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Timing;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Karaoke.Edit.Dialog;
using osu.Game.Rulesets.Karaoke.Objects.Drawables;
using osu.Game.Rulesets.Karaoke.UI.Extension;
using osu.Game.Rulesets.Karaoke.UI.Interface;
using osu.Game.Rulesets.Karaoke.UI.Tool;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.UI
{
    /// <summary>
    /// Karaoke base panel
    /// </summary>
    public class KaraokeBasePlayfield : Playfield, IAmKaraokeField
    {
        public Ruleset Ruleset { get; set; }
        public WorkingBeatmap WorkingBeatmap { get; set; }
        public KaraokeRulesetContainer KaraokeRulesetContainer { get; set; }
        public KaraokeFieldTool KaraokeFieldTool { get; } = new KaraokeFieldTool();
        public List<IAmDrawableKaraokeObject> ListDrawableKaraokeObject { get; set; } = new List<IAmDrawableKaraokeObject>();

        public static readonly Vector2 BASE_SIZE = new Vector2(512, 384);

        /// <summary>
        /// Dialog Layer
        /// </summary>
        protected Container DialogLayer;

        public override Vector2 Size
        {
            get
            {
                var parentSize = Parent.DrawSize;
                var aspectSize = parentSize.X * 0.75f < parentSize.Y ? new Vector2(parentSize.X, parentSize.X * 0.75f) : new Vector2(parentSize.Y * 4f / 3f, parentSize.Y);

                return new Vector2(aspectSize.X / parentSize.X, aspectSize.Y / parentSize.Y) * base.Size;
            }
        }

        public KaraokeBasePlayfield(Ruleset ruleset, WorkingBeatmap beatmap, KaraokeRulesetContainer container)
            : base(BASE_SIZE.X)
        {
            Ruleset = ruleset;
            WorkingBeatmap = beatmap;
            KaraokeRulesetContainer = container;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            InitialRulesetLayer();
        }

        public virtual void InitialRulesetLayer()
        {
            Add(DialogLayer = new Container
            {
                Name = "DialogLayer",
                Clock = new FramedClock(new StopwatchClock(true)),
                RelativeSizeAxes = Axes.Both,
                Depth = 10,
            });
        }

        public override void Add(DrawableHitObject h)
        {
            h.Depth = (float)h.HitObject.StartTime;

            //update template
            this.UpdateObjectTemplate(h as DrawableKaraokeObject);

            //update position
            this.UpdateObjectAutomaticallyPosition(h as DrawableKaraokeObject);

            //add to list
            ListDrawableKaraokeObject.Add(h as DrawableKaraokeObject);

            base.Add(h);
        }

        //post process
        public override void PostProcess()
        {
            // foreach to update Template

            // foreach to update position

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

        #region Dialog

        public void OpenLoadSaveDialog()
        {
            if (!DialogLayer.Children.OfType<LoadSaveDialog>().Any())
            {
                DialogLayer.Add(new LoadSaveDialog(this));
            }
        }

        #endregion
    }
}
