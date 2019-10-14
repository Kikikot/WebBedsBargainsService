using Externals.LoggSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity;
using WebBedsBargariansService.UnitTests._Emulation;

namespace WebBedsBargariansService.UnitTests.Loggers
{
    [TestClass]
    public class BSLoggerTest
    {
        [TestMethod]
        public void Log_LogOnlyMessage_JustNotGeneratesException()
        {
            var container = TestRegistrator.Init();
            var logger = container.Resolve<ILogger>();
            bool ok = true;

            try { logger.Log("Some Message"); }
            catch (Exception) { ok = false; }

            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void Log_LogMessageAndException_JustNotGeneratesException()
        {
            var container = TestRegistrator.Init();
            var logger = container.Resolve<ILogger>();
            bool ok = true;
            Exception e = new Exception("Some Exception");

            try { logger.Log("Some Message", e); }
            catch (Exception) { ok = false; }

            Assert.IsTrue(ok);
        }
    }
}
