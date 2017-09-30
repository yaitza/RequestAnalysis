using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;

namespace RequestAnalysis
{
    public class RequestHandler
    {
        private readonly string _url;

        public RequestHandler(string url)
        {
            _url = url;
        }

        public string GetRequest()
        {
            StringBuilder content = new StringBuilder();

            try
            {
                // 与指定URL创建HTTP请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this._url);

                // 获取对应HTTP请求的响应
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                // 获取响应流
                Stream responseStream = response.GetResponseStream();
                // 对接响应流(以"GBK"字符集)
                StreamReader sReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                // 开始读取数据
                Char[] sReaderBuffer = new Char[256];
                int count = sReader.Read(sReaderBuffer, 0, 256);
                while (count > 0)
                {
                    String tempStr = new String(sReaderBuffer, 0, count);
                    content.Append(tempStr);
                    count = sReader.Read(sReaderBuffer, 0, 256);
                }
                // 读取结束
                sReader.Close();
            }
            catch (Exception ex)
            {
                content = new StringBuilder(ex.Message);
            }

            return content.ToString();
        }
    }
}