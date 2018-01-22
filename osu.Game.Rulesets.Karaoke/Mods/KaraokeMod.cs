// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Timing;
using osu.Game.Graphics;
using osu.Game.Rulesets.Karaoke.UI.Layer.ShowEffect;
using osu.Game.Rulesets.Mods;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    /// <summary>
    /// if click this mod
    /// will introduce how to use karaoke panel and other setting
    /// will ignore the songs you select
    /// maybe
    /// </summary>
    public class KaraokeTutorial : ModNoFail
    {
        public override string Name => "Tutorial";
        public override string ShortenedName => "Tu";
        public override double ScoreMultiplier => 1;
        public override string Description => "Will introduce how to use karaoke panel and other setting.";
        public override bool Ranked => true;
    }

    /// <summary>
    /// will force open the translate for lyrics
    /// even you are not open it in the config
    /// </summary>
    public class KaraokeOpenTranslate : Mod
    {
        public override string Name => "Translate";
        public override string ShortenedName => "Tr";
        public override double ScoreMultiplier => 1;
        public override string Description => "Will force open the translate for lyrics, even you are not open it in the config.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_nofail;
        public override bool Ranked => true;
    }

    /// <summary>
    /// will force close the translate for lyrics
    /// even you are open it in the config.
    /// </summary>
    public class KaraokeCloseTranslate : Mod
    {
        public override string Name => "OffTranslate";
        public override string ShortenedName => "Tr_Close";
        public override double ScoreMultiplier => 1;
        public override string Description => "will force close the translate for lyrics, even you are open it in the config.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_hardrock;
        public override bool Ranked => true;
    }

    /// <summary>
    /// just make slower
    /// </summary>
    public class KaraokeEasy : ModHalfTime
    {
        public override string Name => "KaraokeEasy";
        public override string ShortenedName => "EZ";
        public override double ScoreMultiplier => 1;
        public override string Description => "just make defult song speed slower.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_halftime;
        public override bool Ranked => true;

        public override void ApplyToClock(IAdjustableClock clock)
        {
            clock.Rate = 0.75;
        }
    }

    /// <summary>
    /// just make faster
    /// </summary>
    public class KaraokeDoubleTime : ModDoubleTime
    {
        public override string Name => "KaraokeHard";
        public override string ShortenedName => "HD";
        public override double ScoreMultiplier => 1;
        public override string Description => "just make defult song speed faster.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_doubletime;
        public override bool Ranked => true;

        public override void ApplyToClock(IAdjustableClock clock)
        {
            clock.Rate = 1.25;
        }
    }

    /// <summary>
    /// if sound trach has two parts, close the vocal part
    /// </summary>
    public class CloseVocal : Mod
    {
        public override string Name => "CloseVocal";
        public override string ShortenedName => "CloseVocal";
        public override double ScoreMultiplier => 1;
        public override string Description => "if sound trach has two parts, close the vocal part.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_easy;
        public override bool Ranked => true;
    }

    /// <summary>
    /// if sound trach has two parts, open the vocal part
    /// </summary>
    public class OpenVocal : Mod
    {
        public override string Name => "OpenVocal";
        public override string ShortenedName => "OpenVocal";
        public override double ScoreMultiplier => 1;
        public override string Description => "if sound trach has two parts, open the vocal part.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_relax;
        public override bool Ranked => true;
    }

    /// <summary>
    /// will hide the lyrics
    /// </summary>
    public class KaraokeHidden : ModHidden
    {
        public override string Name => "Hidden";
        public override string ShortenedName => "HD";
        public override double ScoreMultiplier => 1;
        public override string Description => "Hidden the lyric at the start time.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_hidden;
        public override bool Ranked => true;
    }

    /// <summary>
    /// make lytric Transparent.
    /// </summary>
    public class KaraokeTransparentLyrics : Mod
    {
        public override string Name => "Transparent";
        public override string ShortenedName => "Transparent";
        public override double ScoreMultiplier => 1;
        public override string Description => "make lytric Transparent.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_flashlight;
        public override bool Ranked => true;
    }

    /// <summary>
    /// not even shows any lyrics
    /// </summary>
    public class KaraokeCloseLyrics : Mod
    {
        public override string Name => "CloseLyrics";
        public override string ShortenedName => "Cl";
        public override double ScoreMultiplier => 1;
        public override string Description => "not even shows any lyrics.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_suddendeath;
        public override bool Ranked => true;
    }

    /// <summary>
    /// snow mod
    /// </summary>
    public class SnowMod : Mod, IHasLayer
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
