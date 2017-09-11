using System.Reflection;
using System.Windows.Controls;
using DevExpress.Utils.Svg;

namespace MemoryClient.Desktop.WPF.Components
{
    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for GoogleSigninButton.xaml
    /// </summary>
    public partial class GoogleSigninButton : UserControl
    {
        private SvgBitmap _normal;
        private SvgBitmap _hover;
        private SvgBitmap _pressed;

        public GoogleSigninButton()
        {
            InitializeComponent();
            var assembly = Assembly.GetExecutingAssembly();
            _normal = SvgBitmap.FromStream(assembly.GetManifestResourceStream(@"Resources/Images/btn_google_light_normal_ios.svg"));
            _hover = SvgBitmap.FromStream(assembly.GetManifestResourceStream(@"Resources/Images/btn_google_light_focus_ios.svg"));
            _pressed = SvgBitmap.FromStream(assembly.GetManifestResourceStream(@"Resources/Images/btn_google_light_pressed_ios.svg"));

            Logo.Image = _normal.Render(null, 1);
        }
    }
}
