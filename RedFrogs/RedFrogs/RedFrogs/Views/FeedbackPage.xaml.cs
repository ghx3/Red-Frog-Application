using Plugin.Messaging;
using RedFrogs.Data;
using RedFrogs.Helpers;
using RedFrogs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace RedFrogs.Views
{
    public partial class FeedbackPage : ContentPage
    {
        Events ev;
        FeedBack feedBack;
        AzureService azureService;

        public FeedbackPage(Events toClose)
        {
            InitializeComponent();
            ev = toClose;
            feedBack = new FeedBack();
            azureService = DependencyService.Get<AzureService>();
        }

        private void City_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cityPicker.SelectedIndex != -1)
            {
                feedBack.City = cityPicker.Items[cityPicker.SelectedIndex];
            }
        }

        private void SupportProvided_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(SupportProvPicker.SelectedIndex != -1)
            {
                feedBack.SupportTo = SupportProvPicker.Items[SupportProvPicker.SelectedIndex];
            }
        }

        private void SupportGiven_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SupportGivenPicker.SelectedIndex != -1)
            {
                feedBack.SupportGiven = SupportGivenPicker.Items[SupportGivenPicker.SelectedIndex];
            }
        }        

        public async void ClosedEventClicked(object sender, EventArgs e)
        {
            feedBack.EventName = ev.EventName;
            feedBack.EventDate = eventDate.Date;
            feedBack.ActivationLocation = locationFld.Text;
            feedBack.LeaderName = TeamLeaderNameFld.Text;
            feedBack.VolunteerNumber = HelperClass.HasValue(VolNumber.Text);
            
            feedBack.HoursSpent = HelperClass.HasValue(HoursSpent.Text);
            
            feedBack.PatronNumber = HelperClass.HasValue(PatronsFld.Text);
            
            feedBack.PatronInteractionNum = HelperClass.HasValue(InteractionsFld.Text);
            
            //feedBack.LolliesUsed = lolliesSwitch.IsToggled;
            //feedBack.NumberLollies = HelperClass.HasValue(numLollies.Text);
            //feedBack.IcyPolesUsed = iPolesSwitch.IsToggled;
            //feedBack.NumberIcyPoles = HelperClass.HasValue(numIPoles.Text);
            
            feedBack.PancakesProvided = pancakesSwitch.IsToggled;
            feedBack.NumberPancakes = HelperClass.HasValue(numpancakes.Text);
            
            feedBack.WaterProvided = waterSwitch.IsToggled;
            feedBack.AmountWater = HelperClass.HasValue(numWater.Text);
            
            //feedBack.DonutsProvided = donutSwitch.IsToggled;
            //feedBack.NumberDonuts = HelperClass.HasValue(numDonuts.Text);
            
            feedBack.AnyGiveaways = giveAwaySwitch.IsToggled;
            feedBack.GivenAway = giveAway.Text;
            feedBack.AnyPraiseReports = praiseSwitch.IsToggled;
            feedBack.PraiseReport = praise.Text;
            feedBack.AnyIncidents = incidentSwitch.IsToggled;
            feedBack.IncidentDescription = incident.Text;
            feedBack.FollowUpNeeded = followUpSwitch.IsToggled;
            feedBack.FollowUpName = followUp.Text;
            feedBack.EventID = ev.Id;

            await azureService.AddFeedback(feedBack);
            await azureService.UpdateEvent(ev);
            SendEmail();
            await Navigation.PopAsync();
        }

        public void SendEmail()
        {
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            if (emailMessenger.CanSendEmail)
            {
                var email = new EmailMessageBuilder()
                .To("chris.seq17@gmail.com")
                .Subject("Feedback")
                .Body(EmailBody())
                .Build();

                emailMessenger.SendEmail(email);
            }
        }

        public string EmailBody()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Event name: " + feedBack.EventName + "\n");
            builder.Append("City: " + feedBack.City + "\n");
            builder.Append("Date: " + feedBack.EventDate.ToString() + "\n");
            builder.Append("Support Provided to: " + feedBack.SupportTo + "\n");
            builder.Append("Support Given: " + feedBack.SupportGiven + "\n");
            builder.Append("Location of Activation: " + feedBack.ActivationLocation + "\n");
            builder.Append("Team Leader: " + feedBack.LeaderName + "\n");
            builder.Append("Volunteer Number: " + feedBack.VolunteerNumber + "\n");
            builder.Append("Hours Spent: " + feedBack.HoursSpent.ToString() + "\n");
            builder.Append("Amount of Patrons at Activation: " + feedBack.PatronNumber.ToString() + "\n");
            builder.Append("Patron Interaction Number: " + feedBack.PatronInteractionNum.ToString() + "\n");
            builder.Append("Lollies Used: " + feedBack.LolliesUsed.ToString() + "\n");
            builder.Append("Lollies Number: " + feedBack.NumberLollies.ToString() + "\n");
            builder.Append("Icy Poles Used: " + feedBack.IcyPolesUsed.ToString() + "\n");
            builder.Append("Icy Poles Number: " + feedBack.NumberIcyPoles.ToString() + "\n");
            builder.Append("Pancakes Provided: " + feedBack.PancakesProvided.ToString() + "\n");
            builder.Append("Pancakes Number: " + feedBack.NumberPancakes.ToString() + "\n");
            builder.Append("Water Provided: " + feedBack.WaterProvided.ToString() + "\n");
            builder.Append("Amount of Water: " + feedBack.AmountWater.ToString() + "\n");
            builder.Append("Donuts Provided: " + feedBack.DonutsProvided.ToString() + "\n");
            builder.Append("Donuts Number: " + feedBack.NumberDonuts.ToString() + "\n");
            builder.Append("Any Other Givaways: " + feedBack.AnyGiveaways.ToString() + "\n");
            builder.Append("Description: " + feedBack.GivenAway + "\n\n");
            builder.Append("Any Prasie Reports: " + feedBack.AnyPraiseReports.ToString() + "\n");
            builder.Append("Description: " + feedBack.PraiseReport + "\n\n");
            builder.Append("Any Incidents: " + feedBack.AnyIncidents.ToString() + "\n");
            builder.Append("Description: " + feedBack.IncidentDescription + "\n\n");
            builder.Append("Team Member Follow up needed: " + feedBack.FollowUpNeeded.ToString() + "\n");
            builder.Append("Name: " + feedBack.FollowUpName + "\n\n");

            return builder.ToString();
        }


    }
}
