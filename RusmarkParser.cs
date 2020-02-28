using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml;

namespace WpfApplication2
{
    class RusmarkParser
    {
        public XmlDocument file_settings;


        public  RusmarkParser LoadXml(string file)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(file);
            this.file_settings = xDoc;
            return this;
        }

        
        public void run(string path, string settings)
        {
            XmlReader rusMark = new XmlReader();
            rusMark.Init(settings).LoadSettings();
            rusMark.Init(path).Process();
            Console.Read();
        }
    }
   
}
