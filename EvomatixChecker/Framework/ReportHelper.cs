using System;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using RazorEngine.Compilation.ImpromptuInterface.Optimization;
using SeleniumNUnitExtentReport.Framework.model.reporting;
using LogType = SeleniumNUnitExtentReport.Framework.model.reporting.LogType;

namespace SeleniumNUnitExtentReport.Framework
{
    public class ReportHelper
    {

        private ExtentReports extent;
        private ExtentTest test;
        

        public ReportHelper()
        {
            this.InitReporting();
        
        }


        public void CreateTest()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        public void FlushReport()
        {
            extent.Flush();
        }

        public void InitReporting()
        {
            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(projectPath.ToString() + "Reports");
            var reportPath = projectPath + "Reports\\ExecutionReport"+ DateTime.Now.ToString("h_mm_ss") + ".html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            //can be updated
            extent.AddSystemInfo("Host Name", "LocalHost");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("UserName", "TestUser");
        }


        public void EndReporting(IWebDriver driver)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    DateTime time = DateTime.Now;
                    String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
                    String screenShotPath = Capture(driver, fileName);
                    test.Log(Status.Fail, "Fail");
                    test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath("Screenshots\\" + fileName));
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
            extent.Flush();
        }


        public void Log(LogType logType, String stepName, String details)
        {
            try
            {

                switch (logType)
                {

                    case LogType.FAIL:
                        test.Log(Status.Fail,  stepName +" - "+details);
                        break;
                    case LogType.PASS:
                        test.Log(Status.Pass, stepName + " - " + details);
                        break;

                   

                }
            }
            catch (Exception e)
            {
                //todo:add message
                           }
        }


        public static string Capture(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            var pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            var reportPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(reportPath + "Reports\\" + "Screenshots");
            var finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "Reports\\Screenshots\\" + screenShotName;
            var localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
            return reportPath;
        }

    }


}

