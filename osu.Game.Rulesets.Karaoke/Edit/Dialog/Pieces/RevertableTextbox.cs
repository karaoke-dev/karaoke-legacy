using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Game.Graphics;
using osu.Game.Graphics.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces
{
    /// <summary>
    /// common format is string
    /// </summary>
    public class RevertableTextbox : RevertableTextbox<string>
    {
        public override void ConvertOldValueToText()
        {
            Text = OldValue;
            base.ConvertOldValueToText();
        }

        public override void ConvertTextToNewValue()
        {
            NewValue = Text;
            base.ConvertTextToNewValue();
        }
    }

    /// <summary>
    /// Make a textbox thar can show value is edited or 
    /// </summary>
    public class RevertableTextbox<T> : OsuTextBox
    {
        protected override Color4 BackgroundUnfocused => HasEdited? Color4.Red.Opacity(0.1f): Color4.Black.Opacity(0.5f);
        protected override Color4 BackgroundFocused => HasEdited ? Color4.Red.Opacity(0.3f) : OsuColour.Gray(0.3f).Opacity(0.8f);
        protected override Color4 BackgroundCommit => HasEdited ? Color4.Red.Opacity(0.5f) : (Color4)BorderColour;

        /// <summary>
        /// OldValue
        /// </summary>
        protected T _oldValue;
        public virtual T OldValue
        {
            get => _oldValue;
            set
            {
                _oldValue = value;
                ConvertOldValueToText();
                HasEdited = false;
            }
        }

        /// <summary>
        /// New value
        /// </summary>
        public virtual T NewValue { get; set; }

        /// <summary>
        /// Has edited
        /// </summary>
        private bool _hasEdit;
        public virtual bool HasEdited {
            get => _hasEdit;
            set
            {
                _hasEdit = value;
                if (_hasEdit)
                {
                    //this.Background.Colour = BackgroundUnfocused;
                    this.BorderColour = HasEditBorderColor;
                }
                else
                {
                    this.BorderColour = DefauleColor;
                }
            }
        }

        public virtual Color4 HasEditBorderColor { get; set; } = Color4.Red;
        private Color4 DefauleColor;

        /// <summary>
        /// new value will be null and 
        /// </summary>
        public virtual void Revert()
        {
            HasEdited = false;
            //new value will be clear
            NewValue = default(T);
        }

        /// <summary>
        /// override text to record is edited?
        /// </summary>
        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                ConvertTextToNewValue();
            }
        }

        /// <summary>
        /// making text from old value
        /// </summary>
        public virtual void ConvertOldValueToText()
        {
            HasEdited = false;
        }

        /// <summary>
        /// update new value when text changed
        /// </summary>
        public virtual void ConvertTextToNewValue()
        {
            HasEdited = true;
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colour)
        {
            BorderColour = colour.Yellow;
            DefauleColor = BorderColour;
        }
    }
}
