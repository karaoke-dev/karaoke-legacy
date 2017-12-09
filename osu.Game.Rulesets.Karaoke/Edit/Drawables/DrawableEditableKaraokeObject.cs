using osu.Game.Rulesets.Karaoke.Objects.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Framework.Input;
using OpenTK.Graphics;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Pieces;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Karaoke.Tools.Translator;
using osu.Game.Rulesets.Karaoke.Objects.Extension;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables
{
    /// <summary>
    /// Editable karaoke Drawable Object
    /// Right click :
    /// Translate >> Add
    /// </summary>
    public class DrawableEditableKaraokeObject : DrawableKaraokeObject
    {
        protected DrawableKaraokeThumbnail DrawableKaraokeThumbnail { get; set; }
        protected EditableMainKaraokeText EditableMainKaraokeText { get; set; } = new EditableMainKaraokeText(null);
        protected bool IsDrag = false;

        public DrawableEditableKaraokeObject(KaraokeObject hitObject) : base(hitObject)
        {
            DrawableKaraokeThumbnail = new DrawableKaraokeThumbnail(KaraokeObject)
            {
                //Position=new OpenTK.Vector2(0,-50),
                Width = 300,
                Height = 100,
            };
            Add(EditableMainKaraokeText);
            Add(DrawableKaraokeThumbnail);
        }

        protected override void UpdateDrawable()
        {
            base.UpdateDrawable();
            EditableMainKaraokeText.TextObject = Template?.MainText + KaraokeObject.MainText;
            EditableMainKaraokeText.Alpha = 1f;
        }

        #region Input

        protected override bool OnMouseDown(InputState state, MouseDownEventArgs args)
        {
            IsDrag = true;
            int index = GetPointedText(state);
            EditableMainKaraokeText.StartSelectIndex = index;

            return base.OnMouseDown(state, args);
        }

        //detect whith text is picked
        protected override bool OnMouseMove(InputState state)
        {
            if (!IsDrag)
            {
                int index = GetPointedText(state);
                EditableMainKaraokeText.HoverIndex = index;
            }
            else
            {
                int index = GetPointedText(state);
                EditableMainKaraokeText.EndSelectIndex = index;
            }
            return base.OnMouseMove(state);
        }

        protected override bool OnMouseUp(InputState state, MouseUpEventArgs args)
        {
            IsDrag = false;
            int index = GetPointedText(state);
            EditableMainKaraokeText.StartSelectIndex = null;
            EditableMainKaraokeText.EndSelectIndex = null;

            return base.OnMouseUp(state, args);
        }

        protected override void OnHoverLost(InputState state)
        {
            base.OnHoverLost(state);
            EditableMainKaraokeText.HoverIndex = null;
        }

        protected int GetPointedText(InputState state)
        {
            var mousePosition = this.ToLocalSpace(state.Mouse.NativeState.Position);
            return EditableMainKaraokeText.GetIndexByPosition(mousePosition.X); ;
        }

        #endregion

        public void AddPoint(ProgressPoint point)
        {

        }

        public override void AddTranslate(TranslateCode code, string translateResult)
        {
            //Add it into Karaoke object
            string langCode;
            new GoogleTranslator().LangToCodeDictionary.TryGetValue(code, out langCode);
            KaraokeObject.AddNewTranslate(new KaraokeTranslateString(langCode, translateResult));
            //base
            base.AddTranslate(code, translateResult);
        }
    }
}
