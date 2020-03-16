using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WpfApplication2
{
    class SettingsParser : AbstractParser
    {
        public override void Process()
        {
            this.Prepare();

            foreach (XmlNode xnode in this.xRoot)
            {
                string name = "";
                string[] mass = new string[] { "" };
                foreach (XmlNode child in xnode)
                {
                    switch (child.Name)
                    {
                        case "number": name = child.InnerText; break;
                        case "subfield": mass = child.InnerText.Split(','); break;
                    }
                }
                this.settingsFields.Add(name, mass);
            }
        }

    }
}
