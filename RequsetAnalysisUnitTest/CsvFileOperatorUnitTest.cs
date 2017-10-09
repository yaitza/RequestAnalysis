using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RequestAnalysis;

namespace RequsetAnalysisUnitTest
{
    [TestClass]
    public class CsvFileOperatorUnitTest
    {
        private StringAnalysis sa;
        [TestInitialize]
        public void SetUp()
        {
            string strDeviceInfo;
            string directory = System.IO.Directory.GetCurrentDirectory();
            int falg = directory.IndexOf("RequsetAnalysisUnitTest", StringComparison.Ordinal);
            string jsonFile = string.Format($"{directory.Substring(0, falg)}Resource\\json.txt");
            using (StreamReader sr = new StreamReader(jsonFile, Encoding.UTF8))
            {
                strDeviceInfo = sr.ReadLine();
                sr.Close();
            }

            sa = new StringAnalysis(strDeviceInfo);
        }

        [TestMethod]
        public void TestWriteDeviceInfoIntoCsvFile()
        {

            string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");
            foreach (var item in sa.GetDeviceInfos())
            {
                CSVFileOperator cfo = new CSVFileOperator(item);
                cfo.WriteDeviceInfoIntoCsvFile();
            }

        }
    }
}
