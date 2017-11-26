using OpenTK;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.Texture = textures.Get(_resource);
        }
    }
}
