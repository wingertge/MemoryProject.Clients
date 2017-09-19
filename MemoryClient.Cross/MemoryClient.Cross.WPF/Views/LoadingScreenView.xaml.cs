using System.Windows.Controls;
using MvvmCross.Wpf.Views;
using MvvmCross.Wpf.Views.Presenters.Attributes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MemoryClient.Cross.WPF.Views
{
    /// <inheritdoc />
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = true)]
    public sealed partial class LoadingScreenView : MvxWpfView
    {
        public LoadingScreenView()
        {
            this.InitializeComponent();
        }
    }
}
