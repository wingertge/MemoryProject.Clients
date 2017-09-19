using System.Windows;
using MvvmCross.Wpf.Views.Presenters.Attributes;

namespace MemoryClient.Cross.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [MvxWindowPresentation(Identifier = nameof(MainWindow), Modal = false)]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
