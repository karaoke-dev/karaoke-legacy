// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Screens;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Mobile.Pieces
{
    /// <summary>
    /// use to show the into
    /// <see cref="ScreenWhiteBox"/>
    /// </summary>
    public class InfoPiece : Container
    {
        private readonly FillFlowContainer textContainer;

        public InfoPiece()
        {
            Size = new Vector2(0.3f);
            RelativeSizeAxes = Axes.Both;
            CornerRadius = 20;
            Masking = true;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,

                    Colour = getColourFor(GetType()),
                    Alpha = 0.2f,
                    Blending = BlendingMode.Additive,
                },
                textContainer = new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Direction = FillDirection.Vertical,
                    Children = new Drawable[]
                    {
                        new SpriteIcon
                        {
                            Icon = FontAwesome.fa_universal_access,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Size = new Vector2(50),
                        },
                        new OsuSpriteText
                        {
                            Text = GetType().Name,
                            Colour = getColourFor(GetType()).Lighten(0.8f),
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            TextSize = 50,
                        },
                        new OsuSpriteText
                        {
                            Text = "is not yet ready for use!",
                            TextSize = 20,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                        },
                        new OsuSpriteText
                        {
                            Text = "please check back a bit later.",
                            TextSize = 14,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                        },
                    }
                },
            };
        }

        private Color4 getColourFor(System.Type type)
        {
            int hash = type.Name.GetHashCode();
            byte r = (byte)MathHelper.Clamp(((hash & 0xFF0000) >> 16) * 0.8f, 20, 255);
            byte g = (byte)MathHelper.Clamp(((hash & 0x00FF00) >> 8) * 0.8f, 20, 255);
            byte b = (byte)MathHelper.Clamp((hash & 0x0000FF) * 0.8f, 20, 255);
            return new Color4(r, g, b, 255);
        }
    }
}
