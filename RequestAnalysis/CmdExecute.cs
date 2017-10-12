using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;

namespace RequestAnalysis
{
    public class CmdExecute
    {
        private const string CmdExe = @"C:\Windows\System32\cmd.exe";

        private const string cmdNettyStr = @"netstat -ano | find ""nettyPort""";

        private const string cmdPingStr = @"ping vrIP";
        public CmdExecute()
        {
            
        }

        public string ExecuteNetty(string ip, string nettyPort)
        {
            string executeResult = "";
            string str = cmdNettyStr.Replace("nettyPort", nettyPort);
            try
            {
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = "cmd.exe";
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.RedirectStandardInput = true;
                myProcess.StartInfo.RedirectStandardOutput = true;
                myProcess.StartInfo.RedirectStandardError = true;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();

                myProcess.StandardInput.WriteLine(str + "&exit");

                ShowMessageOperator.ShowMessage(myProcess.StandardOutput.ReadToEnd(), Color.Chartreuse);

                myProcess.WaitForExit();
                myProcess.Close();
            }
            catch (Exception ex)
            {
               ShowMessageOperator.ShowMessage(ex.Message, Color.Red); 
            }

            return executeResult;
        }

        public string ExcutePing(string ip)
        {
            string executeResult = "";
            string str = cmdPingStr.Replace("vrIP", ip);
            try
            {
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = "cmd.exe";
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.RedirectStandardInput = true;
                myProcess.StartInfo.RedirectStandardOutput = true;
                myProcess.StartInfo.RedirectStandardError = true;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();

                myProcess.StandardInput.WriteLine(str + "&exit");

                ShowMessageOperator.ShowMessage(myProcess.StandardOutput.ReadToEnd(), Color.Chartreuse);

                myProcess.WaitForExit();
                myProcess.Close();

            }
            catch (Exception ex)
            {
                ShowMessageOperator.ShowMessage(ex.Message, Color.Red);
            }

            return executeResult;
        }
    }
}