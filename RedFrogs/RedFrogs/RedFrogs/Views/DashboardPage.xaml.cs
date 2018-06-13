using Acr.UserDialogs;
using MvvmHelpers;
using RedFrogs.Data;
using RedFrogs.Helpers;
using RedFrogs.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace RedFrogs.Views
{
    /**
     * This class creates the dashboard page where the user can view the cases they have added, recorded
     * interactions, lollies, water etc. 
     * **/
    public partial class DashboardPage : ContentPage
    {
        AzureService azureService;
        Events currEvent;        
        public ObservableRangeCollection<CaseInfo> CasesInfo_cases { get; } = new ObservableRangeCollection<CaseInfo>();
        string nameOfEvent = null;

        public DashboardPage(Events selEvent)
        {
            InitializeComponent();
            azureService = DependencyService.Get<AzureService>();
            currEvent = selEvent;
            nameOfEvent = currEvent.EventName;

            /* Code that get the selected item from the listview, which is then used to open 
             * datainput page. Make sure the object being selected, sent to and the object
             * used in the datainput page are the same */
            caseList.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                var toEdit = e.SelectedItem as CaseInfo;
                Navigation.PushAsync(new DataInputPage(toEdit, true));
            };
          
        }
         
        protected async override void OnAppearing()
        {            
            SetupCounts();
            setupCases();           
        }

        public async void setupCases()
        {
            if (Settings.isTeamLeader)
            {
                var getCaseInfo = await azureService.GetAllEventCases(currEvent.EventName);
                CasesInfo_cases.ReplaceRange(getCaseInfo);
                
            } else
            {
                var getCaseInfo = await azureService.GetEventCases(currEvent.EventName, Settings.VolunteerName);
                CasesInfo_cases.ReplaceRange(getCaseInfo);                
            }

            caseList.ItemsSource = CasesInfo_cases;
        }

        public void SetupCounts()
        {
            currEvent.IndvlInteractions = HelperClass.GreaterThanZero(currEvent.IndvlInteractions);
            intCount.Text = "Interactions: " + currEvent.IndvlInteractions.ToString();            

            currEvent.IndvlWaterCount = HelperClass.GreaterThanZero(currEvent.IndvlWaterCount);
            waterCount.Text = "Litres of Water: " + currEvent.IndvlWaterCount.ToString();

            currEvent.IndvlRFLolliesCount = HelperClass.GreaterThanZero(currEvent.IndvlRFLolliesCount);
            rfCount.Text = "Bags of Red Frogs: " + currEvent.IndvlRFLolliesCount;

            currEvent.IndvlOGCount = HelperClass.GreaterThanZero(currEvent.IndvlOGCount);
            ogCount.Text = "Other goods: " + currEvent.IndvlOGCount;
        }

        private async void AddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DataInputPage(nameOfEvent));
        }
        private async void LogOutClicked(object sender, EventArgs e)

        {
            await Navigation.PopToRootAsync();
        }

        private async void SyncClicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Syncing in Progress", MaskType.Gradient);
            currEvent.NumInteractions = currEvent.NumInteractions + currEvent.IndvlInteractions;
            currEvent.LitresWater = currEvent.LitresWater + currEvent.IndvlWaterCount;
            currEvent.NumRFLollies = currEvent.NumRFLollies + currEvent.IndvlRFLolliesCount;
            currEvent.NumOtherGoods = currEvent.NumOtherGoods + currEvent.IndvlOGCount;
            await azureService.Sync(currEvent);
            UserDialogs.Instance.HideLoading();
        }
        
        //Adding an interaction count
        public async void plus(object sender, EventArgs e)
        {
            currEvent.IndvlInteractions += 1;
            intCount.Text = "Interactions: " + currEvent.IndvlInteractions.ToString();
            await azureService.UpdateEventWithoutSync(currEvent);
        }

        //Substracting an interaction count
        public async void minus(object sender, EventArgs e)
        {
            currEvent.IndvlInteractions -= 1;
            currEvent.IndvlInteractions = HelperClass.GreaterThanZero(currEvent.IndvlInteractions);
            intCount.Text = "Interactions: " + currEvent.IndvlInteractions.ToString();
            await azureService.UpdateEventWithoutSync(currEvent);
        }

        public async void WaterPlus(object sender, EventArgs e)
        {
            currEvent.IndvlWaterCount += 1;
            waterCount.Text = "Litres of Water: " + currEvent.IndvlWaterCount.ToString();
            await azureService.UpdateEventWithoutSync(currEvent);
        }

        public async void WaterMinus(object sender, EventArgs e)
        {
            currEvent.IndvlWaterCount -= 1;
            currEvent.IndvlWaterCount = HelperClass.GreaterThanZero(currEvent.IndvlWaterCount);
            waterCount.Text = "Litres of Water: " + currEvent.IndvlWaterCount.ToString();
            await azureService.UpdateEventWithoutSync(currEvent);
        }

        public async void LolliesPlus(object sender, EventArgs e)
        {
            currEvent.IndvlRFLolliesCount += 1;
            rfCount.Text = "Bags of Red Frogs: " + currEvent.IndvlRFLolliesCount.ToString();
            await azureService.UpdateEventWithoutSync(currEvent);
        }

        public async void LolliesMinus(object sender, EventArgs e)
        {
            currEvent.IndvlRFLolliesCount -= 1;
            currEvent.IndvlRFLolliesCount = HelperClass.GreaterThanZero(currEvent.IndvlRFLolliesCount);
            rfCount.Text = "Bags of Red Frogs: " + currEvent.IndvlRFLolliesCount.ToString();
            await azureService.UpdateEventWithoutSync(currEvent);
        }

        public async void OtherGoodsPlus(object sender, EventArgs e)
        {
            currEvent.IndvlOGCount += 1;
            ogCount.Text = "Other goods: " + currEvent.IndvlOGCount.ToString();
            await azureService.UpdateEventWithoutSync(currEvent);
        }

        public async void OtherGoodsMinus(object sender, EventArgs e)
        {
            currEvent.IndvlOGCount -= 1;
            currEvent.IndvlOGCount = HelperClass.GreaterThanZero(currEvent.IndvlOGCount);
            ogCount.Text = "Other goods: " + currEvent.IndvlOGCount.ToString();
            await azureService.UpdateEventWithoutSync(currEvent);
        }
    }
}
