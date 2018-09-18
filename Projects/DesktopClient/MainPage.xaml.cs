using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

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
            ServiceExamples = new ObservableCollection<IServiceExample>()
            {
                new ServiceStackExample(),
                new OnionStackExample()
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
