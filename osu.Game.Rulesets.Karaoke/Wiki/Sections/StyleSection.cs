// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces;
using osu.Game.Rulesets.Karaoke.Helps;
using OpenTK;
using Symcol.Rulesets.Core.Wiki;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections
{
    /// <summary>
    ///     [3] introduce which style can be setting
    ///     3.1 : template
    ///     3.2 : singer(maybe)
    /// </summary>
    internal class StyleSection : BaseWikiSection
    {
        public override string Title => "Style";

        protected RomajiMenuSettings RomajiMenuSettings;
        protected DrawableKaraokeTemplate DrawableKaraokeTemplate { get; set; }

        protected override void InitialView()
        {
            Content.Add(new WikiTextSection("Setting karaoke Text and other."));
            Content.Add(new WikiTextSection(" \n\n"));

            Content.Add(new WikiSubSectionHeader("Template"));

            var karaokeLyricConfig = RulesetConfig.GetObjectBindable<KaraokeLyricConfig>(KaraokeSetting.LyricStyle);
            var lyricTemplate = RulesetConfig.GetBindable<LyricTemplate>(KaraokeSetting.Template);

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
                            DrawableKaraokeTemplate = new DrawableKaraokeTemplate(DemoKaraokeObject.GenerateDeomKaraokeLyric())
                            {
                                Position = new Vector2(100, -5),
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre
                            }
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
                        Child = RomajiMenuSettings = new RomajiMenuSettings()
                    }
                }
            });

            //lyricTemplate.BindTo(DrawableKaraokeTemplate.Template);
            //karaokeLyricConfig.BindTo(DrawableKaraokeTemplate.Style);
            //karaokeLyricConfig.BindTo(RomajiMenuSettings.Bindnig);
            RomajiMenuSettings.Bindnig.BindTo(karaokeLyricConfig);
            DrawableKaraokeTemplate.Template.BindTo(lyricTemplate);
            DrawableKaraokeTemplate.Style.BindTo(RomajiMenuSettings.Bindnig);


            Content.Add(new WikiTextSection(" \n\n"));


            Content.Add(new WikiSubSectionHeader("Singer"));
            //TODO : show singer

            Content.Add(new WikiTextSection(" \n\n"));
        }
    }

    public class RomajiMenuSettings : SettingsSubsection
    {
        public BindableObject<KaraokeLyricConfig> Bindnig { get; set; } = new BindableObject<KaraokeLyricConfig>(new KaraokeLyricConfig());
        protected override string Header => "BaseLyric Config";

        private readonly SettingsCheckbox _topTextVisibleCheckBox;
        private readonly SettingsCheckbox _romajiVisibleCheckBox;
        private readonly SettingsCheckbox _romajiFirstCheckBox;
        private readonly SettingsCheckbox _translateCheckBox;

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
                }
            };
            _topTextVisibleCheckBox.Bindable.ValueChanged += a =>
            {
                Bindnig.Value.SubTextVislbility = a;
                Bindnig.Value = Bindnig.Value;
            };
            _romajiVisibleCheckBox.Bindable.ValueChanged += a =>
            {
                Bindnig.Value.RomajiVislbility = a;
                Bindnig.Value = Bindnig.Value;
            };
            _romajiFirstCheckBox.Bindable.ValueChanged += a =>
            {
                Bindnig.Value.RomajiFirst = a;
                Bindnig.Value = Bindnig.Value;
            };
            _translateCheckBox.Bindable.ValueChanged += a =>
            {
                Bindnig.Value.ShowTranslate = a;
                Bindnig.Value = Bindnig.Value;
            };
        }

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
    }
}
