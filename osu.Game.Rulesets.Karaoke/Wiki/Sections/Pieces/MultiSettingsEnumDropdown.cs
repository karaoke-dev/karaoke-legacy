// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.UserInterface;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Karaoke.Configuration;
using OpenTK;
using Symcol.Rulesets.Core.Wiki;

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

        //public BindableObject<Dictionary<int,T>> Bindnig { get; set; } = new BindableObject<Dictionary<int, T>>(new Dictionary<int, T>());

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
        protected SettingsDropdown<T> Dropdown;
        protected WikiTextSection TextSection;

        public Bindable<T> Bindable => Dropdown.Bindable;

        public DropDownSelectionSection()
        {
            Direction = FillDirection.Horizontal;

            Children = new Drawable[]
            {
                new Container
                {
                    Position = new Vector2(-10, 0),
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    AutoSizeAxes = Axes.Y,
                    Width = 200,
                    Child = Dropdown = new SettingsDropdown<T>
                    {

                    }
                },
                new Container
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Width = 400,
                    AutoSizeAxes = Axes.Y,
                    AutoSizeDuration = 100,
                    AutoSizeEasing = Easing.OutQuint,
                    Child = TextSection = new WikiTextSection("before setting, you have to press this button to enable microphone")
                }
            };
        }
    }

    /// <summary>
    /// Alert Selection
    /// </summary>
    public class AlertSection : FillFlowContainer
    {
        protected WikiTextSection TextSection;

        public AlertSection()
        {
            Direction = FillDirection.Horizontal;

            Children = new Drawable[]
            {
                new Container
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Width = 400,
                    AutoSizeAxes = Axes.Y,
                    AutoSizeDuration = 100,
                    AutoSizeEasing = Easing.OutQuint,
                    Child = TextSection = new WikiTextSection("before setting, you have to press this button to enable microphone")
                }
            };
        }
    }

    /// <summary>
    /// Add more drop down selection section. button
    /// </summary>
    public class AddMoreDropDownSelectionSection : FillFlowContainer
    {
        protected OsuButton Button;

        public AddMoreDropDownSelectionSection()
        {
            Direction = FillDirection.Horizontal;

            Children = new Drawable[]
            {
                new Container
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Width = 400,
                    AutoSizeAxes = Axes.Y,
                    AutoSizeDuration = 100,
                    AutoSizeEasing = Easing.OutQuint,
                    Child = Button = new OsuButton
                    {
                        Text = "Add Section"
                    }
                }
            };
        }
    }
}
