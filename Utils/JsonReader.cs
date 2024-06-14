using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

namespace PlaywrightSpecFlowFramework.Utils
{
    public class JsonReader
    {
        public static T LoadJsonData<T>(string filePath)
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
    }
}