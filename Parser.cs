using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WpfApplication2
{
    class Parser : AbstractParser
    {
        public  Record ListsForeach(XmlNode xnode)
        {
            Record record = new Record { elements = new List<Fields>() };
            foreach (XmlNode child in xnode.ChildNodes)
            {
                Fields field = new Fields { fields = new Dictionary<string, Subfields>() };
                Subfields subfields = new Subfields { props = new Dictionary<string, string>() };
                if (this.settingsFields.Keys.Contains(child.Name))
                {
                    if (child.ChildNodes.Count > 1)
                    {
                        subfields = this.ReformatNode(child, subfields);
                        field.fields.Add(child.Name, subfields);
                        record.elements.Add(field);

                    }
                    else
                    {
                        subfields.props.Add("value", child.InnerText);
                        field.fields.Add(child.Name, subfields);
                        record.elements.Add(field);

                    }
                }
            }
            return record;
        }

        public  Subfields ReformatNode(XmlNode node, Subfields subfields)
        {
            XmlNode parrent = node;
            if (node.ChildNodes.Count > 1)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    string index = node.Name;


                    if (this.settingsFields.Keys.Contains(index) && this.settingsFields[index].Contains(child.Name))
                    {
                        //record_list.Add(new TableCreator.Element(parrent.ParentNode.InnerText, node.Name, child.Name, child.InnerText));
                        subfields.props.Add(child.Name, child.InnerText);
                    }
                }
            }
            return subfields;
        }

        public override void Process()
        {
            this.Prepare();

            foreach (XmlNode xnode in this.xRoot)
            {
                // xnode == record
                this.records.Enqueue(this.ListsForeach(xnode));

            }
        }
    }
}
