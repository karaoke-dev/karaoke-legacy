using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Timing;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.UI.Extension;
using osu.Game.Rulesets.Karaoke.UI.Interface;
using osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Desktop.Pieces;
using osu.Game.Tests.Visual;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Develop
{
    [TestFixture]
    public class KaraokeDesktopPanel : OsuTestCase
    {
        public KaraokeDesktopPanel()
        {
            KaraokePanelOverlay panel = new KaraokePanelOverlay()
            {
                Clock = new FramedClock(new StopwatchClock(true)),
                RelativeSizeAxes = Axes.X,
                Origin = Anchor.BottomCentre,
                Anchor = Anchor.BottomCentre,
                Scale = new Vector2(1.0f),
                Depth = 10f
            };
            Add(panel);

            //open panel
            AddStep("open panel", ()=> panel.ToggleVisibility());
        }
    }

    public partial class KaraokePanelOverlay : WaveContainer
    {
        //TODO : all the setting object
        public KaraokeButton FirstLyricButton;
        public KaraokeButton PreviousLyricButton;
        public KaraokeButton NextLyricButton;
        public KaraokePlayPauseButton PlayPauseButton;
        public KaraokeTimerSliderBar TimeSlideBar;
        public WithUpAndDownButtonSlider SpeedSlider;
        public WithUpAndDownButtonSlider ToneSlider;
        public WithUpAndDownButtonSlider LyricOffectSlider;

        public bool LoadComplete;
        private const float content_width = 0.8f;
        private const int object_height = 30;
        private const int height = 130;
        private const int horizontal_conponent_spacing = 10;
        private const int margin_padding = 10;


        private readonly IAmKaraokeField _playField;

        protected override void Update()
        {
            if (_playField != null && LoadComplete)
            {
                //Update current time
                var current = _playField.GetCurrentTime();
                TimeSlideBar.CurrentTime = current;
            }
        }

        /// <summary>
        ///     Ctor
        /// </summary>
        /// <param name="playField"></param>
        public KaraokePanelOverlay(IAmKaraokeField playField = null)
        {
            _playField = playField;

            InitialPanel();
        }

        protected void InitialPanel()
        {
            FirstWaveColour = OsuColour.FromHex(@"19b0e2").Opacity(50);
            SecondWaveColour = OsuColour.FromHex(@"2280a2").Opacity(50);
            ThirdWaveColour = OsuColour.FromHex(@"005774").Opacity(50);
            FourthWaveColour = OsuColour.FromHex(@"003a4e").Opacity(50);

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
                        }
                    }
                },
                new Container
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Children = new Drawable[]
                    {
                        // Body
                        new Container
                        {
                            Name = "Panel Container",
                            Origin = Anchor.TopCentre,
                            Anchor = Anchor.TopCentre,
                            RelativeSizeAxes = Axes.X,
                            Width = content_width,
                            Height = height,
                            Scale = new Vector2(1.0f), // if on playfield , make UI smaller
                            Padding = new MarginPadding(margin_padding),
                            Children = new Drawable[]
                            {
                                new GridContainer
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Content = new Drawable[][]
                                    {
                                        new Drawable[]
                                        {
                                            new Container()
                                            {
                                                Name = "Time Section",
                                                RelativeSizeAxes = Axes.Both,
                                                Anchor = Anchor.CentreLeft,
                                                Origin = Anchor.CentreLeft,
                                                Children = new Drawable[]
                                                {
                                                    new FillFlowContainer()
                                                    {
                                                        Name = "Sentence",
                                                        Anchor = Anchor.CentreLeft,
                                                        Origin = Anchor.CentreLeft,
                                                        Direction =  FillDirection.Horizontal,
                                                        Spacing = new Vector2(horizontal_conponent_spacing),
                                                        AutoSizeAxes = Axes.X,
                                                        Children = new Drawable[]
                                                        {
                                                            //"sentence" introduce text
                                                            new KaraokeIntroduceText
                                                            {
                                                                Text = "Sentence",
                                                                TooltipText = "Choose the sentence you want to sing."
                                                            },

                                                            //switch to first sentence
                                                            FirstLyricButton = new KaraokeButton
                                                            {
                                                                Origin = Anchor.CentreLeft,
                                                                Width = object_height,
                                                                Height = object_height,
                                                                Text = "1",
                                                                TooltipText = "Move to first sentence",
                                                                Action = () => { _playField?.NavigationToFirst(); }
                                                            },

                                                            //switch to previous sentence
                                                            PreviousLyricButton = new KaraokeButton
                                                            {
                                                                Origin = Anchor.CentreLeft,
                                                                Width = object_height,
                                                                Height = object_height,
                                                                Text = "<-",
                                                                TooltipText = "Move to previous sentence",
                                                                Action = () => { _playField?.NavigationToPrevious(); }
                                                            },

                                                            //switch to next sentence
                                                            NextLyricButton = new KaraokeButton
                                                            {
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
                                                                Text = "Play",
                                                                TooltipText = "Pause,play the song and adjust time"
                                                            },

                                                            //Play and pause
                                                            PlayPauseButton = new KaraokePlayPauseButton
                                                            {
                                                                Origin = Anchor.CentreLeft,
                                                                Width = object_height,
                                                                Height = object_height,
                                                                //Text="P",
                                                                KaraokeShowingState = KaraokePlayState.Pause,
                                                                Action = () =>
                                                                {
                                                                    //TODO :
                                                                    if (_playField != null)
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
                                                            },
                                                        }
                                                    },
                                                    new Container
                                                    {
                                                        Name = "Time Section",
                                                        RelativeSizeAxes = Axes.X,
                                                        Anchor = Anchor.CentreLeft,
                                                        Origin = Anchor.CentreLeft,
                                                        Padding = new MarginPadding(){Left = 320, Right = 50},
                                                        Children = new Drawable[]
                                                        {
                                                            //time slider
                                                            TimeSlideBar = new KaraokeTimerSliderBar
                                                            {
                                                                RelativeSizeAxes = Axes.X,
                                                                Origin = Anchor.CentreLeft,
                                                                StartTime = _playField != null ? (_playField?.FirstObjectTime()).Value : 0,
                                                                EndTime = _playField != null ? (_playField?.LastObjectTime()).Value : 100000, //1:40
                                                                OnValueChanged = (eaa, newValue) => { _playField?.NavigateToTime(newValue); }
                                                            },
                                                        }
                                                    }
                                                   
                                                }
                                            },
                                        },
                                        new Drawable[]
                                        {
                                            new GridContainer
                                            {
                                                RelativeSizeAxes = Axes.X,
                                                Anchor = Anchor.CentreLeft,
                                                Origin = Anchor.CentreLeft,
                                                Content = new Drawable[][]
                                                {
                                                    new Drawable[]
                                                    {
                                                        new Container()
                                                        {
                                                            Name = "Speed Section",
                                                            RelativeSizeAxes = Axes.X,
                                                            Anchor = Anchor.CentreLeft,
                                                            Origin = Anchor.CentreLeft,
                                                            Children = new Drawable[]
                                                            {
                                                                //"speed" introduce
                                                                new KaraokeIntroduceText
                                                                {
                                                                    Text = "Speed",
                                                                    TooltipText = "Adjust song speed."
                                                                },

                                                                new Container
                                                                {
                                                                    RelativeSizeAxes = Axes.X,
                                                                    Padding = new MarginPadding(){Left = 100, Right = 50},
                                                                    Children = new Drawable[]
                                                                    {
                                                                        //speed
                                                                        SpeedSlider = new WithUpAndDownButtonSlider
                                                                        {
                                                                            Origin = Anchor.CentreLeft,
                                                                            RelativeSizeAxes = Axes.X,
                                                                            
                                                                            MinValue = 0.5f,
                                                                            MaxValue = 1.5f,
                                                                            Value = 1,
                                                                            DefauleValue = 1,
                                                                            KeyboardStep = 0.05f,
                                                                            OnValueChanged = (eaa, newValue) => { _playField?.AdjustSpeed(newValue); }
                                                                        },
                                                                    }
                                                                }
                                                            }
                                                        },
                                                        new Container()
                                                        {
                                                            Name = "Tone Section",
                                                            RelativeSizeAxes = Axes.X,
                                                            Anchor = Anchor.CentreLeft,
                                                            Origin = Anchor.CentreLeft,
                                                            Children = new Drawable[]
                                                            {
                                                                //"tone" introduce
                                                                new KaraokeIntroduceText
                                                                {
                                                                    Text = "Tone",
                                                                    TooltipText = "Adjust song tone."
                                                                },

                                                                new Container
                                                                {
                                                                    RelativeSizeAxes = Axes.X,
                                                                    Padding = new MarginPadding(){Left = 100, Right = 50},
                                                                    Children = new Drawable[]
                                                                    {
                                                                        //Tone
                                                                        ToneSlider = new WithUpAndDownButtonSlider
                                                                        {
                                                                            Origin = Anchor.CentreLeft,
                                                                            RelativeSizeAxes = Axes.X,
                                                                            MinValue = 0.5f,
                                                                            MaxValue = 1.5f,
                                                                            Value = 1.0f,
                                                                            DefauleValue = 1,
                                                                            KeyboardStep = 0.05f,
                                                                            OnValueChanged = (eaa, newValue) => { _playField?.AdjustTone(newValue); }
                                                                        },
                                                                    }
                                                                }
                                                            }
                                                        },
                                                        new Container()
                                                        {
                                                            Name = "Offset Section",
                                                            RelativeSizeAxes = Axes.X,
                                                            Anchor = Anchor.CentreLeft,
                                                            Origin = Anchor.CentreLeft,
                                                            Children = new Drawable[]
                                                            {
                                                                //"offset" introduce
                                                                new KaraokeIntroduceText
                                                                {
                                                                    Text = "Offset",
                                                                    TooltipText = "Adjust lyrics appear offset."
                                                                },

                                                                new Container
                                                                {
                                                                    RelativeSizeAxes = Axes.X,
                                                                    Padding = new MarginPadding(){Left = 100, Right = 50},
                                                                    Children = new Drawable[]
                                                                    {
                                                                        //offset
                                                                        LyricOffectSlider = new WithUpAndDownButtonSlider
                                                                        {
                                                                            Origin = Anchor.CentreLeft,
                                                                            RelativeSizeAxes = Axes.X,
                                                                            MinValue = -5.0f,
                                                                            MaxValue = 5.0f,
                                                                            Value = 0,
                                                                            DefauleValue = 0,
                                                                            KeyboardStep = 0.5f,
                                                                            OnValueChanged = (eaa, newValue) => { _playField?.AdjustlyricsOffset(newValue); }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                            }
                        }
                    }
                }
            };

            //initialize value
            SpeedSlider.Value = _playField.GetSpeed();
            ToneSlider.Value = _playField.GetTone();

            LoadComplete = true;
        }
    }
}
