using MvvmHelpers;
using RedFrogs.Data;
using RedFrogs.Helpers;
using RedFrogs.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedFrogs.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TLEventsPage : ContentPage
    {
        AzureService azureService;
        public ObservableRangeCollection<Events> events { get; } = new ObservableRangeCollection<Events>();

        public TLEventsPage()
        {
            InitializeComponent();
            azureService = DependencyService.Get<AzureService>();
        }

        protected async override void OnAppearing()
        {
            await loadEventList();
        }

        public async Task loadEventList()
        {
            IEnumerable<Events> getEvents = null;
            getEvents = await azureService.GetOpenEvents();

            events.ReplaceRange(getEvents);
            EventsList.ItemsSource = events;
        }

        private async void EventSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Events sel = (Events)e.SelectedItem;
                // clear selection
                EventsList.SelectedItem = null;
                //open dashboard page

                var dashboardPage = new TabbedDashboard(sel);
                await Navigation.PushAsync(dashboardPage);

            }
        }

        //button function link to sign up page
        private void SignUpClicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }

        private async void addClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.ClassId.Equals("editBtn"))
            {
                var item = (Events)((Button)sender).BindingContext;
                await Navigation.PushAsync(new NewEventPage(true, item));
            } else
            {
                await Navigation.PushAsync(new NewEventPage(false, null));
            }
            
        }
        private async void LogOutClicked(object sender, EventArgs e)

        {
            var loginPage = new Login();
            await Navigation.PushAsync(loginPage);
        }

        private async void CloseClicked(object sender, EventArgs e)
        {
            var item = (Events)((Button)sender).BindingContext;

            await Navigation.PushAsync(new FeedbackPage(item));
        }
    }
}