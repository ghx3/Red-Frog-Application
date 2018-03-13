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

            ObservableCollection<Events> eventsName = new ObservableCollection<Events>();

            EventsList.ItemsSource = eventsName;

            eventsName.Add(new Events { EventName = "Drake 2017" });
            eventsName.Add(new Events { EventName = "Future 2017" });
            eventsName.Add(new Events { EventName = "Post Malone 2018" });
            eventsName.Add(new Events { EventName = "Rihanna 2018" });
            eventsName.Add(new Events { EventName = "Demi Lovato 2018" });
        }

        private async void EventSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Events sel = (Events) e.SelectedItem;
            await Navigation.PushAsync(new DashboardPage(sel));
        }
    }
}