using System;
using System.IO;

namespace RequestAnalysis
{
    public class CsvFileOperator
    {
        private readonly DeviceInfo _deviceInfo;

        private readonly string _csvFileDirectory;

        public CsvFileOperator(DeviceInfo deviceInfo)
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
                                       $"clientName,clientCode,deviceStatus,nettyState,wifiPing";
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

        public void WirteNetWorkInfoCsvFile(string networkInfo)
        {
            using (StreamWriter sw = new StreamWriter(this._csvFileDirectory, true))
            {
                sw.Write($",{networkInfo}");
                sw.Close();
            }
        }
    }

    public class TxtFileOperator
    {
        private readonly string _inputStr;

        private readonly string _txtFileDirectory; 

        public TxtFileOperator(string inputStr)
        {
            this._inputStr = inputStr;
            string directory = System.IO.Directory.GetCurrentDirectory();
            this._txtFileDirectory = $"{directory}\\{"Total"}.csv";
        }

        public void WriteShowMsgIntoTxtFile()
        {
            if (!File.Exists(this._txtFileDirectory))
            {
                using (StreamWriter sw = new StreamWriter(this._txtFileDirectory, true))
                {
                    sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}  {this._inputStr}");
                    sw.Close();
                }
            }
            using (StreamWriter sw = new StreamWriter(this._txtFileDirectory, true))
            {
                sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}  {this._inputStr}");
                sw.Close();
                
            }
        }
    }
}