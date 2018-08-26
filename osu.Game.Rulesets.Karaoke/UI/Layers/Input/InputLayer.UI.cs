// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Input
{
    public partial class InputLayer
    {
        private FillFlowContainer<Button> _fillFlowContainer;
        private void initialUi()
        {
            _fillFlowContainer = new FillFlowContainer<Button>
            {
                RelativeSizeAxes = Axes.Y,
                Origin = Anchor.CentreRight,
                Anchor = Anchor.CentreRight,
                Width = 80
            };
            var content = new Drawable[2][];
            content[0] = new[] { new Container() };
            content[1] = new[] { _fillFlowContainer };
            InternalChild  = new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                Content = content
            };
        }

        public void AddRightSideButton(Button button)
        {
            _fillFlowContainer.Add(button);
        }
    }
}
