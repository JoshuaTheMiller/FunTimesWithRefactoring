namespace ClientFramework
{
    public interface IRoutedServiceClientFactory
    {
        IRoutedServiceClient Get(string baseUrl);
    }
}
