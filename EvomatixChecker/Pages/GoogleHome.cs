using System;
using SeleniumNUnitExtentReport.Framework.model;
using SeleniumNUnitExtentReport.Framework.model.locator;

namespace SeleniumNUnitExtentReport.Pages
{
    public class GoogleHome : Page
    {
        public GoogleHome()
        {
            this.URL = "https://google.com";
        }


        public static ObjectLocator txt_IFL = new ObjectLocator("txt_Login", "html/body/div[1]/div[3]/form/div[1]/div[1]/div[3]/center/input[2]", LocatorType.XPATH);

        public static ObjectLocator btn_Search = new ObjectLocator("btn_IFL", "//input[@name='#{{name}}#']", LocatorType.XPATH);


    }



}

