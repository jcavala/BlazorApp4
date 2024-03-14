using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WinForms1.Data;

namespace WinForms1
{
     class UserSettingsHelper
    {
        public static void load()
        {
            XmlSerializer serializer =new XmlSerializer(typeof(List<UserSettings>));
            FileStream fs = new FileStream(Program.xmlFile, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);
            var users = (List<UserSettings>)serializer.Deserialize(reader);
            var user =users.Where(u => u.active).First();
            Program.Language = user.language;
            Program.Location = user.location;
            Program.user.name = user.name;
            fs.Close();
        }
        public static void write()
        {
            var usrs= new List<UserSettings>();
            //usrs.Add(new UserSettings() { name = "user2@gm.com", language = "en", location = "Zagreb", active = true });
            XmlSerializer serializer = new XmlSerializer(typeof(List<UserSettings>));
            FileStream fs = new FileStream(Program.xmlFile, FileMode.Create);
            XmlWriter reader = XmlWriter.Create(fs);
            serializer.Serialize(reader, usrs);
            fs.Close();
        }
    }
}
