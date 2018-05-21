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
    public partial class DashboardPage : ContentPage
    {
        AzureService azureService;
        Events currEvent;
        public ObservableRangeCollection<Cases> cases { get; } = new ObservableRangeCollection<Cases>();
        string nameOfEvent = null;
        //ObservableCollection<CaseInfo> cases;

        public DashboardPage(Events selEvent)
        {
            InitializeComponent();
            azureService = DependencyService.Get<AzureService>();
            currEvent = selEvent;
            nameOfEvent = currEvent.EventName;

            caseList.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                var toEdit = e.SelectedItem as CaseInfo;
                Navigation.PushAsync(new DataInputPage(toEdit, true));
            };
          
        }
         
        protected async override void OnAppearing()
        {            
            SetupCounts();
            cases.Clear();
            var getCaseInfo = await azureService.GetEventCases(currEvent.EventName, Settings.VolunteerName);
            Cases c = new Cases();
            Symptoms symptomColour;
            ColorTypeConverter converter = new ColorTypeConverter();

            foreach (CaseInfo cinfo in getCaseInfo)
            {                
                symptomColour = await App.DB.getSymptom(cinfo.Symptom);
                c.PersonName = cinfo.Name;
                c.SymptomName = cinfo.Symptom;
                c.colour = (Color)(converter.ConvertFromInvariantString(symptomColour.Colour));

                cases.Add(c);
            }
            
            caseList.ItemsSource = cases;            
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
        
        public async void plus(object sender, EventArgs e)
        {
            currEvent.IndvlInteractions += 1;
            intCount.Text = "Interactions: " + currEvent.IndvlInteractions.ToString();
            await azureService.UpdateEventWithoutSync(currEvent);
        }

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
