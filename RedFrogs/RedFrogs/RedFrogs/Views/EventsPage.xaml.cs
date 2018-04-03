using RedFrogs.Models;
using System;
using System.Collections.ObjectModel;
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

            ObservableCollection<Events> eventsColl = new ObservableCollection<Events>();
            
            Device.BeginInvokeOnMainThread(async () =>
            {
                var events = await App.firebaseDB.getEvents();               
                foreach (var item in events)
                {
                    eventsColl.Add(item);
                }

                EventsList.ItemsSource = eventsColl;
            });

            newEventBtn.Clicked += addClicked;                        
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