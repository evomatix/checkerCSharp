using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LumenWorks.Framework.IO.Csv;
using NUnit.Framework;

namespace EvomatixTester.DataDriven.DataSource
{
    public class DataSources
    {
        private IEnumerable<String[]> userData()
        {
            using (var csv = new CsvReader(new StreamReader(@"DataDriven\Data\Data.csv"), true))
            {
                while (csv.ReadNextRecord())
                {
                    string username = csv[0].ToString();
                    string password = csv[1].ToString();
                   
                    yield return new[] { username,password };
                }

            }
        }
    }
}

