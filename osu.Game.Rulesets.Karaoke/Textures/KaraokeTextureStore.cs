// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;

namespace osu.Game.Rulesets.Karaoke.Textures
{
    public class KaraokeTextureStore : Container
    {
        public static ResourceStore<byte[]> Resources;
        public static TextureStore KaraokeTexture;


        public KaraokeTextureStore()
        {
            /*
            KaraokeResources = new ResourceStore<byte[]>();
            KaraokeResources.AddStore(new DllResourceStore("osu.Game.Rulesets.Karaoke.dll"));
            KaraokeTexture = new TextureStore(new RawTextureLoaderStore(new NamespacedResourceStore<byte[]>(KaraokeResources, @"Textures")));
            KaraokeTexture.AddStore(new RawTextureLoaderStore(new OnlineStore()));
            */
        }

        /*
        [BackgroundDependencyLoader]
        private void load(OsuGameBase osuGameBase)
        {
            if (Resources == null)
            {
                Resources = osuGameBase.Resources;
                Resources.AddStore(new DllResourceStore(@"osu.Game.Rulesets.Karaoke.dll"));
            }
        }
        */
    }
}
