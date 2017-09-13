using Autofac;
using MemoryClient.Desktop.ViewModels;
using Prism.Autofac;
using Prism.Autofac.Forms;
using MemoryClient.Desktop.Views;
using Xamarin.Forms;

namespace MemoryClient.Desktop
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage, MainPageViewModel>();
        }
    }
}
