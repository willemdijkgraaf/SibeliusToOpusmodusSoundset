using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpusModusSoundSet
{
    public class OpusModusSoundSets
    {
        private List<OpusModusSoundSet> _soundSets;

        public OpusModusSoundSets(SibeliusSoundSetType sibeliusSoundSet)
        {
            _soundSets = new List<OpusModusSoundSet>();
            foreach (var patch in sibeliusSoundSet.PatchList)
            {
                _soundSets.Add(new OpusModusSoundSet(patch, sibeliusSoundSet));
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
