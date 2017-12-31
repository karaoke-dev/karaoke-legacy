﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Timing;
using osu.Game.Beatmaps;
using osu.Game.Graphics.UserInterface;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Karaoke.Judgements;
using osu.Game.Rulesets.Karaoke.Mods;
using osu.Game.Rulesets.Karaoke.Tools.Translator;
using osu.Game.Rulesets.Karaoke.UI.Panel;
using osu.Game.Rulesets.Objects.Drawables;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.UI
{
    /// <summary>
    /// Karaoke PlayField
    /// </summary>
    public class KaraokePlayfield : KaraokeBasePlayfield
    {
        private readonly Container judgementLayer;
        private readonly Container KaraokecontrolLayer;
        private readonly KaraokePanelOverlay karaokePanelOverlay;

        //public override bool ProvidingUserCursor => true;

        public KaraokePlayfield(Ruleset ruleset, WorkingBeatmap beatmap, KaraokeRulesetContainer container)
            : base(ruleset, beatmap, container)
        {
            AddRange(new Drawable[]
            {
                judgementLayer = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Depth = 1,
                },
                KaraokecontrolLayer = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Depth = -2,
                    Clock = new FramedClock(new StopwatchClock(true)),
                    Children = new Drawable[]
                    {
                        new TriangleButton()
                        {
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,

                            Position = new Vector2(110, 100),
                            Width = 70,
                            Height = 30,
                            Text = "Panel",
                            Action = () => { karaokePanelOverlay.ToggleVisibility(); }
                        }
                    }
                },
                karaokePanelOverlay = new KaraokePanelOverlay(this)
                {
                    Clock = new FramedClock(new StopwatchClock(true)),
                    RelativeSizeAxes = Axes.X,
                    Origin = Anchor.BottomCentre,
                    Anchor = Anchor.BottomCentre,
                    Position = new Vector2(-100, -100),
                    Scale = new Vector2(1.0f),
                    Depth = -1.5f,
                },
                new KaraokeHotkeyPanel(karaokePanelOverlay)
                {
                    RelativeSizeAxes = Axes.Both,
                    Depth = -2,
                    Clock = new FramedClock(new StopwatchClock(true)),
                },
            });

            //create all layer if contains in mod
            foreach (var singleMod in WorkingBeatmap.Mods.Value)
            {
                if (singleMod is IHasLayer iHasLayer)
                {
                    Add(iHasLayer.CreateNewLayer());
                    break;
                }
            }

            KaraokeFieldTool.Translateor.OnTranslateMultiStringSuccess += (a, multiSting) =>
            {
                for (int i = 0; i < multiSting.Count; i++)
                {
                    ListDrawableKaraokeObject[i].AddTranslate(TranslateCode.Chinese_Traditional, multiSting[i]);
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            //AddInternal(new GameplayCursor());
        }

        public override void Add(DrawableHitObject h)
        {
            base.Add(h);
        }

        public override void PostProcess()
        {
            bool needTranslate = true;
            if (needTranslate)
            {
                var listTranslateString = new List<string>();
                foreach (var singleKaraokeObject in ListDrawableKaraokeObject)
                {
                    listTranslateString.Add(singleKaraokeObject.KaraokeObject.MainText.Text);
                }
                //translate list string 
                KaraokeFieldTool.Translateor.Translate(TranslateCode.Default, TranslateCode.Chinese_Traditional, listTranslateString);
            }
        }

        public override void OnJudgement(DrawableHitObject judgedObject, Judgement judgement)
        {
            var osuJudgement = (KaraokeJudgement)judgement;

            if (!judgedObject.DisplayJudgement)
                return;

            //judgementLayer.Add(explosion);
        }
    }
}
