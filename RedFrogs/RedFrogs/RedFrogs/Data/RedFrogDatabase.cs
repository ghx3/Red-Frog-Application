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
            
        }

        public List<Events> GetAllEvents()
        {
            return conn.Query<Events>("Select * FROM [Events]");
        }

        public Events GetEvent(string id)
        {
            return conn.Table<Events>().Where(i => i.ID == id).FirstOrDefault();
        }

        public void SaveAllEvents(List<Events> toSave)
        {
            foreach(Events e in toSave)
            {
                conn.Insert(e);
            }
        }

        public void DeleteAllEvents()
        {
            conn.Query<Events>("DELETE FROM [Events]");
        }

        public List<FeedBack> GetAllFeedBack()
        {
            return conn.Query<FeedBack>("Select * FROM [FeedBack]");
        }

        public List<Symptoms> GetAllSymptoms()
        {
            return conn.Query<Symptoms>("Select * FROM [Symptoms]");
        }       

        public FeedBack GetFeedback(int id)
        {
            return conn.Table<FeedBack>().Where(i => i.ID == id).FirstOrDefault();
        }

        public Symptoms getSymptom(int id)
        {
            return conn.Table<Symptoms>().Where(i => i.Id == id).FirstOrDefault();
        }

        public int SaveCaseInfo(CaseInfo item)
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
