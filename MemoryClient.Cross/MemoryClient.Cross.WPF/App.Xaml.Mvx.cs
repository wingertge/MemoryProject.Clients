using System;
using System.Windows;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Wpf.Views.Presenters;
using MemoryClient.Cross.Core;
using Microsoft.Identity.Client;

namespace MemoryClient.Cross.WPF
{
    public partial class App : Application
    {
        private bool _setupComplete;

        private void DoSetup()
        {
            LoadMvxAssemblyResources();

            var presenter = new MvxWpfViewPresenter(MainWindow);

            Core.App.UIParent = new UIParent(MainWindow);

            var setup = new Setup(Dispatcher, presenter);
            setup.Initialize();

            RegisterPlatformTypes();

            var start = Mvx.Resolve<IMvxAppStart>();
            start.Start();

            _setupComplete = true;
        }

        private static void RegisterPlatformTypes()
        {
            Mvx.RegisterType<IApplicationSettings, ApplicationSettings>();
        }

        protected override void OnActivated(EventArgs e)
        {
            if (!_setupComplete)
            {
                DoSetup();
            }

            base.OnActivated(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Mvx.Resolve<IApplicationSettings>().CleanUp();
            base.OnExit(e);
        }

        private void LoadMvxAssemblyResources()
        {
            for (var i = 0; ; i++)
            {
                var key = "MvxAssemblyImport" + i;
                var data = TryFindResource(key);
                if (data == null)
                {
                    return;
                }
            }
        }
    }
}