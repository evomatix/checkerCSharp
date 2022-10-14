using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace SeleniumNUnitExtentReport.Framework.model.locator
{
    public class LocatorParam
    {
        public LocatorParam()
        {
        }

        private Dictionary<String, String> map = new Dictionary<string, string>();

        public LocatorParam add(String param, String value)
        {
            this.map.Add(param, value);
            return this;
        }

        public Dictionary<string, string> build()
        {
            Dictionary<String, String> temp = new Dictionary<string, string>(map);
            map.Clear();
            return temp;
        }
    }
}

