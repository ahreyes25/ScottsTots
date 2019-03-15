using RemindMeAlready.Repositories;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RemindMeAlready
{
    public partial class App : Application
    {
        public static ReminderRepository ReminderRepo { get; private set; }
        public static ScheduleRepository ScheduleRepo { get; private set; }
        public static INavigation Navigation { get; private set; }
        //INotificationHelper _notificationHelper;
        //public static Page GetMainPage()
        //{
        //    return new NavigationPage(new MainPage());
        //}

        public App(string dbPath)
        {
            InitializeComponent();

            ConnectionHelper connectionHelper = new ConnectionHelper(dbPath);

            ReminderRepo = new ReminderRepository(connectionHelper.Connection);
            ScheduleRepo = new ScheduleRepository(connectionHelper.Connection);         
            var navPage = new NavigationPage(new MainPage());
            MainPage = navPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
