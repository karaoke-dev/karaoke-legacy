// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Rulesets.Mania.Objects;
using osu.Game.Rulesets.UI;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics.Containers;
using System;
using osu.Game.Graphics;
using osu.Framework.Allocation;
using System.Linq;
using System.Collections.Generic;
using osu.Framework.Configuration;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Mania.Objects.Drawables;
using osu.Framework.Graphics.Shapes;
using osu.Game.Rulesets.Judgements;

namespace osu.Game.Rulesets.Mania.UI
{
    public class ManiaPlayfield : ScrollingPlayfield
    {
        /// <summary>
        /// list fall down mania group
        /// </summary>
        List<FallDownControlContainer> ListFallDownControlContainer { get; set; } = new List<FallDownControlContainer>();


        /// <summary>
        /// Whether this playfield should be inverted. This flips everything inside the playfield.
        /// </summary>
        public readonly Bindable<bool> Inverted = new Bindable<bool>(true);

        public List<Column> Columns
        {
            get
            {
                var list = new List<Column>();
                foreach (var single in ListFallDownControlContainer)
                {
                    list.AddRange(single.Columns);
                }
                return list;
            }
        }

        private readonly int columnCount;

        public ManiaPlayfield(int columnCount,bool coop): base(Axes.Y)
        {
            this.columnCount = columnCount;

            if (columnCount <= 0)
                throw new ArgumentException("Can't have zero or fewer columns.");

            Inverted.Value = true;

            if (coop)
            {
                FallDownControlContainer leftPart = null;
                FallDownControlContainer rightPart = null;
                InternalChildren = new Drawable[]
                {
                    //Left
                    leftPart=new FallDownControlContainer(columnCount/2)
                    {
                        Position=new OpenTK.Vector2(-200,0),
                    },
                    //Right
                    rightPart=new FallDownControlContainer(columnCount/2)
                    {
                        Position=new OpenTK.Vector2(200,0),
                    },
                };
                ListFallDownControlContainer.Clear();
                ListFallDownControlContainer.Add(leftPart);
                ListFallDownControlContainer.Add(rightPart);
            }
            else
            {
                FallDownControlContainer centerPart = null;
                InternalChildren = new Drawable[]
                {
                    //Left
                    centerPart=new FallDownControlContainer(columnCount)
                    {
                        //Position=new OpenTK.Vector2(-400,0),
                    },
                };
                ListFallDownControlContainer.Clear();
                ListFallDownControlContainer.Add(centerPart);
            }

            var currentAction = ManiaAction.Key1;
            for (int i = 0; i < columnCount; i++)
            {
                var c = new Column();
                c.VisibleTimeRange.BindTo(VisibleTimeRange);
                //c.Action = c.IsSpecial ? ManiaAction.Special : currentAction++;
                c.Action = currentAction++;

                /*
                c.IsSpecial = isSpecialColumn(i);
                topLevelContainer.Add(c.TopLevelContainer.CreateProxy());
                columns.Add(c);
                */
                getFallDownControlContainerByActualColumn(i).AddColumn(c);
                AddNested(c);
            }

            Inverted.ValueChanged += invertedChanged;
            Inverted.TriggerChange();
        }

        private void invertedChanged(bool newValue)
        {
            Scale = new Vector2(1, newValue ? -1 : 1);

            //judgements.Scale = Scale;
            foreach (var single in ListFallDownControlContainer)
            {
                single.Judgements.Scale = Scale;
            }
        }

        

        public override void OnJudgement(DrawableHitObject judgedObject, Judgement judgement)
        {
            var maniaObject = (ManiaHitObject)judgedObject.HitObject;
            int column = maniaObject.Column;
            Columns[maniaObject.Column].OnJudgement(judgedObject, judgement);



            /*
            judgements.Clear();
            judgements.Add(new DrawableManiaJudgement(judgement)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
            */
            getFallDownControlContainerByActualColumn(column).AddJudgement(judgement);
        }

        public override void Add(DrawableHitObject h) => Columns.ElementAt(((ManiaHitObject)h.HitObject).Column).Add(h);

        public void Add(DrawableBarLine barline) => HitObjects.Add(barline);

        protected override void Update()
        {
            // Due to masking differences, it is not possible to get the width of the columns container automatically
            // While masking on effectively only the Y-axis, so we need to set the width of the bar line container manually
            //content.Width = columns.Width;
        }

        private FallDownControlContainer getFallDownControlContainerByActualColumn(int actualColumn)
        {
            int sum = 0;
            foreach (var single in ListFallDownControlContainer)
            {
                sum = sum + single.ColumnCount;
                if (sum > actualColumn)
                {
                    return single;
                }
            }

            return null;
        }
    }
}
