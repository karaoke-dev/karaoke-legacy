using System;
using System.Collections.Generic;
using System.Text;
using osu.Framework.Graphics.Containers;
using osu.Game.Overlays.Settings;
using system.Collection.Generic;


namespace osu.Game.Rulesets.Karaoke.Wiki.Sections.Pieces
{
    /// <summary>
    /// create multi selection 
    /// Filter cannot select same
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MultiSettingsEnumDropdown<T> : FillFlowContainer
    {
        //List selection
        protected List<DropDownSection<T>> ListSelectin = new List<DropDownSection<T>>();


        public string AlretMessage { get; set; } = nameof(T) + "Cannot be same";

        public int StartSelectionNumber { get; set; } = 1;

        public int MaxSelextionNumber { get; set } = -1;

        public bool EnableAddMore { get; set; } = true;

        /// <summary>
        /// Ctor
        /// </summary>
        public MultiSettingsEnumDropdown()
        {

        }

        /// <summary>
        /// Loads the complete.
        /// </summary>
        protected override void LoadComplete()
        {
            base.LoadComplete();
            //TODO : add section in here
        }
    }

    /// <summary>
    /// Drop down section.
    /// </summary>
    public class DropDownSelectionSection<T> : FillFlowContainer
    {
        
    }

    /// <summary>
    /// Alert Selection
    /// </summary>
    public class AlertSection : FillFlowContainer
    {
        
    }

    /// <summary>
    /// Add more drop down selection section. button
    /// </summary>
    public class AddMoreDropDownSelectionSection : FillFlowContainer
    {
        
    }

}
