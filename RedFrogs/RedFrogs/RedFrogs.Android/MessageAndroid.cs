using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RedFrogs.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace RedFrogs.Droid
{
    public class MessageAndroid : IMessage
    {        
        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }

        void IMessage.LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }
    }
}