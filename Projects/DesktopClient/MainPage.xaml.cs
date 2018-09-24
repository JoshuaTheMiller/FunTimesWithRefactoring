using ClientPlatform.ServiceStack.RoutedClient;
using System.Collections.ObjectModel;
using System.Net.Http;
using Windows.UI.Xaml.Controls;
using DesktopClient.Platform;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
namespace DesktopClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<IServiceExample> ServiceExamples { get; }

        public MainPage()
        {
            IStringToVerbMapper stringToVerbMapper = new StringToVerbMapper();
            IAttributeReader attributeReader = new AttributeReader();
            IServiceStackRouteReader routeReader = new ServiceStackRouteReader(attributeReader, stringToVerbMapper);
            SerializerManager serializerManager = new SerializerManager();
            IWebClient webClient = new WebClient(new HttpClient(), serializerManager, serializerManager);
            var serviceClientFactory = new RoutedServiceClientFactory(routeReader, webClient);

            ServiceExamples = new ObservableCollection<IServiceExample>()
            {
                new ServiceStackExample(),
                new OnionStackExample(serviceClientFactory)
            };

            this.DataContext = this;
            this.InitializeComponent();
        }

        private void ServiceUrlBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach(var example in ServiceExamples)
            {
                example.SetHostUrl(ServiceUrlBox.Text);
            }
        }
    }
}
