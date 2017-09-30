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
                    string strDevice = $"deviceID,sn,lanIp,battery,handleBattery,totalMemory,memory," +
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
}