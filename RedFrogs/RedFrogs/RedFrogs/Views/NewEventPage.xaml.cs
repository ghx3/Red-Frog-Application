using RedFrogs.Data;
using RedFrogs.Models;
using System;

using Xamarin.Forms;

namespace RedFrogs.Views
{
    public partial class NewEventPage : ContentPage
    {
        AzureService azureService;
        Events newEvent;

        public NewEventPage()
        {
            InitializeComponent();

            azureService = DependencyService.Get<AzureService>();
            newEvent = new Events();

            eventDate.MinimumDate = DateTime.Today;
            addBtn.Clicked += sendEvent;
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
            else
            {
                newEvent.EventName = nameFld.Text;
                newEvent.NumInteractions = 0;
                newEvent.EndDate = eventDate.Date;
                newEvent.IsClosed = false;

                await azureService.AddEvent(newEvent);
                SendMessage();
                await Navigation.PopAsync();
            }
        }

        private void SendMessage()
        {
            MessagingCenter.Send<NewEventPage>(this, "SaveEvents");
        }
    }
}
