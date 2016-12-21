using System.Collections.Generic;
using System.Linq;

namespace OpusModusSoundSet
{
    public class OpusModusSoundSet
    {
        private OpusModusArticulation[] _articulations;
        private PatchType _sibeliusPatch;

        public OpusModusSoundSet(PatchType sibeliusPatch, SibeliusSoundSetType sibeliusSoundSet)
        {
            _sibeliusPatch = sibeliusPatch;
            var switchTypeIndex = sibeliusPatch.ItemsElementName.TakeWhile(x => x != ItemsChoiceType.SwitchType).Count();
            if (switchTypeIndex == sibeliusPatch.ItemsElementName.Length)
            {
                return; // todo for Drums
            }
            var switchTypeName = sibeliusPatch.Items[switchTypeIndex].ToString();
            var switchTypes = sibeliusSoundSet.SwitchTypeList.First(x => x.Name == switchTypeName);
            var opusModusArticulations = new List<OpusModusArticulation>();
            foreach (var sibeliusSwitch in switchTypes.Switch)
            {
                opusModusArticulations.Add(new OpusModusArticulation(sibeliusSwitch, sibeliusSoundSet));
            }
            _articulations = opusModusArticulations.ToArray();

            // myCars.TakeWhile(car => !myCondition(car)).Count();
            //var sibeliusSwitchType = sibeliusSoundSet.SwitchTypeList.First(x=>x.Name == sibelItemsChoiceType.SwitchType))
        }

        private string Name => _sibeliusPatch.Name.Replace(" ", "-").Replace("(", "").Replace(")", "").Replace(".", "");
        private string Articulations
        {
            get
            {
                string result = "        :programs\r\n   (:group All\r\n";
                foreach (var articulation in _articulations)
                {
                    result += articulation.ToString();
                }

                result += "\r\n        :group omn\r\n";
                foreach (var articulation in _articulations)
                {
                    result += articulation.Omn();
                }
                return result + "   )\r\n";
            }
        }

        private string Controllers
        {
            get
            {
                string result = "   :controllers\r\n   (:group default-settings\r\n" +
                                "          pitch 0\r\n" +
                                "          velocity-xf 2\r\n" +
                                "          volume 7\r\n" +
                                "          pan 10\r\n" +
                                "          expression 11\r\n" +
                                "          reverb-dry/wet 14\r\n" +
                                "          reverb-on/off 15\r\n" +
                                "          slot-xf 20\r\n" +
                                "          start-scaler 21\r\n" +
                                "          master-attack 22\r\n" +
                                "          master-release 23\r\n" +
                                "          master-filter 24\r\n" +
                                "          delay-scaler 25\r\n" +
                                "          tuning-scaler 26\r\n" +
                                "          humanize 27\r\n" +
                                "          velocity-xf-on-off 28\r\n" +
                                "          rsamp-on-off 29\r\n" +
                                "          dynamics-scaler 30\r\n" +
                                "\r\n          :group Pedal\r\n" +
                                "          Ped 64\r\n" +
                                "          Sost-Ped 66\r\n" +
                                "          Una-Corda 67\r\n" +
                                "\r\n          :group matrix\r\n" +
                                "          cc1 1\r\n" +
                                "          cc2 2\r\n" +
                                "          cc12 12\r\n" +
                                "          cc13 13\r\n";
                return result + "   )\r\n";
            }
        }
        public override string ToString()
        {
            if (_articulations == null) return null;
            return $";;;---------------------------------------------------------\r\n" +
                   $";;; VIENNA INSTRUMENTS - {Name}\r\n" +
                   $";;; ---------------------------------------------------------\r\n" +
                   $"(def-sound-set VSL-{Name}\r\n" +
                   $"{Articulations}\r\n" +
                   $"\r\n" +
                   $"{Controllers}\r\n" +
                   ")\r\n\r\n";

        }
    }
}
