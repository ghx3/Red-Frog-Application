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
    public partial class ResetPage : ContentPage
    {
        public ResetPage()
        {
            InitializeComponent();

        }
        void Submit_Clicked(object sender, System.EventArgs e)
        {
            var email = EmailAddress.Text;
            var emailPattern = "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$";
            if (!String.IsNullOrWhiteSpace(email) && !Regex.IsMatch(email, emailPattern))
            {
                DisplayAlert("Oops", "The Email address is invalid, please enter a valid Email address.", "OK");

            }
            else
            {
                DisplayAlert("Great", "A Email has been sent to the assigned Email address" +
                                                "\nPlease follow the instruction and reset your passwor", "OK");
            }

        }
    }
}