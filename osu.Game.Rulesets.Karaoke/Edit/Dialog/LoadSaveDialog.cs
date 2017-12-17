using Newtonsoft.Json;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Game.Graphics.UserInterface;
using OpenTK.Graphics;
using osu.Framework.Platform;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Dialog
{
    /// <summary>
    /// before editor can save beatmap ,
    /// use this to get serialize string from object
    /// and past json string to unserialize to object
    /// </summary>
    public class LoadSaveDialog : DialogContainer
    {
        public override string Title => "Load/Save";

        protected FocusedTextBox LoadSaveTextbox { get; set; }

        protected KaraokeBasePlayfield KaraokeBasePlayfield;
        public LoadSaveDialog(KaraokeBasePlayfield karaokeBasePlayfield)
        {
            //Width = 600;
            //Height = 300;
            KaraokeBasePlayfield = karaokeBasePlayfield;
        }

        public override void InitialDialog()
        {
            MainContext = new Container()
            {
                Padding = new MarginPadding(0),
                RelativeSizeAxes = Axes.Y,
                Width = 600,
                Children = new Drawable[]
                {
                    LoadSaveTextbox= new FocusedTextBox
                    {
                        //Position=new OpenTK.Vector2(100,100),
                        Padding = new MarginPadding(10),
                        //RelativeSizeAxes = Axes.Y,
                        Width=500,
                        //Height=200,
                        Text="Helloooooooooooooooooooooooooooooooooo!" + '\n' + "World",
                        Colour=Color4.White,
                    },
                }
            };


            base.InitialDialog();
        }

        public void ShowSerializeResult()
        {
            //get list object
            List<KaraokeObject> beatmap = new List<KaraokeObject>();
            //1. get result
            var result = JsonConvert.SerializeObject(beatmap);
            //2. fill in textbox
            LoadSaveTextbox.Text = result;
        }

        public void DeSerializeresult()
        {
            string textboxString = LoadSaveTextbox.Text;
            //1. get result
            List<KaraokeObject> result = JsonConvert.DeserializeObject<List<KaraokeObject>>(textboxString);
            //2. update result to playFiled

        }
    }
}
