﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Judgements;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Objects.Drawables;

namespace osu.Game.Rulesets.Karaoke.UI
{
    /// <summary>
    ///     Karaoke PlayField
    /// </summary>
    public partial class KaraokePlayfield : KaraokeBasePlayfield
    {
        public KaraokePlayfield(Ruleset ruleset, WorkingBeatmap beatmap, KaraokeRulesetContainer container)
            : base(ruleset, beatmap, container)
        {
            KaraokeFieldTool.Translateor.OnTranslateMultiStringSuccess += (a, multiSting) =>
            {
                for (var i = 0; i < multiSting.Count; i++)
                    //assign language
                    KaraokeLyricPlayField.ListDrawableKaraokeObject[i].Lyric.Translates.Add(TranslateCode.Chinese_Traditional, new LyricTranslate(multiSting[i]));
            };
        }

        public override void Add(DrawableHitObject h)
        {
            base.Add(h);
        }

        public override void PostProcess()
        {
            var needTranslate = true;
            if (needTranslate)
            {
                var listTranslateString = new List<string>();
                foreach (var singleKaraokeObject in KaraokeLyricPlayField.ListDrawableKaraokeObject)
                    listTranslateString.Add(singleKaraokeObject.Lyric.Lyric.Text);

                //translate list string 
                KaraokeFieldTool.Translateor.Translate(TranslateCode.Default, TranslateCode.Chinese_Traditional, listTranslateString);
            }
        }

        public void OnJudgement(DrawableHitObject judgedObject, Judgement judgement)
        {
            var karaokeJudgement = (KaraokeJudgement)judgement;

            if (!judgedObject.DisplayJudgement)
                return;
        }

        /*
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            KaraokeLyricPlayField.Dispose();
        }
        */

        protected override void LoadComplete()
        {
            base.LoadComplete();
            //AddInternal(new GameplayCursor());
        }

        [BackgroundDependencyLoader]
        private void load(KaraokeConfigManager karaokeConfig)
        {
            if (karaokeConfig != null)
            {
                var style = karaokeConfig.GetObjectBindable<KaraokeLyricConfig>(KaraokeSetting.LyricStyle);
                var template = karaokeConfig.GetObjectBindable<LyricTemplate>(KaraokeSetting.Template);
                var singerTemplate = karaokeConfig.GetObjectBindable<SingerTemplate>(KaraokeSetting.SingerTemplate);
                var translateCode = karaokeConfig.GetBindable<TranslateCode>(KaraokeSetting.DefaultTranslateLanguage);

                KaraokeLyricPlayField.Style.BindTo(style);
                KaraokeLyricPlayField.Template.BindTo(template);
                KaraokeLyricPlayField.SingerTemplate.BindTo(singerTemplate);
                KaraokeLyricPlayField.TranslateCode.BindTo(translateCode);
            }
        }
    }
}
