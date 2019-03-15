using RemindMeAlready.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RemindMeAlready
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SchedulePage : ContentPage
	{
        private List<Schedule> scheduleList;
        private Dictionary<string, int> FormStatus = new Dictionary<string, int> { { "No", 0 }, { "Yes", 1 }, { "Yes and stay", 2 }, { "Yes and Leave", 3 } }; 
        private TapGestureRecognizer formChanged;
        private int IsFormDirty = 0;
        public SchedulePage ()
		{
			InitializeComponent ();
            formChanged = new TapGestureRecognizer();
            formChanged.Tapped += (s, e) =>
            {
                IsFormDirty = 1;
            };
            GetScheduleData();
            //CreateGrid();
        }


        private async void GetScheduleData()
        {
            scheduleList = await App.ScheduleRepo.GetAllSchedulesAsync();
            if (scheduleList.Count <= 0)
            {
                var initialSchedules = new InitialSchedule();
                foreach (var schedule in initialSchedules.InitialSchedules)
                {
                    await App.ScheduleRepo.AddNewScheduleAsync(schedule);
                }
                scheduleList = await App.ScheduleRepo.GetAllSchedulesAsync();
            }



            ////scheduleGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(.5, GridUnitType.Star) });
            ////scheduleGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            ////scheduleGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            ////scheduleGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            ////scheduleGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            //BoxView backGroundBoxView = new BoxView { Color = Color.CadetBlue, CornerRadius = 1 };
            ////Button cancelButton = new Button { Text = "Cancel", BackgroundColor = Color.BurlyWood, TextColor = Color.Gray };
            ////cancelButton.Clicked += CancelButton_ClickedAsync;
            ////Button saveButton = new Button { Text = "Save", BackgroundColor = Color.BurlyWood, TextColor = Color.Green };
            ////saveButton.Clicked += SaveButton_ClickedAsync;
            ////Label switchHeader = new Label { Text = "Remind Me?", VerticalTextAlignment = TextAlignment.Center };
            ////Label dayHeader = new Label { Text = "Days", VerticalTextAlignment = TextAlignment.Center };
            ////Label TimeHeader = new Label { Text = "Time", VerticalTextAlignment = TextAlignment.Center };

            //scheduleGrid.Children.Add(backGroundBoxView, 0, 3,0, 1);
            ////scheduleGrid.Children.Add(cancelButton, 0, 0);
            ////scheduleGrid.Children.Add(saveButton, 2, 0);
            ////scheduleGrid.Children.Add(switchHeader, 0, 1);
            ////scheduleGrid.Children.Add(dayHeader, 1, 1);
            ////scheduleGrid.Children.Add(TimeHeader, 2, 1);

            var mondaySchedule = scheduleList.FirstOrDefault(x => x.Day == "Monday");
            var tuesdaySchedule = scheduleList.FirstOrDefault(x => x.Day == "Tuesday");
            var wednesdaySchedule = scheduleList.FirstOrDefault(x => x.Day == "Wednesday");
            var thursdaySchedule = scheduleList.FirstOrDefault(x => x.Day == "Thursday");
            var fridaySchedule = scheduleList.FirstOrDefault(x => x.Day == "Friday");
            var saturdaySchedule = scheduleList.FirstOrDefault(x => x.Day == "Saturday");
            var sundaySchedule = scheduleList.FirstOrDefault(x => x.Day == "Sunday");

            mondaySwitch.IsToggled = mondaySchedule.IsActive;
            mondaySchedule.TimeSpan = TimeSpan.FromMinutes(mondaySchedule.Time);
            mondayTimePicker.Time = mondaySchedule.TimeSpan;

            tuesdaySwitch.IsToggled = tuesdaySchedule.IsActive;
            tuesdaySchedule.TimeSpan = TimeSpan.FromMinutes(tuesdaySchedule.Time);
            tuesdayTimePicker.Time = tuesdaySchedule.TimeSpan;

            wednesdaySwitch.IsToggled = wednesdaySchedule.IsActive;
            wednesdaySchedule.TimeSpan = TimeSpan.FromMinutes(wednesdaySchedule.Time);
            wednesdayTimePicker.Time = wednesdaySchedule.TimeSpan;

            thursdaySwitch.IsToggled = thursdaySchedule.IsActive;
            thursdaySchedule.TimeSpan = TimeSpan.FromMinutes(thursdaySchedule.Time);
            thursdayTimePicker.Time = thursdaySchedule.TimeSpan;

            fridaySwitch.IsToggled = fridaySchedule.IsActive;
            fridaySchedule.TimeSpan = TimeSpan.FromMinutes(fridaySchedule.Time);
            fridayTimePicker.Time = fridaySchedule.TimeSpan;

            saturdaySwitch.IsToggled = saturdaySchedule.IsActive;
            saturdaySchedule.TimeSpan = TimeSpan.FromMinutes(saturdaySchedule.Time);
            saturdayTimePicker.Time = saturdaySchedule.TimeSpan;

            sundaySwitch.IsToggled = sundaySchedule.IsActive;
            sundaySchedule.TimeSpan = TimeSpan.FromMinutes(sundaySchedule.Time);
            sundayTimePicker.Time = sundaySchedule.TimeSpan;
            ////foreach (var schedule in scheduleList)
            ////{
            ////    scheduleGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            ////    Switch scheduleSwitch = new Switch { HorizontalOptions = LayoutOptions.Start };
            ////    scheduleSwitch.BindingContext = scheduleSwitch;
            ////    scheduleSwitch.SetBinding(IsEnabledProperty, schedule.IsActive);
            ////    scheduleSwitch.GestureRecognizers.Add(formChanged);
            ////    var scheduleLabel = new Label { Text = schedule.Day, VerticalTextAlignment = TextAlignment.Center };
            ////    var scheduleTime = new TimePicker { BindingContext = schedule.TimeSpan, HorizontalOptions = LayoutOptions.End };
            ////    scheduleTime.GestureRecognizers.Add(formChanged);

            ////    scheduleGrid.Children.Add(scheduleSwitch, 0, schedule.Id + 2);
            ////    scheduleGrid.Children.Add(scheduleLabel, 1, schedule.Id + 2);
            ////    scheduleGrid.Children.Add(scheduleTime, 2, schedule.Id + 2);
            ////}
        }

        private async void SaveButton_ClickedAsync(object sender, EventArgs e)
        {
            foreach (var schedule in scheduleList)
            {
                switch (schedule.Day)
                {
                    case "Monday":
                        schedule.IsActive = mondaySwitch.IsToggled;
                        schedule.TimeSpan = mondayTimePicker.Time;
                        schedule.Time = (int)schedule.TimeSpan.TotalMinutes;
                        break;
                    case "Tuesday":
                        schedule.IsActive = tuesdaySwitch.IsToggled;
                        schedule.TimeSpan = tuesdayTimePicker.Time;
                        schedule.Time = (int)schedule.TimeSpan.TotalMinutes;
                        break;
                    case "Wednesday":
                        schedule.IsActive = wednesdaySwitch.IsToggled;
                        schedule.TimeSpan = wednesdayTimePicker.Time;
                        schedule.Time = (int)schedule.TimeSpan.TotalMinutes;
                        break;
                    case "Thursday":
                        schedule.IsActive = thursdaySwitch.IsToggled;
                        schedule.TimeSpan = thursdayTimePicker.Time;
                        schedule.Time = (int)schedule.TimeSpan.TotalMinutes;
                        break;
                    case "Friday":
                        schedule.IsActive = fridaySwitch.IsToggled;
                        schedule.TimeSpan = fridayTimePicker.Time;
                        schedule.Time = (int)schedule.TimeSpan.TotalMinutes;
                        break;
                    case "Saturday":
                        schedule.IsActive = saturdaySwitch.IsToggled;
                        schedule.TimeSpan = saturdayTimePicker.Time;
                        schedule.Time = (int)schedule.TimeSpan.TotalMinutes;
                        break;
                    case "Sunday":
                        schedule.IsActive = sundaySwitch.IsToggled;
                        schedule.TimeSpan = sundayTimePicker.Time;
                        schedule.Time = (int)schedule.TimeSpan.TotalMinutes;
                        break;
                    default:
                        break;
                }
                await App.ScheduleRepo.UpdateScheduleAsync(schedule);
            }
            await Navigation.PopModalAsync();
        }

        private async void CancelButton_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            if (IsFormDirty == FormStatus["Yes"])
            {
                ShowExitDialogAsync();
            }
            else if (IsFormDirty == FormStatus["No"] || IsFormDirty == FormStatus["Yes and Leave"])
            {
                return base.OnBackButtonPressed();
            }
            IsFormDirty = FormStatus["Yes"];
            return true;
            
        }
        
        private async void ShowExitDialogAsync()
        {
            var answer = await DisplayAlert("Discard Changes", "Are you sure you want to leave without saving changes?", "Yes", "No");
            if (answer)
            {
                IsFormDirty = FormStatus["Yes and Leave"];
            }
            else
            {
                IsFormDirty = FormStatus["Yes and stay"];
            }
            
            OnBackButtonPressed();
        }

        //private void CreateGrid()
        //{
        //    //var schlist = scheduleList.ToList();
        //    scheduleGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        //    scheduleGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        //    scheduleGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        //    scheduleGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        //    Label switchHeader = new Label { Text = "Remind Me?", VerticalTextAlignment = TextAlignment.Center };
        //    Label dayHeader = new Label { Text = "Days", VerticalTextAlignment = TextAlignment.Center };
        //    Label TimeHeader = new Label { Text = "Time", VerticalTextAlignment = TextAlignment.Center };

        //    scheduleGrid.Children.Add(switchHeader, 0, 0);
        //    scheduleGrid.Children.Add(dayHeader, 1, 0);
        //    scheduleGrid.Children.Add(TimeHeader, 2, 0);


        //    foreach (var schedule in schlist)
        //    {
        //        scheduleGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

        //        var scheduleSwitch = new Switch { IsEnabled = schedule.IsActive, BindingContext=schedule.IsActive};
        //        var scheduleLabel = new Label { Text = schedule.Day };
        //        var scheduleTime = new TimePicker { Time = schedule.TimeSpan, BindingContext = schedule.TimeSpan };

        //        scheduleGrid.Children.Add(scheduleSwitch, 0, schedule.Id + 1);
        //        scheduleGrid.Children.Add(scheduleLabel, 1, schedule.Id + 1);
        //        scheduleGrid.Children.Add(scheduleTime, 2, schedule.Id + 1);

        //    }
        //}
    }
}