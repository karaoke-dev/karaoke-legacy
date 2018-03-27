// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Input;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Input
{
    public partial class InputLayer
    {
        // Touch screen config
        public BindableObject<MobileScrollAnixConfig> MobileScrollAnixConfig { get; set; } = new BindableObject<MobileScrollAnixConfig>(new MobileScrollAnixConfig());

        //mouse move dircetion
        private Vector2 _moveDirection = Vector2.Zero;

        //judge the direction when moving more then this pixal
        private double _judgePixal = 20;

        protected override bool OnMouseDown(InputState state, MouseDownEventArgs args)
        {
            return base.OnMouseDown(state, args);
        }

        protected override bool OnMouseMove(InputState state)
        {
            return base.OnMouseMove(state);
        }

        protected override bool OnMouseUp(InputState state, MouseUpEventArgs args)
        {
            if (_moveDirection == Vector2.Zero)
            {

            }
            return base.OnMouseUp(state, args);
        }

        /// <summary>
        /// tap
        /// </summary>
        /// <returns><c>true</c>, if pressed was oned, <c>false</c> otherwise.</returns>
        /// <param name="action">Action.</param>
        protected void OnTap(KaraokeTapAction action)
        {
            TapAction tapAction = new TapAction()
            {
                KaraokeTapAction = action,
                Tap = true,
            };
            InputAction.Value = tapAction;
        }

        /// <summary>
        /// scroll
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected void OnScroll(KaraokeScrollAction action)
        {
            ScrollAction scrollAction = new ScrollAction()
            {
                KaraokeScrollAction = action,
                Touch = false,
            };
            InputAction.Value = scrollAction;
        }
    }
}
