namespace ClientFramework
{
    public interface IStringDeserializer
    {
        T Deserialize<T>(string stringToDeserialize);
    }
}
