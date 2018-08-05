// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Lines;
using osu.Game.Rulesets.Objects;
using osu.Game.Tests.Visual;
using OpenTK;
using OpenTK.Graphics.ES30;

namespace osu.Game.Rulesets.Karaoke.Tests
{
    /// <summary>
    /// a literesting problem : line renderer problem
    /// .
    /// 2017/12/24
    /// I'm not sure this post will help
    /// https://forum.libcinder.org/topic/smooth-thick-lines-using-geometry-shader
    /// </summary>
    /// [TestFixture]
    [Ignore("getting CI working")]
    [System.ComponentModel.Description("Test EditableMainKaraokeText class")]
    public class TestCaseLineRenderer : OsuTestCase
    {
        private Path path;

        private Path path2;

        private BufferedContainer container;

        private List<Vector2> points = new List<Vector2>
        {
            new Vector2(0, 120),
            new Vector2(200, 300),
            new Vector2(500, 350)
        };

        [BackgroundDependencyLoader]
        private void load(RulesetStore rulesets)
        {
            Add(new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    container = new BufferedContainer
                    {
                        CacheDrawnFrameBuffer = true,
                        Width = 400,
                        Height = 400,
                        Position = new Vector2(-400, -600),
                        Scale = new Vector2(2),
                        Children = new Drawable[]
                        {
                            path2 = new Path
                            {
                                Name = "Path1",
                                Blending = BlendingMode.None,
                            },
                        }
                    },
                    path = new Path
                    {
                        Name = "Path2",
                        Origin = Anchor.Centre,
                        PathWidth = 25,
                    },
                },
            });

            initialTexture();

            initialPath();

            container.Attach(RenderbufferInternalFormat.DepthComponent16);
        }

        private void initialPath()
        {
            points = new List<Vector2>
            {
                new Vector2(0, 120),
                new Vector2(200, 300),
                new Vector2(500, 0)
            };

            path.ClearVertices();
            path2.ClearVertices();
            //if (points.Count >= 3)
            //{
            var ps = new BezierApproximator(points).CreateBezier();
            foreach (var point in ps)
                path2.AddVertex(point);
            //}
            //else
            //{
            foreach (var point in points)
                path.AddVertex(point);
            //}
            //points.RemoveAt(0);
        }

        private void initialTexture()
        {
            /*
            int textureWidth = (int)path.PathWidth * 2;

            var texture = new Texture(textureWidth, 1);

            //initialise background
            var upload = new TextureUpload(textureWidth * 4);
            var bytes = upload.Data;

            const float aa_portion = .5f;
            const float border_portion = 0.5f;

            for (int i = 0; i < textureWidth; i++)
            {
                float progress = (float)i / textureWidth;

                if (progress <= border_portion)
                {
                    bytes[i * 4] = 255;
                    bytes[i * 4 + 1] = 255;
                    bytes[i * 4 + 2] = 255;
                    bytes[i * 4 + 3] = (byte)(Math.Min(progress / aa_portion, 1) * 255);
                }
                else
                {
                    bytes[i * 4] = 255;
                    bytes[i * 4 + 1] = 255;
                    bytes[i * 4 + 2] = 255;
                    //bytes[i * 4 + 3] = 255;
                    bytes[i * 4 + 3] = (byte)(Math.Min(progress / aa_portion, 1) * 255);
                }
            }

            texture.SetData(upload);
            path.Texture = texture;
            path2.Texture = texture;
            */


            /*
            Color4 AccentColour = Color4.White;
            int textureWidth = (int)path.PathWidth * 2;
            var texture = new Texture(textureWidth, 1);

            //initialise background
            var upload = new TextureUpload(textureWidth * 4);
            var bytes = upload.Data;

            const float aa_portion = 0.02f;
            const float border_portion = 0.128f;
            const float gradient_portion = 1 - border_portion;

            const float opacity_at_centre = 0.3f;
            const float opacity_at_edge = 0.8f;

            for (int i = 0; i < textureWidth; i++)
            {
                float progress = (float)i / (textureWidth - 1);

                progress -= border_portion;

                bytes[i * 4] = (byte)(AccentColour.R * 255);
                bytes[i * 4 + 1] = (byte)(AccentColour.G * 255);
                bytes[i * 4 + 2] = (byte)(AccentColour.B * 255);
                bytes[i * 4 + 3] = (byte)(255 - (opacity_at_edge - (opacity_at_edge - opacity_at_centre) * progress / gradient_portion) * (AccentColour.A * 255));
            }

            texture.SetData(upload);
            path.Texture = texture;
            path2.Texture = texture;
            */


            /*
            Color4 AccentColour = Color4.White;
            int textureWidth = (int)path.PathWidth * 2;
            var texture = new Texture(textureWidth, 1);

            //initialise background
            var upload = new TextureUpload(textureWidth * 4);
            var bytes = upload.Data;

            const float aa_portion = 0.02f;
            const float border_portion = 0.128f;
            const float gradient_portion = 1 - border_portion;

            const float opacity_at_centre = 0.3f;
            const float opacity_at_edge = 0.8f;

            for (int i = 0; i < textureWidth; i++)
            {
                float progress = (float)i / (textureWidth - 1);

                if (progress <= border_portion)
                {
                    bytes[i * 4] = 255;
                    bytes[i * 4 + 1] = 255;
                    bytes[i * 4 + 2] = 255;
                    bytes[i * 4 + 3] = (byte)(Math.Min(progress / aa_portion, 1) * 255);
                }
                else
                {
                    progress -= border_portion;

                    bytes[i * 4] = (byte)(AccentColour.R * 255);
                    bytes[i * 4 + 1] = (byte)(AccentColour.G * 255);
                    bytes[i * 4 + 2] = (byte)(AccentColour.B * 255);
                    bytes[i * 4 + 3] = (byte)((opacity_at_edge - (opacity_at_edge - opacity_at_centre) * progress / gradient_portion) * (AccentColour.A * 255));
                }
            }

            texture.SetData(upload);
            path.Texture = texture;
            path2.Texture = texture;
            */
        }
    }
}
