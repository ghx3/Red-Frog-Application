using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace RedFrogs.Views
{
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage(string eventName)
        {
            InitializeComponent();

            eventN.Text = eventName;
            add.Clicked += addClicked;
        }

        private async void addClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DataInputPage());
        }
    }
}
