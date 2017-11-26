﻿using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Timing;
using osu.Framework.Allocation;
using osu.Game.Configuration;
using osu.Framework.Graphics.Containers;
using osu.Framework.MathUtils;
using osu.Framework.Graphics.Shapes;

namespace osu.Game.Rulesets.Karaoke.UI.Layer.ShowEffect
{

    /// <summary>
    /// show Visualisation layer
    /// </summary>
    public class SnowLayer : Container
    {
        public int SnowGenerateParSecond { get; set; } = 5;//max can have 1000 snow at the scene
        public bool EnableNewSnow { get; set; } = true;//if disable ,will stop snow
        public int SnowExpireTime { get; set; } = 6000;//snow will appear
        public bool Enabled { get; set; } = true;//if disable ,will pause and no show will fall down
        public float Speed { get; set; } = 1; //snow speed
        public float WingAffection { get; set; } = 3; //as wing speed
        public float SnowSize { get; set; } = 0.3f; //snow size
        public String TexturePath { get; set; } = @"Play/Karaoke/Layer/Snow/Snow";

        private TextureStore texture;
        private Container snowContainer = new Container();
        Random random;

        /// <summary>
        /// initialize
        /// </summary>
        public SnowLayer(int snowNumber = 600)
        {
            Width = 512;
            Height = 450;

            this.Children = new Drawable[]
            {
                snowContainer,
            };

            random = new Random();
        }

        /// <summary>
        /// dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            snowContainer.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// update
        /// </summary>
        protected override void Update()
        {
            if (!Enabled)
                return;

            base.Update();

            double currentTime = this.Time.Current;

            bool isCreateShow = !snowContainer.Children.Any() || (snowContainer.Children.LastOrDefault() as SnowSpitie).CreateTime + (1000 / SnowGenerateParSecond) < currentTime;

            //if can generate new snow
            if (isCreateShow && EnableNewSnow)
            {
                float currentAlpha = (float)random.Next(0, 255) / 255;
                SnowSpitie newFlake = new SnowSpitie()
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
                    HorizontalSpeed = random.Next(-100, 100) + WingAffection * 10,
                };
                snowContainer.Add(newFlake);
            }

            //update each snow position
            foreach (SnowSpitie sprite in snowContainer.Children)
            {
                if (sprite is SnowSpitie snow)
                {
                    snow.X = snow.X + snow.HorizontalSpeed / 1000f;
                    snow.Y = snow.Y + 1 * Speed;

                    //recycle
                    if (snow.CreateTime + SnowExpireTime < currentTime)
                    {
                        snowContainer.Children.ToList().Remove(snow);
                    }
                }
            }   
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            texture = textures;
        }
    }

    /// <summary>
    /// show spirit
    /// </summary>
    public class SnowSpitie : Sprite
    {
        /// <summary>
        /// Horizontal speed
        /// </summary>
        public float HorizontalSpeed { get; set; }

        /// <summary>
        /// create time
        /// </summary>
        public double CreateTime { get; set; }
    }
}
