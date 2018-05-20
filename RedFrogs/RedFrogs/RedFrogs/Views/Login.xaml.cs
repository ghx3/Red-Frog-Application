using Acr.UserDialogs;
using RedFrogs.Data;
using RedFrogs.Helpers;
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

        public Login()
        {
            InitializeComponent();
            azureService = DependencyService.Get<AzureService>();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (button.ClassId.Equals("TLBtn"))
            {
                await Navigation.PushAsync(new LoginHandlePage(true));
            } else
            {
                await Navigation.PushAsync(new LoginHandlePage(false));
            }
        }   
    }
}