// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;

namespace osu.Game.Rulesets.Karaoke.UI.Panel
{
    /// <summary>
    /// this container is use for detect HotKey pressing
    /// </summary>
    public class KaraokeHotkeyPanel : Container, IKeyBindingHandler<KaraokeAction>
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
        public bool OnPressed(KaraokeAction action)
        {
            switch (action)
            {
                case KaraokeAction.FirstLyric:
                    KaraokePanelOverlay?.FirstLyricButton.Action?.Invoke();
                    break;
                case KaraokeAction.PreviousLyric:
                    KaraokePanelOverlay?.PreviousLyricButton.Action?.Invoke();
                    break;
                case KaraokeAction.NextLyric:
                    KaraokePanelOverlay?.NextLyricButton.Action?.Invoke();
                    break;
                case KaraokeAction.PlayAndPause:
                    KaraokePanelOverlay?.PlayPauseButton.Action?.Invoke();
                    break;

                case KaraokeAction.IncreaseSpeed:
                    KaraokePanelOverlay?.SpeedSlider.IncreaseButton.Action?.Invoke();
                    break;
                case KaraokeAction.DecreaseSpeed:
                    KaraokePanelOverlay?.SpeedSlider.DecreaseButton.Action?.Invoke();
                    break;
                case KaraokeAction.ResetSpeed:
                    KaraokePanelOverlay?.SpeedSlider.ResetToDefauleValue();
                    break;


                case KaraokeAction.IncreaseTone:
                    KaraokePanelOverlay?.ToneSlider.IncreaseButton.Action?.Invoke();
                    break;
                case KaraokeAction.DecreaseTone:
                    KaraokePanelOverlay?.ToneSlider.DecreaseButton.Action?.Invoke();
                    break;
                case KaraokeAction.ResetTone:
                    KaraokePanelOverlay?.ToneSlider.ResetToDefauleValue();
                    break;


                case KaraokeAction.IncreaseLyricAppearTime:
                    KaraokePanelOverlay?.LyricOffectSlider.IncreaseButton.Action?.Invoke();
                    break;
                case KaraokeAction.DecreaseLyricAppearTime:
                    KaraokePanelOverlay?.LyricOffectSlider.DecreaseButton.Action?.Invoke();
                    break;

                case KaraokeAction.ResetLyricAppearTime:
                    KaraokePanelOverlay?.LyricOffectSlider.ResetToDefauleValue();
                    break;

                case KaraokeAction.OpenPanel:
                    KaraokePanelOverlay?.ToggleVisibility();
                    break;
            }

            return true;
        }

        public bool OnReleased(KaraokeAction action) => true;
    }
}
