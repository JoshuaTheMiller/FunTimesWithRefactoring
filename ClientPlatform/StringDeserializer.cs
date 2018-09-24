using ClientFramework;
using Newtonsoft.Json;

namespace ClientPlatform
{
    public sealed class StringDeserializer : IStringDeserializer
    {
        public T Deserialize<T>(string stringToDeserialize)
        {
            return JsonConvert.DeserializeObject<T>(stringToDeserialize);
        }
    }
}
