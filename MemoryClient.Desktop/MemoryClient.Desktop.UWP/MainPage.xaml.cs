using Autofac;
using Prism.Autofac;

namespace MemoryClient.Desktop.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoadApplication(new Desktop.App(new UwpInitializer()));
        }
    }

    public class UwpInitializer : IPlatformInitializer
    {
        public void RegisterTypes(ContainerBuilder container)
        {

        }
    }

}
