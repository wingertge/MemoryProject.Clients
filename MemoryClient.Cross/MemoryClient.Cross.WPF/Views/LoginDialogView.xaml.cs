using MvvmCross.Wpf.Views.Presenters.Attributes;
using MvvmCross.Wpf.Views;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MemoryClient.Cross.WPF.Views
{
    [MvxWindowPresentation(Identifier = nameof(LoginDialogView), Modal = true)]
    public sealed partial class LoginDialogView
    {
        public LoginDialogView()
        {
            this.InitializeComponent();         
        }
    }
}
