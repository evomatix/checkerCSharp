using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumNUnitExtentReport.Config;
using SeleniumNUnitExtentReport.PageMethods;
using SeleniumNUnitExtentReport.Pages;
using EvomatixTester.DataDriven.DataSource;
using EvomatixTester.DataSource;

namespace SeleniumNUnitExtentReport.TestCases
{
    [TestFixture]
    public class LoginTest : Executor
    {


        [Test]
        [Category("Search Google")]
        public void test_validLogin()
        {

            handler.Open("https://google/com", 1000);
            handler.Click(GoogleHome.txt_IFL);
        }

        [Test]
        [Category("Search Google POM")]
        public void test_invalidLogin()
        {

            ExcelManager excel = new ExcelManager();
            //change the path accordingly
            excel.openWorkBook("/Users/vdhhewapathirana/Storage/Projects/evomatix/checkerCSharp/EvomatixChecker/Data/loginData.xlsx", "Data");
            List<Dictionary<string, object>> data = excel.readExcelWithHeaders();

            foreach(Dictionary<string, object> record in data)
            {
                String username = (String) record["username"];
                Console.WriteLine(username);

                String password = (String)record["password"];
                Console.WriteLine(password);
            }



            GoogleHomePOM HomePage = new GoogleHomePOM(handler);
            HomePage.ClickOnFeelingLuckey();
        }

        [Test, TestCaseSource(typeof(DataSources), "SearchTermsFromCSV")]
        [Category("Search Google")]
        public void test_validLogin(String username, String password)
        {
            Console.Write(username);
            Console.Write(password);
            handler.Open("https://google/com", 1000);

        }
    }
 }
