using Symcol.Rulesets.Core.Wiki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Tools.Translator;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using OpenTK;
using osu.Game.Overlays.Settings;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections
{
    /// <summary>
    /// [3] introduce which style can be setting
    ///     3.1 : template
    ///     3.2 : singer(maybe)
    /// </summary>
    class StyleSection : WikiSection
    {
        public override string Title => "Style";

        public LyricTemplate KarokeTemplate { get; set; } = new LyricTemplate();

        public Lyric Lyric { get; set; } = new Lyric()
        {
            MainText = (TextObject)"カラオケ",
            ListSubTextObject = new List<SubTextObject>()
            {
                new SubTextObject() { Text = "か" },
                new SubTextObject() { Text = "ら", CharIndex = 1 },
                new SubTextObject() { Text = "お", CharIndex = 2 },
                new SubTextObject() { Text = "け", CharIndex = 3 },
            },
            ListTranslate = new ListKaraokeTranslateString()
            {
                new KaraokeTranslateString(LangTagConvertor.GetCode(TranslateCode.English), "Karaoke")
            }
        };

        public StyleSection()
        {
            Content.Add(new WikiTextSection("Setting karaoke Text and other."));
            Content.Add(new WikiTextSection(" \n\n"));


            Content.Add(new WikiSubSectionHeader("Template"));
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
                            new DrawableKaraokeTemplate(Lyric, KarokeTemplate)
                            {
                                Position=new Vector2(100,-5),
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
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
                        Child= new RomajiMenuSettings
                        {
                           
                        }
                    },
                }
            });
            Content.Add(new WikiTextSection(" \n\n"));

            Content.Add(new WikiSubSectionHeader("Singer"));
            //TODO : show singer

            Content.Add(new WikiTextSection(" \n\n"));
        }
    }

    public class RomajiMenuSettings : SettingsSubsection
    {
        protected override string Header => "Main Menu";

        /*
        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            
        }
        */

        public RomajiMenuSettings()
        {
            Children = new Drawable[]
            {
                //SubText Vislbility(default is true)
                new SettingsCheckbox
                {
                    LabelText = "SubText Vislbility",
                    //Bindable = config.GetBindable<bool>(DebugSetting.BypassCaching)
                },
                //Romaji Wislbility(default is true)
                new SettingsCheckbox
                {
                    LabelText = "Romaji Wislbility",
                    //Bindable = config.GetBindable<bool>(DebugSetting.BypassCaching)
                },
                //Romaji Wislbility(default is false)
                new SettingsCheckbox
                {
                    LabelText = "Bypass caching",
                    //Bindable = config.GetBindable<bool>(DebugSetting.BypassCaching)
                },
            };
        }
    }
}
