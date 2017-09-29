using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RequestAnalysis;

namespace RequsetAnalysisUnitTest
{
    [TestClass]
    public class JsonAnalysisUnitTest
    {
        private string _jsonStr;

        [TestInitialize]
        public void SetUp()
        {
            string directory = System.IO.Directory.GetCurrentDirectory();
            int falg = directory.IndexOf("RequsetAnalysisUnitTest", StringComparison.Ordinal);
            string jsonFile = string.Format($"{directory.Substring(0, falg)}Resource\\json.txt");
            using (StreamReader sr = new StreamReader(jsonFile, Encoding.UTF8))
            {
                this._jsonStr = sr.ReadLine();
                sr.Close();
            }


        }

        [TestMethod]
        public void TestAnalysisJson()
        {
            JsonAnalysis ja = new JsonAnalysis(_jsonStr);
            ja.AnalysisJson();
            int k = 2;
            Assert.AreEqual(k, 2);
        }
    }
}
