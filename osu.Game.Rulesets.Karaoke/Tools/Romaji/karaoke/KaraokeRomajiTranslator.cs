// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Objects;

namespace osu.Game.Rulesets.Karaoke.Tools.Romaji.karaoke
{
    /// <summary>
    /// this translate server is provided by : 
    /// https://romaji-translator.herokuapp.com/
    /// </summary>
    public class KaraokeRomajiTranslator
    {
        /// <summary>
        /// translte list 
        /// </summary>
        /// <param name="sourceLangeCode"></param>
        /// <param name="translateListString"></param>
        /// <returns></returns>
        public async Task<Lyric> Translate(Lyric translateListString)
        {
            return (await TranslatePart(new List<Lyric>() { translateListString })).FirstOrDefault();
        }


        /// <summary>
        /// translte list 
        /// </summary>
        /// <param name="sourceLangeCode"></param>
        /// <param name="translateListString"></param>
        /// <returns></returns>
        public async Task<List<Lyric>> Translate(List<Lyric> translateListString)
        {
            foreach (var single in translateListString)
            {
            }
            return translateListString;
        }

        /// <summary>
        /// translate it part
        /// </summary>
        /// <param name="translateListString"></param>
        /// <returns></returns>
        protected async Task<List<Lyric>> TranslatePart(List<Lyric> translateListString)
        {
            return null;
        }
    }

    /// <summary>
    /// translate result
    /// </summary>
    public class TranslateResult
    {
    }
}
