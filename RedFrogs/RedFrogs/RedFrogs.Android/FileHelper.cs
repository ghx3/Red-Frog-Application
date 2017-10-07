using System.IO;
using Xamarin.Forms;
using RedFrogs.Droid;
using SQLite.Net;

[assembly: Dependency(typeof(FileHelper))]
namespace RedFrogs.Droid
{
    public class FileHelper : IFileHelper
    {
         
        public SQLiteConnection GetConnection()
        {
            var dbName = "redfrogs.db";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, dbName);

            // copy prepopulated db
            if (!File.Exists(path))
            {
                var s = Forms.Context.Resources.OpenRawResource(Resource.Raw.redfrogs);
                // create a write stream
                FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                // write to the stream
                ReadWriteStream(s, writeStream);
            }

            var plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var conn = new SQLiteConnection(plat, path);

            return conn;
        }
    
        // Helper method to get db out of Raw folder and into filesystem
        void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            byte[] buffer = new byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the bytes
            while(bytesRead != 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }
    }
}