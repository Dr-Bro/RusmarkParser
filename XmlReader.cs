using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace WpfApplication2
{
    struct Record
    {
        public List<Fields> elements { get; set; }
        
    }

    struct Fields
    {
        public Dictionary<string, Subfields> fields { get; set; }
    }

    struct Subfields
    {
        public Dictionary<string, string> props { get; set; }
    }
    class XmlReader
    {
        //документ для анализа
        private XmlDocument xDoc;

        //Начало древа xml файла
        private XmlElement xRoot;

        //словарь настроек
        public string settings { get; set; }

        //Dictionary records
        private Queue<Record> records = new Queue<Record>();
        //private Dictionary<string, Record> records = new Dictionary<string, Record>();
        public Dictionary<string, string[]> settingsFields = new Dictionary<string,string[]>();

        private Dictionary<string, string[]> fields = new TableCreator().getFields();
        private List<XmlNode> records_list = new List<XmlNode>();
        private List<TableCreator.Element> record_list = new List<TableCreator.Element>();

        private string[] attr2 = {
                                 "FIELD.1",
                                 "FIELD.10",
                                 "FIELD.200",
                                 "FIELD.210",
                                 "FIELD.215",
                                 "FIELD.225",
                                 "FIELD.300", 
                                 "FIELD.331",
                                 "FIELD.461",
                                 "FIELD.702",
                                 "FIELD.852",
                                 "FIELD.899",
                                 "FIELD.905",
                                 "FIELD.907",
                                 "FIELD.910"
                                };


        public XmlReader Init(string path)
        {
            this.xDoc = new XmlDocument();
            xDoc.Load(path);
            return this;
        }

        private void Prepare()
        {
            this.xRoot = this.xDoc.DocumentElement;
        }

        public Dictionary<string, string[]> LoadSettings()
        {
            //this.xDoc = new XmlDocument();
            //this.xDoc.Load(this.settings);
            this.Prepare();

            foreach (XmlNode xnode in this.xRoot)
            {
               string name = "";
               string[] mass = new string[] {""};
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

            return settingsFields; 
        }

        private void FindElement(XmlNode node)
        {
            // node == внутренний элемент record
            // если Элемент имеет свойства то вызывается ReformatNode
            // если нет выводится результат
            if (node.ChildNodes.Count > 1)
            {
                //this.ReformatNode(node);
            }
            else
            {
                
                this.WriteResult(node.Name);
                //this.WriteResult(node.Name, node.InnerText);
            }
        }

        public Queue<Record> Process()
        {
            this.Prepare();

            foreach (XmlNode xnode in this.xRoot)
            {
                // xnode == record
                this.WriteResult("------BOOK---------");
                this.records_list.Add(xnode);
                this.records.Enqueue(this.ListsForeach(xnode));
                this.WriteResult("------END BOOK---------");
                
            }

            this.WriteResult(this.records_list.ToString());
            return this.records;
        }


        private Record ListsForeach(XmlNode xnode)
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
                      subfields =  this.ReformatNode(child, subfields);
                      field.fields.Add(child.Name, subfields);
                      record.elements.Add(field);
                  
                    }
                    else
                    {
                        subfields.props.Add("value",child.InnerText);
                        field.fields.Add(child.Name, subfields);
                        record.elements.Add(field);
                   
                    }
               }
            }
           return record;
        }

        private XmlNodeList TakeNode(XmlNode Node)
        {
            return Node.ChildNodes;
        }

        private Subfields ReformatNode(XmlNode node, Subfields subfields)
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

        private string SubStrReplace(string str, string substr, string text)
        {
            return str.Replace(substr, text);
        }

        private void WriteResult(string str1, string str2 = "")
        {
            //string readPath = @"hta.txt";
            string writePath = @"ath.txt";

            //string text = "";
            try
            {
                //using (StreamReader sr = new StreamReader(readPath, System.Text.Encoding.Default))
                // {
                //  text = sr.ReadToEnd();
                // }
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine("{0} : {1}", str1, str2);
                }

                //using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                //{
                // sw.WriteLine("Дозапись");
                //  sw.Write(4.5);
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
