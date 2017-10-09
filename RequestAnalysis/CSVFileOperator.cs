using System;
using System.IO;

namespace RequestAnalysis
{
    public class CSVFileOperator
    {
        private readonly DeviceInfo _deviceInfo;

        private readonly string _csvFileDirectory;

        public CSVFileOperator(DeviceInfo deviceInfo)
        {
            this._deviceInfo = deviceInfo;
            string directory = System.IO.Directory.GetCurrentDirectory();
            this._csvFileDirectory = $"{directory}\\{this._deviceInfo.sn}.csv";
        }

        public void WriteDeviceInfoIntoCsvFile()
        {
            if (!File.Exists(this._csvFileDirectory))
            {
                using (StreamWriter sw = new StreamWriter(this._csvFileDirectory, true))
                {
                    string strDevice = $"time,deviceID,sn,lanIp,battery,handleBattery,totalMemory,memory," +
                                       $"packVersion,packVersionOfServer,romVersion,alias,charging,clientID," +
                                       $"clientName,clientCode,deviceStatus";
                    sw.WriteLine(strDevice);
                    sw.Close();
                }
            }

            using (StreamWriter sw = new StreamWriter(this._csvFileDirectory, true))
            {
                sw.WriteLine(this._deviceInfo.ToString());
                sw.Close();
            }
        }
    }

    public class TXTFileOperator
    {
        private string _inputStr;

        private string _txtFileDirectory; 

        public TXTFileOperator(string inputStr)
        {
            this._inputStr = inputStr;
            string directory = System.IO.Directory.GetCurrentDirectory();
            this._txtFileDirectory = $"{directory}\\{"Total"}.txt";
        }

        public void WriteShowMsgIntoTxtFile()
        {
            using (StreamWriter sw = new StreamWriter(this._txtFileDirectory, true))
            {
                sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff")}  {this._inputStr}");
                sw.Close();
                
            }
        }
    }
}