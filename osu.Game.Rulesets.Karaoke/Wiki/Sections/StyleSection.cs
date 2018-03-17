// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces;
using osu.Game.Rulesets.Karaoke.Helps;
using osu.Game.Rulesets.Karaoke.Objects;
using OpenTK;
using Symcol.Rulesets.Core.Wiki;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections
{
    /// <summary>
    /// [3] introduce which style can be setting
    ///     3.1 : template
    ///     3.2 : singer(maybe)
    /// </summary>
    internal class StyleSection : BaseWikiSection
    {
        public override string Title => "Style";

        private RomajiMenuSettings _menuSetting;

        private KaraokeConfigManager _config;

        public LyricTemplate KarokeTemplate { get; set; } = new LyricTemplate();
        public KaraokeLyricConfig LyricConfig { get; set; } = new KaraokeLyricConfig();

        public Lyric Lyric { get; set; } = DemoKaraokeObject.GenerateDeomKaraokeLyric();

        public DrawableKaraokeTemplate DrawableKaraokeTemplate { get; set; }

        protected override void InitialView()
        {
            Content.Add(new WikiTextSection("Setting karaoke Text and other."));
            Content.Add(new WikiTextSection(" \n\n"));

            Content.Add(new WikiSubSectionHeader("Template"));

            var karaokeLyricConfig = RulesetConfig.GetObjectBindable<KaraokeLyricConfig>(KaraokeSetting.LyricStyle);

            //show settingTemplate
            Content.Add(new Container
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                AutoSizeAxes = Axes.Y,
                RelativeSizeAxes = Axes.X,
                Masking = true,

                Children = new Drawable[]
                {
                    //Karaoke Text Template
                    new Container
                    {
                        Position = new Vector2(-10, 0),
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                        Width = 400,
                        Height = 250,
                        Children = new Drawable[]
                        {
                            DrawableKaraokeTemplate = new DrawableKaraokeTemplate(Lyric)
                            {
                                Position = new Vector2(100, -5),
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                OnValueChanged = (template) =>
                                {
                                    RulesetConfig.GetBindable<LyricTemplate>(KaraokeSetting.Template).Value = template;
                                }
                            },
                        }
                    },

                    //Romaji and subtext setting
                    new Container
                    {
                        Position = new Vector2(-10, 0),
                        Anchor = Anchor.TopRight,
                        Origin = Anchor.TopRight,
                        Width = 400,
                        AutoSizeAxes = Axes.Y,
                        AutoSizeDuration = 100,
                        AutoSizeEasing = Easing.OutQuint,
                        Child = _menuSetting = new RomajiMenuSettings
                        {
                            Bindnig = karaokeLyricConfig,
                        }
                    },
                }
            });

            //if value is changed
            _menuSetting.Bindnig.BindTo(karaokeLyricConfig);
            Content.Add(new WikiTextSection(" \n\n"));


            Content.Add(new WikiSubSectionHeader("Singer"));
            //TODO : show singer

            Content.Add(new WikiTextSection(" \n\n"));
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            //config = new KaraokeConfigManager(settings,ruleset,0);
            KarokeTemplate = new LyricTemplate(); //_config.GetObject<LyricTemplate>(KaraokeSetting.Template);
        }
    }

    public class RomajiMenuSettings : SettingsSubsection
    {
        protected override string Header => "Lyric Config";

        public Bindable<KaraokeLyricConfig> Bindnig { get; set; }

        private SettingsCheckbox _topTextVisibleCheckBox;
        private SettingsCheckbox _romajiVisibleCheckBox;
        private SettingsCheckbox _romajiFirstCheckBox;
        private SettingsCheckbox _translateCheckBox;

        protected override void LoadComplete()
        {
            base.LoadComplete();
            if (Bindnig != null)
            {
                _topTextVisibleCheckBox.Bindable.Value = Bindnig.Value.SubTextVislbility;
                _romajiVisibleCheckBox.Bindable.Value = Bindnig.Value.RomajiVislbility;
                _romajiFirstCheckBox.Bindable.Value = Bindnig.Value.RomajiFirst;
                _translateCheckBox.Bindable.Value = Bindnig.Value.ShowTranslate;
            }
        }

        public RomajiMenuSettings()
        {
            Children = new Drawable[]
            {
                //TopText Vislbility(default is true)
                _topTextVisibleCheckBox = new SettingsCheckbox
                {
                    LabelText = "TopText vislbility",
                    Bindable = new Bindable<bool>()
                },
                //Romaji Wislbility(default is true)
                _romajiVisibleCheckBox = new SettingsCheckbox
                {
                    LabelText = "Romaji vislbility",
                    Bindable = new Bindable<bool>()
                },
                //Romaji Wislbility(default is false)
                _romajiFirstCheckBox = new SettingsCheckbox
                {
                    LabelText = "Romaji first",
                    Bindable = new Bindable<bool>()
                },
                //Translate Wislbility(default is true)
                _translateCheckBox = new SettingsCheckbox
                {
                    LabelText = "Translate",
                    Bindable = new Bindable<bool>()
                },
            };
            _topTextVisibleCheckBox.Bindable.ValueChanged += (a) =>
            {
                Bindnig.Value.SubTextVislbility = a;
                Bindnig?.TriggerChange();
            };
            _romajiVisibleCheckBox.Bindable.ValueChanged += (a) =>
            {
                Bindnig.Value.RomajiVislbility = a;
                Bindnig?.TriggerChange();
            };
            _romajiFirstCheckBox.Bindable.ValueChanged += (a) =>
            {
                Bindnig.Value.RomajiFirst = a;
                Bindnig?.TriggerChange();
            };
            _translateCheckBox.Bindable.ValueChanged += (a) =>
            {
                Bindnig.Value.ShowTranslate = a;
                Bindnig?.TriggerChange();
            };
        }
    }
}
