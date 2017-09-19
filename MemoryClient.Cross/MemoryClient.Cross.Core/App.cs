using Acr.UserDialogs;
using MemoryApi.HttpClient;
using MemoryClient.Cross.Core.Localization;
using MemoryClient.Cross.Core.ViewModels;
using Microsoft.Identity.Client;
using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MvvmCross.Plugins.ResxLocalization;

namespace MemoryClient.Cross.Core
{
    /// <inheritdoc />
    public class App : MvxApplication
    {
        /// <inheritdoc />
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.RegisterSingleton<IMvxTextProvider>(() => new MvxResxTextProvider(Locale.ResourceManager));
            Mvx.RegisterSingleton(() => UserDialogs.Instance);
            Mvx.RegisterSingleton(() => new ValidationUtils());
            Mvx.RegisterSingleton(() => new MicrosoftSignInHandler());

            const string apiUrl = "http://localhost:5000"; //TODO: Clean up

            Mvx.RegisterSingleton<IAuthClient>(() => new AuthClient(apiUrl));
            Mvx.RegisterSingleton<IUsersClient>(() => new UsersClient(apiUrl));

            RegisterAppStart<LoadingScreenViewModel>();
        }

        public static UIParent UIParent { get; set; }
    }
}
