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
            //var fList = App.access.GetAllFeedBack();            
            //var sList = App.access.GetAllSymptoms();
            var caseInfo = App.access.GetAllCaseInfo();

            ObservableCollection<Cases> cases = new ObservableCollection<Cases>();
            foreach(CaseInfo cinfo in caseInfo)
            { 
                if(cinfo.EventName == currEvent.EventName)
                {
                    string symptom = Convert.ToString(cinfo.Symptom);
                    cases.Add(new Cases { PersonName = cinfo.Name, SymptomName = symptom, Age = cinfo.Age });
                }                
            }
            caseList.ItemsSource = cases;
            add.Clicked += AddClicked;

            MessagingCenter.Subscribe<DataInputPage>(this, "SaveValue", (sender) =>
            {
               
                InitializeComponent();
                ToolbarItems.Remove(add);
                ToolbarItems.Remove(sync);
                currEvent = selEvent;

                Title = currEvent.EventName;
                var caseInfo1 = App.access.GetAllCaseInfo();

                ObservableCollection<Cases> cases1 = new ObservableCollection<Cases>();
                foreach (CaseInfo cinfo1 in caseInfo1)
                {
                    if (cinfo1.EventName == currEvent.EventName)
                    {
                        string symptom = Convert.ToString(cinfo1.Symptom);
                        cases1.Add(new Cases { PersonName = cinfo1.Name, SymptomName = symptom, Age = cinfo1.Age });
                    }
                }

                caseList.ItemsSource = cases1;
                add.Clicked += AddClicked;
            });

           
            
            
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
