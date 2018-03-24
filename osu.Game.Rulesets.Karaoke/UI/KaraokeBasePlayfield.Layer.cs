// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Timing;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Edit.Dialog;
using osu.Game.Rulesets.Karaoke.UI.Layers.Dialog;
using osu.Game.Rulesets.Karaoke.UI.Layers.Lyric;
using osu.Game.Rulesets.Karaoke.UI.Layers.Type;

namespace osu.Game.Rulesets.Karaoke.UI
{
    public partial class KaraokeBasePlayfield
    {
        protected List<ILayer> Layers = new List<ILayer>();

        protected DialogLayer DialogLayer;
        public KaraokeLyricPlayField KaraokeLyricPlayField;


        #region Layer

        /// <summary>
        /// Dialog
        /// </summary>
        public virtual void InitialDialogLayer()
        {
            Add(DialogLayer = new DialogLayer
            {
                Name = "DialogLayer",
                Clock = new FramedClock(new StopwatchClock(true)),
                RelativeSizeAxes = Axes.Both,
                Depth = 10,
            });
            Layers.Add(DialogLayer);
        }

        /// <summary>
        /// Frontend
        /// </summary>
        public virtual void InitialFrontendLayer()
        {
        }

        /// <summary>
        /// Ruleset
        /// </summary>
        public virtual void InitialRulesetLayer()
        {
        }

        /// <summary>
        /// Backend
        /// </summary>
        public virtual void InitialBackendLayer()
        {
        }

        /// <summary>
        /// PostProcessLayer
        /// </summary>
        public virtual void PostProcessLayer(KaraokeConfigManager manager)
        {
            //ProcessInput
            var inputLayer = Layers.OfType<IAcceptControlLayer>().FirstOrDefault();
            if (inputLayer != null)
            {
                var acceeptsControlLayers = Layers.OfType<IAcceptControlLayer>();
                foreach (var singleLayer in acceeptsControlLayers)
                {
                    singleLayer.KeyAction.BindTo(inputLayer.KeyAction);
                    singleLayer.ScrollAction.BindTo(inputLayer.ScrollAction);
                    singleLayer.TapAction.BindTo(inputLayer.TapAction);
                }
            }

            //processPlatform
            var platformLayers = Layers.OfType<IPlatformLayer>();
            foreach (var singleLayer in platformLayers)
            {
                singleLayer.PlatformType.BindTo(manager.GetBindable<PlatformType>(KaraokeSetting.Device));
            }
        }

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
