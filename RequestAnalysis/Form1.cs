using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

            this.requestUrl = ConfigurationSettings.AppSettings["ReuqestUrl"];
            this.internalTime = int.Parse(ConfigurationSettings.AppSettings["IntervalTime"]);
            this.tbRequestUrl.Text = this.requestUrl;
            this.tbInternalTime.Text = this.internalTime.ToString();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            UpdateConfigInfo();
           
        }

        

        private void UpdateConfigInfo()
        {
            if (!this.tbInternalTime.Text.Trim().ToLower().Equals(this.requestUrl.ToLower()))
            {
                ConfigurationSettings.AppSettings["ReuqestUrl"] = this.tbInternalTime.Text.Trim();
                this.requestUrl = this.tbInternalTime.Text.Trim();
            }

            if (int.Parse(this.tbInternalTime.Text.Trim()) != this.internalTime)
            {
                ConfigurationSettings.AppSettings["IntervalTime"] = this.internalTime.ToString();
                this.internalTime = int.Parse(this.tbInternalTime.Text.Trim());
            }
        }

        private delegate void DisplayMessage(string msg);

        private void OutputShow(string msg)
        {
            if (this.rtbMsgOutput.InvokeRequired)
            {
                DisplayMessage dm = new DisplayMessage(OutputShow);
                this.Invoke(dm, new object[] { msg });
            }
            else
            {
                this.rtbMsgOutput.Text = $"{msg} {Environment.NewLine}";
                this.rtbMsgOutput.Focus();
            }
        }
    }
}
