// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;

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
