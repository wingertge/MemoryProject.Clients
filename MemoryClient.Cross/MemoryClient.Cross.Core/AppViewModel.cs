using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;

namespace MemoryClient.Cross.Core
{
    public abstract class AppViewModel : MvxViewModel
    {
        public IMvxLanguageBinder TextSource => new MvxLanguageBinder("", GetType().Name.Substring(0, GetType().Name.Length - 9));
    }

    public abstract class AppViewModel<T> : MvxViewModel<T>
    {
        public IMvxLanguageBinder TextSource => new MvxLanguageBinder("", GetType().Name.Substring(0, GetType().Name.Length - 9));
    }
}