// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Timers;
using osu.Framework.Input.Events;
using osu.Framework.Input.States;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action;
using osuTK;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Input
{
    public partial class InputLayer
    {
        // Touch screen config
        public BindableObject<MobileScrollAnixConfig> MobileScrollAnixConfig { get; set; } = new BindableObject<MobileScrollAnixConfig>(new MobileScrollAnixConfig());

        //judge the direction when moving more then this pixal
        private readonly double _judgePixal = 20;

        //Hold time
        private readonly Timer _timer = new Timer
        {
            Interval = 200
        };

        //mouse move dircetion
        private Vector2 _moveDirection = Vector2.Zero;

        protected void InitialTouchScreen()
        {
            _timer.Elapsed += (a, b) =>
            {
                _timer.Stop();

                //var tapConfig = MobileScrollAnixConfig.Value.TagConfigs[TouchScreenTapInteractive.Hold];
                //OnTap(tapConfig);
            };
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            _timer.Start();
            return base.OnMouseDown(e);
        }

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            var mouseDownPosition = e.ScreenSpaceMousePosition;
            var nowPosition = e.ScreenSpaceMousePosition;
            var movingPosition = nowPosition - mouseDownPosition;
            var deltaPosition = nowPosition - e.ScreenSpaceMousePosition;//TODO : last position

            //if noe scroll mode
            if (_moveDirection == Vector2.Zero)
            {
                _moveDirection = new Vector2((int)(movingPosition.X / _judgePixal), (int)(movingPosition.Y / _judgePixal));
                _timer.Stop();
            }
            else //scroll mode
            {
                if (_moveDirection.X != 0)
                {
                    SingleAnixConfig singleAnixConfig;
                    if (MobileScrollAnixConfig.Value.ScrollConfigs.TryGetValue(TouchScreenScrollInteractive.XAnix, out singleAnixConfig))
                        OnScroll(singleAnixConfig, true, deltaPosition.X, movingPosition.X);
                }
                else if (_moveDirection.Y != 0)
                {
                    SingleAnixConfig singleAnixConfig;
                    if (MobileScrollAnixConfig.Value.ScrollConfigs.TryGetValue(TouchScreenScrollInteractive.YAnix, out singleAnixConfig))
                        OnScroll(singleAnixConfig, true, deltaPosition.Y, movingPosition.Y);
                }
            }

            return base.OnMouseMove(e);
        }

        protected override bool OnMouseUp(MouseUpEvent e)
        {
            var mouseDownPosition = e.ScreenSpaceMouseDownPosition;
            var nowPosition = e.ScreenSpaceMousePosition;
            var movingPosition = nowPosition - mouseDownPosition;
            var deltaPosition = nowPosition - e.ScreenSpaceMousePosition;//TODO : last position


            if (_moveDirection == Vector2.Zero) //tap mode
            {
                TapConfig tapConfig;
                if (MobileScrollAnixConfig.Value.TagConfigs.TryGetValue(TouchScreenTapInteractive.SingleTap, out tapConfig))
                    OnTap(tapConfig);
            }
            else //scroll mode
            {
                if (_moveDirection.X != 0)
                {
                    SingleAnixConfig singleAnixConfig;
                    if (MobileScrollAnixConfig.Value.ScrollConfigs.TryGetValue(TouchScreenScrollInteractive.XAnix, out singleAnixConfig))
                        OnScroll(singleAnixConfig, false, deltaPosition.X, movingPosition.X);
                }
                else if (_moveDirection.Y != 0)
                {
                    SingleAnixConfig singleAnixConfig;
                    if (MobileScrollAnixConfig.Value.ScrollConfigs.TryGetValue(TouchScreenScrollInteractive.YAnix, out singleAnixConfig))
                        OnScroll(singleAnixConfig, false, deltaPosition.Y, movingPosition.Y);
                }
            }

            //clear direction
            _moveDirection = Vector2.Zero;
            _timer.Stop();

            return base.OnMouseUp(e);
        }

        protected override bool OnDoubleClick(DoubleClickEvent e)
        {
            //trigger double tap
            TapConfig tapConfig;
            if (MobileScrollAnixConfig.Value.TagConfigs.TryGetValue(TouchScreenTapInteractive.DoubleTap, out tapConfig))
                OnTap(tapConfig);

            return base.OnDoubleClick(e);
        }

        /// <summary>
        ///     tap
        /// </summary>
        /// <returns><c>true</c>, if pressed was oned, <c>false</c> otherwise.</returns>
        /// <param name="config">Action.</param>
        /// <param name="tap">Action.</param>
        protected void OnTap(TapConfig config, bool tap = true)
        {
            if (config == null)
                return;

            var tapAction = new TapAction
            {
                KaraokeTapAction = config.KaraokeTapAction,
                Tap = tap
            };
            InputAction.Value = tapAction;
        }

        /// <summary>
        ///     scroll
        /// </summary>
        /// <param name="config"></param>
        /// <param name="touch"></param>
        /// <param name="relativePosition"></param>
        /// <param name="totalmovingPosition"></param>
        /// <returns></returns>
        protected void OnScroll(SingleAnixConfig config, bool touch, double relativePosition, double totalmovingPosition)
        {
            if (config == null)
                return;

            var scrollAction = new ScrollAction
            {
                KaraokeScrollAction = config.KaraokeScrollAction,
                Touch = touch,
                RelativeMovingPosition = relativePosition,
                TotalMovingPosition = totalmovingPosition
            };
            InputAction.Value = scrollAction;
        }
    }
}
