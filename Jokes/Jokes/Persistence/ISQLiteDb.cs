using SQLite;

namespace Jokes.Persistence
{
    /// <summary>
    /// Connection to SQLite DB
    /// </summary>
    public interface ISQLiteDb
    {
        /// <summary>
        /// Gets a SQLite Connection
        /// </summary>
        /// <returns>SQLite COnnection</returns>
        SQLiteAsyncConnection GetConnection();
    }
}
