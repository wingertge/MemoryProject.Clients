using DevExpress.Mvvm.POCO;
using MemoryCore.JsonModels;

namespace MemoryClient.Desktop.WPF.ViewModels
{
    public class WpfLoginModel : LoginModel
    {
        public static WpfLoginModel Create() => ViewModelSource.Create<WpfLoginModel>();
    }
}