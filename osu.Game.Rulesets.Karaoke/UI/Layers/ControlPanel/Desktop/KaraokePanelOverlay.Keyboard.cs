// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Desktop
{
    public partial class KaraokePanelOverlay
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns><c>true</c>, if pressed was oned, <c>false</c> otherwise.</returns>
        /// <param name="action">Action.</param>
        protected void OnKeyAction(BaseAction action)
        {
            if (action == null)
                return;

            if (action is KeyAction keyAction)
            {
                if ((keyAction?.Press ?? false) == false)
                    return;

                switch (keyAction.KaraokeKeyAction)
                {
                    case KaraokeKeyAction.FirstLyric:
                        FirstLyricButton.Action?.Invoke();
                        break;
                    case KaraokeKeyAction.PreviousLyric:
                        PreviousLyricButton.Action?.Invoke();
                        break;
                    case KaraokeKeyAction.NextLyric:
                        NextLyricButton.Action?.Invoke();
                        break;
                    case KaraokeKeyAction.PlayAndPause:
                        PlayPauseButton.Action?.Invoke();
                        break;

                    case KaraokeKeyAction.IncreaseSpeed:
                        SpeedSlider.IncreaseButton.Action?.Invoke();
                        break;
                    case KaraokeKeyAction.DecreaseSpeed:
                        SpeedSlider.DecreaseButton.Action?.Invoke();
                        break;
                    case KaraokeKeyAction.ResetSpeed:
                        SpeedSlider.ResetToDefauleValue();
                        break;


                    case KaraokeKeyAction.IncreaseTone:
                        ToneSlider.IncreaseButton.Action?.Invoke();
                        break;
                    case KaraokeKeyAction.DecreaseTone:
                        ToneSlider.DecreaseButton.Action?.Invoke();
                        break;
                    case KaraokeKeyAction.ResetTone:
                        ToneSlider.ResetToDefauleValue();
                        break;


                    case KaraokeKeyAction.IncreaseLyricAppearTime:
                        LyricOffectSlider.IncreaseButton.Action?.Invoke();
                        break;
                    case KaraokeKeyAction.DecreaseLyricAppearTime:
                        LyricOffectSlider.DecreaseButton.Action?.Invoke();
                        break;

                    case KaraokeKeyAction.ResetLyricAppearTime:
                        LyricOffectSlider.ResetToDefauleValue();
                        break;

                    case KaraokeKeyAction.OpenPanel:
                        ToggleVisibility();
                        break;
                }
            }
            else if (action is TapAction tapAction)
            {
            }
            else if (action is ScrollAction scrollAction)
            {
            }
        }
    }
}
