// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Pieces
{
    public class ImagePicec : Sprite
    {
        private string _resource;

        public ImagePicec(string resource)
        {
            _resource = resource;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            Texture = textures.Get(_resource);
        }
    }
}
