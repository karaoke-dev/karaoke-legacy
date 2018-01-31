using OpenTK;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Overlays.Settings;
using Symcol.Rulesets.Core.Wiki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections
{
    /// <summary>
    /// [4] introduce v2 system
    ///     4.1 : open the microphone button
    ///     4.1 : device
    ///     4.2 : volumn
    ///     4.3 : echo
    /// </summary>
    class MicrophoneSection : WikiSection
    {
        public override string Title => "Microphone";

        public SettingsButton OpenMicrophoneButton;

        private bool _microphoneOpen;
        public bool IsMicrophoneOpen
        {
            get => _microphoneOpen;
            set
            {
                _microphoneOpen = value;
                if (_microphoneOpen == true)
                {
                    OpenMicrophoneButton.Text = "Close Microphone";
                    OpenMicrophone();
                }
                else
                {
                    OpenMicrophoneButton.Text = "Open Microphone";
                    CloseMicrophone();
                }
            }
        }

        public MicrophoneSection()
        {
            Content.Add(new WikiTextSection("TODO : Introduce about V2 system"));
            

            Content.Add(new WikiSubSectionHeader("BeforeSetting"));
            //
            Content.Add(new Container
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                AutoSizeAxes = Axes.Y,
                RelativeSizeAxes = Axes.X,
                Masking = true,

                Children = new Drawable[]
                {
                    new Container
                    {
                        Position = new Vector2(-10, 0),
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                        AutoSizeAxes = Axes.Y,
                        Width = 200,

                        Child = OpenMicrophoneButton = new SettingsButton
                        {
                            Action=()=>
                            {
                                IsMicrophoneOpen = !IsMicrophoneOpen;
                            },
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
                        Child = new WikiTextSection("before setting, you have to press this button to enable microphone")
                    }
                }
            });

            Content.Add(new WikiSubSectionHeader("Device"));
            //list microphone device
            Content.Add(new Container
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                AutoSizeAxes = Axes.Y,
                RelativeSizeAxes = Axes.X,
                Masking = true,

                Children = new Drawable[]
                {
                    new Container
                    {
                        Position = new Vector2(-10, 0),
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                        AutoSizeAxes = Axes.Y,
                        Width = 200,

                        Child = new SettingsDropdown<int>
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
                        Child = new WikiTextSection("before setting, you have to press this button to enable microphone")
                    }
                }
            });

            Content.Add(new WikiSubSectionHeader("Volumn"));
            //list microphone device
            Content.Add(new Container
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                AutoSizeAxes = Axes.Y,
                RelativeSizeAxes = Axes.X,
                Masking = true,

                Children = new Drawable[]
                {
                    new Container
                    {
                        Position = new Vector2(-10, 0),
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                        AutoSizeAxes = Axes.Y,
                        Width = 200,

                        Child = new SettingsSlider<double>
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
                        Child = new WikiTextSection("before setting, you have to press this button to enable microphone")
                    }
                }
            });

            Content.Add(new WikiSubSectionHeader("Other Setting"));
            //TODO
            Content.Add(new WikiTextSection("Waiting to implement"));

            IsMicrophoneOpen = false;
        }

        protected void OpenMicrophone()
        {

        }

        protected void CloseMicrophone()
        {

        }
    }
}
