using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Helps;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;
using osu.Game.Tests.Visual;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Develop
{
    [TestFixture]
    public class DevelopLyricPlayField : OsuTestCase
    {
        public DevelopLyricPlayField()
        {
            var field = new KaraokeLyricPlayField()
            {
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding{Left = 30,Right = 30}
            };

            for (int i = 0; i < 4; i++)
            {
                var lyric = DemoKaraokeObject.GenerateDemo001();
                field.Add(new DrawableLyric(lyric)
                {
                    ProgressUpdateByTime = false
                });
            }
            Add(field);
        }
    }

    /// <summary>
    /// Playfield
    /// </summary>
    public class KaraokeLyricPlayField : FillFlowContainer<DrawableLyric>
    {
        public List<float> LineSpacing { get; set; }

        public KaraokeLyricPlayField()
        {
            LineSpacing = new List<float>()
            {
                0,100
            };
        }

        public override void Add(DrawableLyric drawable)
        {
            //drawable.RelativeSizeAxes = Axes.X;
            base.Add(drawable);
        }

        public override bool Remove(DrawableLyric drawable)
        {
            return base.Remove(drawable);
        }
        
        protected override IEnumerable<Vector2> ComputeLayoutPositions()
        {
            var positions = base.ComputeLayoutPositions().ToArray();

            for (int i = 0; i < positions.Length; i++)
            {
                var lyric = Children[i];

                //TODO : compute layout
                var layoutIndex = i % LineSpacing.Count;

                Children[i].Margin
                = new MarginPadding{Left = LineSpacing[layoutIndex], Right = 0};
            }
            return positions;
        }
        
    }
}
