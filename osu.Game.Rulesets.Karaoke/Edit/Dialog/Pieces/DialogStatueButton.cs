// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.UserInterface;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces
{
    /// <summary>
    ///     manage button
    /// </summary>
    public class DialogStatueButton : FillFlowContainer<StatueButton>
    {
    }

    /// <summary>
    ///     statueButton
    /// </summary>
    public class StatueButton : TriangleButton
    {
        public StatueButton()
        {
            Width = 100;
            Height = 30;
        }
    }

    /// <summary>
    ///     Ok button
    /// </summary>
    public class OkButton : TriangleButton
    {
        public OkButton()
        {
            Text = "OK";
            BackgroundColour = Color4.MediumVioletRed;
        }
    }

    /// <summary>
    ///     ApplyButton
    /// </summary>
    public class ApplyButton : OkButton
    {
        public ApplyButton()
        {
            Text = "Apply";
        }
    }

    /// <summary>
    ///     cancel Button
    /// </summary>
    public class CancelButton : StatueButton
    {
        public CancelButton()
        {
            Text = "Cancel";
        }
    }
}
