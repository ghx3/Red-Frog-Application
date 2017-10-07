using RedFrogs.Models;
using SQLite.Net;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RedFrogs.Data
{
    public class RedFrogDatabase
    {
        SQLiteConnection conn;

        public RedFrogDatabase()
        {
            conn = DependencyService.Get<IFileHelper>().GetConnection();
            conn.CreateTable<Events>();

        }

        public List<Events> GetAllEvents()
        {
            return conn.Query<Events>("Select * FROM [Events]");
        }

        public List<FeedBack> GetAllFeedBack()
        {
            return conn.Query<FeedBack>("Select * FROM [FeedBack]");
        }

        public List<Symptoms> GetAllSymptoms()
        {
            return conn.Query<Symptoms>("Select * FROM [Symptoms]");
        }

        public Events GetEvent(int id)
        {
            return conn.Table<Events>().Where(i => i.ID == id).FirstOrDefault();
        }

        public FeedBack GetFeedback(int id)
        {
            return conn.Table<FeedBack>().Where(i => i.ID == id).FirstOrDefault();
        }

        public Symptoms getSymptom(int id)
        {
            return conn.Table<Symptoms>().Where(i => i.Id == id).FirstOrDefault();
        }

        public int SaveFeedback(FeedBack item)
        {
            if (item.ID != 0)
            {
                return conn.Update(item);
            }
            else
            {
                return conn.Insert(item);
            }
        }
    }
}
