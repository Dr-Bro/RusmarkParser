using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WpfApplication2
{
    class SqlGenerator
    {
        const string INSERT_START = "INSERT INTO `catalog` VALUES (";
        const string INSERT_END = ");";

        List<string> rows = new List<string>();
        public Queue<Record> books { get; set; }
        public Dictionary<string, string[]> settings { get; set; }

        private SqlGenerator Prepare()
        {
            this.settings.ToString();
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
