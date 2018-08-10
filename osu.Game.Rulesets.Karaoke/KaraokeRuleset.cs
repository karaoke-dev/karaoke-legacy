// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Configuration;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Karaoke.Beatmaps;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Edit;
using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Karaoke.KaraokeDifficulty;
using osu.Game.Rulesets.Karaoke.Mods;
using osu.Game.Rulesets.Karaoke.Mods.Mod2017;
using osu.Game.Rulesets.Karaoke.Mods.Mod2018;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Common.Pieces;
using osu.Game.Rulesets.Karaoke.Textures;
using osu.Game.Rulesets.Karaoke.UI;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Karaoke
{
    /// <summary>
    ///     this the the lagacy karaoke project
    ///     and will not have any update version.
    ///     means that it will not looks like Joysound or other different Karakoe tools in the future : )
    /// </summary>
    public class KaraokeRuleset : Ruleset
    {
        public override RulesetContainer CreateRulesetContainerWith(WorkingBeatmap beatmap) => new KaraokeRulesetContainer(this, beatmap);
        public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) => new KaraokeBeatmapConverter(beatmap);
        public override IBeatmapProcessor CreateBeatmapProcessor(IBeatmap beatmap) => new KaraokeBeatmapProcessor(beatmap);

        public override IEnumerable<int> AvailableVariants => new[] { 0, 1 };

        public override string Description => "カラオケ!";

        public override string ShortName => "karaoke";

        //TODO : give it a id temporatory
        public override int? LegacyID => 0;

        public override IRulesetConfigManager CreateConfig(SettingsStore settings) => new KaraokeConfigManager(settings, RulesetInfo);

        public KaraokeRuleset(RulesetInfo rulesetInfo = null)
            : base(rulesetInfo)
        {
            var karaokeTextureStore = new KaraokeTextureStore();
        }

        public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0)
        {
            if (variant == 0) //Main
                return new[]
                {
                    new KeyBinding(InputKey.Number1, KaraokeKeyAction.FirstLyric),
                    new KeyBinding(InputKey.Keypad1, KaraokeKeyAction.FirstLyric),

                    new KeyBinding(InputKey.Left, KaraokeKeyAction.PreviousLyric),
                    new KeyBinding(InputKey.Right, KaraokeKeyAction.NextLyric),

                    new KeyBinding(InputKey.Enter, KaraokeKeyAction.PlayAndPause),
                    new KeyBinding(InputKey.KeypadEnter, KaraokeKeyAction.PlayAndPause),

                    new KeyBinding(InputKey.Q, KaraokeKeyAction.IncreaseSpeed),
                    new KeyBinding(InputKey.A, KaraokeKeyAction.DecreaseSpeed),
                    new KeyBinding(InputKey.Z, KaraokeKeyAction.ResetSpeed),

                    new KeyBinding(InputKey.W, KaraokeKeyAction.IncreaseTone),
                    new KeyBinding(InputKey.S, KaraokeKeyAction.DecreaseTone),
                    new KeyBinding(InputKey.X, KaraokeKeyAction.ResetTone),

                    new KeyBinding(InputKey.E, KaraokeKeyAction.IncreaseLyricAppearTime),
                    new KeyBinding(InputKey.D, KaraokeKeyAction.DecreaseLyricAppearTime),
                    new KeyBinding(InputKey.C, KaraokeKeyAction.ResetLyricAppearTime),

                    new KeyBinding(InputKey.P, KaraokeKeyAction.OpenPanel)
                };
            return new[]
            {
                new KeyBinding(InputKey.T, KaraokeEditorKeyAction.TemplateDialog),
                new KeyBinding(InputKey.L, KaraokeEditorKeyAction.LyricsDialog),
                new KeyBinding(InputKey.R, KaraokeEditorKeyAction.TranslateDialog),
                new KeyBinding(InputKey.G, KaraokeEditorKeyAction.SingerDialog)
            };
        }

        public override string GetVariantName(int variant)
        {
            if (variant == 0)
                return "Karaoke Hotkey";
            return "Editor Hotkey";
        }

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.DifficultyReduction: //general setting of karaoke
                    return new Mod[]
                    {
                        new KaraokeModEasy(),
                    };

                case ModType.DifficultyIncrease: // pecial setting or effect
                    return new Mod[]
                    {
                        new KaraokeModHidden(),
                        new KaraokeModDoubleTime(),
                        new MultiMod(new KaraokeModTransparentLyrics(),new KaraokeModCloseLyrics())
                    };

                case ModType.Automation:
                    return new Mod[]
                    {
                        new KaraokeModTutorial(),
                        new KaraokeModPractice()
                    };
                case ModType.Conversion:
                    return new Mod[]
                    {
                        new MultiMod( new KaraokeModOpenTranslate(),new KaraokeModCloseTranslate()),
                        new MultiMod( new KaraokeModCloseVocal(),new KaraokeModOpenVocal())
                    };
                    break;
                case ModType.Fun: //only event mod
                    return new Mod[]
                    {
                        new KaraokeMod2017_Christmas(),
                        new KaraokeModPDUMCWAMFUW()
                    };

                default:
                    return new Mod[] { };
            }
        }

        public override Drawable CreateIcon()
        {
            return new ImagePicec(@"Icon/Icon");
        }

        public override DifficultyCalculator CreateDifficultyCalculator(WorkingBeatmap beatmap)
        {
            return new KaraokeDifficultyCalculator(this,beatmap);
        }

        public override HitObjectComposer CreateHitObjectComposer()
        {
            return new KaraokeHitObjectComposer(this);
        }

        public override RulesetSettingsSubsection CreateSettings()
        {
            return new KaraokeSettings(this);
        }
    }
}
