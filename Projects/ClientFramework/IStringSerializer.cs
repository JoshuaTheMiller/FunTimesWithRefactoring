namespace ClientFramework
{
    public interface IStringSerializer
    {
        string Serialize<T>(T objectToSerialize);
    }
}
