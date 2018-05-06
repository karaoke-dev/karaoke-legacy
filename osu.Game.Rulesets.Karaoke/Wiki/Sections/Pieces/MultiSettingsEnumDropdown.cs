// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Graphics.Containers;

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
        protected List<DropDownSelectionSection<T>> ListSelectin = new List<DropDownSelectionSection<T>>();


        public string AlretMessage { get; set; } = nameof(T) + "Cannot be same";

        public int StartSelectionNumber { get; set; } = 1;

        public int MaxSelextionNumber { get; set; } = -1;

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
