// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Graphics.UserInterface;
using osu.Game.Rulesets.Karaoke.Input;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Input
{
    public partial class InputLayer
    {
        private void initialUi()
        {
            Children = new Drawable[]
            {
                new TriangleButton()
                {
                    Origin = Anchor.BottomRight,
                    Anchor = Anchor.BottomRight,

                    Position = new Vector2(-50, -50),
                    Width = 70,
                    Height = 30,
                    Text = "Panel",
                    Action = () =>
                    {
                        //as press openPanel hotkey
                        OnPressed(KaraokeKeyAction.OpenPanel);
                    }
                }
            };
        }
    }
}
