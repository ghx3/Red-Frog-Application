using RedFrogs.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace RedFrogs.Views
{
    public partial class DashboardPage : ContentPage
    {
        string nameOfEvent = null;
        Events currEvent;

        public DashboardPage(Events selEvent)
        {
            InitializeComponent();

            currEvent = selEvent;

            Title = currEvent.EventName;
            nameOfEvent = currEvent.EventName;
            var fList = App.access.GetAllFeedBack();            
            var sList = App.access.GetAllSymptoms();

            ObservableCollection<Cases> cases = new ObservableCollection<Cases>();
            foreach(FeedBack f in fList)
            { 
                if(f.EventName == currEvent.EventName)
                {
                    Symptoms info = App.access.getSymptom(f.Symptom);

                    cases.Add(new Cases { PersonName = f.Name, SymptomName = info.SympName, Colour = info.Colour });
                }                
            }

            MessagingCenter.Subscribe<DataInputPage>(this, "SaveValue", (sender) =>
            {
                Debug.WriteLine("Now at Dashboard Page");
                Debug.WriteLine(currEvent.EventName + "This is the Event Name");
                InitializeComponent();
                ToolbarItems.Remove(add);
                var fList1 = App.access.GetAllFeedBack();
                var sList1 = App.access.GetAllSymptoms();

                ObservableCollection<Cases> cases1 = new ObservableCollection<Cases>();
                foreach (FeedBack f in fList1)
                {
                    if (f.EventName == currEvent.EventName)
                    {
                        Symptoms info = App.access.getSymptom(f.Symptom);

                        cases1.Add(new Cases { PersonName = f.Name, SymptomName = info.SympName, Colour = info.Colour });
                    }
                }

                DisplayAlert("Title", cases1.Count.ToString(), "OK");

                caseList.ItemsSource = cases1;

            });

            caseList.ItemsSource = cases;
            
            add.Clicked += AddClicked;
            //plusBtn.Clicked += plus;
            //minusBtn.Clicked += minus;
        }

        private async void AddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DataInputPage(nameOfEvent));
        }

        private async void Client_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new DataInputPage(currEvent, true));
        }

        //public void plus(object sender, EventArgs e)
        //{
        //    currEvent.NumInteractions += 1;
        //    intCount.Text = currEvent.NumInteractions.ToString();
        //}

        //public void minus(object sender, EventArgs e)
        //{
        //    currEvent.NumInteractions -= 1;
        //    intCount.Text = currEvent.NumInteractions.ToString();
        //}
    }
}
