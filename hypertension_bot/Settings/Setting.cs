using hypertension_bot.Models;
using System.IO;
using System.Xml.Serialization;

namespace hypertension_bot.Settings
{
    public class Setting
    {
        private static Setting _istance;
        private Configuration _configuration;
        public Configuration Configuration { get => _configuration; set => _configuration = value; }

        public static Setting Istance
        {
            get
            {
                if (_istance == null)
                {
                    _istance = new Setting();
                }
                return _istance;
            }
        }

        private Setting()
        {
            Deserialize(@"configuration.xml");
        }

        private void Deserialize(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
            Configuration i;
            using (Stream reader = new FileStream(filename, FileMode.Open))
            {
                i = (Configuration)serializer.Deserialize(reader);
            }
            Configuration = i;
        }
    }
}
