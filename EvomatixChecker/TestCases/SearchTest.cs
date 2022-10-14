﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumNUnitExtentReport.Config;
using SeleniumNUnitExtentReport.PageMethods;
using SeleniumNUnitExtentReport.Pages;

namespace SeleniumNUnitExtentReport.TestCases
{
    [TestFixture]
    public class LoginTest : ReportsGenerationClass
    {
        

        [Test]
        [Category("Search Google")]
        public void test_validLogin()
        {

            handler.Open("https://google/com",1000);
            handler.Click(GoogleHome.txt_IFL);
        }

        [Test]
        [Category("Search Google POM")]
        public void test_invalidLogin()
        {
            GoogleHomePOM HomePage = new GoogleHomePOM(handler);
            HomePage.ClickOnFeelingLuckey();
        }
    }
}