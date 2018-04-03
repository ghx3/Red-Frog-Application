using RedFrogs.Models;
using System;

using Xamarin.Forms;

namespace RedFrogs.Views
{
    public partial class NewEventPage : ContentPage
    {

        public NewEventPage()
        {
            InitializeComponent();

            eventDate.MinimumDate = DateTime.Today;
            addBtn.Clicked += sendEvent;
        }

        public async void sendEvent(object sender, EventArgs e)
        {
            Events toSend = new Events();
            if (string.IsNullOrWhiteSpace(nameFld.Text))
            {
                await DisplayAlert("Event Name empty", "Please enter a name for the event", "Ok");
            } else
            {
                toSend.EventName = nameFld.Text;
                toSend.NumInteractions = 0;
                toSend.EndDate = eventDate.Date.ToString("dd/MM/yyyy");

                App.firebaseDB.saveEvent(toSend);

                await Navigation.PopAsync();
            }            
        }
    }
}
