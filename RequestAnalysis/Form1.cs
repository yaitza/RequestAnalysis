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
            int threadCount = 1;
            if (!string.IsNullOrEmpty(this.ThreadCounttB.Text))
            {
                threadCount = int.Parse(this.ThreadCounttB.Text);
            }

            for (int i = 0; i < threadCount; i++)
            {
                Thread th = new Thread(RequestUrl);
                th.Start();
            }

        }

        private void RequestUrl()
        {
            while (true)
            {
                RequestHandler rh = new RequestHandler(this.requestUrl);

                if (this.requestUrl.Contains("device"))
                {
                    StringAnalysis sa = new StringAnalysis(rh.GetRequest());

                    List<DeviceInfo> diList = sa.GetDeviceInfos();
                    string showMessage = string.Format($"共计设备：{diList.Count}," +
                                                       $"VR设备：{diList.Count(item => (item.clientCode != null && item.clientCode.Equals("VR")))}," +
                                                       $"在线设备：{diList.Count(item => item.lanIp != null)}");
                    foreach (DeviceInfo deviceInfo in diList)
                    {
                        //                    Thread th = new Thread(new ParameterizedThreadStart(CollectInfo));
                        //                    th.Start(deviceInfo);

                        CsvFileOperator cfo = new CsvFileOperator(deviceInfo);
                        cfo.WriteDeviceInfoIntoCsvFile();
                        //                    if (deviceInfo.lanIp != null)
                        //                    {
                        //                        CmdExecute ce = new CmdExecute();
                        //                        ShowMessageOperator.ShowMessage(ce.ExcutePing(deviceInfo.lanIp), Color.Aqua);
                        //                        ShowMessageOperator.ShowMessage(ce.ExecuteNetty(deviceInfo.lanIp, "9999"), Color.Aqua);
                        //                    }
                    }

                    ShowMessageOperator.ShowMessage(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + " " + showMessage, Color.White);
                    TxtFileOperator tfo = new TxtFileOperator(showMessage);
                    tfo.WriteShowMsgIntoTxtFile();
                }
                else
                {
                    ShowMessageOperator.ShowMethod(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), Color.Aqua);
                    ShowMessageOperator.ShowMessage(rh.GetRequest(), Color.White);
                }


                Thread.Sleep(this.internalTime);
            }
        }

        public void CollectInfo(object obj)
        {
            DeviceInfo deviceInfo = (DeviceInfo)obj;
            CsvFileOperator cfo = new CsvFileOperator(deviceInfo);
            cfo.WriteDeviceInfoIntoCsvFile();
            if (deviceInfo.lanIp != null)
            {
                CmdExecute ce = new CmdExecute();
                ShowMessageOperator.ShowMessage(ce.ExcutePing(deviceInfo.lanIp), Color.Aqua);
                ShowMessageOperator.ShowMessage(ce.ExecuteNetty(deviceInfo.lanIp, "9999"), Color.Aqua);
            }
        }


        private void UpdateConfigInfo()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (!this.tbRequestUrl.Text.Trim().ToLower().Equals(this.requestUrl.ToLower()))
            {
                config.AppSettings.Settings["ReuqestUrl"].Value = this.tbRequestUrl.Text.Trim();
                //                ConfigurationSettings.AppSettings["ReuqestUrl"] = this.tbRequestUrl.Text.Trim();
                this.requestUrl = this.tbRequestUrl.Text.Trim();
            }

            if (int.Parse(this.tbInternalTime.Text.Trim()) != this.internalTime)
            {
                config.AppSettings.Settings["IntervalTime"].Value = this.internalTime.ToString();
                //                ConfigurationSettings.AppSettings["IntervalTime"] = this.internalTime.ToString();
                this.internalTime = int.Parse(this.tbInternalTime.Text.Trim());
            }
            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
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
