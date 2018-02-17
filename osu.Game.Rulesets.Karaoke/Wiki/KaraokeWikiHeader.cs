// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Textures;
using osu.Game.Rulesets.Karaoke.Textures;
using osu.Game.Users;
using Symcol.Rulesets.Core.Wiki;

namespace osu.Game.Rulesets.Karaoke.Wiki
{
    public class KaraokeWikiHeader : WikiHeader
    {
        protected override Texture RulesetIcon => KaraokeTextureStore.KaraokeTexture.Get("Icon/Icon");

        protected override string RulesetName => "Karaoke(カラオケ)";

        protected override string RulesetDescription => "Karaoke! is a 3rd party ruleset developed for osu!lazer. \n" +
                                                        "It is a project that can let everyone make their karaoke songs and share it on osu!.";

        protected override string RulesetUrl => $@"https://github.com/osu-Karaoke/osu-Karaoke";

        protected override User Creator => new User
        {
            Username = "andy840119",
            Id = 1030492
        };

        protected override string DiscordInvite => $@"https://discord.gg/ga2xZXk";

        //protected override Texture HeaderBackground => VitaruRuleset.VitaruTextures.Get("VitaruTouhosuModeTrue2560x1440");
    }
}
