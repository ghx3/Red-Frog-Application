using System;
using System.IO;
using RedFrogs.Droid;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace RedFrogs.Droid
{
    public class FileHelper : IFileHelper
    {
        public FileHelper() { }

        public SQLiteAsyncConnection GetConnection()
        {
            var dbFilename = "redfrogs.db";
            string docPath = Environment.GetFolderPath
                (Environment.SpecialFolder.Personal);   // Documents folder
            var path = Path.Combine(docPath, dbFilename);

            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                var s = Android.App.Application.Context.Resources.OpenRawResource(Resource.Raw.redfrogs);  // RESOURCE NAME ###

                // create a write stream
                FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                // write to the stream
                ReadWriteStream(s, writeStream);
            }

            var conn = new SQLiteAsyncConnection(path);

            return conn;
        }

        // method to get db out of Raw folder and into filesystem
        void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
			{
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }
    }


}