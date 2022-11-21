using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
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

        private bool acceptNextAlert = true;

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


        public bool IsAlertPresent()
        {
            try
            {
            driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        public string CloseAlertAndGetItsText()
        {
            try
            {

                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }



        public void NavigateToURL(string url)
        {

            driver.Navigate().GoToUrl(url);

        }


        public string GetAttributeValue(ObjectLocator locator, Dictionary<String, String> locatorParams, string attributeName)
        {
            locator.Parameters = locatorParams;
            return GetAttributeValue(locator, attributeName);
        }


        public string GetAttributeValue(ObjectLocator locator, string attributeName)
        {
            string txtValue = this.findElement(locator).GetAttribute(attributeName);
            return txtValue;

        }

        public void SwitchToDilog()
        {
            driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
        }


        public void DragAndDrop(ObjectLocator sourceLocator, ObjectLocator destinationLocator)
        {
            Actions ActBuilder = new Actions(driver);
            ActBuilder.ClickAndHold(this.findElement(sourceLocator)).MoveToElement(this.findElement(destinationLocator))
            .Release(this.findElement(destinationLocator))
            .Build()
            .Perform();
            
        }

        public void ScrollToElementUsingJavaScript(ObjectLocator element)
        {

            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].scrollIntoView(true);", this.findElement(element));

        }



        public int GetNumberOfRowsInTable(ObjectLocator tableLocator)
        {
            int numberOfRowsInTable = this.findElement(tableLocator).FindElements(By.CssSelector("tbody>tr")).Count;

            return numberOfRowsInTable;

        }


        public List<string> GetValuesOfColumnByNameFromTable(ObjectLocator tableIdentifier, string columnName)
        {

            List<IWebElement> tableHeaders = this.findElement(tableIdentifier).FindElements(By.CssSelector("thead>tr>th")).ToList();
            int indexOfRequiredColumnName = tableHeaders.FindIndex(x => x.Text.Contains(columnName));


            List<IWebElement> tr_tableElements = this.findElement(tableIdentifier).FindElements(By.CssSelector("tbody>tr")).ToList();

            List<string> valuesFromSelectedTableColumn = new List<string>();

            foreach (IWebElement tr_element in tr_tableElements)
            {
                List<IWebElement> td_collection = tr_element.FindElements(By.CssSelector("td")).ToList();
                valuesFromSelectedTableColumn.Add(td_collection[indexOfRequiredColumnName].Text.Trim());

            }
            //loma

            return valuesFromSelectedTableColumn;

        }


        public int GetIndexOfTableRowContainingSpecificText(ObjectLocator tableIdentifier, string stringToBeSearched)
        {
            IWebElement table = this.findElement(tableIdentifier);

            List<IWebElement> tableRows = table.FindElements(By.CssSelector("tbody>tr")).ToList();
            int indexOfRequiredText = tableRows.FindIndex(x => x.Text.Contains(stringToBeSearched));
            return indexOfRequiredText;
            //loma

        }


        public  string GetValueFromSpecificCellFromTable(ObjectLocator tableIdentifier, int rowIndex, int columnIndex)
        {
            IWebElement table = this.findElement(tableIdentifier);
            return table.FindElements(By.CssSelector("tbody>tr"))[rowIndex].FindElements(By.CssSelector("td"))[columnIndex].Text;
            //loma

        }


        public void ClickOnCellTextFromTable(string txtTOBeClicked)
        {
            driver.FindElement(By.XPath("//tr[td[contains(text(),'" + txtTOBeClicked + "')]]")).Click();

            //DriverUtil.driver.FindElement(By.XPath("//tr[td[contains(text(),'W695')]]")).Click();
            //DriverUtil.driver.FindElement(By.CssSelector("td:contains('W695')")).Click();
        }



       


    }

   
}


