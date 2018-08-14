﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Caching;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Note.Pieces
{
    /// <summary>
    ///     Represents length-wise portion of a hold note.
    /// </summary>
    public class BodyPiece : Container, IHasAccentColour
    {
        public Color4 AccentColour
        {
            get => accentColour;
            set
            {
                if (accentColour == value)
                    return;
                accentColour = value;

                updateAccentColour();
            }
        }

        private readonly Container subtractionLayer;

        private readonly Drawable background;
        private readonly BufferedContainer foreground;
        private readonly BufferedContainer subtractionContainer;

        private Color4 accentColour;

        private Cached subtractionCache = new Cached();

        public BodyPiece()
        {
            Blending = BlendingMode.Additive;

            Children = new[]
            {
                background = new Box { RelativeSizeAxes = Axes.Both },
                foreground = new BufferedContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    CacheDrawnFrameBuffer = true,
                    Children = new Drawable[]
                    {
                        new Box { RelativeSizeAxes = Axes.Both },
                        subtractionContainer = new BufferedContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            // This is needed because we're blending with another object
                            BackgroundColour = Color4.White.Opacity(0),
                            CacheDrawnFrameBuffer = true,
                            // The 'hole' is achieved by subtracting the result of this container with the parent
                            Blending = new BlendingParameters { AlphaEquation = BlendingEquation.ReverseSubtract },
                            Child = subtractionLayer = new CircularContainer
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                // Height computed in Update
                                Width = 1,
                                Masking = true,
                                Child = new Box
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Alpha = 0,
                                    AlwaysPresent = true
                                }
                            }
                        }
                    }
                }
            };
        }

        public override bool Invalidate(Invalidation invalidation = Invalidation.All, Drawable source = null, bool shallPropagate = true)
        {
            if ((invalidation & Invalidation.DrawSize) > 0)
                subtractionCache.Invalidate();

            return base.Invalidate(invalidation, source, shallPropagate);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            updateAccentColour();
        }

        protected override void Update()
        {
            base.Update();

            /*
            if (!subtractionCache.IsValid)
            {
                subtractionLayer.Width = 5;
                subtractionLayer.Height = Math.Max(0, DrawHeight - DrawWidth);
                subtractionLayer.EdgeEffect = new EdgeEffectParameters
                {
                    Colour = Color4.White,
                    Type = EdgeEffectType.Glow,
                    Radius = DrawWidth
                };

                foreground.ForceRedraw();
                subtractionContainer.ForceRedraw();

                subtractionCache.Validate();
            }
            */
        }

        private void updateAccentColour()
        {
            if (!IsLoaded)
                return;

            foreground.Colour = AccentColour.Opacity(0.8f);
            background.Colour = AccentColour.Opacity(0.4f);

            subtractionCache.Invalidate();
        }
    }
}
