// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Timing;
using osu.Game.Rulesets.Karaoke.Mods.Types;
using osu.Game.Rulesets.Karaoke.UI.Layers.Effect.ShowEffect;
using osu.Game.Rulesets.Mods;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    /// <summary>
    /// snow mod
    /// </summary>
    public class KaraokeModSnow : Mod, IHasLayer
    {
        public override string Name => "Snow";
        public override string ShortenedName => "SW";
        public override double ScoreMultiplier => 1.0f;
        public virtual string TextureLayer => @"Play/Karaoke/Layer/Snow/Snow";

        public Container CreateNewLayer()
        {
            return new SnowLayer
            {
                Clock = new FramedClock(new StopwatchClock(true)),
                RelativeSizeAxes = Axes.Both,
                Depth = 1,
                Width = 900,
                Position = new Vector2(-200, 0),
                TexturePath = TextureLayer,
            };
        }
    }
}
