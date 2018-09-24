using ClientFramework;
using Newtonsoft.Json;

namespace ClientPlatform
{
    public sealed class StringSerializer : IStringSerializer
    {
        public string Serialize<T>(T objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize);
        } 
    }
}
