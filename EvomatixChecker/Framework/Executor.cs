using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using SeleniumNUnitExtentReport.Framework;
using System.Runtime.InteropServices;

namespace SeleniumNUnitExtentReport.Config
{
    [SetUpFixture]
    public abstract class Executor
    {

        public ExecutionHandler handler;
        

        [OneTimeSetUp]
        protected void Setup()
        {
          
            handler = new ExecutionHandler();

        }

        [OneTimeTearDown]
        protected void TearDown()
        {
            handler.Reporter.FlushReport();
        }   

        [SetUp]
        public void BeforeTest()
        {
            //initDrvier
            handler.IntTest();
          
        }

        [TearDown]
        public void AfterTest()
        {
            handler.EndTest();
           
        }
        public IWebDriver GetDriver()
        {
            return handler.Driver;
        }
       
    }
}
