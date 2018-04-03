using Firebase.Xamarin.Database;
using RedFrogs.Data;
using RedFrogs.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedFrogs.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsPage : ContentPage
    {
        public EventsPage()
        {
            InitializeComponent();
            Title = "Events Page";

            var fireDB = new RedFrogFirebaseDB();

            ObservableCollection<Events> eventsColl = new ObservableCollection<Events>();
            

            Device.BeginInvokeOnMainThread(async () =>
            {
                var events = await fireDB.getEvents();               
                foreach (var item in events)
                {
                    eventsColl.Add(item);
                }

                EventsList.ItemsSource = eventsColl;
            });

            newEventBtn.Clicked += addClicked;

           MessagingCenter.Subscribe<NewEventPage>(this, "SaveEvents", (async) =>
            {
                ObservableCollection<Events> eventsColl_1 = new ObservableCollection<Events>();
               
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var newEvents = await fireDB.getEvents();
                    foreach (var item in newEvents)
                    {
                        Debug.WriteLine(item.EventName);
                        eventsColl_1.Add(item);
                    }

                    EventsList.ItemsSource = eventsColl_1;
                });
            });
        }

        private async void EventSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Events sel = (Events) e.SelectedItem;            
            await Navigation.PushAsync(new DashboardPage(sel));
        }        

        private async void addClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewEventPage());
        }

    }
}