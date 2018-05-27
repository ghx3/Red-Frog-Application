using RedFrogs.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using RedFrogs.Helpers;
using RedFrogs.Data;
using System.Diagnostics;

namespace RedFrogs.Views
{
    public partial class DataInputPage : ContentPage
    {
        CaseInfo saveCase;
        AzureService azureService;
        private object selectedItem;
        string nameOfEvent;
        List<int> listofNum = new List<int>();

        public DataInputPage(string eventName)
        {
            InitializeComponent();
            azureService = DependencyService.Get<AzureService>();
            nameOfEvent = eventName;
            saveCase = new CaseInfo();
            PopulateSymptomPicker();
            ToolBarItems();
            
        }

        public DataInputPage(CaseInfo selectedItem, bool isEdit)
        {            
            InitializeComponent();
            ToolBarItems();
            this.selectedItem = selectedItem;
            
            PopulateSymptomPicker();

           // saveCase = selectedItem;
            nameFld.Text = selectedItem.Name;
            ageFld.Text = selectedItem.Age.ToString();
            actionFld.Text = selectedItem.ActionTaken;

            IfEdit();
            genderEntry.Text = selectedItem.Gender;
            //symptomEntry.Text = selectedItem.Symptom; 
            incidentEntry.Text = selectedItem.IncidentType;
            specificEntry.Text = selectedItem.Specific;
            if(selectedItem.SeenByMedic == true)
            {
                medicSwitch.IsToggled = true;
            }

            if (selectedItem.IncidentReported == true)
            {
                reportSwitch.IsToggled = true;
            }
        }

        private void IfEdit()
        {
            gender.IsEnabled = false;
            gender.IsVisible = false;
            genderEntry.IsEnabled = true;
            genderEntry.IsVisible = true;
            //symptomEntry.IsVisible = true;
            //symptomEntry.IsEnabled = true;
            incidentEntry.IsVisible = true;
            incidentEntry.IsEnabled = true;
            specificEntry.IsVisible = true;
            specificEntry.IsEnabled = true;
            sympPicker.IsEnabled = false;
            sympPicker.IsVisible = false;
            sympPicker1.IsEnabled = false;
            sympPicker1.IsVisible = false;
            sympPicker2.IsEnabled = false;
            sympPicker2.IsVisible = false;
            addBtn.IsEnabled = false;
            addBtn.IsVisible = false;
        }

        public void ToolBarItems()
        {
            var toolBarItem = new ToolbarItem
            {
                Text = "Cancel",
                Icon = "Cancel",
                Command = new Command(this.GoBack),
            };

            this.ToolbarItems.Add(toolBarItem);
        }

        private void GoBack()
        {
            Navigation.PopAsync();
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

        void SymptomPickerChanged(object sender, EventArgs e)
        {
            if (sympPicker.SelectedIndex != -1)
            {
                saveCase.Symptom = sympPicker.Items[sympPicker.SelectedIndex];
            }

            PopulateIncidentPicker();
           
        }

        async void PopulateIncidentPicker()
        {
            var incidents = await App.DB.GetAllIncidents();
            List<string> incidentNames = new List<string>();
            foreach (Incident name in incidents)
            {
                if(sympPicker.SelectedIndex + 1 == name.SymptomID)
                {
                    incidentNames.Add(name.IncidentName);
                    
                }
              
            }

            sympPicker1.ItemsSource = incidentNames;

        }

       
        async void PopulateSpecificPicker()
        {
            var specifics = await App.DB.GetAllSpecifics();
            List<string> specificNames = new List<string>();
            foreach (Specific name in specifics)
            {
                  foreach(int num in listofNum)
                {
                    if(name.IncidentID == num)
                    {
                        specificNames.Add(name.Name);
                    }

               }

            }

            if(specificNames.Count == 0)
            {
                specificNames.Add("No specific concerns");
            }

            listofNum.Clear();

           sympPicker2.ItemsSource = specificNames;

        }

        private async void sympPicker1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sympPicker1.SelectedIndex != -1)
            {
                saveCase.IncidentType = sympPicker1.Items[sympPicker1.SelectedIndex];
                
            }
            var incidents = await App.DB.GetAllIncidents();
            foreach (Incident incidentCase in incidents)
            {
                if(saveCase.IncidentType == incidentCase.IncidentName)
                {
                    listofNum.Add(incidentCase.Id);
                }
            }
            PopulateSpecificPicker();
          
            
        }

        private void sympPicker2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sympPicker2.SelectedIndex != -1)
            {
                saveCase.Specific = sympPicker2.Items[sympPicker2.SelectedIndex];
            }

        }

        void GenderPickerChanged(object sender, EventArgs e)
        {            
            if(gender.SelectedIndex != -1)
            {
                saveCase.Gender = gender.Items[gender.SelectedIndex];
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
