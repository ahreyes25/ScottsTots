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
	public partial class AddReminders : ContentPage
	{
		public AddReminders ()
		{
			InitializeComponent ();
            NavigationPage.SetBackButtonTitle(this, "BAACK");
        }

        public async void OnAddClicked(object sender, EventArgs args)
        {
            statusMessage.Text = "";

            await App.ReminderRepo.AddNewReminderAsync(addReminder.Text);
            statusMessage.Text = App.ReminderRepo.StatusMessage;
            await Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }


    }
}