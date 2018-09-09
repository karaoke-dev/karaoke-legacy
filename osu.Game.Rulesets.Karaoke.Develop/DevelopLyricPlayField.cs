using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Helps;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;
using osu.Game.Tests.Visual;
using OpenTK;
using osu.Framework.Graphics.Shapes;
using OpenTK.Graphics;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Extension;

namespace osu.Game.Rulesets.Karaoke.Develop
{
    [TestFixture]
    public class DevelopLyricPlayField : OsuTestCase
    {
        public DevelopLyricPlayField()
        {
            var field = new LyricLagacyLayoutContainer()
            {
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding{Left = 100,Right = 100, Top =100, Bottom = 100}
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

    public class FakeLyricContainer : Container
    {
        public virtual bool ProgressUpdateByTime{get;set;}
        public FakeLyricContainer(Lyric lyric)
        {
            RelativeSizeAxes = Axes.X;
            Height = 100;
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.White,
                }
            };
        }
    }

    /// <summary>
    /// Playfield
    /// </summary>
    public class LyricLagacyLayoutContainer : FlowContainer<DrawableLyric>
    {
        public List<float> LineSpacing { get; set; }

        public LyricLagacyLayoutContainer()
        {
            LineSpacing = new List<float>()
            {
                0,100
            };
        }

        public override void Add(DrawableLyric drawable)
        {
            base.Add(drawable);

            if (IsLoaded)
                ComputeLyricLayout();

        }

        public override bool Remove(DrawableLyric drawable)
        {
            var remove = base.Remove(drawable);

            if (IsLoaded)
                ComputeLyricLayout();

            return remove;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            ComputeLyricLayout();
        }

        /// <summary>
        /// Set object's margin and padding(left and right)
        /// </summary>
        protected virtual void ComputeLyricLayout()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                var lyric = Children[i];
                var layoutIndex = i % LineSpacing.Count;

                lyric.DisplayLayer = layoutIndex;
                lyric.Padding
                = new MarginPadding{Left = LineSpacing[layoutIndex], Right = 100};
            }
        }
        
        
        /// <summary>
        /// re-compute layout
        /// </summary>
        protected override IEnumerable<Vector2> ComputeLayoutPositions()
        {
            //the child that only display on the screen
            var children = FlowingChildren.ToArray();
            if (children.Length == 0)
                return new List<Vector2>();

            Vector2[] result = new Vector2[children.Length];
            for(int i=0;i<children.Length;i++)
            {
                var drawableLyric = Children.FirstOrDefault(x => x == children[i]);
                if(drawableLyric!=null)
                {
                    if(drawableLyric.DisplayLayer != 0)
                    {
                        var pervous = Children.GetPrevious(drawableLyric);
                        var next = Children.GetNext(drawableLyric);
                        var perviousHeight = pervous?.Y + pervous?.DrawHeight ?? 0;
                        var nextHeight = next?.Y + next?.DrawHeight ?? 0;
                        result[i].Y = Math.Max(perviousHeight, nextHeight);
                    }
                }
            }

            
            return result;
        }
    }
}
