using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Karaoke.Edit.Dialog;
using osu.Game.Rulesets.Karaoke.UI.Layers.Lyric;

namespace osu.Game.Rulesets.Karaoke.Edit
{
    public partial class KaraokeEditPlayfield
    {
        /// <summary>
        /// Frontend
        /// </summary>
        public override void InitialFrontendLayer()
        {

        }

        /// <summary>
        /// Ruleset
        /// </summary>
        public override void InitialRulesetLayer()
        {
            AddRange(new Drawable[]
            {
                //layer
                KaraokeLyricPlayField = new KaraokeLyricPlayField()
                {
                    KaraokeRulesetContainer = KaraokeRulesetContainer
                }
            });
        }

        /// <summary>
        /// Backend
        /// </summary>
        public override void InitialBackendLayer()
        {

        }

        #region Dialog

        public void OpenListKaraokeLyricsDialog()
        {
            if (!DialogLayer.Children.OfType<ListKaraokeLyricsDialog>().Any())
            {
                DialogLayer.Add(new ListKaraokeLyricsDialog(this));
            }
        }

        public void OpenListKaraokeTranslateDialog()
        {
            if (!DialogLayer.Children.OfType<ListKaraokeLyricsDialog>().Any())
            {
                DialogLayer.Add(new ListKaraokeTranslateDialog(this));
            }
        }

        #endregion
    }
}
