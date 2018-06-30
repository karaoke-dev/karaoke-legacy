using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using MusixMatch_API;
using MusixMatch_API.APIMethods.Artist;
using MusixMatch_API.APIMethods.Matcher;
using MusixMatch_API.APIMethods.Track;
using MusixMatch_API.ReturnTypes;
using NUnit.Framework;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Karaoke.Tests
{
    [TestFixture]
    [Ignore("getting CI working")]
    [System.ComponentModel.Description("Test MusixMatch")]
    public class TeseCaseMusixMatch : OsuTestCase
    {
        public TeseCaseMusixMatch()
        {
            MusixMatchApi api = new MusixMatchApi("");

            List<TrackList> listResult = new List<TrackList>();

            api.TrackSearch(new TrackSearch()
            {
                Query = "宝石の国",
            },
            list =>
            {
                listResult = list;
                var first = listResult.FirstOrDefault();
                if (first!=null)
                {
                    api.TrackLyricsGet(new MusixMatch_API.APIMethods.Track.TrackLyricsGet()
                    {
                        MusixMatchId = first.Track.TrackId,
                    }, lyrics =>
                    {
                        var lyric = lyrics;

                    }, falls =>
                    {

                    });
                }

            }, s =>
            {
                
            });


            Debug.WriteLine(listResult.Count);
        }
    }
}
