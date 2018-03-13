using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Configuration;
using osu.Game.Overlays;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Karaoke.Configuration;
using OpenTK;
using Symcol.Rulesets.Core.Wiki;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections
{
    /// <summary>
    /// Desktop or mobile device
    /// </summary>
    internal class DeviceSection : BaseWikiSection
    {
        public override string Title => "Device";

        protected override void InitialView()
        {
            Content.Add(new WikiTextSection("Choose the Target Device"));
            Content.Add(new WikiTextSection(" \n\n"));

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

                        Child = new SettingsEnumDropdown<DeviceType>
                        {
                            Bindable = RulesetConfig.GetBindable<DeviceType>(KaraokeSetting.Device)
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
                        Child = new WikiTextSection("Choose the target device fit to you.")
                    }
                }
            });
        }

       
    }
}
