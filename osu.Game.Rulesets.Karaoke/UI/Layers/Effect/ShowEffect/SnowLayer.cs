// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Effect.ShowEffect
{
    /// <summary>
    ///     show Visualisation layer
    /// </summary>
    public class SnowLayer : Container
    {
        public int SnowGenerateParSecond { get; set; } = 5; //max can have 1000 snow at the scene
        public bool EnableNewSnow { get; set; } = true; //if disable ,will stop snow
        public int SnowExpireTime { get; set; } = 6000; //snow will appear
        public bool Enabled { get; set; } = true; //if disable ,will pause and no show will fall down
        public float Speed { get; set; } = 1; //snow speed
        public float WingAffection { get; set; } = 3; //as wing speed
        public float SnowSize { get; set; } = 0.3f; //snow size
        public string TexturePath { get; set; } = @"Play/Karaoke/Layer/Snow/Snow";

        private TextureStore texture;
        private readonly Container snowContainer = new Container();
        private readonly Random random;

        /// <summary>
        ///     initialize
        /// </summary>
        public SnowLayer(int snowNumber = 600)
        {
            Width = 512;
            Height = 450;

            Children = new Drawable[]
            {
                snowContainer
            };

            random = new Random();
        }

        #region Disposal

        /// <summary>
        ///     dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            snowContainer.Dispose();
            base.Dispose(disposing);
        }

        #endregion

        /// <summary>
        ///     update
        /// </summary>
        protected override void Update()
        {
            if (!Enabled)
                return;

            base.Update();

            var currentTime = Time.Current;

            var isCreateShow = !snowContainer.Children.Any() || (snowContainer.Children.LastOrDefault() as SnowSpitie).CreateTime + 1000 / SnowGenerateParSecond < currentTime;

            //if can generate new snow
            if (isCreateShow && EnableNewSnow)
            {
                var currentAlpha = (float)random.Next(0, 255) / 255;
                var newFlake = new SnowSpitie
                {
                    Texture = texture.Get(TexturePath),
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    Colour = Color4.White,
                    Position = new Vector2(random.Next(0, (int)Width), -20),
                    Depth = 1,
                    CreateTime = currentTime,
                    Scale = new Vector2(1f, 1f) * SnowSize,
                    Alpha = currentAlpha,
                    HorizontalSpeed = random.Next(-100, 100) + WingAffection * 10
                };
                snowContainer.Add(newFlake);
            }

            //update each snow position
            foreach (SnowSpitie sprite in snowContainer.Children)
                if (sprite is SnowSpitie snow)
                {
                    snow.X = snow.X + snow.HorizontalSpeed / 1000f;
                    snow.Y = snow.Y + 1 * Speed;

                    //recycle
                    if (snow.CreateTime + SnowExpireTime < currentTime)
                        snowContainer.Children.ToList().Remove(snow);
                }
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            texture = textures;
        }
    }

    /// <summary>
    ///     show spirit
    /// </summary>
    public class SnowSpitie : Sprite
    {
        /// <summary>
        ///     Horizontal speed
        /// </summary>
        public float HorizontalSpeed { get; set; }

        /// <summary>
        ///     create time
        /// </summary>
        public double CreateTime { get; set; }
    }
}
