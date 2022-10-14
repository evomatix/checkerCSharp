using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumNUnitExtentReport.Framework.model;
using SeleniumNUnitExtentReport.Framework.model.locator;
using LogType = SeleniumNUnitExtentReport.Framework.model.reporting.LogType;
using ObjectLocator = SeleniumNUnitExtentReport.Framework.model.locator.ObjectLocator;

namespace SeleniumNUnitExtentReport.Framework
{
    public class ExecutionHandler
    {

        private IWebDriver driver;

        private ReportHelper reporter;


        public ExecutionHandler()
        {
            reporter = new ReportHelper();
        }

        public IWebDriver Driver { get => driver; }
        public ReportHelper Reporter { get => reporter; }

        public void IntTest(){
            ChromeDriverService service = ChromeDriverService.CreateDefaultService("webdriver.chrome.driver", @"D:\\Automation\\WebDrivers\\chromedriver.exe");
            driver = new ChromeDriver(service);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
            driver.Manage().Window.Maximize();
            reporter.CreateTest();
        }


        public void EndTest()
        {
            reporter.EndReporting(driver);
            driver.Quit();
        }



        public void Open(String url, int timeout)
        {
            this.driver.Navigate().GoToUrl(url);
            reporter.Log(LogType.PASS, "Open", url);
            this.pause(timeout);

        }



        public void pause(int timeout)
        {
            Thread.Sleep(timeout);
             
        }




        private IWebElement findElement(ObjectLocator element)
        {

            int retry = 10;
            int counter = retry;
            int retryInterval =  1000;
            bool elementNotPresent = true;

            do
            {

                IWebElement webElement = driver.FindElement(element.GetResolvedLocator());


                if (webElement != null)
                {
                    //scroll the element to view
                 

                    return webElement;
                }
                else
                {
                    try
                    {
                        Thread.Sleep(retryInterval);
                    }
                    catch (Exception e)
                    {
                        //e.printStackTrace();
                    }
                }

                if (counter > 0)
                {
                    counter--;
                }
                else
                {
                    elementNotPresent = false;
                }

            } while (elementNotPresent);

            throw new Exception("Element is not Found");



        }


        //commands


        //click

        public void Click(ObjectLocator locator)
        {
            this.findElement(locator).Click();
            reporter.Log(LogType.PASS, "Click", "Clicked on Element [" + locator.name + "]");
        }

        public void Click(ObjectLocator  locator, Dictionary<String, String> locatorParams)
        {
            locator.Parameters = locatorParams;
            this.Click(locator);
        }


        //type

        public void Type(ObjectLocator locator, String text)
        {
            IWebElement webElement = this.findElement(locator);
            try
            {
                webElement.Clear();

            }
            catch (Exception e)
            {
              //  e.printStackTrace();
            }
            webElement.SendKeys(text);
            reporter.Log(LogType.PASS, "Type", "Typed on Element [" + locator.name + "]");
        }

        public void Type(ObjectLocator locator, Dictionary<String, String> locatorParams, String text)
        {
            locator.Parameters = locatorParams;
            this.Type(locator, text);
        }

        //Select

        public void select(ObjectLocator locator, String value)
        {
            SelectElement select = new SelectElement(this.findElement(locator));
            select.SelectByValue(value);
            reporter.Log(LogType.PASS, "Select", "Selected Value [" + value + "] on Element [" + locator.name + "]");
        }

        public void select(ObjectLocator locator, Dictionary<String, String> locatorParams, String value)
        {
            locator.Parameters = locatorParams;
            this.select(locator, value);
        }


        //GetText

        public String getText(ObjectLocator locator)
        {

            String txt = this.findElement(locator).Text;
            reporter.Log(LogType.PASS, "GetText", "Read value [" + txt + "] from Element [" + locator.name + "]");

            return txt;
        }


        //CheckElement Present

        public bool checkElementPresent(ObjectLocator locator)
        {
            try
            {
                this.findElement(locator);
                reporter.Log(LogType.PASS, "checkElementPresent", "Element [" + locator.name + "] is present");
                return true;


            }
            catch (Exception e)
            {
                reporter.Log(LogType.PASS, "checkElementPresent", "Element [" + locator.name + "] is not present");
                return false;
            }
        }



        public bool checkElementPresent(ObjectLocator locator, Dictionary<String, String> locatorParams)
        {
            locator.Parameters = locatorParams;
            return this.checkElementPresent(locator);
        }
    }
}


