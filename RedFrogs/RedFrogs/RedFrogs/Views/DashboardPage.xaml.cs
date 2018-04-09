using RedFrogs.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace RedFrogs.Views
{
    public partial class DashboardPage : ContentPage
    {
        string nameOfEvent = null;
        Events currEvent;
        ObservableCollection<CaseInfo> cases;

        public DashboardPage(Events selEvent)
        {
            InitializeComponent();

            currEvent = selEvent;            
            cases = new ObservableCollection<CaseInfo>();
            Title = currEvent.EventName;
            nameOfEvent = currEvent.EventName;

            LoadEvents();
            add.Clicked += AddClicked;
            MessagingCenter.Subscribe<DataInputPage>(this, "SaveValue", async (sender) =>
            {

                InitializeComponent();
                ToolbarItems.Remove(add);
                ToolbarItems.Remove(sync);
                //currEvent = selEvent;

                Title = currEvent.EventName;
                var caseInfo1 = await App.DB.GetAllCaseInfo();

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

        private async void LoadEvents()
        {
            List<CaseInfo> caseInfo = await App.DB.GetAllCaseInfo();

            foreach (CaseInfo cinfo in caseInfo)
            {
                if (cinfo.EventName == currEvent.EventName)
                {
                    //string symptom = Convert.ToString(cinfo.Symptom); dont need
                    cases.Add(cinfo);
                }
            }
            caseList.ItemsSource = cases;            
            
        }

        private async void AddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DataInputPage(nameOfEvent));
        }

        private async void Client_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CaseInfo toEdit = (CaseInfo)sender;
            await Navigation.PushAsync(new DataInputPage(toEdit, true));
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
