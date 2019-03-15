using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;

namespace RemindMeAlready
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            addRemindersView.Clicked += async (s, e) => { await this.Navigation.PushModalAsync(new AddReminders()); };
            gotoSchedule.Clicked += async (s, e) => { await this.Navigation.PushModalAsync(new SchedulePage()); };
            notificationButton.Clicked += SendNotiication;
        }

        private void SendNotiication(object sender, EventArgs e)
        {
            var notifHelper = DependencyService.Get<INotificationHelper>();
            if (notifHelper != null)
            {
                notifHelper.SetUpAlarm(); 
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            
            var reminders = await App.ReminderRepo.GetAllRemindersAsync();
            //var schedules = await App.ScheduleRepo.GetAllSchedulesAsync();
            if (reminders.Count <= 0)
            {
                await App.ReminderRepo.AddNewReminderAsync("First Reminder");
                statusMessage.Text = App.ReminderRepo.StatusMessage;
                reminders = await App.ReminderRepo.GetAllRemindersAsync();
            }
            remindersList.ItemsSource = reminders;
        }

        async void OnReminderTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return;
            await DisplayAlert("Tapped", $"Tapped {e.Item} row was tapped", "OK");

        }

        
    }
}
