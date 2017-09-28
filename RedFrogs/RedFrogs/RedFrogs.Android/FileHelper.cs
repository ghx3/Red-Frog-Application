using System;
using System.IO;
using Xamarin.Forms;
using RedFrogs.Droid;

[assembly: Dependency(typeof(FileHelper))]
namespace RedFrogs.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.combine(path, filename);
        }
    }
}