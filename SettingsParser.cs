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
        public List<SqlSetting> sql = new List<SqlSetting>();

        public override void Process()
        {

            this.Prepare();

            foreach (XmlNode xnode in this.xRoot)
            {
                string name = "";
                string[] subfields = new string[] { };
                string[] db_mass = new string[] { };

                foreach (XmlNode child in xnode)
                {
                    switch (child.Name)
                    {
                        case "number": name = child.InnerText; break;
                        case "subfield": subfields = child.InnerText.Split(','); break;
                        case "dbnames": db_mass = child.InnerText.Split(','); break;
                    }

                }
                this.settingsFields.Add(name, subfields);
                this.sql.Add(new SqlSetting(name, subfields, db_mass));

            }

        }
    }
}
