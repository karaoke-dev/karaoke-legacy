// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Overlays;
using osu.Game.Rulesets.Karaoke.UI.Extension;
using osu.Game.Rulesets.Karaoke.UI.Interface;
using osu.Game.Rulesets.Karaoke.UI.Panel.Pieces;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.UI.Panel
{
    /// <summary>
    /// to show the Karaoke panel on Playfield 
    /// </summary>
    public class KaraokePanelOverlay : WaveOverlayContainer
    {
        private IAmKaraokeField PlayField;

        private const float content_width = 0.8f;

        //define the position of object
        private const int oneLayerYPosition = 30;

        private const int twoLayerYPosition = 75;
        private const int objectHeight = 30;
        private const int startXPositin = -60;

        //panel container
        private Container panelContainer;

        //TODO : all the setting object
        public KaraokeButton FirstLyricButton;

        public KaraokeButton PreviousLyricButton;
        public KaraokeButton NextLyricButton;
        public KaraokePlayPauseButton PlayPauseButton;
        public KaraokeTimerSliderBar TimeSlideBar;
        public WithUpAndDownButtonSlider SpeedSlider;
        public WithUpAndDownButtonSlider ToneSlider;
        public WithUpAndDownButtonSlider LyricOffectSlider;

        protected override void Update()
        {
            if (PlayField != null)
            {
                //Update current time
                double current = PlayField.GetCurrentTime();
                TimeSlideBar.CurrentTime = current;
            }
        }

        public KaraokePanelOverlay(IAmKaraokeField playField = null)
        {
            PlayField = playField;

            FirstWaveColour = OsuColour.FromHex(@"19b0e2").Opacity(50);
            SecondWaveColour = OsuColour.FromHex(@"2280a2").Opacity(50);
            ThirdWaveColour = OsuColour.FromHex(@"005774").Opacity(50);
            FourthWaveColour = OsuColour.FromHex(@"003a4e").Opacity(50);
            //FourthWaveColour = new Color4(0, 0, 0, 0);

            Height = 110;
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
                        panelContainer = new Container
                        {
                            Origin = Anchor.TopCentre,
                            Anchor = Anchor.TopCentre,
                            RelativeSizeAxes = Axes.X,
                            Width = content_width,
                            Height = playField != null ? 110.0f / 0.7f : 110,
                            Scale = playField != null ? new Vector2(0.7f) : new Vector2(1.0f), // if on playfield , make UI smaller
                            Children = new Drawable[]
                            {
                                //"sentence" introduce text
                                new KaraokeIntroduceText
                                {
                                    Position = new Vector2(startXPositin - 35, oneLayerYPosition),
                                    Text = "Sentence",
                                    TooltipText = "Choose the sentence you want to sing."
                                },

                                //switch to first sentence
                                FirstLyricButton = new KaraokeButton()
                                {
                                    Position = new Vector2(startXPositin + 40, oneLayerYPosition),
                                    Origin = Anchor.CentreLeft,
                                    Width = objectHeight,
                                    Height = objectHeight,
                                    Text = "1",
                                    TooltipText = "Move to first sentence",
                                    Action = () => { playField?.NavigationToFirst(); }
                                },

                                //switch to previous sentence
                                PreviousLyricButton = new KaraokeButton()
                                {
                                    Position = new Vector2(startXPositin + 80, oneLayerYPosition),
                                    Origin = Anchor.CentreLeft,
                                    Width = objectHeight,
                                    Height = objectHeight,
                                    Text = "<-",
                                    TooltipText = "Move to previous sentence",
                                    Action = () => { playField?.NavigationToPrevious(); }
                                },

                                //switch to next sentence
                                NextLyricButton = new KaraokeButton()
                                {
                                    Position = new Vector2(startXPositin + 120, oneLayerYPosition),
                                    Origin = Anchor.CentreLeft,
                                    Width = objectHeight,
                                    Height = objectHeight,
                                    Text = "->",
                                    TooltipText = "Move to next sentence",
                                    Action = () => { playField?.NavigationToNext(); }
                                },

                                //"play" introduce text
                                new KaraokeIntroduceText
                                {
                                    Position = new Vector2(startXPositin + 160, oneLayerYPosition),
                                    Text = "Play",
                                    TooltipText = "Pause,play the song and adjust time"
                                },

                                //Play and pause
                                PlayPauseButton = new KaraokePlayPauseButton()
                                {
                                    Position = new Vector2(startXPositin + 200, oneLayerYPosition),
                                    Origin = Anchor.CentreLeft,
                                    Width = objectHeight,
                                    Height = objectHeight,
                                    //Text="P",
                                    KaraokeShowingState = KaraokePlayState.Pause,
                                    Action = () =>
                                    {
                                        //TODO :
                                        if (playField != null)
                                        {
                                            if (PlayPauseButton.KaraokeShowingState == KaraokePlayState.Pause)
                                            {
                                                playField?.Pause();
                                                PlayPauseButton.KaraokeShowingState = KaraokePlayState.Play;
                                            }
                                            else
                                            {
                                                playField?.Play();
                                                PlayPauseButton.KaraokeShowingState = KaraokePlayState.Pause;
                                            }
                                        }
                                    }
                                },

                                //time slider
                                TimeSlideBar = new KaraokeTimerSliderBar()
                                {
                                    Position = new Vector2(startXPositin + 280, oneLayerYPosition),
                                    Origin = Anchor.CentreLeft,
                                    Width = 500,
                                    StartTime = playField != null ? (playField?.FirstObjectTime()).Value : 0,
                                    EndTime = playField != null ? (playField?.LastObjectTime()).Value : 100000, //1:40
                                    OnValueChanged = (eaa, newValue) => { playField?.NavigateToTime(newValue); },
                                },

                                //"speed" introduce
                                new KaraokeIntroduceText
                                {
                                    Position = new Vector2(startXPositin - 30, twoLayerYPosition),
                                    Text = "Speed",
                                    TooltipText = "Adjust song speed."
                                },

                                //speed
                                SpeedSlider = new WithUpAndDownButtonSlider()
                                {
                                    Position = new Vector2(startXPositin + 60, twoLayerYPosition),
                                    Origin = Anchor.CentreLeft,
                                    Width = 150,
                                    MinValue = 0.5f,
                                    MaxValue = 1.5f,
                                    Value = 1,
                                    DefauleValue = 1,
                                    KeyboardStep = 0.05f,
                                    OnValueChanged = (eaa, newValue) => { playField?.AdjustSpeed(newValue); },
                                },

                                //"tone" introduce
                                new KaraokeIntroduceText
                                {
                                    Position = new Vector2(startXPositin + 255, twoLayerYPosition),
                                    Text = "Tone",
                                    TooltipText = "Adjust song tone."
                                },

                                //Tone
                                ToneSlider = new WithUpAndDownButtonSlider()
                                {
                                    Position = new Vector2(startXPositin + 340, twoLayerYPosition),
                                    Origin = Anchor.CentreLeft,
                                    Width = 150,
                                    MinValue = 0.5f,
                                    MaxValue = 1.5f,
                                    Value = 1.0f,
                                    DefauleValue = 1,
                                    KeyboardStep = 0.05f,
                                    OnValueChanged = (eaa, newValue) => { playField?.AdjustTone(newValue); },
                                },

                                //"offset" introduce
                                new KaraokeIntroduceText
                                {
                                    Position = new Vector2(startXPositin + 535, twoLayerYPosition),
                                    Text = "Offset",
                                    TooltipText = "Adjust lyrics appear offset."
                                },

                                //offset
                                LyricOffectSlider = new WithUpAndDownButtonSlider()
                                {
                                    Position = new Vector2(startXPositin + 630, twoLayerYPosition),
                                    Origin = Anchor.CentreLeft,
                                    Width = 150,
                                    MinValue = -5.0f,
                                    MaxValue = 5.0f,
                                    Value = 0,
                                    DefauleValue = 0,
                                    KeyboardStep = 0.5f,
                                    OnValueChanged = (eaa, newValue) => { playField?.AdjustlyricsOffset(newValue); },
                                },
                            },
                        },
                    },
                },
            };

            //initialize value
            this.SpeedSlider.Value = PlayField.GetSpeed();
            this.ToneSlider.Value = PlayField.GetTone();
        }
    }
}
