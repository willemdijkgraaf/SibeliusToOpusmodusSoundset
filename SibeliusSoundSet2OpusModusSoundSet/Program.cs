using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SibeliusSoundSet2OpusModusSoundSet
{
    class Program
    {
        private static readonly Dictionary<string, string> SoundSetPaths = new Dictionary<string, string>()
        {
            { ".\\SibeliusSoundSets\\VE Strings.xml",".\\OpusmodusSoundSets\\VslSibeliusStringsSoundSet.opmo"},
            { ".\\SibeliusSoundSets\\VE Choir.xml",".\\OpusmodusSoundSets\\VslSibeliusChoirSoundSet.opmo"},
            { ".\\SibeliusSoundSets\\VE Epic Orchestra.xml",".\\OpusmodusSoundSets\\VslSibeliusEpicOrchestraSoundSet.opmo"},
            { ".\\SibeliusSoundSets\\VE Percussion.xml",".\\OpusmodusSoundSets\\VslSibeliusPercussionSoundSet.opmo"},
            { ".\\SibeliusSoundSets\\VE Special Edition.xml",".\\OpusmodusSoundSets\\VslSibeliusSpecialEditionSoundSet.opmo"},
            { ".\\SibeliusSoundSets\\VE Special Edition PLUS.xml",".\\OpusmodusSoundSets\\VslSibeliusSpecialEditionPlusSoundSet.opmo"},
            { ".\\SibeliusSoundSets\\VE Woodwinds+Brass.xml",".\\OpusmodusSoundSets\\VslSibeliusWoodwindsAndBrassSoundSet.opmo"},
        };
        static void Main(string[] args)
        {
            foreach (var soundSetPath in SoundSetPaths)
            {
                var sibeliusSoundSet = GetSoundSet(soundSetPath.Key);
                var opusModusSoundSets = new OpusModusSoundSet.OpusModusSoundSets(sibeliusSoundSet);
                var result = opusModusSoundSets.ToString();

                var path = soundSetPath.Value;
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                File.WriteAllText(path, result);
            }

        }

        private static SibeliusSoundSetType GetSoundSet(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SibeliusSoundSetType));

            StreamReader reader = new StreamReader(filePath);
            var soundSet = (SibeliusSoundSetType)serializer.Deserialize(reader);
            reader.Close();

            return soundSet;
        }
    }
}
