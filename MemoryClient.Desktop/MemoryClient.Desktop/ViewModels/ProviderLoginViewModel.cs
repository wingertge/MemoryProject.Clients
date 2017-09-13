using System.Reflection;
using Prism.Mvvm;

namespace MemoryClient.Desktop.ViewModels
{
	public class ProviderLoginViewModel : BindableBase
	{
	    private static readonly Assembly SvgAssembly = typeof(App).GetTypeInfo().Assembly;

        public ProviderLoginViewModel()
        {

        }
	}
}
