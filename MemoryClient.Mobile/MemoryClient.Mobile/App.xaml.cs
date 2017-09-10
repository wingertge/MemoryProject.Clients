using MemoryClient.Mobile.ViewModels;
using Autofac;
using Prism.Autofac;
using Prism.Autofac.Forms;
using MemoryClient.Mobile.Views;
using Xamarin.Forms;

namespace MemoryClient.Mobile
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
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<LessonPack, LessonPackViewModel>();
            Container.RegisterTypeForNavigation<NewLesson, NewLessonViewModel>();
            Container.RegisterTypeForNavigation<Review, ReviewViewModel>();
            Container.RegisterTypeForNavigation<LessonList, LessonListViewModel>();
            Container.RegisterTypeForNavigation<Account, AccountViewModel>();
            Container.RegisterTypeForNavigation<Login, LoginViewModel>();
            Container.RegisterTypeForNavigation<Register, RegisterViewModel>();
            Container.RegisterTypeForNavigation<Dashboard, DashboardViewModel>();
            Container.RegisterTypeForNavigation<Register, RegisterViewModel>();
            Container.RegisterTypeForNavigation<Login, LoginViewModel>();
            Container.RegisterTypeForNavigation<Dashboard, DashboardViewModel>();
            Container.RegisterTypeForNavigation<Review, ReviewViewModel>();
            Container.RegisterTypeForNavigation<LessonList, LessonListViewModel>();
            Container.RegisterTypeForNavigation<LessonSet, LessonSetViewModel>();
            Container.RegisterTypeForNavigation<Lesson, LessonViewModel>();
            Container.RegisterTypeForNavigation<Account, AccountViewModel>();
            Container.RegisterTypeForNavigation<CreateEditLesson, CreateEditLessonViewModel>();
        }
    }
}
