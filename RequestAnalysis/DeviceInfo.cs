namespace RequestAnalysis
{
    public class DeviceInfo
    {
        public string deviceID;

        public string sn;

        public string lanIp;

        public string battery;

        public string handleBattery;

        public string totalMemory;

        public string memory;

        public string packVersion;

        public string packVersionOfServer;

        public string romVersion;

        public string alias;

        public string charging;

        public string clientID;

        public string clientName;

        public string clientCode;

        public string deviceStatus;

        public override string ToString()
        {
            return $"{this.deviceID},{this.sn},{this.lanIp},{this.battery},{this.handleBattery},{this.totalMemory},{this.memory}," +
                   $"{this.packVersion},{this.packVersionOfServer},{this.romVersion},{this.alias},{this.charging},{this.clientID}," +
                   $"{this.clientName},{this.clientCode},{this.deviceStatus}";
        }
    }
}