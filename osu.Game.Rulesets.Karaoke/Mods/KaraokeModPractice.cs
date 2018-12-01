// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Timing;
using osu.Game.Graphics;
using osu.Game.Graphics.UserInterface;
using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Karaoke.UI.Interface;
using osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input;
using osu.Game.Rulesets.Karaoke.UI.Layers.Type;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.UI;
using osuTK;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    public class KaraokeModPractice : Mod, IApplicableCreatePlayfieldLayer, IApplicableCreateRightSideButton
    {
        public override string Name => "Practice";
        public override string Acronym => "Practice";
        public override double ScoreMultiplier => 0.0f;
        public override Type[] IncompatibleMods => new[] { typeof(ModDoubleTime), typeof(ModHalfTime) };
        public override FontAwesome Icon => FontAwesome.fa_play_circle;

        public IModLayer CreateNewLayer(Playfield playfield)
        {
            return new ControlPanelLayer(playfield as IAmKaraokeField)
            {
                Clock = new FramedClock(new StopwatchClock(true)),
                RelativeSizeAxes = Axes.X,
                Origin = Anchor.BottomCentre,
                Anchor = Anchor.BottomCentre,
                Scale = new Vector2(1.0f),
                Depth = 10f
            };
        }

        public Button CreateButton(InputLayer inputLayer)
        {
            return new TriangleButton
            {
                Width = 60,
                Height = 60,
                Text = "Panel",
                Action = () =>
                {
                    //as press openPanel hotkey
                    inputLayer.OnPressed(KaraokeKeyAction.OpenPanel);
                }
            };
        }
    }
}
