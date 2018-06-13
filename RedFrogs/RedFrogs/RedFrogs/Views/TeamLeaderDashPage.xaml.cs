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
    /**
    * This class is used to create DashboardPage, but for team leaders. In this class the leaders can 
    * view a high level information from all the other volunteers data, like the total number of cases recorded,
    * products given out etc. 
    * **/
    public partial class TeamLeaderDashPage : ContentPage
    {
        AzureService azureService;
        Events eventObj;

        public TeamLeaderDashPage(Events e)
        {
            InitializeComponent();
            eventObj = e;
            azureService = DependencyService.Get<AzureService>();

            volName.Text = "Welcome " + Settings.VolunteerName + " - " + eventObj.EventName;
            
        }

        protected async override void OnAppearing()
        {

            setupData();
        }
        private async void LogOutClicked(object sender, EventArgs e)

        {
            var loginPage = new Login();
            await Navigation.PushAsync(loginPage);
        }

        public async void  setupData()
        {
            interactions.Text = eventObj.NumInteractions.ToString();
            var caseCount = await azureService.GetEventCasesCount(eventObj.EventName);
            cases.Text = caseCount.ToString();


            Water.Text = eventObj.LitresWater.ToString() + " Litres of Water";
            Lollies.Text = eventObj.NumRFLollies.ToString() + " Bags of Red Frogs";
            OtherGoods.Text = eventObj.NumOtherGoods.ToString() + " Other Goods Handed Out";
        }
    }
}