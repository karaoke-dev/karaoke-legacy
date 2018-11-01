﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Timing;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Edit.Dialog;
using osu.Game.Rulesets.Karaoke.UI.Layers.Dialog;
using osu.Game.Rulesets.Karaoke.UI.Layers.Lyric;
using osu.Game.Rulesets.Karaoke.UI.Layers.Note;
using osu.Game.Rulesets.Karaoke.UI.Layers.Type;

namespace osu.Game.Rulesets.Karaoke.UI
{
    public partial class KaraokeBasePlayfield
    {
        public KaraokeLyricPlayField KaraokeLyricPlayField;
        public KaraokeTonePlayfield KaraokeTonePlayfield;

        protected DialogLayer DialogLayer;

        #region Dialog

        public void OpenLoadSaveDialog()
        {
            if (!DialogLayer.Children.OfType<LoadSaveDialog>().Any())
                DialogLayer.Add(new LoadSaveDialog(this));
        }

        #endregion


        #region Layer

        /// <summary>
        ///     Dialog
        /// </summary>
        public virtual void InitialDialogLayer()
        {
            AddInternal(DialogLayer = new DialogLayer
            {
                Name = "DialogLayer",
                Clock = new FramedClock(new StopwatchClock(true)),
                RelativeSizeAxes = Axes.Both,
                Depth = 10
            });
        }

        /// <summary>
        ///     Frontend
        /// </summary>
        public virtual void InitialFrontendLayer()
        {
        }

        /// <summary>
        ///     Ruleset
        /// </summary>
        public virtual void InitialRulesetLayer()
        {
        }

        /// <summary>
        ///     Backend
        /// </summary>
        public virtual void InitialBackendLayer()
        {
        }

        /// <summary>
        ///     PostProcessLayer
        /// </summary>
        public virtual void PostProcessLayer(KaraokeConfigManager manager)
        {
            //ProcessInput
            var inputLayer = InternalChildren.OfType<IControlLayer>().FirstOrDefault();
            if (inputLayer != null)
            {
                var acceeptsControlLayers = InternalChildren.OfType<IAcceptControlLayer>();
                foreach (var singleLayer in acceeptsControlLayers)
                    singleLayer.InputAction.BindTo(inputLayer.InputAction);
            }

            //processPlatform
            var platformLayers = InternalChildren.OfType<IPlatformLayer>();
            foreach (var singleLayer in platformLayers)
            {
                var bindable = manager.GetBindable<PlatformType>(KaraokeSetting.Device);
                singleLayer.PlatformType.BindTo(bindable);

                //if same then trigger change
                if (singleLayer.PlatformType == bindable.Value)
                    singleLayer.PlatformType.TriggerChange();
            }
        }

        #endregion
    }
}
