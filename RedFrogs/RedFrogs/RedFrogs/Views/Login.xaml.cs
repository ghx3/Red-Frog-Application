using Acr.UserDialogs;
using RedFrogs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedFrogs.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        AzureService azureService;

        public Login ()
		{
			InitializeComponent();
            azureService = DependencyService.Get<AzureService>();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            string user = username.Text;
            string pass = password.Text;

            if(!String.IsNullOrWhiteSpace(user) && !String.IsNullOrWhiteSpace(pass))
            {
                UserDialogs.Instance.ShowLoading("Logging in", MaskType.Gradient);
                var res = await azureService.GetUserInfo(user, pass);
                UserDialogs.Instance.HideLoading();

                if (res)
                {
                    // get user details
                    //go to next page
                    await Navigation.PushAsync(new EventsPage());
                } else
                {
                    await DisplayAlert("Invalid Login", "Incorrect Username or Password", "Try Again");
                }
            } else
            {
                await DisplayAlert("Invalid Login", "Please enter a Username or Password", "Try Again");
            }

            
        }
    }
}