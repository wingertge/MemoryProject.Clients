using System.Reflection;
using MemoryClient.Desktop.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MemoryClient.Desktop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProviderLoginView : ContentView
    {
        public ProviderLoginView()
        {
            InitializeComponent();
            BindingContext = new ProviderLoginViewModel();
        }
    }
}