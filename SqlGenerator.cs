using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WpfApplication2
{

    struct SqlSetting
    {
        public string name;
        public string[] props;
        public string[] db_names;
        public SqlSetting(string name, string[] props, string[] db_names)
        {
            this.name = name;
            this.props = props;
            this.db_names = db_names;
        }
    }


    class SqlGenerator
    {
        const string INSERT_START = "INSERT INTO `catalog` VALUES (";
        const string INSERT_END = ");";

        List<string> rows = new List<string>();
        public List<RecordFinal> books { get; set; }
        public List<SqlSetting> settings { get; set; }

        private SqlGenerator Prepare()
        {
           
           foreach(RecordFinal book in books){
               string[] foos = new string[book.fields.Count];
               book.fields.Values.CopyTo(foos, 0);
               WriteResult(GetInsertSql(foos));
           }
            return this;
        }

        public void Generate()
        {
            this.Prepare();
        }

        private string GetInsertSql(string[] mass)
        {
            return INSERT_START + String.Join(",", mass) + INSERT_END;
        }

        private SqlGenerator AddRows(string row)
        {
            this.rows.Add(row);
            return this;
        }

        private void WriteResult(string str1)
        {
            string writePath = @"catalog.sql";

            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine("{0}\n", str1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
