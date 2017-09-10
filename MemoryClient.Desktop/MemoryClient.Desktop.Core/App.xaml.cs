using MemoryClient.Desktop.ViewModels;
using MemoryClient.Desktop.Views;
using MemoryClient.Mobile.ViewModels;
using Prism.Autofac;
using Prism.Autofac.Forms;
using Xamarin.Forms;
using Account = MemoryClient.Desktop.Views.Account;
using Dashboard = MemoryClient.Desktop.Views.Dashboard;
using LessonList = MemoryClient.Desktop.Views.LessonList;
using LessonPack = MemoryClient.Desktop.Views.LessonPack;
using NewLesson = MemoryClient.Desktop.Views.NewLesson;
using Register = MemoryClient.Desktop.Views.Register;
using Review = MemoryClient.Desktop.Views.Review;

namespace MemoryClient.Desktop
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/MainPage?title=Hello%20from%20Xamarin.Forms");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<LessonPack, LessonPackViewModel>();
            Container.RegisterTypeForNavigation<NewLesson, NewLessonViewModel>();
            Container.RegisterTypeForNavigation<Review, ReviewViewModel>();
            Container.RegisterTypeForNavigation<LessonList, LessonListViewModel>();
            Container.RegisterTypeForNavigation<Account, AccountViewModel>();
            Container.RegisterTypeForNavigation<Login, LoginViewModel>();
            Container.RegisterTypeForNavigation<Register, RegisterViewModel>();
            Container.RegisterTypeForNavigation<Dashboard, DashboardViewModel>();
        }
    }
}
