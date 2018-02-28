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
        string nameOfEvent = null;
        public DashboardPage(string eventName)
        {
            InitializeComponent();

            Title = eventName;
            nameOfEvent = eventName;
            var fList = App.access.GetAllFeedBack();            
            var sList = App.access.GetAllSymptoms();

            ObservableCollection<Cases> cases = new ObservableCollection<Cases>();
            foreach(FeedBack f in fList)
            { 
                if(f.EventName == eventName)
                {
                    Symptoms info = App.access.getSymptom(f.Symptom);

                    cases.Add(new Cases { PersonName = f.Name, SymptomName = info.SympName, Colour = info.Colour });
                }
                
            }

            MessagingCenter.Subscribe<DataInputPage>(this, "SaveValue", (sender) =>
            {
                InitializeComponent();
                ToolbarItems.Remove(add);
                var fList1 = App.access.GetAllFeedBack();
                var sList1 = App.access.GetAllSymptoms();

                ObservableCollection<Cases> cases1 = new ObservableCollection<Cases>();
                foreach (FeedBack f in fList1)
                {
                    if (f.EventName == eventName)
                    {
                        Symptoms info = App.access.getSymptom(f.Symptom);

                        cases1.Add(new Cases { PersonName = f.Name, SymptomName = info.SympName, Colour = info.Colour });
                    }

                }


                caseList.ItemsSource = cases1;

            });

            caseList.ItemsSource = cases;
            
            add.Clicked += AddClicked;
        }

        private async void AddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DataInputPage(nameOfEvent));
        }

        private async void Client_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new DataInputPage(e.SelectedItem));
        }
    }
}
