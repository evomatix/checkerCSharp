using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.IO;
using System.Linq;
using SeleniumNUnitExtentReport.Framework.model.locator;
using System.Collections;

namespace SeleniumNUnitExtentReport.Framework.model.locator
{
    public class ObjectLocator
    {
        public ObjectLocator()
        {
        }


        public String name;

        public String locator;

        private String resolvedLocator;

        private Dictionary<String, String> parameters;

        public LocatorType locatorType;

        public Dictionary<string, string> Parameters { get => parameters; set => parameters = value; }

        public ObjectLocator(String name, String locator, LocatorType locatorType)
        {

            this.name = name;
            this.locator = locator;
            this.locatorType = locatorType;

        }


        public By GetResolvedLocator()
        {

            if (Parameters!=null)
            {
                resolvedLocator = ""+this.locator+"";

                foreach (KeyValuePair<string, string> parameter in Parameters)
                {
                    String param = "#{{" + parameter.Key + "}}#";

                    if (resolvedLocator.Contains(param))
                    {
                        resolvedLocator = resolvedLocator.Replace(param, parameter.Value);
                    }
                    else
                    {
                        throw new Exception("Parameter [" + parameter.Key + "] is not found in the locator [" + locator + "] of element [" + name + "]");
                    }
                }

                return this.GetBy(resolvedLocator);
            }
            else
            {
                return this.GetBy(locator);
            }
        }


        private By GetBy(String resolvedLocator)
        {
            switch (this.locatorType)
            {

                case LocatorType.XPATH:
                    return By.XPath(resolvedLocator);
                case LocatorType.CSS:
                    return By.CssSelector(resolvedLocator);
                default:
                    throw new Exception("Unsupported Locator Type");
            }
        }


        public void ClearParameters()
        {
            this.Parameters = null;
        }
    }
}

