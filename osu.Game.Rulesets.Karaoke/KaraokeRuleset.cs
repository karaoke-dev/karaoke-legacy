// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;
using osu.Game.Beatmaps;
using osu.Game.Graphics;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Karaoke.Edit;
using osu.Game.Rulesets.Karaoke.KaraokeDifficulty;
using osu.Game.Rulesets.Karaoke.Mods;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Pieces;
using osu.Game.Rulesets.Karaoke.UI;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.Karaoke.Textures;
using osu.Game.Rulesets.Configuration;
using osu.Game.Configuration;
using osu.Game.Rulesets.Karaoke.Configuration;

namespace osu.Game.Rulesets.Karaoke
{
    /// <summary>
    /// this the the lagacy karaoke project
    /// and will not have any update version.
    /// means that it will not looks like Joysound or other different Karakoe tools in the future : )
    /// </summary>
    public class KaraokeRuleset : Ruleset
    {
        public override RulesetContainer CreateRulesetContainerWith(WorkingBeatmap beatmap, bool isForCurrentRuleset) => new KaraokeRulesetContainer(this, beatmap, isForCurrentRuleset);

        public override IEnumerable<int> AvailableVariants => new[] { 0, 1 };

        public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0)
        {
            if (variant == 0)//Main
            {
                return new[]
                {
                    new KeyBinding(InputKey.Number1, KaraokeAction.FirstLyric),
                    new KeyBinding(InputKey.Keypad1, KaraokeAction.FirstLyric),

                    new KeyBinding(InputKey.Left, KaraokeAction.PreviousLyric),
                    new KeyBinding(InputKey.Right, KaraokeAction.NextLyric),

                    new KeyBinding(InputKey.Enter, KaraokeAction.PlayAndPause),
                    new KeyBinding(InputKey.KeypadEnter, KaraokeAction.PlayAndPause),

                    new KeyBinding(InputKey.Q, KaraokeAction.IncreaseSpeed),
                    new KeyBinding(InputKey.A, KaraokeAction.DecreaseSpeed),
                    new KeyBinding(InputKey.Z, KaraokeAction.ResetSpeed),

                    new KeyBinding(InputKey.W, KaraokeAction.IncreaseTone),
                    new KeyBinding(InputKey.S, KaraokeAction.DecreaseTone),
                    new KeyBinding(InputKey.X, KaraokeAction.ResetTone),

                    new KeyBinding(InputKey.E, KaraokeAction.IncreaseLyricAppearTime),
                    new KeyBinding(InputKey.D, KaraokeAction.DecreaseLyricAppearTime),
                    new KeyBinding(InputKey.C, KaraokeAction.ResetLyricAppearTime),

                    new KeyBinding(InputKey.P, KaraokeAction.OpenPanel),
                };
            }
            else//Editor
            {
                return new[]
                {
                    new KeyBinding(InputKey.T, KaraokeAction.TemplateDialog),
                    new KeyBinding(InputKey.L, KaraokeAction.LyricsDialog),
                    new KeyBinding(InputKey.R, KaraokeAction.TranslateDialog),
                    new KeyBinding(InputKey.G, KaraokeAction.SingerDialog),
                };
            }
        }

        public override string GetVariantName(int variant)
        {
            if (variant == 0)
                return "Karaoke Key";
            else
                return "Dialog Hotkey";
        }

        public override IEnumerable<BeatmapStatistic> GetBeatmapStatistics(WorkingBeatmap beatmap) => new[]
        {
            //TODO Change to foreach and calculate each singer's lyric number


            new BeatmapStatistic
            {
                Name = @"Circle count",
                Content = beatmap.Beatmap.HitObjects.Count(h => h is KaraokeObject).ToString(),
                Icon = FontAwesome.fa_dot_circle_o
            },
            new BeatmapStatistic
            {
                Name = @"Slider count",
                Content = beatmap.Beatmap.HitObjects.Count(h => h is KaraokeObject).ToString(),
                Icon = FontAwesome.fa_circle_o
            }
        };

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.DifficultyReduction: //general setting of karaoke
                    return new Mod[]
                    {
                        new KaraokeTutorial(),
                        new MultiMod
                        {
                            Mods = new Mod[]
                            {
                                new KaraokeOpenTranslate(),
                                new KaraokeCloseTranslate(),
                            },
                        },
                        new MultiMod
                        {
                            Mods = new Mod[]
                            {
                                new KaraokeEasy(),
                                new KaraokeDoubleTime(),
                            },
                        },
                        new MultiMod
                        {
                            Mods = new Mod[]
                            {
                                new CloseVocal(),
                                new OpenVocal(),
                            },
                        },
                    };

                case ModType.DifficultyIncrease: // pecial setting or effect
                    return new Mod[]
                    {
                        new KaraokeHidden(),
                        new MultiMod
                        {
                            Mods = new Mod[]
                            {
                                new KaraokeTransparentLyrics(),
                                new KaraokeCloseLyrics(),
                            },
                        },
                    };

                case ModType.Special: //only event mod
                    return new Mod[]
                    {
                        new ChristmasMod(), //sing the song when christmas
                    };

                default:
                    return new Mod[] { };
            }
        }

        public override Drawable CreateIcon() => new ImagePicec(@"Icon/Icon");

        public override DifficultyCalculator CreateDifficultyCalculator(Beatmap beatmap, Mod[] mods = null) => new KaraokeDifficultyCalculator(beatmap, mods);

        public override HitObjectComposer CreateHitObjectComposer() => new KaraokeHitObjectComposer(this);

        public override string Description => "カラオケ!";

        public override string ShortName => "karaoke";

        public override SettingsSubsection CreateSettings() => new KaraokeSettings();

        //TODO : give it a id temporatory
        public override int LegacyID => 0;

        public KaraokeRuleset(RulesetInfo rulesetInfo)
            : base(rulesetInfo)
        {
            var karaokeTextureStore = new KaraokeTextureStore();
        }
    }
}
