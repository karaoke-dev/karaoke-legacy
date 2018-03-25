// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Mobile.Pieces;
using osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Mobile.Type;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Mobile
{
    public partial class KaraokeLightPanel
    {
        private const double transition_time = 1000;
        private const double panel_display_time = 2000;

        //record all the piece
        private List<IInfoPiece> _infoPieses = new List<IInfoPiece>();

        //background dim
        private Box _backgroundDimBox = new Box();

        /// <summary>
        /// prepare the InfoPiece for different action
        /// </summary>
        protected void PrepareKeyInfoPanel(BaseAction action)
        {
            if (action is KeyAction keyAction)
            {
                if ((keyAction?.Press ?? false) == false)
                    return;
            }
            else if (action is TapAction tapAction)
            {

            }
            else if (action is ScrollAction scrollAction)
            {

            }

            IInfoPiece boxContainer = new InfoPiece();

            PrepareShowInfoPanel(boxContainer);
        }

        /// <summary>
        /// show info panel
        /// </summary>
        protected void PrepareShowInfoPanel(IInfoPiece boxContainer)
        {
            //1. add to child list
            Add(boxContainer as Container);

            //2. call ShowInfoPiece
            ShowInfoPiece(boxContainer as Container);

            //3. set timer
            using (Content.BeginDelayedSequence(panel_display_time, true))
            {
                //4. call HideInfoPiece
                HideInfoPiece(boxContainer as Container);
            }
        }

        protected void ShowInfoPiece(Container boxContainer)
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

        protected void HideInfoPiece(Container boxContainer)
        {
            boxContainer.Alpha = 0;
        }
    }
}
