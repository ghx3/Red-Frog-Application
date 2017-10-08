using RedFrogs.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            Title = eventName;

            var fList = App.access.GetAllFeedBack();            
            var sList = App.access.GetAllSymptoms();

            ObservableCollection<Cases> cases = new ObservableCollection<Cases>();
            foreach(FeedBack f in fList)
            {                
                Symptoms info = App.access.getSymptom(f.Symptom);
                
                cases.Add(new Cases { PersonName = f.Name, SymptomName = info.SympName, Colour = info.Colour});
            }

            DisplayAlert("test", cases.Count.ToString(), "ok");
            caseList.ItemsSource = cases;
            
            add.Clicked += addClicked;
        }

        private async void addClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DataInputPage());
        }
    }
}
