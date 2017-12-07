using osu.Game.Rulesets.Karaoke.Objects.Drawables.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Framework.Input;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Pieces
{
    public class EditableMainKaraokeText : MainKaraokeText
    {
        public int? HoverIndex;
        public int? StartSelectIndex;
        public int? EndSelectIndex;
        public EditableMainKaraokeText(TextObject textObject) : base(textObject)
        {
            
        }

        protected override void Update()
        {
            base.Update();
            foreach (var single in Children)
            {
                single.Colour =new Color4(0f,0f,0f,0f);
                single.Alpha = 0.001f;
            }

            try
            {
                if (HoverIndex != null)
                {
                    Children[HoverIndex.Value].Colour = Color4.Red;
                    Children[HoverIndex.Value].Alpha = 1;
                }

                if (StartSelectIndex != null && EndSelectIndex != null)
                {
                    if (StartSelectIndex <= EndSelectIndex)
                    {
                        for (int i = StartSelectIndex.Value; i <= EndSelectIndex; i++)
                        {
                            Children[i].Colour = Color4.Blue;
                            Children[i].Alpha = 1;
                        }
                    }
                    else
                    {
                        for (int i = EndSelectIndex.Value; i <= StartSelectIndex; i++)
                        {
                            Children[i].Colour = Color4.Blue;
                            Children[i].Alpha = 1;
                        }
                    }
                }
            }
            catch
            {

            }
            
        }
    }
}
