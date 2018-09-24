using System;
using ClientFramework;

namespace DesktopClient.Platform
{
    public sealed class SerializerManager : IStringSerializer, IStringDeserializer 
    {
        public string Serialize<T>(T objectToSerialize)
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(string stringToDeserialize)
        {
            throw new NotImplementedException();
        }
    }
}
