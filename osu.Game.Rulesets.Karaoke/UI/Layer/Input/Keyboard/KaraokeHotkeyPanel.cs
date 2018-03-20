// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;

namespace osu.Game.Rulesets.Karaoke.UI.Layer.ControlPanel.Desktop
{
    /// <summary>
    /// this container is use for detect HotKey pressing
    /// </summary>
    public class KaraokeHotkeyPanel : Container, IKeyBindingHandler<KaraokeKeyAction>
    {
        protected KaraokePanelOverlay KaraokePanelOverlay;

        public KaraokeHotkeyPanel(KaraokePanelOverlay karaokePanelOverlay)
        {
            KaraokePanelOverlay = karaokePanelOverlay;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns><c>true</c>, if pressed was oned, <c>false</c> otherwise.</returns>
        /// <param name="action">Action.</param>
        public bool OnPressed(KaraokeKeyAction action)
        {
            switch (action)
            {
                case KaraokeKeyAction.FirstLyric:
                    KaraokePanelOverlay?.FirstLyricButton.Action?.Invoke();
                    break;
                case KaraokeKeyAction.PreviousLyric:
                    KaraokePanelOverlay?.PreviousLyricButton.Action?.Invoke();
                    break;
                case KaraokeKeyAction.NextLyric:
                    KaraokePanelOverlay?.NextLyricButton.Action?.Invoke();
                    break;
                case KaraokeKeyAction.PlayAndPause:
                    KaraokePanelOverlay?.PlayPauseButton.Action?.Invoke();
                    break;

                case KaraokeKeyAction.IncreaseSpeed:
                    KaraokePanelOverlay?.SpeedSlider.IncreaseButton.Action?.Invoke();
                    break;
                case KaraokeKeyAction.DecreaseSpeed:
                    KaraokePanelOverlay?.SpeedSlider.DecreaseButton.Action?.Invoke();
                    break;
                case KaraokeKeyAction.ResetSpeed:
                    KaraokePanelOverlay?.SpeedSlider.ResetToDefauleValue();
                    break;


                case KaraokeKeyAction.IncreaseTone:
                    KaraokePanelOverlay?.ToneSlider.IncreaseButton.Action?.Invoke();
                    break;
                case KaraokeKeyAction.DecreaseTone:
                    KaraokePanelOverlay?.ToneSlider.DecreaseButton.Action?.Invoke();
                    break;
                case KaraokeKeyAction.ResetTone:
                    KaraokePanelOverlay?.ToneSlider.ResetToDefauleValue();
                    break;


                case KaraokeKeyAction.IncreaseLyricAppearTime:
                    KaraokePanelOverlay?.LyricOffectSlider.IncreaseButton.Action?.Invoke();
                    break;
                case KaraokeKeyAction.DecreaseLyricAppearTime:
                    KaraokePanelOverlay?.LyricOffectSlider.DecreaseButton.Action?.Invoke();
                    break;

                case KaraokeKeyAction.ResetLyricAppearTime:
                    KaraokePanelOverlay?.LyricOffectSlider.ResetToDefauleValue();
                    break;

                case KaraokeKeyAction.OpenPanel:
                    KaraokePanelOverlay?.ToggleVisibility();
                    break;
            }

            return true;
        }

        public bool OnReleased(KaraokeKeyAction action) => true;
    }
}
