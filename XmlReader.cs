using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApplication7
{
    class XmlReader
    {
        private XmlDocument xDoc;
        private XmlElement  xRoot;
        //private XmlNode xNode;
        private string[] attr = {
                                 "FIELD.1",
                                 "FIELD.200",
                                 "FIELD.210",
                                 "FIELD.215",
                                 "FIELD.225",
                                 "FIELD.300", 
                                 "FIELD.331",
                                 "FIELD.702",
                                 "FIELD.852",
                                 "FIELD.899"
                                };
        private Dictionary<string, string> countries = new Dictionary<string, string>
        {
                                {"FIELD.200", "Париж"},
                                {"FIELD.225", "SUBFIELD.a"},
                                {"Великобритания", "Лондон"}
        };

        public XmlReader Init(string path)
        {
            this.xDoc = new XmlDocument();
            xDoc.Load(path);
            return this;
        }

        private  void Prepare()
        {
            this.xRoot = this.xDoc.DocumentElement;
        }

        private void FindElement(XmlNode node)
        {
            if (this.attr.Contains(node.Name))
            {
                if (node.ChildNodes.Count > 1)
                {
                    this.ReformatNode(node);
                }
                else
                {
                    Console.WriteLine("XML: {0}, {1}", node.InnerText, node.Name);
                }
                //Console.WriteLine("-----------------------");
            }
        }

        public string Process()
        {
            this.Prepare();
            foreach (XmlNode xnode in this.xRoot)
            {
                Console.WriteLine("------BOOK---------");
               //Console.WriteLine(xnode.InnerXml.ToString());
                foreach (XmlNode child in xnode.ChildNodes)
                {
                  
                   this.FindElement(child);
                  
                }
                Console.WriteLine("-------END BOOK--------");
               // Console.WriteLine("Название: {0}",buf.Item(2).InnerXml);
            }
           
            return "cool";
        }

        private XmlNodeList TakeNode(XmlNode Node)
        {
            return Node.ChildNodes;
        }

        private string ReformatNode(XmlNode node){
            foreach (XmlNode child in node.ChildNodes)
            {
                //Console.WriteLine("------- ATTR ------");

                    
                        //Console.Write(node.Name);
                Console.WriteLine("XML:{0} , {1} ", child.InnerXml, child.Name);

                
                //Console.WriteLine("-------- END -------");
            }
            return "";
        }
    }
}
