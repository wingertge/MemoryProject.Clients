using MemoryClient.Cross.Core.Localization;
using MemoryClient.Cross.Core.ViewModels;
using Microsoft.Identity.Client;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MvvmCross.Plugins.ResxLocalization;

namespace MemoryClient.Cross.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.RegisterSingleton(new MvxResxTextProvider(Locale.ResourceManager));

            RegisterAppStart<LoadingScreenViewModel>();
        }
    }
}
