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
            SettingsParser sett = new SettingsParser();
            sett.Init(settings).Process();
            Dictionary<string, string[]> settin = sett.settingsFields;

            Parser pars = new Parser();
            pars.settingsFields = settin;
            pars.Init(path).Process();
            Queue<Record> books1 = pars.records;

            SqlGenerator sql = new SqlGenerator();
            sql.books = books1;
            sql.settings = settin;
            
            XmlReader rusMark = new XmlReader();
            
            rusMark.settingsFields = settin;
            Queue<Record> books = rusMark.Init(path).Process();
            
             TableCreator table = new TableCreator();
             table.data = books;
             table.settingsPath = settings;
             SqlGenerator sql2 = new SqlGenerator();
             sql2.settings = rusMark.Init(settings).LoadSettings(); 
             sql2.Generate();
             //table.CheckSettings();
        }
    }
   
}
