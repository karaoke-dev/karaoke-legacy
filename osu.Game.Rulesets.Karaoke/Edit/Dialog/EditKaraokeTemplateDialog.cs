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

        public LyricTemplate KarokeTemplate { get; set; } = new LyricTemplate();

        public Lyric Lyric { get; set; } = new Lyric()
        {
            MainText = MainTextList.SetJapaneseLyric("カラオケ"),
            SubTexts = new Dictionary<int, SubText>()
            {
                { 0, new SubText() { Text = "か" } },
                { 1, new SubText() { Text = "ら" } },
                { 2, new SubText() { Text = "お" } },
                { 3, new SubText() { Text = "け" } },
            },
            Translates = new ListKaraokeTranslateString()
            {
                new LyricTranslate(LangTagConvertor.GetCode(TranslateCode.English), "Karaoke")
            }
        };

        protected DrawableKaraokeTemplate DrawableKaraokeTemplate;

        public EditKaraokeTemplateDialog(KaraokeEditPlayfield editPlayField, Lyric demoKaraokeText)
        {
            //KarokeTemplate =

            if (demoKaraokeText != null)
                Lyric = demoKaraokeText;
        }

        public override void InitialDialog()
        {
            //create drawable
            MainContext.Add(DrawableKaraokeTemplate = new DrawableKaraokeTemplate(Lyric, KarokeTemplate)
            {
                Position = new Vector2(250, 100)
            });

            base.InitialDialog();
        }
    }
}
