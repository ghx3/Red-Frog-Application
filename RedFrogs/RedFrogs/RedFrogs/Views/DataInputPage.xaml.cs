using RedFrogs.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using RedFrogs.Helpers;
using RedFrogs.Data;

namespace RedFrogs.Views
{
    public partial class DataInputPage : ContentPage
    {
        CaseInfo saveCase;
        AzureService azureService;
        private object selectedItem;
        string nameOfEvent;

        public DataInputPage(string eventName)
        {
            InitializeComponent();
            azureService = DependencyService.Get<AzureService>();
            nameOfEvent = eventName;
            saveCase = new CaseInfo();
            PopulateSymptomPicker();
            
        }

        public DataInputPage(CaseInfo selectedItem, bool isEdit)
        {            
            InitializeComponent();
            this.selectedItem = selectedItem;
            
            PopulateSymptomPicker();

            saveCase = selectedItem;
            nameFld.Text = saveCase.Name;
            ageFld.Text = Convert.ToString(saveCase.Age);
            actionFld.Text = saveCase.ActionTaken;            
            
        }

        private async void PopulateSymptomPicker()
        {
            var symptoms = await App.DB.GetAllSymptoms();
            List<string> sympNames = new List<string>();
            foreach (Symptoms name in symptoms)
            {
                sympNames.Add(name.SympName);
            }

            sympPicker.ItemsSource = sympNames;
        }

        void GenderPickerChanged(object sender, EventArgs e)
        {            
            if(gender.SelectedIndex != -1)
            {
                saveCase.Gender = gender.Items[gender.SelectedIndex];
            }            
        }

        void SymptomPickerChanged(object sender, EventArgs e)
        {         
            if(sympPicker.SelectedIndex != -1)
            {
                saveCase.Symptom = sympPicker.Items[sympPicker.SelectedIndex];
            }
        }

        public async void AddClicked(object sender, EventArgs e)
        {
            saveCase.EventName = nameOfEvent;
            saveCase.Name = nameFld.Text;
            saveCase.Age = HelperClass.HasValue(ageFld.Text);
            saveCase.SeenByMedic = medicSwitch.IsToggled;
            saveCase.IncidentReported = reportSwitch.IsToggled;
            saveCase.ActionTaken = actionFld.Text;
            saveCase.TimeInCare = "have to implement";
            saveCase.VolunteerName = Settings.VolunteerName;

            await azureService.AddCaseInfo(saveCase);
            MessagingCenter.Send<DataInputPage>(this, "SaveValue");
            await Navigation.PopAsync();
        }
        
    }
}
