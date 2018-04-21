// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Timers;
using osu.Framework.Input;
using osu.Game.Rulesets.Karaoke.Configuration;
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

        //Hold time
        private Timer _timer = new Timer()
        {
            Interval = 200,
        };

        protected void InitialTouchScreen()
        {
            _timer.Elapsed += (a, b) =>
            {
                _timer.Stop();

                var tapConfig = MobileScrollAnixConfig.Value.TagConfigs[TouchScreenTapInteractive.Hold];
                OnTap(tapConfig);
            };
        }

        protected override bool OnMouseDown(InputState state, MouseDownEventArgs args)
        {
            _timer.Start();
            return base.OnMouseDown(state, args);
        }

        protected override bool OnMouseMove(InputState state)
        {
            var mouseDownPosition = state.Mouse.PositionMouseDown ?? state.Mouse.Position;
            var nowPosition = state.Mouse.Position;
            var movingPosition = nowPosition - mouseDownPosition;
            var deltaPosition = nowPosition - state.Mouse.LastPosition;

            //if noe scroll mode
            if (_moveDirection == Vector2.Zero)
            {
                _moveDirection = new Vector2((int)(movingPosition.X / _judgePixal), (int)(movingPosition.Y / _judgePixal));
                _timer.Stop();
            }
            else//scroll mode
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
            return base.OnMouseMove(state);
        }

        protected override bool OnMouseUp(InputState state, MouseUpEventArgs args)
        {
            var mouseDownPosition = state.Mouse.PositionMouseDown ?? state.Mouse.Position;
            var nowPosition = state.Mouse.Position;
            var movingPosition = nowPosition - mouseDownPosition;
            var deltaPosition = nowPosition - state.Mouse.LastPosition;


            if (_moveDirection == Vector2.Zero)//tap mode
            {
                TapConfig tapConfig;
                if (MobileScrollAnixConfig.Value.TagConfigs.TryGetValue(TouchScreenTapInteractive.SingleTap, out tapConfig))
                    OnTap(tapConfig);
            }
            else//scroll mode
            {
                if (_moveDirection.X != 0)
                {
                    SingleAnixConfig singleAnixConfig;
                    if(MobileScrollAnixConfig.Value.ScrollConfigs.TryGetValue(TouchScreenScrollInteractive.XAnix, out singleAnixConfig))
                    OnScroll(singleAnixConfig, false,deltaPosition.X, movingPosition.X);
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

            return base.OnMouseUp(state, args);
        }

        protected override bool OnDoubleClick(InputState state)
        {
            //trigger double tap
            TapConfig tapConfig;
            if (MobileScrollAnixConfig.Value.TagConfigs.TryGetValue(TouchScreenTapInteractive.DoubleTap, out tapConfig))
                OnTap(tapConfig);

            return base.OnDoubleClick(state); 
        }

        /// <summary>
        /// tap
        /// </summary>
        /// <returns><c>true</c>, if pressed was oned, <c>false</c> otherwise.</returns>
        /// <param name="config">Action.</param>
        /// <param name="tap">Action.</param>
        protected void OnTap(TapConfig config,bool tap=true)
        {
            if(config==null)
                return;
            
            TapAction tapAction = new TapAction()
            {
                KaraokeTapAction = config.KaraokeTapAction,
                Tap = tap,
            };
            InputAction.Value = tapAction;
        }

        /// <summary>
        /// scroll
        /// </summary>
        /// <param name="config"></param>
        /// <param name="touch"></param>
        /// <param name="relativePosition"></param>
        /// <param name="totalmovingPosition"></param>
        /// <returns></returns>
        protected void OnScroll(SingleAnixConfig config, bool touch,double relativePosition,double totalmovingPosition)
        {
            if (config==null)
                return;

            ScrollAction scrollAction = new ScrollAction()
            {
                KaraokeScrollAction = config.KaraokeScrollAction,
                Touch = touch,
                RelativeMovingPosition = relativePosition,
                TotalMovingPosition = totalmovingPosition,
            };
            InputAction.Value = scrollAction;
        }
    }
}
