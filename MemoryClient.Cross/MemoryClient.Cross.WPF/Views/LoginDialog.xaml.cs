using System.Windows;
using MemoryClient.Cross.Core.ViewModels;
using MvvmCross.Wpf.Views.Presenters.Attributes;
using MvvmCross.BindingEx.Wpf.WindowsBinding;
using MvvmCross.Wpf.Views;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MemoryClient.Cross.WPF.Views
{
    [MvxWindowPresentation(Identifier = nameof(LoginDialog), Modal = true)]
    public sealed partial class LoginDialog : MvxWpfView
    {
        public LoginDialog()
        {
            CreateBindings();

            this.InitializeComponent();
        }

        private void CreateBindings()
        {
            CreateBindingSet<LoginDialog, LoginDialogViewModel>();
        }
    }
}
