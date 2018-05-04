using RedFrogs.Data;
using RedFrogs.Helpers;
using RedFrogs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedFrogs.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamLeaderDashPage : ContentPage
    {
        AzureService azureService;

        public TeamLeaderDashPage(Events e)
        {
            InitializeComponent();

            azureService = DependencyService.Get<AzureService>();
            
            volName.Text = "Welcome " + Settings.VolunteerName;
            interactions.Text = e.NumInteractions.ToString();
            
        }
    }
}