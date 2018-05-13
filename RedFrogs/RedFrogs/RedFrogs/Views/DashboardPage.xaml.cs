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
        public ObservableRangeCollection<CaseInfo> cases { get; } = new ObservableRangeCollection<CaseInfo>();
        string nameOfEvent = null;
        //ObservableCollection<CaseInfo> cases;

        public DashboardPage(Events selEvent)
        {
            InitializeComponent();
            azureService = DependencyService.Get<AzureService>();
            currEvent = selEvent;
            Title = currEvent.EventName;
            nameOfEvent = currEvent.EventName;

            caseList.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                var toEdit = e.SelectedItem as CaseInfo;
                Navigation.PushAsync(new DataInputPage(toEdit, true));
            };
            
        }
         
        protected async override void OnAppearing()
        {
            var getCaseInfo = await azureService.GetEventCases(currEvent.EventName, Settings.VolunteerName);
           // cases.ReplaceRange(getCaseInfo);

           // List<Cases> display = new List<Cases>();
            ColorTypeConverter converter = new ColorTypeConverter();

            foreach (CaseInfo cinfo in getCaseInfo)
            {
                //Symptoms symptomColour = await App.DB.getSymptom(cinfo.Symptom);                
                //Cases item = new Cases();
                //item.PersonName = cinfo.Name;
                //item.SymptomName = cinfo.Symptom;
                //item.colour = (Color)(converter.ConvertFromInvariantString(symptomColour.Colour));

                //display.Add(item);
                cases.Add(cinfo);

            }
            caseList.ItemsSource = cases;
            
        }

        private async void AddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DataInputPage(nameOfEvent));
        }

        //private async void Client_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var toEdit = e.SelectedItem as CaseInfo; 
        //    await DisplayAlert(toEdit.Name, toEdit.EventName, "OK");
        //    await Navigation.PushAsync(new DataInputPage(toEdit, true));
        //}

        public void plus(object sender, EventArgs e)
        {
            currEvent.NumInteractions += 1;
            intCount.Text = currEvent.NumInteractions.ToString();
        }

        public void minus(object sender, EventArgs e)
        {
            currEvent.NumInteractions -= 1;
            currEvent.NumInteractions = HelperClass.GreaterThanZero(currEvent.NumInteractions);
            intCount.Text = currEvent.NumInteractions.ToString();
        }
    }
}
