using Firebase.Xamarin.Database;
using Plugin.Connectivity;
using RedFrogs.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RedFrogs.Data
{
    public class RedFrogFirebaseDB
    {
        FirebaseClient client;

        public RedFrogFirebaseDB()
        {
            
            client = new FirebaseClient("https://rdrf-1a167.firebaseio.com/");
        }

        public async Task<List<Events>> getEvents()
        {
            List<Events> data = new List<Events>();

            if (CrossConnectivity.Current.IsConnected) {
              data = (await client.Child("Events")
              .OnceAsync<Events>())
              .Select(item =>
              {
                  return new Events
                  {
                      ID = item.Key,
                      EventName = item.Object.EventName,
                      NumInteractions = item.Object.NumInteractions,
                      EndDate = item.Object.EndDate,
                      IsClosed = item.Object.IsClosed

                  };
              }).ToList();
                // save only open events
                data.RemoveAll(x => x.IsClosed == 1);

                App.DB.DeleteAllEvents();
                await App.DB.SaveAllEvents(data);
                DependencyService.Get<IMessage>().ShortAlert("Events saved locally");
                
            } else
            {
                data = await App.DB.GetAllEvents();
                DependencyService.Get<IMessage>().ShortAlert("Events loaded from local database");
            }
            return data;
        }

        public async void saveEvent(Events toSend)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var send = await client
                .Child("Events")
                .PostAsync(toSend);
            } else
            {
                DependencyService.Get<IMessage>().ShortAlert("Internet Connection required to create new event");
            }
                
        }
    }
}
