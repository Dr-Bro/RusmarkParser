using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WpfApplication2
{
    abstract class AbstractParser
    {
        //документ для анализа
        protected XmlDocument xDoc;

        //Начало древа xml файла
        protected XmlElement xRoot;

        //Dictionary records
        public Queue<Record> records = new Queue<Record>();
        //private Dictionary<string, Record> records = new Dictionary<string, Record>();
        public Dictionary<string, string[]> settingsFields = new Dictionary<string, string[]>();

        public AbstractParser Init(string path)
        {
            this.xDoc = new XmlDocument();
            xDoc.Load(path);
            return this;
        }

        public void Prepare()
        {
            this.xRoot = this.xDoc.DocumentElement;
        }

        public abstract void Process();

    }
}
