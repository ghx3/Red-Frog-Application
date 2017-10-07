using RedFrogs.iOS;
using System;
using System.IO;
using Xamarin.Forms;
using SQLite.Net;

[assembly: Dependency(typeof(FileHelper))]
namespace RedFrogs.iOS
{
    public class FileHelper : IFileHelper
    {
        public SQLiteConnection GetConnection()
        {
            var dbName = "redfrogs.db";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, dbName);

            // copy in prepoulated db
            if (!File.Exists(path))
            {
                File.Copy(dbName, path);
            }

            var plat = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
            var conn = new SQLiteConnection(plat, path);

            return conn;

        }

    }
}