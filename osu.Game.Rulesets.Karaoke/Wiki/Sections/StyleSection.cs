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

        public KaraokeTemplate KarokeTemplate { get; set; } = new KaraokeTemplate();

        public KaraokeObject KaraokeObject { get; set; } = new KaraokeObject()
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
                    new DrawableKaraokeTemplate(KaraokeObject, KarokeTemplate),
                    //Romaji and subtext setting
                    new Container
                    {
                        Position = new Vector2(-10, 0),
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                        AutoSizeAxes = Axes.Y,
                        Width = 200,
                        Children=new Drawable[]
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
                        }
                    },
                }
            });
            

            Content.Add(new WikiSubSectionHeader("Singer"));
            //TODO : show singer
        }
    }
}
