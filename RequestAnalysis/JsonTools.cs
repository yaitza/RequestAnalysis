using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace RequestAnalysis
{
    public class JsonTools
    {
        public static T JsonToObject<T>(string jsonStr)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonStr));
            T obj = (T) serializer.ReadObject(ms);
            ms.Dispose();
            return obj;
        }


    }
}