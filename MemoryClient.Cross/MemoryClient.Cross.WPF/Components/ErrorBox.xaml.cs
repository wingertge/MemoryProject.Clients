using System.Windows;
using System.Windows.Controls;

namespace MemoryClient.Cross.WPF.Components
{
    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for ErrorBox.xaml
    /// </summary>
    public partial class ErrorBox : UserControl
    {
        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        // Using a DependencyProperty as the backing store for Message.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(ErrorBox), new PropertyMetadata(""));

        public ErrorBox()
        {
            InitializeComponent();
        }
    }
}
