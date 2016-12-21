using System.Collections.Generic;
using SoundSet.Sibelius;

namespace SoundSet.Opusmodus
{
    public class OpusmodusSoundSets
    {
        private List<OpusmodusSoundSet> _soundSets;

        public OpusmodusSoundSets(SibeliusSoundSetType sibeliusSoundSet)
        {
            _soundSets = new List<OpusmodusSoundSet>();
            foreach (var patch in sibeliusSoundSet.PatchList)
            {
                _soundSets.Add(new OpusmodusSoundSet(patch, sibeliusSoundSet));
            }
        }

        public override string ToString()
        {
            var result = "";
            foreach (var soundSet in _soundSets)
            {
                result += soundSet.ToString();
            }
            return result;
        }
    }
}
