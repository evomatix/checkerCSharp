using System;
using OpenQA.Selenium;
using SeleniumNUnitExtentReport.Framework;
using SeleniumNUnitExtentReport.Framework.model;
using SeleniumNUnitExtentReport.Framework.model.locator;

namespace SeleniumNUnitExtentReport.Pages
{
    public class GoogleHomePOM : Page
    {
        public GoogleHomePOM(ExecutionHandler handler)
           
        {
            this.handler = handler;
            this.URL = "https://google.com";
        }


        private ExecutionHandler handler;

        public ObjectLocator txt_IFL = new ObjectLocator("txt_Login", "html/body/div[1]/div[3]/form/div[1]/div[1]/div[3]/center/input[2]", LocatorType.XPATH);

        public ObjectLocator btn_Search = new ObjectLocator("btn_IFL", "//input[@name='#{{name}}#']", LocatorType.XPATH);


        public void ClickOnFeelingLuckey()
        {
            handler.Open(this.URL, 1000);
            handler.Click(txt_IFL);

        }


    }



}

