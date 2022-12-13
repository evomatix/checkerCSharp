using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EvomatixTester.DataSource;
using LumenWorks.Framework.IO.Csv;
using NUnit.Framework;

namespace EvomatixTester.DataDriven.DataSource
{
    public class ExcelDataSources
    {
        private IEnumerable<String[]> userData()
        {
            ExcelManager excel = new ExcelManager();
            //change the path accordingly
            excel.openWorkBook("/Users/vdhhewapathirana/Storage/Projects/evomatix/checkerCSharp/EvomatixChecker/Data/loginData.xlsx", "Data");
            List<Dictionary<string, object>> data = excel.readExcelWithHeaders();

            foreach (Dictionary<string, object> record in data)
            {
                String username = (String)record["username"];
                String password = (String)record["password"];

                yield return new[] { username, password };
            }
        }
    }
}

