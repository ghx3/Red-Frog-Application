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

            ObservableCollection<Events> eventsName = new ObservableCollection<Events>();

            EventsList.ItemsSource = eventsName;

            eventsName.Add(new Events { DisplayEvent = "Drake 2017" });
            eventsName.Add(new Events { DisplayEvent = "Future 2017" });
            eventsName.Add(new Events { DisplayEvent = "Post Malone 2018" });
            eventsName.Add(new Events { DisplayEvent = "Rihanna 2018" });
            eventsName.Add(new Events { DisplayEvent = "Demi Lovato 2018" });
        }
    }
}