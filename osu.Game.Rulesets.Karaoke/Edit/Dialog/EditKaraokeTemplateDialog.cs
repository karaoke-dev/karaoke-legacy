// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Tools.Translator;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog
{
    /// <summary>
    /// edit karaoke termplate
    /// </summary>
    public class EditKaraokeTemplateDialog : DialogContainer
    {
        public override string Title => "Karaoke Template";

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

        protected DrawableKaraokeTemplate DrawableKaraokeTemplate;

        public EditKaraokeTemplateDialog(KaraokeEditPlayfield editPlayField, KaraokeObject demoKaraokeText)
        {
            //KarokeTemplate =

            if (demoKaraokeText != null)
                KaraokeObject = demoKaraokeText;
        }

        public override void InitialDialog()
        {
            //create drawable
            MainContext.Add(DrawableKaraokeTemplate = new DrawableKaraokeTemplate(KaraokeObject, KarokeTemplate)
            {
                Position = new Vector2(250, 100)
            });

            base.InitialDialog();
        }
    }
}
