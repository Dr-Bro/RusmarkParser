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

      public List<SqlSetting> sql = new List<SqlSetting>();

      public List<RecordFinal> result = new List<RecordFinal>(); 

        public  Record ListsForeach(XmlNode xnode)
        {
            Record record = new Record { elements = new List<Fields>() };
            RecordFinal rec = new RecordFinal { fields = new Dictionary<string, string>() };
            foreach (XmlNode child in xnode.ChildNodes)
            {
                Fields field = new Fields { fields = new Dictionary<string, Subfields>() };
                Subfields subfields = new Subfields { props = new Dictionary<string, string>() };
               
                if (this.settingsFields.Keys.Contains(child.Name))
                {
                    if (child.ChildNodes.Count > 0)
                    {
                        subfields = this.ReformatNode(child, subfields, rec);
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
            Console.WriteLine("asdaf");
            this.result.Add(rec);
            return record;
        }

        public  Subfields ReformatNode(XmlNode node, Subfields subfields, RecordFinal rec)
        {
            XmlNode parrent = node;
            
            if (node.ChildNodes.Count > 0)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    string index = node.Name;
                   
                        
                    if (this.settingsFields.Keys.Contains(index) && this.settingsFields[index].Contains(child.Name))
                    {

                        for (int i = 0; i < this.sql.Count; i++ )
                        {
                            for (int j = 0; j < this.sql[i].props.Length; j++)
                            {
                                if(this.sql[i].name == index && this.sql[i].props[j] == child.Name){

                                    rec.fields.Add(this.sql[i].db_names[j], child.InnerText);
                                }
                            }
                        }
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
               // this.records.Enqueue(this.ListsForeach(xnode));
                this.ListsForeach(xnode);

            }
        }
    }
}
