using SQLite;
using System;
using System.IO;

namespace Jokes.Persistence
{
    /// <summary>
    /// Connection to SQLite DB
    /// </summary>
    public class SQLiteDb : ISQLiteDb
    {
        /// <summary>
        /// Gets a SQLite Connection
        /// </summary>
        /// <returns>SQLite COnnection</returns>
        public SQLiteAsyncConnection GetConnection()
        {
            return new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "JokeDb.db3"));
        }
    }
}
