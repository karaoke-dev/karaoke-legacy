using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Mobile.Pieces;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Mobile
{
    public partial class KaraokeLightPanel
    {
        private const double transition_time = 1000;
        private const double panel_display_time = 2000;

        /// <summary>
        /// prepare the InfoPiece for different action
        /// </summary>
        protected void PrepareKeyInfoPanel(KeyAction action)
        {
            if((action?.Press ?? false) == false)
                return;


            InfoPiece boxContainer = new InfoPiece();

            PrepareShowInfoPanel(boxContainer);
        }

        /// <summary>
        /// prepare the InfoPiece for different action
        /// </summary>
        protected void PrepareTapInfoPanel(TapAction action)
        {
            if ((action?.Tap ?? false) == false)
                return;

            InfoPiece boxContainer = new InfoPiece();

            PrepareShowInfoPanel(boxContainer);
        }

        /// <summary>
        /// prepare the InfoPiece for different action
        /// </summary>
        protected void PrepareScrollInfoPanel(ScrollAction action)
        {
            if ((action?.Touch ?? false) == false)
                return;

            InfoPiece boxContainer = new InfoPiece();

            PrepareShowInfoPanel(boxContainer);
        }

        /// <summary>
        /// show info panel
        /// </summary>
        protected void PrepareShowInfoPanel(InfoPiece boxContainer)
        {
            //1. add to child list
            Add(boxContainer);

            //2. call ShowInfoPiece
            ShowInfoPiece(boxContainer);

            //3. set timer
            using (Content.BeginDelayedSequence(panel_display_time, true))
            {
                //4. call HideInfoPiece
                HideInfoPiece(boxContainer);
            }
        }

        protected void ShowInfoPiece(InfoPiece boxContainer)
        {
            boxContainer.Alpha = 1;
            boxContainer.ScaleTo(0.2f);
            boxContainer.RotateTo(-20);

            using (Content.BeginDelayedSequence(300, true))
            {
                boxContainer.ScaleTo(1, transition_time, Easing.OutElastic);
                boxContainer.RotateTo(0, transition_time / 2, Easing.OutQuint);

                //textContainer.MoveTo(Vector2.Zero, transition_time, Easing.OutExpo);
                //Content.FadeIn(transition_time, Easing.OutExpo);
            }
        }

        protected void HideInfoPiece(InfoPiece boxContainer)
        {
            boxContainer.Alpha = 0;
        }
    }
}
