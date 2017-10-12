using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RequestAnalysis;

namespace RequsetAnalysisUnitTest
{
    [TestClass]
    public class CmdExecuteUnitTest
    {
        [TestMethod]
        public void TestExecuteNetty()
        {
            CmdExecute ce = new CmdExecute();

            ce.ExecuteNetty("192.168.0.148", "9999");
        }

        [TestMethod]
        public void TestExcutePing()
        {
            CmdExecute ce = new CmdExecute();
            System.Console.WriteLine(ce.ExcutePing("192.168.0.148"));
        }
    }
}
