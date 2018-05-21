using RedFrogs.Data;
using RedFrogs.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RedFrogs.Views
{
    public partial class NewEventPage : ContentPage
    {
        List<String> locations;
        AzureService azureService;
        Events newEvent;
        bool edit;

        public NewEventPage(bool isEdit, Events e)
        {
            InitializeComponent();

            azureService = DependencyService.Get<AzureService>();
            edit = isEdit;
            newEvent = e;
            setupLocationList();
            setupFields();
                        
            addBtn.Clicked += sendEvent;
        }

        public void setupLocationList()
        {
            locations = new List<string>();
            locations.Add("Auckland");
            locations.Add("Hamilton");
            locations.Add("Palmerston North");
            locations.Add("Wellington");
            locations.Add("Dunedin");
            locations.Add("Queenstown");
            locations.Add("Christchurch");
            locations.Add("Invercargill");

            cityPicker.ItemsSource = locations;
        }

        public int findSelectedLocation(string location)
        {
            return locations.IndexOf(location);
        }

        public void setupFields()
        {
            if (edit)
            {
                nameFld.Text = newEvent.EventName;
                cityPicker.SelectedIndex = findSelectedLocation(newEvent.Location);
                eventDate.Date = newEvent.EndDate;
            } else
            {
                newEvent = new Events();
            }
        }

        private void City_SelectedIndexChanged(object sender, EventArgs e)
        {
            newEvent.Location = cityPicker.Items[cityPicker.SelectedIndex];
        }

        public async void sendEvent(object sender, EventArgs e)
        {            
            if (string.IsNullOrWhiteSpace(nameFld.Text))
            {
                await DisplayAlert("Event Name empty", "Please enter a name for the event", "Ok");
            }
            else if (edit)
            {
                await azureService.UpdateEvent(newEvent);
                SendMessage();
                await Navigation.PopAsync();
            }
            else
            {
                newEvent.EventName = nameFld.Text;
                newEvent.NumInteractions = 0;
                newEvent.EndDate = eventDate.Date;
                newEvent.IsClosed = false;

                await azureService.AddEvent(newEvent);
                saveInitialFeedback();
                SendMessage();
                await Navigation.PopAsync();
            }
        }

        private void SendMessage()
        {
            MessagingCenter.Send<NewEventPage>(this, "SaveEvents");
        }

        public async void saveInitialFeedback()
        {
            FeedBack fb = new FeedBack()
            {
                EventName = newEvent.EventName,
                City = newEvent.Location,
                SupportTo = "",
                EventDate = newEvent.EndDate,
                SupportGiven = "",
                ActivationLocation = "",
                LeaderName = "",
                VolunteerNumber = 0,
                HoursSpent = 0,
                PatronNumber = 0,
                PatronInteractionNum = 0,
                PancakesProvided = false,
                NumberPancakes = 0,
                WaterProvided = false,
                AmountWater = 0,
                AnyGiveaways = false,
                GivenAway = "",
                AnyPraiseReports = false,
                PraiseReport = "",
                AnyIncidents = false,
                IncidentDescription = "",
                FollowUpNeeded = false,
                FollowUpName = "",
                EventID = newEvent.Id
            };

            await azureService.AddFeedback(fb);
                
        }
    }
}
