using MemoryClient.Desktop.Views;
using Prism.Autofac;
using Xamarin.Forms;

namespace MemoryClient.Desktop
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
            MainPage = new Dashboard();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync("Dashboard");
        }

        protected override void RegisterTypes()
        {
            Builder.RegisterTypeForNavigation<NavigationPage>();
        }
    }
}
