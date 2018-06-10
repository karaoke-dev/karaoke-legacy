// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using Newtonsoft.Json;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.UserInterface;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.UI;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog
{
    /// <summary>
    ///     before editor can save beatmap ,
    ///     use this to get serialize string from object
    ///     and past json string to unserialize to object
    /// </summary>
    public class LoadSaveDialog : DialogContainer
    {
        public override string Title => "Load/Save";

        protected KaraokeBasePlayfield KaraokeBasePlayfield;

        protected FocusedTextBox LoadSaveTextbox { get; set; }

        public LoadSaveDialog(KaraokeBasePlayfield karaokeBasePlayfield)
        {
            //Width = 600;
            //Height = 300;
            KaraokeBasePlayfield = karaokeBasePlayfield;
        }

        public override void InitialDialog()
        {
            MainContext = new Container
            {
                Padding = new MarginPadding(0),
                RelativeSizeAxes = Axes.Y,
                Width = 600,
                Children = new Drawable[]
                {
                    LoadSaveTextbox = new FocusedTextBox
                    {
                        //Position=new OpenTK.Vector2(100,100),
                        Padding = new MarginPadding(10),
                        //RelativeSizeAxes = Axes.Y,
                        Width = 500,
                        //Height=200,
                        Text = "Helloooooooooooooooooooooooooooooooooo!" + '\n' + "World",
                        Colour = Color4.White
                    }
                }
            };


            base.InitialDialog();
        }

        public void ShowSerializeResult()
        {
            //get list object
            var beatmap = new List<BaseLyric>();
            //1. get result
            var result = JsonConvert.SerializeObject(beatmap);
            //2. fill in textbox
            LoadSaveTextbox.Text = result;
        }

        public void DeSerializeresult()
        {
            var textboxString = LoadSaveTextbox.Text;
            //1. get result
            var result = JsonConvert.DeserializeObject<List<BaseLyric>>(textboxString);
            //2. update result to playFiled
        }
    }
}
