using Windows.ApplicationModel.Resources;
using Mntone.SvgForXaml;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;

namespace MemoryClient.Desktop.UWP.ViewModels
{
    public class LoginDialogViewModel : MvxViewModel
    {
        private readonly IMvxResourceLoader _resources;
        public SvgDocument GoogleLogo { get; set; }

        public LoginDialogViewModel(IMvxResourceLoader resources)
        {
            _resources = resources;
        }

        public void Init()
        {
            var googleLogoFile = _resources.GetTextResource("Assets/google_logo.svg");
            GoogleLogo = SvgDocument.Parse(googleLogoFile);
        }
    }
}