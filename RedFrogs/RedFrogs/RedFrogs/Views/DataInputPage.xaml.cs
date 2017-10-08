using RedFrogs.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using RedFrogs.Helpers;

namespace RedFrogs.Views
{
    public partial class DataInputPage : ContentPage
    {
        FeedBack saveFB;
        private object selectedItem;

        public DataInputPage()
        {
            InitializeComponent();

            saveFB = new FeedBack();
            addBtn.Clicked += AddClicked;
            PopulateSymptomPicker();
            
        }

        public DataInputPage(object selectedItem)
        {
            this.selectedItem = selectedItem;
            InitializeComponent();

            saveFB = new FeedBack();
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
                saveFB.Gender = (string)gender.ItemsSource[selectedIndex];
            }
        }

        void SymptomPickerChanged(object sender, EventArgs e)
        {
            var symp = (Picker)sender;
            int selectedIndex = gender.SelectedIndex;

            if(selectedIndex != -1)
            {
                saveFB.Symptom = selectedIndex + 1;
            }
        }

        public async void AddClicked(object sender, EventArgs e)
        {
            saveFB.Name = nameFld.Text;
            saveFB.Age = int.Parse(ageFld.Text);
            saveFB.SeenByMedic = HelperClass.ConvertToInt(medicSwitch.IsToggled);
            saveFB.IncidentReported = HelperClass.ConvertToInt(reportSwitch.IsToggled);
            saveFB.ActionTaken = actionFld.Text;

            App.access.SaveFeedback(saveFB);

            await Navigation.PopAsync();
        }        
    }
}
