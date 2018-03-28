using RedFrogs.Data;
using RedFrogs.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace RedFrogs
{
    public partial class App : Application
    {
        static RedFrogDatabase db;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new FeedbackPage());
            MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#bf3336"));
            MainPage.SetValue(NavigationPage.BarTextColorProperty, Color.White);
        }

        public static RedFrogDatabase access
        {
            get {
                if(db == null)
                {
                    db = new RedFrogDatabase();
                }

                return db;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
