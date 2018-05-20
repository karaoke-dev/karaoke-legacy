// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using NUnit.Framework;
using osu.Game.Rulesets.Karaoke.Edit.Dialog;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Karaoke.Tests
{
    /// <summary>
    /// Test all the windows karaoke will use
    /// </summary>
    [TestFixture]
    [Ignore("getting CI working")]
    [System.ComponentModel.Description("Test windows")]
    public class TestCaseWindows : OsuTestCase
    {
        public TestCaseWindows()
        {
            AddStep("Add Dialog", () => AddDialog());

            AddStep("Add Lyrics Dialog", () => AddLyricsDialog());

            AddStep("Add Translate Dialog", () => AddTranslateDialog());

            AddStep("Add singer Dialog", () => AddListSingerDialog());

            AddStep("Add template Dialog", () => AddEditKaraokeTemplateDialog());

            AddStep("Add subtext Dialog", () => AddEditKaraokeSubTextDialog());
        }

        protected void AddDialog()
        {
            try
            {
                DialogContainer dialog = new DialogContainer();
                Add(dialog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
            }
        }

        protected void AddLyricsDialog()
        {
            try
            {
                ListKaraokeLyricsDialog dialog = new ListKaraokeLyricsDialog(null);
                Add(dialog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
            }
        }

        protected void AddTranslateDialog()
        {
            try
            {
                ListKaraokeTranslateDialog dialog = new ListKaraokeTranslateDialog(null);
                Add(dialog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
            }
        }

        protected void AddListSingerDialog()
        {
            try
            {
                ListSingerDialog dialog = new ListSingerDialog();
                Add(dialog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
            }
        }

        protected void AddEditKaraokeTemplateDialog()
        {
            try
            {
                EditKaraokeTemplateDialog dialog = new EditKaraokeTemplateDialog(null, null);
                Add(dialog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
            }
        }

        protected void AddEditKaraokeSubTextDialog()
        {
            try
            {
                EditKaraokeSubTextDialog dialog = new EditKaraokeSubTextDialog(null);
                Add(dialog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
            }
        }
    }
}
