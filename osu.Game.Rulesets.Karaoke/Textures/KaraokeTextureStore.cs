using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Textures
{
    public class KaraokeTextureStore
    {
        public static ResourceStore<byte[]> KaraokeResources;
        public static TextureStore KaraokeTexture;


        public KaraokeTextureStore()
        {
            KaraokeResources = new ResourceStore<byte[]>();
            KaraokeResources.AddStore(new DllResourceStore("osu.Game.Rulesets.Karaoke.dll"));
            KaraokeTexture = new TextureStore(new RawTextureLoaderStore(new NamespacedResourceStore<byte[]>(KaraokeResources, @"Textures")));
            KaraokeTexture.AddStore(new RawTextureLoaderStore(new OnlineStore()));
        }
    }
}
