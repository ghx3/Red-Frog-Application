using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace RedFrogs.Views
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }
        //validate the account information
        private void CreateAccount(object sender, System.EventArgs e)
        {
            string username = accountName.Text;
            string pswd = accountPassword.Text;
            string namePattern = "^[a-zA-Z][a-zA-Z0-9]{5,11}$";
            string pswdPattern = "^[a-zA-Z][a-zA-Z0-9]{5,11}$";

            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(pswd))
            {
                if (!Regex.IsMatch(username, namePattern))
                {
                    DisplayAlert("Oops", "username input is invalid, please check the enter information", "OK");
                }else if (!Regex.IsMatch(pswd, pswdPattern)){
                    DisplayAlert("Oops", "password input is invalid, please check the enter information", "OK");
                }
                else
                {
                    DisplayAlert("Congraduations", "New account has been created", "OK");
                }
            }
            else
            {
                DisplayAlert("Sorry", "Both username and password field cannot be empty, please enter the valid value", "OK");
            }



        }
    }
}
