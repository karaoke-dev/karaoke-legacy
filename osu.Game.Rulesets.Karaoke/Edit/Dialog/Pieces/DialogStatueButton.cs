using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.UserInterface;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces
{
    /// <summary>
    /// manage button
    /// </summary>
    public class DialogStatueButton : FillFlowContainer<StatueButton>
    {

    }

    /// <summary>
    /// statueButton
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
    /// Ok button
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
    /// ApplyButton
    /// </summary>
    public class ApplyButton : OkButton
    {
        public ApplyButton()
        {
            Text = "Apply";
        }
    }

    /// <summary>
    /// cancel Button
    /// </summary>
    public class CancelButton : StatueButton
    {
        public CancelButton()
        {
            Text = "Cancel";
        }
    }
}
