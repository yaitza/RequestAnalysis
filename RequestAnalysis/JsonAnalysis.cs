using System.Collections.Generic;

namespace RequestAnalysis
{
    public class JsonAnalysis
    {
        private string _jsonStr;

        public JsonAnalysis(string jsonStr)
        {
            _jsonStr = jsonStr;
        }


        public void AnalysisJson()
        {
            JObject jo = new JObjec t(this._jsonStr);
            var test = JsonTools.JsonToObject<deviceInfo>(this._jsonStr);
        }
    }

    public class deviceInfo
    {
        public string deviceId;

        public string sn;

        public string lanIp;

        public string battery;

        public string handleBattery;

        public string totalMemory;

        public string memory;

        public string packVersion;

        public string packVersionOfServer;

        public string romVersion;

        public string clientId;

        public string clientName;

        public string clientCode;

        public string deviceStatus;
    }
}