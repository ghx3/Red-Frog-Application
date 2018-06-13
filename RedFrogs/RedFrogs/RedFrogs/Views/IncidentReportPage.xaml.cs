using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;



namespace RedFrogs.Views
{
    public partial class IncidentReportPage : ContentPage
    {
        public IncidentReportPage()
        {
            InitializeComponent();
        }
        //Validate incident report input
        public void CreateClicked(object sender, EventArgs e)
        {
            string story = storyEditor.Text;
            string type = IncidentEditor.Text;
            string name = NameEditor.Text;
            string detail = detailEditor.Text;
            string action = actionEditor.Text;
             

            if (!String.IsNullOrEmpty(story) && !String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(name)
               && !String.IsNullOrEmpty(detail) && !String.IsNullOrEmpty(action))
            {
                if ((followUp.IsToggled == true) && (activeIncident.IsToggled == true) && (MoreIncident.IsToggled == true))
                {
                    DisplayAlert("Congraduations", "New incident report has been created", "OK");
                }
               

            }else if ((followUp.IsToggled == false) || (activeIncident.IsToggled == false) || (MoreIncident.IsToggled == false)
                      ||String.IsNullOrEmpty(story) || String.IsNullOrEmpty(type) ||String.IsNullOrEmpty(name)
               || String.IsNullOrEmpty(detail) || String.IsNullOrEmpty(action)){
                DisplayAlert("Oops", "You missing some parts did not fill in, please enter the necessary information", "OK");
            }
           
        }
    }
}
