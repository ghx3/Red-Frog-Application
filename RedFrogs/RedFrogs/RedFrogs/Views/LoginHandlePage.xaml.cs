using Acr.UserDialogs;
using RedFrogs.Data;
using RedFrogs.Helpers;
using RedFrogs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedFrogs.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginHandlePage : ContentPage
	{
        AzureService azureService;
        bool teamLeader;

		public LoginHandlePage (bool tlLogin)
		{
			InitializeComponent ();
            azureService = DependencyService.Get<AzureService>();
            teamLeader = tlLogin;
            SetupLabel();
            
		}

        public void SetupLabel()
        {
            if (teamLeader)
            {
                TLUserPwd.Text = "Team Leader Password";
            }
            else
            {
                TLUserPwd.Text = "User Password";
            }
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            string personName = name.Text;
            string password = pwd.Text;

            if (!String.IsNullOrWhiteSpace(personName) && !String.IsNullOrWhiteSpace(personName))
            {
                UserDialogs.Instance.ShowLoading("Logging in", MaskType.Gradient);
                var res = await azureService.GetUserInfo(password);
                UserDialogs.Instance.HideLoading();

                if(res != null)
                {
                    MoveToEventsPages(res);
                }
                else
                {
                    await DisplayAlert("Invalid Login", "Incorrect Password", "Try Again");
                }

                MoveToEventsPages(res);
            }
            else
            {
                await DisplayAlert("Invalid Login", "Please enter your name or Password", "Try Again");
            }
        }

        public async void MoveToEventsPages(Users user)
        {
            if(IsUsingRightPW(user))
            {
                UpdateAppSettings();
                if (teamLeader)
                {
                    await Navigation.PushAsync(new TLEventsPage());
                }
                else
                {
                    await Navigation.PushAsync(new EventsPage());
                }
            } else
            {
                await DisplayAlert("Invalid Login", "Incorrect Password", "Try Again");
            }
        }

        public void UpdateAppSettings()
        {
            Settings.VolunteerName = name.Text;
            Settings.isTeamLeader = teamLeader;
        }

        public bool IsUsingRightPW(Users user)
        {
            if (user.IsTeamLeader == teamLeader)
            {
                return true;
            }

            return false;
        }
    }
}
 