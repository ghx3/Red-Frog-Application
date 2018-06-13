using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedFrogs.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPage : ContentPage
    {
        public ResetPage()
        {
            InitializeComponent();

        }
        //validate email address
        private void Submit_Clicked(object sender, System.EventArgs e)
        {
            string email = EmailAddress.Text;
            string emailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                            + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

           
            if (!String.IsNullOrEmpty(email))
            {
                if (!Regex.IsMatch(email, emailPattern))
                {
                    DisplayAlert("Oops", "The Email address is invalid, please enter a valid Email address.", "OK");

                }
                else
                {
                    DisplayAlert("Great", "A Email has been sent to the assigned Email address" +
                                                    "\nPlease follow the instruction and reset your passwor", "OK");
                }
            }else{
                DisplayAlert("Sorry", "This field cannot be empty,Please enter the valid Email address", "OK");
            }

          
        }
    }
}