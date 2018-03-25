﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Desktop;
using osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Mobile;
using osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Type;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel
{
    public partial class ControlPanelLayer
    {
        private IControlPanel _panelLayer;
        /// <summary>
        /// On platform change
        /// </summary>
        /// <param name="platformType"></param>
        protected void OnPlatformChanged(PlatformType platformType)
        {
            //change platform by 
            switch (platformType)
            {
                case Configuration.PlatformType.Desktop:
                    _panelLayer = new KaraokePanelOverlay(_playField);
                    break;
                    default:
                    _panelLayer = new KaraokeLightPanel(_playField);
                    break;
            }

            //initial key eveht
            _panelLayer.KeyAction.BindTo(KeyAction);
            _panelLayer.TapAction.BindTo(TapAction);
            _panelLayer.ScrollAction.BindTo(ScrollAction);

            //add to child
            Children = new Drawable[]
            {
                _panelLayer as Container
            };
        }
    }
}
