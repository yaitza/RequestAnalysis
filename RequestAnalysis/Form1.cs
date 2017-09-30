using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace RequestAnalysis
{
    public partial class Form1 : Form
    {
        private string requestUrl;

        private int internalTime;

        public Form1()
        {
            InitializeComponent();
            ShowMessageOperator.ShowMethod += OutputShow;

            this.requestUrl = ConfigurationSettings.AppSettings["ReuqestUrl"];
            this.internalTime = int.Parse(ConfigurationSettings.AppSettings["IntervalTime"]);
            this.tbRequestUrl.Text = this.requestUrl;
            this.tbInternalTime.Text = this.internalTime.ToString();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            UpdateConfigInfo();

            Thread th = new Thread(RequestUrl);
            th.Start();
        }

        private void RequestUrl()
        {
            while (true)
            {
                RequestHandler rh = new RequestHandler(this.requestUrl);
                StringAnalysis sa = new StringAnalysis(rh.GetRequest());

                List<DeviceInfo> diList = sa.GetDeviceInfos();
                string showMessage = string.Format($"共计设备：{diList.Count},VR设备：{diList.Count(item => item.clientCode.Equals("VR"))},在线设备：{diList.Count(item => item.lanIp != null)}");
                foreach (DeviceInfo deviceInfo in diList)
                {
                    CSVFileOperator cfo = new CSVFileOperator(deviceInfo);
                    cfo.WriteDeviceInfoIntoCsvFile();
                }

                ShowMessageOperator.ShowMessage(showMessage, Color.White);

                Thread.Sleep(this.internalTime);
            }
        }


        private void UpdateConfigInfo()
        {
            if (!this.tbRequestUrl.Text.Trim().ToLower().Equals(this.requestUrl.ToLower()))
            {
                ConfigurationSettings.AppSettings["ReuqestUrl"] = this.tbRequestUrl.Text.Trim();
                this.requestUrl = this.tbInternalTime.Text.Trim();
            }

            if (int.Parse(this.tbInternalTime.Text.Trim()) != this.internalTime)
            {
                ConfigurationSettings.AppSettings["IntervalTime"] = this.internalTime.ToString();
                this.internalTime = int.Parse(this.tbInternalTime.Text.Trim());
            }
        }

        private delegate void DisplayMessage(string msg, Color color);

        private void OutputShow(string msg, Color color)
        {
            if (this.rtbMsgOutput.InvokeRequired)
            {
                DisplayMessage dm = new DisplayMessage(OutputShow);
                this.Invoke(dm, new object[] { msg, color });
            }
            else
            {
                this.rtbMsgOutput.SelectionColor = color;
                this.rtbMsgOutput.AppendText($"{msg} {Environment.NewLine}");
                this.rtbMsgOutput.Focus();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
