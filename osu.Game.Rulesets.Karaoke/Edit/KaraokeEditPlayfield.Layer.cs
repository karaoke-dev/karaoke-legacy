// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Karaoke.Beatmaps;
using osu.Game.Rulesets.Karaoke.Edit.Dialog;
using osu.Game.Rulesets.Karaoke.Edit.Layers.Lyric;
using osu.Game.Rulesets.Karaoke.Edit.Layers.Note;
using osu.Game.Rulesets.Karaoke.Objects.Note;

namespace osu.Game.Rulesets.Karaoke.Edit
{
    public partial class KaraokeEditPlayfield
    {
        /// <summary>
        ///     Frontend
        /// </summary>
        public override void InitialFrontendLayer()
        {
        }

        /// <summary>
        ///     Ruleset
        /// </summary>
        public override void InitialRulesetLayer()
        {
            AddRange(new Drawable[]
            {
                //layer
                KaraokeLyricPlayField = new KaraokeLyricEditPlayField
                {
                    KaraokeRulesetContainer = KaraokeRulesetContainer
                },
                KaraokeTonePlayfield = new KaraokeToneEditPlayfield(new List<KaraokeStageDefinition>
                {
                    new KaraokeStageDefinition
                    {
                        Columns = 11,
                        DefaultTone = new Tone()
                    }
                })
                {
                    KaraokeRulesetContainer = KaraokeRulesetContainer
                }
            });

            Layers.Add(KaraokeLyricPlayField);
            Layers.Add(KaraokeLyricPlayField);
        }

        /// <summary>
        ///     Backend
        /// </summary>
        public override void InitialBackendLayer()
        {
        }

        #region Dialog

        public void OpenListKaraokeLyricsDialog()
        {
            if (!DialogLayer.Children.OfType<ListKaraokeLyricsDialog>().Any())
                DialogLayer.Add(new ListKaraokeLyricsDialog(this));
        }

        public void OpenListKaraokeTranslateDialog()
        {
            if (!DialogLayer.Children.OfType<ListKaraokeLyricsDialog>().Any())
                DialogLayer.Add(new ListKaraokeTranslateDialog(this));
        }

        #endregion
    }
}
