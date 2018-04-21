using RedFrogs.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedFrogs.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsPage : ContentPage
    {
        ObservableCollection<Events> eventsColl;

        public EventsPage()
        {
            InitializeComponent();

            eventsColl = new ObservableCollection<Events>();
            loadEvents();
            
            MessagingCenter.Subscribe<NewEventPage>(this, "SaveEvents", (async) =>
            {
                ObservableCollection<Events> eventsColl_1 = new ObservableCollection<Events>();

                Device.BeginInvokeOnMainThread(async () =>
                {
                    var newEvents = await App.firebaseDB.getEvents();
                    foreach (var item in newEvents)
                    {
                        eventsColl_1.Add(item);
                    }

                    EventsList.ItemsSource = eventsColl_1;
                });
            });

            newEventBtn.Clicked += addClicked;
            deleteBtn.Clicked += deleteClicked;
        }

        private async void loadEvents()
        {
            var events = await App.firebaseDB.getEvents();
            foreach (var item in events)
            {
                eventsColl.Add(item);
            }
            EventsList.ItemsSource = eventsColl;
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

        private void deleteClicked(object sender, EventArgs e)
        {
            App.DB.DeleteAllEvents();
        }
        
        private async void CloseClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FeedbackPage());
        }

    }
}