// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Overlays;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.UI.Extension;
using osu.Game.Rulesets.Karaoke.UI.Interface;
using osu.Game.Rulesets.Karaoke.UI.Layer.ControlPanel.Desktop.Pieces;
using osu.Game.Rulesets.Karaoke.UI.Layer.Input.Action;
using osu.Game.Rulesets.Karaoke.UI.Layer.Type;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.UI.Layer.ControlPanel.Desktop
{
    /// <summary>
    /// to show the Karaoke panel on Playfield 
    /// </summary>
    public partial class KaraokePanelOverlay : WaveOverlayContainer , IControlLayer
    {
        /// <summary>
        /// Key action
        /// </summary>
        public BindableObject<KeyAction> KeyAction { get; set; } = new BindableObject<KeyAction>(null);

        /// <summary>
        /// Tap action
        /// </summary>
        public BindableObject<TapAction> TapAction { get; set; } = new BindableObject<TapAction>(null);

        /// <summary>
        /// Scroll action
        /// </summary>
        public BindableObject<ScrollAction> ScrollAction { get; set; } = new BindableObject<ScrollAction>(null);

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="playField"></param>
        public KaraokePanelOverlay(IAmKaraokeField playField = null)
        {
            PlayField = playField;

            //key changed
            KeyAction.ValueChanged += OnKeyAction;
        }
    }
}
