// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Diagnostics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.OpenGL.Buffers;
using osu.Framework.Graphics.OpenGL.Vertices;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input;
using osu.Framework.Timing;
using OpenTK;
using OpenTK.Graphics.ES30;

namespace osu.Game.Rulesets.Karaoke.UI.Cursor
{
    internal class CursorTrail : Drawable
    {
        private Vector2 size => texture.Size * Scale;

        private readonly TrailDrawNodeSharedData trailDrawNodeSharedData = new TrailDrawNodeSharedData();
        private const int max_sprites = 2048;

        private readonly TrailPart[] parts = new TrailPart[max_sprites];

        private readonly InputResampler resampler = new InputResampler();
        //public override bool HandleInput => true;

        private int currentIndex;

        private Shader shader;
        private Texture texture;

        private double timeOffset;

        private float time;

        private Vector2? lastPosition;

        public CursorTrail()
        {
            // as we are currently very dependent on having a running clock, let's make our own clock for the time being.
            Clock = new FramedClock();

            RelativeSizeAxes = Axes.Both;

            for (var i = 0; i < max_sprites; i++)
            {
                parts[i].InvalidationID = 0;
                parts[i].WasUpdated = true;
            }
        }

        public override bool ReceiveMouseInputAt(Vector2 screenSpacePos)
        {
            return true;
        }

        protected override DrawNode CreateDrawNode()
        {
            return new TrailDrawNode();
        }

        protected override void ApplyDrawNode(DrawNode node)
        {
            base.ApplyDrawNode(node);

            var tNode = (TrailDrawNode)node;
            tNode.Shader = shader;
            tNode.Texture = texture;
            tNode.Size = size;
            tNode.Time = time;
            tNode.Shared = trailDrawNodeSharedData;

            for (var i = 0; i < parts.Length; ++i)
                if (parts[i].InvalidationID > tNode.Parts[i].InvalidationID)
                    tNode.Parts[i] = parts[i];
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            resetTime();
        }

        protected override void Update()
        {
            base.Update();

            Invalidate(Invalidation.DrawNode, shallPropagate: false);

            const int fade_clock_reset_threshold = 1000000;

            time = (float)(Time.Current - timeOffset) / 500f;
            if (time > fade_clock_reset_threshold)
                resetTime();
        }

        protected override bool OnMouseMove(InputState state)
        {
            if (lastPosition == null)
            {
                lastPosition = state.Mouse.NativeState.Position;
                resampler.AddPosition(lastPosition.Value);
                return base.OnMouseMove(state);
            }

            foreach (var pos2 in resampler.AddPosition(state.Mouse.NativeState.Position))
            {
                Trace.Assert(lastPosition.HasValue);

                var pos1 = lastPosition.Value;
                var diff = pos2 - pos1;
                var distance = diff.Length;
                var direction = diff / distance;

                var interval = size.X / 2 * 0.9f;

                for (var d = interval; d < distance; d += interval)
                {
                    lastPosition = pos1 + direction * d;
                    addPosition(lastPosition.Value);
                }
            }

            return base.OnMouseMove(state);
        }

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, TextureStore textures)
        {
            shader = shaders?.Load(@"CursorTrail", FragmentShaderDescriptor.TEXTURE);
            texture = textures.Get(@"Cursor/cursortrail");
            Scale = new Vector2(1 / texture.ScaleAdjust);
        }

        private void resetTime()
        {
            for (var i = 0; i < parts.Length; ++i)
            {
                parts[i].Time -= time;
                ++parts[i].InvalidationID;
            }

            time = 0;
            timeOffset = Time.Current;
        }

        private void addPosition(Vector2 pos)
        {
            parts[currentIndex].Position = pos;
            parts[currentIndex].Time = time;
            ++parts[currentIndex].InvalidationID;

            currentIndex = (currentIndex + 1) % max_sprites;
        }

        private struct TrailPart
        {
            public Vector2 Position;
            public float Time;
            public long InvalidationID;
            public bool WasUpdated;
        }

        private class TrailDrawNodeSharedData
        {
            public VertexBuffer<TexturedVertex2D> VertexBuffer;
        }

        private class TrailDrawNode : DrawNode
        {
            public readonly TrailPart[] Parts = new TrailPart[max_sprites];
            public Shader Shader;
            public Texture Texture;

            public float Time;
            public TrailDrawNodeSharedData Shared;
            public Vector2 Size;

            public TrailDrawNode()
            {
                for (var i = 0; i < max_sprites; i++)
                {
                    Parts[i].InvalidationID = 0;
                    Parts[i].WasUpdated = false;
                }
            }

            public override void Draw(Action<TexturedVertex2D> vertexAction)
            {
                if (Shared.VertexBuffer == null)
                    Shared.VertexBuffer = new QuadVertexBuffer<TexturedVertex2D>(max_sprites, BufferUsageHint.DynamicDraw);

                Shader.GetUniform<float>("g_FadeClock").Value = Time;

                int updateStart = -1, updateEnd = 0;
                for (var i = 0; i < Parts.Length; ++i)
                    if (Parts[i].WasUpdated)
                    {
                        if (updateStart == -1)
                            updateStart = i;
                        updateEnd = i + 1;

                        var start = i * 4;
                        var end = start;

                        var pos = Parts[i].Position;
                        var colour = DrawInfo.Colour;
                        colour.TopLeft.Linear.A = Parts[i].Time + colour.TopLeft.Linear.A;
                        colour.TopRight.Linear.A = Parts[i].Time + colour.TopRight.Linear.A;
                        colour.BottomLeft.Linear.A = Parts[i].Time + colour.BottomLeft.Linear.A;
                        colour.BottomRight.Linear.A = Parts[i].Time + colour.BottomRight.Linear.A;

                        Texture.DrawQuad(
                            new Quad(pos.X - Size.X / 2, pos.Y - Size.Y / 2, Size.X, Size.Y),
                            colour,
                            null,
                            v => Shared.VertexBuffer.Vertices[end++] = v);

                        Parts[i].WasUpdated = false;
                    }
                    else if (updateStart != -1)
                    {
                        Shared.VertexBuffer.UpdateRange(updateStart * 4, updateEnd * 4);
                        updateStart = -1;
                    }

                // Update all remaining vertices that have been changed.
                if (updateStart != -1)
                    Shared.VertexBuffer.UpdateRange(updateStart * 4, updateEnd * 4);

                base.Draw(vertexAction);

                Shader.Bind();

                Texture.TextureGL.Bind();
                Shared.VertexBuffer.Draw();

                Shader.Unbind();
            }
        }
    }
}
