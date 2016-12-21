using System.Collections.Generic;
using System.Linq;

namespace OpusModusSoundSet
{
    public class OpusModusArticulation
    {
        private SwitchType _switch;
        private readonly SibeliusSoundSetType _sibeliusSoundSet;
        private Dictionary<sbyte, string> _keys = new Dictionary<sbyte, string>()
        {
            { 0, "c"},
            { 1, "cs"},
            { 2, "d"},
            { 3, "ds"},
            { 4, "e"},
            { 5, "f"},
            { 6, "fs"},
            { 7, "g"},
            { 8, "gs"},
            { 9, "a"},
            { 10, "as"},
            { 11, "b"},
        };

        private Dictionary<string, string> _SibeliusToOpusmodusArticulation = new Dictionary<string, string>()
        {
            { "staccato" ,"stacc"},
            { "staccatissimo" ,"stacs"},
            { "detache" ,"deta"},
            //{ "" ,"deta+non-vib"},
            //{ "" ,"ord"},
            //{ "" ,"nat"},
            { "vibrato" ,"vib"},
            //{ "" ,"arco"},
            //{ "" ,"vib+marc"},
            //{ "" ,"vib+espr"},
            { "non-vibrato" ,"non-vib"},
            { "sul-ponticello-staccato" ,"ponte+stacc"},
            { "sul-ponticello-detache" ,"ponte+deta"},
            //{ "" ,"da-ponte"},
            { "sul-ponticello" ,"ponte"},
            //{ "" ,"ponte+sfz"},
            //{ "" ,"ponte+sffz"},
            //{ "" ,"ponte+trem"},
            { "sul-ponticello-legato" ,"leg+ponte"},
            //{ "" ,"tasto+stacc"},
            //{ "" ,"tasto+deta"},
            //{ "" ,"tasto"},
            //{ "" ,"tasto+non-vib"},
            //{ "" ,"tasto+sfz"},
            //{ "" ,"tasto+trem"},
            //{ "" ,"leg+tasto"},
            //{ "" ,"vib+pfp"},
            //{ "" ,"vib+fp"},
            //{ "" ,"vib+sfz"},
            //{ "" ,"vib+sffz"},
            { "pizzicato" ,"pizz"},
            //{ "" ,"secco"},
            { "pizzicato-snap" ,"snap"},
            { "col-legno" ,"legno"},
            { "harmonic-staccato" ,"harm+stacc"},
            { "harmonic" ,"harm"},
            //{ "" ,"harm+gliss"},
            { "tremolo-unmeasured" ,"trem"},
            { "trill-whole" ,"tr2"},
            { "trill-half" ,"tr1"},
            //{ "" ,"tr2+marc"},
            //{ "" ,"tr1+marc"},
            { "legato" ,"leg"},
            { "portato" ,"port"},
            //{ "" ,"leg+stacc"},
            //{ "" ,"leg+spicc"},
            //{ "" ,"leg+mart"},
        };

        public OpusModusArticulation(SwitchType sibeliusSwitch, SibeliusSoundSetType sibeliusSoundSet)
        {
            _switch = sibeliusSwitch;
            _sibeliusSoundSet = sibeliusSoundSet;
        }

        private string Name => $"          {TrimSoundId(_switch.SoundIDChange).PadRight(32)}";

        private string TrimSoundId(string soundId)
        {
            return soundId.Replace("+", "").Replace("-", "").Replace("[", "").Replace("]", "").Replace(".","-").Replace(" ","-");
        }

        private string Controller
        {
            get
            {
                if (_switch.ControllerSwitch == null) return null;
                string result = "";
                foreach (var controllerSwitch in _switch.ControllerSwitch)
                {
                    var controllerNumber =
                        _sibeliusSoundSet.ControllerTypeList.First(x => x.Name == controllerSwitch.Name).Number;
                    result += $"cc{controllerNumber} {controllerSwitch.Value} ".PadRight(8);
                }
                return result;
            }
        }

        private string KeySwitches
        {
            get
            {
                if (_switch.KeySwitch == null) return null;
                string result = "";
                foreach (var keySwitch in _switch.KeySwitch)
                {
                    var key = keySwitch.Key;
                    var keyName = _keys[(sbyte)(key%12)];
                    var octave = (sbyte)(key/12)-1;
                    result += $":key {keyName}{octave} ";
                }
                return result;
            }
        }
        public override string ToString()
        {
            return $"{Name}({Controller} {KeySwitches})\r\n";
        }

        public string Omn()
        {
            var keyName = TrimSoundId(_switch.SoundIDChange);
            if (_SibeliusToOpusmodusArticulation.ContainsKey(keyName))
            {
                var name = $"          {_SibeliusToOpusmodusArticulation[keyName].PadRight(32)}";
                return $"{name}({Controller} {KeySwitches})\r\n";
            }
            return null;
        }
    }
}