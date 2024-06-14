using System.IO;
using System.Text.Json;

namespace PlaywrightSpecFlowFramework.Utils
{
    public static class JsonReader
    {
        public static T? ReadJsonFile<T>(string filePath)
        {
            var jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}