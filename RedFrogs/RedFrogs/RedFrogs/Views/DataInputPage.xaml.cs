using RedFrogs.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using RedFrogs.Helpers;

namespace RedFrogs.Views
{
    public partial class DataInputPage : ContentPage
    {
        CaseInfo saveCase;
        private object selectedItem;
        string nameOfEvent = null;

        public DataInputPage(string eventName)
        {
            InitializeComponent();
            nameOfEvent = eventName;
            saveCase = new CaseInfo();
            addBtn.Clicked += AddClicked;
            PopulateSymptomPicker();
            
        }

        public DataInputPage(object selectedItem)
        {
            this.selectedItem = selectedItem;
            InitializeComponent();

            saveCase = new CaseInfo();
            addBtn.Clicked += AddClicked;
            PopulateSymptomPicker();
        }

        void PopulateSymptomPicker()
        {
            var symptoms = App.access.GetAllSymptoms();
            List<string> sympNames = new List<string>();
            foreach (Symptoms name in symptoms)
            {
                sympNames.Add(name.SympName);
            }

            sympPicker.ItemsSource = sympNames;
        }

        void GenderPickerChanged(object sender, EventArgs e)
        {
            var gender = (Picker)sender;
            int selectedIndex = gender.SelectedIndex;

            if(selectedIndex != -1)
            {
                saveCase.Gender = (string)gender.ItemsSource[selectedIndex];
            }
        }

        void SymptomPickerChanged(object sender, EventArgs e)
        {
            var symp = (Picker)sender;
            int selectedIndex = gender.SelectedIndex;

            if(selectedIndex != -1)
            {
                saveCase.Symptom = selectedIndex + 1;
            }
        }

        public async void AddClicked(object sender, EventArgs e)
        {
            saveCase.EventName = nameOfEvent;
            saveCase.Name = nameFld.Text;
            saveCase.Age = int.Parse(ageFld.Text);
            saveCase.SeenByMedic = HelperClass.ConvertToInt(medicSwitch.IsToggled);
            saveCase.IncidentReported = HelperClass.ConvertToInt(reportSwitch.IsToggled);
            saveCase.ActionTaken = actionFld.Text;

            App.access.SaveCaseInfo(saveCase);
            MessagingCenter.Send<DataInputPage>(this, "SaveValue");
            await Navigation.PopAsync();
        }
        
    }
}
