using SQLite;

namespace RedFrogs
{
    public interface IFileHelper
    {
        SQLiteAsyncConnection GetConnection();
    }
}
