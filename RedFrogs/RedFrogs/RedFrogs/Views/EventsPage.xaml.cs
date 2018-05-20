using MvvmHelpers;
using RedFrogs.Data;
using RedFrogs.Helpers;
using RedFrogs.Models;
using System;
using System.Collections.Generic;
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
        AzureService azureService;
        public ObservableRangeCollection<Events> events { get; } = new ObservableRangeCollection<Events>();

        public EventsPage()
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
                
                var dashboardPage = new DashboardPage(sel);
                await Navigation.PushAsync(dashboardPage);
                
                              
            }            
        }

        private async void addClicked(object sender, EventArgs e)
        {
            var item = (Events)((Button)sender).BindingContext;

            await Navigation.PushAsync(new NewEventPage(true, item));
        }

        private async void CloseClicked(object sender, EventArgs e)
        {
            var item = (Events)((Button)sender).BindingContext;

            await Navigation.PushAsync(new FeedbackPage(item));
        }

    }
}