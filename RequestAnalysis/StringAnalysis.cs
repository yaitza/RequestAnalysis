using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RequestAnalysis
{
    public class StringAnalysis
    {
        private string _analysisStr;

        public StringAnalysis(string analysisStr)
        {
            this._analysisStr = analysisStr;
        }

        public List<DeviceInfo> GetDeviceInfos()
        {
            List<DeviceInfo> devices = new List<DeviceInfo>();
            if (!this._analysisStr.Contains("listItems"))
            {
                return devices;
            }
            List<string> deviceList = this.GetDeviceStr(this._analysisStr);

            foreach (string device in deviceList)
            {
                devices.Add(this.StrToDeviceInfo(device));
            }

            return devices;
        }

        private List<string> GetDeviceStr(string devicesStr)
        {
            List<string> deviceList = new List<string>();
            int startIndex = devicesStr.IndexOf("[", StringComparison.Ordinal);
            int endIndex = devicesStr.IndexOf("]", StringComparison.Ordinal);
            string devices = devicesStr.Substring(startIndex, endIndex - startIndex + 1);
            string[] deviceArrays = Regex.Split(devices, "},{", RegexOptions.IgnoreCase);

            foreach (string device in deviceArrays)
            {
                if (device.Contains("[{"))
                {
                    deviceList.Add(device.Replace("[{", ""));
                }
                else if (device.Contains("}]"))
                {
                    deviceList.Add(device.Replace("}]", ""));
                }
                else
                {
                    deviceList.Add(device);
                }
            }
            return deviceList;
        }

        private DeviceInfo StrToDeviceInfo(string strDevice)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            strDevice = strDevice.Replace("\"refClient\":", "");
            strDevice = strDevice.Replace("\"refContext\":", "");
            strDevice = strDevice.Replace("{", "");
            strDevice = strDevice.Replace("}", "");

            string[] propertyArray = strDevice.Split(',');
            foreach (string property in propertyArray)
            {
                if (property.Equals("") || property.Equals("null"))
                {
                    continue;
                }
                dic.Add(property.Split(':')[0].Replace("\"",""), property.Split(':')[1].Replace("\"", ""));
            }

            DeviceInfo di = new DeviceInfo();
            di.deviceID = GetKeyValue(dic, "deviceID");
            di.sn = GetKeyValue(dic, "sn");
            di.lanIp = GetKeyValue(dic, "lanIp");
            di.battery = GetKeyValue(dic, "battery");
            di.handleBattery = GetKeyValue(dic, "handleBattery");
            di.totalMemory = GetKeyValue(dic, "totalMemory");
            di.memory = GetKeyValue(dic, "memory");
            di.packVersion = GetKeyValue(dic, "packVersion");
            di.packVersionOfServer = GetKeyValue(dic, "packVersionOfServer");
            di.romVersion = GetKeyValue(dic, "romVersion");
            di.alias = GetKeyValue(dic, "alias");
            di.charging = GetKeyValue(dic, "charging");
            di.clientID = GetKeyValue(dic, "clientID");
            di.clientName = GetKeyValue(dic, "clientName");
            di.clientCode = GetKeyValue(dic, "clientCode");
            di.deviceStatus = GetKeyValue(dic, "deviceStatus");

            return di;
        }

        private string GetKeyValue(Dictionary<string, string> dic, string key)
        {
            if (dic.ContainsKey(key))
            {
                if (dic[key].Equals("null"))
                {
                    return null;
                }
                return dic[key];
            }
            else
            {
                return null;
            }
        }
    }
}