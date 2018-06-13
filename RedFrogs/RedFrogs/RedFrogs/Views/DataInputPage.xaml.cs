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

        /*The following constructor is called when the datainput page is called
         * to add a case */
        public DataInputPage(string eventName)
        {
            InitializeComponent();
            azureService = DependencyService.Get<AzureService>();
            nameOfEvent = eventName;
            saveCase = new CaseInfo();
            PopulateSymptomPicker();
            ToolBarItems();
            
        }

        /*The following constructor is called when the datainput page is called
        * to view an already added case. In this constructor all the field values
        * should be populated with picker fields being hidden and disables in favour 
          of text fields. */
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
            if(selectedItem.IncidentType == null)
            {
                incidentEntry.Text = "No incident value chosen";
            }
            else
            {
                incidentEntry.Text = selectedItem.IncidentType;
            }
            if(selectedItem.Specific == null)
            {
                specificEntry.Text = "No specific value chosen";
            }
            else
            {
                specificEntry.Text = selectedItem.Specific;
            }
            
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

        async void SymptomPickerChanged(object sender, EventArgs e)
        {
          if (sympPicker.SelectedIndex != -1)   //If the picker has a value
            {
                saveCase.Symptom = sympPicker.Items[sympPicker.SelectedIndex];
                Symptoms symptom = await App.DB.getSymptom(saveCase.Symptom);
                saveCase.SymptomColour = symptom.Colour;
            }
           
            PopulateIncidentPicker();
           
        }

        async void PopulateIncidentPicker()
        {
            var incidents = await App.DB.GetAllIncidents();
            List<string> incidentNames = new List<string>();
            foreach (Incident name in incidents)
            {
                //The following code looks up the incidents that comes under the chosen symptom in the database table
                if(sympPicker.SelectedIndex + 1 == name.SymptomID)
                {
                    incidentNames.Add(name.IncidentName);
                    
                }
              
            }
            sympPicker1.ItemsSource = incidentNames;

        }

        /* Method for getting the value from Incident Picker. In this method it checks whether
         * the picker has value, then it sets that value as the incident for that specific case.
         * What it also foes in the foreach loop is running the list of all incidents against the saved 
         * incident and adding the ID of saved incident on to a list. This is done so that the picker
         * for specific would be properly populate relevant specifics that comes under the selected Incident */
        private async void sympPicker1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sympPicker1.SelectedIndex != -1)
            {
                saveCase.IncidentType = sympPicker1.Items[sympPicker1.SelectedIndex];

            }
            
            var incidents = await App.DB.GetAllIncidents();
            foreach (Incident incidentCase in incidents)
            {
                if (saveCase.IncidentType == incidentCase.IncidentName)
                {
                    listofNum.Add(incidentCase.Id);
                }
            }
            PopulateSpecificPicker();


        }


        async void PopulateSpecificPicker()
        {
            var specifics = await App.DB.GetAllSpecifics();
            List<string> specificNames = new List<string>();
            foreach (Specific name in specifics)
            {
                  foreach(int num in listofNum)
                {
                    //If the specifics's foreign ID of incident matches the number that was added to the
                    //list when selecting incident, add that specific object onto the picker.
                    if(name.IncidentID == num)
                    {
                        specificNames.Add(name.Name);
                    }

               }

            }
            //There are a few incidents in the database which doesn't have specifics, so for those cases
            //display a message saying that there are no specifics for this certain incident

            if(specificNames.Count == 0)
            {
                specificNames.Add("No specific concerns");
            }

            listofNum.Clear();

           sympPicker2.ItemsSource = specificNames;

        }

       //Sets the selected value as the specific.
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
            if(sympPicker.SelectedIndex == -1)
            {
               await  DisplayAlert("Warning!", "Please select a value for symptom", "OK");
            }
            else
            {
                await azureService.AddCaseInfo(saveCase);
                MessagingCenter.Send<DataInputPage>(this, "SaveValue");
                await Navigation.PopAsync();
            }
           
        }
    }
}
