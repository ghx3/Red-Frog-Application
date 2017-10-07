using RedFrogs.Models;
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

            var fList = App.access.GetAllFeedBack();
            /*
            var sList = App.access.GetAllSymptoms();

            List<Cases> cases = new List<Cases>();
            foreach(FeedBack f in fList)
            {
                Cases c = new Cases();
                Symptoms info = sList.Where(s => s.Id == f.Symptom).FirstOrDefault();
                c.PersonName = f.Name;
                c.SymptomName = info.SympName;
                c.Color = info.Colour;

                cases.Add(c);
            }
            */
            caseList.ItemsSource = fList;
            
            add.Clicked += addClicked;
        }

        private async void addClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DataInputPage());
        }
    }
}
