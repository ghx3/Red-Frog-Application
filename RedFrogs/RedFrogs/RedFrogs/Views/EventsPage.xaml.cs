using Firebase.Xamarin.Database;
using RedFrogs.Data;
using RedFrogs.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            
            //EventsList.ItemsSource = eventsName;

            /*eventsName.Add(new Events { EventName = "Drake 2017" });
            eventsName.Add(new Events { EventName = "Future 2017" });
            eventsName.Add(new Events { EventName = "Post Malone 2018" });
            eventsName.Add(new Events { EventName = "Rihanna 2018" });
            eventsName.Add(new Events { EventName = "Demi Lovato 2018" });*/
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