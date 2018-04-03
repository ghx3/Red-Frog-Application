using Firebase.Xamarin.Database;
using RedFrogs.Data;
using RedFrogs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace RedFrogs.Views
{
    public partial class NewEventPage : ContentPage
    {
        RedFrogFirebaseDB fireDB;

        public NewEventPage()
        {
            InitializeComponent();
            fireDB = new RedFrogFirebaseDB();

            eventDate.MinimumDate = DateTime.Today;
            addBtn.Clicked += sendEvent;
        }

        public async void sendEvent(object sender, EventArgs e)
        {
            Events toSend = new Events();
            toSend.EventName = nameFld.Text;
            toSend.NumInteractions = 0;
            toSend.EndDate = eventDate.Date.ToString("dd/MM/yyyy");

            fireDB.saveEvent(toSend);
            SendMessage();
            await Navigation.PopAsync();
        }

        private void SendMessage()
        {
            MessagingCenter.Send<NewEventPage>(this, "SaveEvents");
        }
    }
}
