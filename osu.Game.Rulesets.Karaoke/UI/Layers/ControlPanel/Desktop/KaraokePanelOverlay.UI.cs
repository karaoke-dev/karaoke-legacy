// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Rulesets.Karaoke.UI.Extension;
using osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Desktop.Pieces;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Desktop
{
    public partial class KaraokePanelOverlay
    {
        private const float content_width = 0.8f;

        //define the position of object
        private const int one_layer_y_position = 30;
        private const int two_layer_y_position = 75;
        private const int object_height = 30;
        private const int start_x_positin = 60;
        private const int height = 130;

        //TODO : all the setting object
        public KaraokeButton FirstLyricButton;
        public KaraokeButton PreviousLyricButton;
        public KaraokeButton NextLyricButton;
        public KaraokePlayPauseButton PlayPauseButton;
        public KaraokeTimerSliderBar TimeSlideBar;
        public WithUpAndDownButtonSlider SpeedSlider;
        public WithUpAndDownButtonSlider ToneSlider;
        public WithUpAndDownButtonSlider LyricOffectSlider;

        public bool LoadComplete = false;

        protected override void Update()
        {
            if (_playField != null && LoadComplete)
            {
                //Update current time
                double current = _playField.GetCurrentTime();
                TimeSlideBar.CurrentTime = current;
            }
        }

        protected void InitialPanel()
        {
            FirstWaveColour = OsuColour.FromHex(@"19b0e2").Opacity(50);
            SecondWaveColour = OsuColour.FromHex(@"2280a2").Opacity(50);
            ThirdWaveColour = OsuColour.FromHex(@"005774").Opacity(50);
            FourthWaveColour = OsuColour.FromHex(@"003a4e").Opacity(50);
            //FourthWaveColour = new Color4(0, 0, 0, 0);

            Height = height;
            Content.RelativeSizeAxes = Axes.X;
            Content.AutoSizeAxes = Axes.Y;
            Children = new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.Black.Opacity(0.4f)
                        },
                        //new Triangles
                        //{
                        //    TriangleScale = 5,
                        //    RelativeSizeAxes = Axes.X,
                        //    Height = Height, //set the height from the start to ensure correct triangle density.
                        //    ColourLight = new Color4(53, 66, 82, 150),
                        //    ColourDark = new Color4(41, 54, 70, 150),
                        //},
                    },
                },
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0f, 10f),
                    Children = new Drawable[]
                    {
                        // Body
                        new Container
                        {
                            Origin = Anchor.TopCentre,
                            Anchor = Anchor.TopCentre,
                            RelativeSizeAxes = Axes.X,
                            Width = content_width,
                            Height = height,
                            Scale = new Vector2(1.0f), // if on playfield , make UI smaller
                            Children = new Drawable[]
                            {
                                //"sentence" introduce text
                                new KaraokeIntroduceText
                                {
                                    Position = new Vector2(start_x_positin - 35, one_layer_y_position),
                                    Text = "Sentence",
                                    TooltipText = "Choose the sentence you want to sing."
                                },

                                //switch to first sentence
                                FirstLyricButton = new KaraokeButton()
                                {
                                    Position = new Vector2(start_x_positin + 40, one_layer_y_position),
                                    Origin = Anchor.CentreLeft,
                                    Width = object_height,
                                    Height = object_height,
                                    Text = "1",
                                    TooltipText = "Move to first sentence",
                                    Action = () => { _playField?.NavigationToFirst(); }
                                },

                                //switch to previous sentence
                                PreviousLyricButton = new KaraokeButton()
                                {
                                    Position = new Vector2(start_x_positin + 80, one_layer_y_position),
                                    Origin = Anchor.CentreLeft,
                                    Width = object_height,
                                    Height = object_height,
                                    Text = "<-",
                                    TooltipText = "Move to previous sentence",
                                    Action = () => { _playField?.NavigationToPrevious(); }
                                },

                                //switch to next sentence
                                NextLyricButton = new KaraokeButton()
                                {
                                    Position = new Vector2(start_x_positin + 120, one_layer_y_position),
                                    Origin = Anchor.CentreLeft,
                                    Width = object_height,
                                    Height = object_height,
                                    Text = "->",
                                    TooltipText = "Move to next sentence",
                                    Action = () => { _playField?.NavigationToNext(); }
                                },

                                //"play" introduce text
                                new KaraokeIntroduceText
                                {
                                    Position = new Vector2(start_x_positin + 160, one_layer_y_position),
                                    Text = "Play",
                                    TooltipText = "Pause,play the song and adjust time"
                                },

                                //Play and pause
                                PlayPauseButton = new KaraokePlayPauseButton()
                                {
                                    Position = new Vector2(start_x_positin + 200, one_layer_y_position),
                                    Origin = Anchor.CentreLeft,
                                    Width = object_height,
                                    Height = object_height,
                                    //Text="P",
                                    KaraokeShowingState = KaraokePlayState.Pause,
                                    Action = () =>
                                    {
                                        //TODO :
                                        if (_playField != null)
                                        {
                                            if (PlayPauseButton.KaraokeShowingState == KaraokePlayState.Pause)
                                            {
                                                _playField?.Pause();
                                                PlayPauseButton.KaraokeShowingState = KaraokePlayState.Play;
                                            }
                                            else
                                            {
                                                _playField?.Play();
                                                PlayPauseButton.KaraokeShowingState = KaraokePlayState.Pause;
                                            }
                                        }
                                    }
                                },

                                //time slider
                                TimeSlideBar = new KaraokeTimerSliderBar()
                                {
                                    Position = new Vector2(start_x_positin + 280, one_layer_y_position),
                                    Origin = Anchor.CentreLeft,
                                    Width = 500,
                                    StartTime = _playField != null ? (_playField?.FirstObjectTime()).Value : 0,
                                    EndTime = _playField != null ? (_playField?.LastObjectTime()).Value : 100000, //1:40
                                    OnValueChanged = (eaa, newValue) => { _playField?.NavigateToTime(newValue); },
                                },

                                //"speed" introduce
                                new KaraokeIntroduceText
                                {
                                    Position = new Vector2(start_x_positin - 30, two_layer_y_position),
                                    Text = "Speed",
                                    TooltipText = "Adjust song speed."
                                },

                                //speed
                                SpeedSlider = new WithUpAndDownButtonSlider()
                                {
                                    Position = new Vector2(start_x_positin + 60, two_layer_y_position),
                                    Origin = Anchor.CentreLeft,
                                    Width = 150,
                                    MinValue = 0.5f,
                                    MaxValue = 1.5f,
                                    Value = 1,
                                    DefauleValue = 1,
                                    KeyboardStep = 0.05f,
                                    OnValueChanged = (eaa, newValue) => { _playField?.AdjustSpeed(newValue); },
                                },

                                //"tone" introduce
                                new KaraokeIntroduceText
                                {
                                    Position = new Vector2(start_x_positin + 255, two_layer_y_position),
                                    Text = "Tone",
                                    TooltipText = "Adjust song tone."
                                },

                                //Tone
                                ToneSlider = new WithUpAndDownButtonSlider()
                                {
                                    Position = new Vector2(start_x_positin + 340, two_layer_y_position),
                                    Origin = Anchor.CentreLeft,
                                    Width = 150,
                                    MinValue = 0.5f,
                                    MaxValue = 1.5f,
                                    Value = 1.0f,
                                    DefauleValue = 1,
                                    KeyboardStep = 0.05f,
                                    OnValueChanged = (eaa, newValue) => { _playField?.AdjustTone(newValue); },
                                },

                                //"offset" introduce
                                new KaraokeIntroduceText
                                {
                                    Position = new Vector2(start_x_positin + 535, two_layer_y_position),
                                    Text = "Offset",
                                    TooltipText = "Adjust lyrics appear offset."
                                },

                                //offset
                                LyricOffectSlider = new WithUpAndDownButtonSlider()
                                {
                                    Position = new Vector2(start_x_positin + 630, two_layer_y_position),
                                    Origin = Anchor.CentreLeft,
                                    Width = 150,
                                    MinValue = -5.0f,
                                    MaxValue = 5.0f,
                                    Value = 0,
                                    DefauleValue = 0,
                                    KeyboardStep = 0.5f,
                                    OnValueChanged = (eaa, newValue) => { _playField?.AdjustlyricsOffset(newValue); },
                                },
                            },
                        },
                    },
                },
            };

            //initialize value
            SpeedSlider.Value = _playField.GetSpeed();
            ToneSlider.Value = _playField.GetTone();

            LoadComplete = true;
        }
    }
}
