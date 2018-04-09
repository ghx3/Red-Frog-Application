using System;
using System.IO;
using RedFrogs.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace RedFrogs.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string path)
        {
            string p = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(p, path);
        }
    }
}