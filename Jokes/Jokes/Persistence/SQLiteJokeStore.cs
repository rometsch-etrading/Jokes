using Jokes.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jokes.Persistence
{
    /// <summary>
    /// Provides CRUD Functionality for Joke DB
    /// </summary>
    public class SQLiteJokeStore : IJokeStore
    {
        private SQLiteAsyncConnection _connection;

        /// <summary>
        /// Constructor
        /// Gets a DB Connection and creates the Joke table, if not already created
        /// </summary>
        /// <param name="db"></param>
        public SQLiteJokeStore(ISQLiteDb db)
        {
            _connection = db.GetConnection();
            _connection.CreateTableAsync<Joke>().Wait();
        }

        /// <summary>
        /// Adds a Joke
        /// </summary>
        /// <param name="joke">Joke</param>
        /// <returns></returns>
        public async Task<int> AddJokeAsync(Joke joke)
        {
            return await _connection.InsertAsync(joke);
        }

        /// <summary>
        /// Deletes a Joke
        /// </summary>
        /// <param name="joke">Joke</param>
        /// <returns></returns>
        public async Task DeleteJokeAsync(Joke joke)
        {
            await _connection.DeleteAsync(joke);
        }

        /// <summary>
        /// Gets a joke by Id
        /// </summary>
        /// <param name="id">Joke Id</param>
        /// <returns>The Joke</returns>
        public async Task<Joke> GetJokeAsync(int id)
        {
            return await _connection.FindAsync<Joke>(id);
        }

        /// <summary>
        /// Gets Jokes List
        /// </summary>
        /// <returns>List of Jokes</returns>
        public async Task<IEnumerable<Joke>> GetJokesAsync()
        {
            return await _connection.Table<Joke>().OrderByDescending(j => j.Date).ToListAsync();
        }

        /// <summary>
        /// Updates a Joke
        /// </summary>
        /// <param name="joke">Joke</param>
        /// <returns></returns>
        public async Task UpdateJokeAsync(Joke joke)
        {
            await _connection.UpdateAsync(joke);
        }
    }
}
