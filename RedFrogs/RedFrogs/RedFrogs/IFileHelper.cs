using SQLite.Net;

namespace RedFrogs
{
    public interface IFileHelper
    {
        SQLiteConnection GetConnection();
    }
}
