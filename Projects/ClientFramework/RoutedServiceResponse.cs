namespace ClientFramework
{
    public sealed class RoutedServiceResponse<T>
    {
        public RoutedServiceResponse(T value, bool succeeded, string message)
        {
            Value = value;
            Succeeded = succeeded;
            Message = message;
        }

        public T Value { get; }
        public bool Succeeded { get; }
        public string Message { get; }        
    }
}
