using RedFrogs.iOS;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace RedFrogs.iOS
{
    public class FileHelper : IFileHelper
    {
        public FileHelper() { }

        public SQLiteAsyncConnection GetConnection()
        {
            var dbFilename = "RedFrogs.db";
            string documentsPath = Environment.GetFolderPath
                (Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, dbFilename);

            // Copy in the prepopulated database
            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                File.Copy(dbFilename, path);
            }
            
            var conn = new SQLiteAsyncConnection(path);

            // Return the database connection 
            return conn;            
        }
    }
}