using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RequestAnalysis;

namespace RequsetAnalysisUnitTest
{
    [TestClass]
    public class StringAnalysisUnitTest
    {
        private string _strStr;

        [TestInitialize]
        public void SetUp()
        {
            string directory = System.IO.Directory.GetCurrentDirectory();
            int falg = directory.IndexOf("RequsetAnalysisUnitTest", StringComparison.Ordinal);
            string jsonFile = string.Format($"{directory.Substring(0, falg)}Resource\\json.txt");
            using (StreamReader sr = new StreamReader(jsonFile, Encoding.UTF8))
            {
                this._strStr = sr.ReadLine();
                sr.Close();
            }

        }

        [TestMethod]
        public void TestGetDeviceInfos()
        {
            StringAnalysis sa = new StringAnalysis(this._strStr);
            sa.GetDeviceInfos();
        }
    }
}
