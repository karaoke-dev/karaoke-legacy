using osu.Framework.Graphics.Textures;
using osu.Game.Rulesets.Karaoke.Textures;
using osu.Game.Users;
using Symcol.Rulesets.Core.Wiki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Wiki
{
    public class KaraokeWikiHeader : WikiHeader
    {
        protected override Texture RulesetIcon => KaraokeTextureStore.KaraokeTexture.Get("Icon/Icon");

        protected override string RulesetName => "Karaoke(カラオケ)";

        protected override string RulesetDescription => "Karaoke! is a 3rd party ruleset developed for osu!lazer. \n"+
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
