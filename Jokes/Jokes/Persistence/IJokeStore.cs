using Jokes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jokes.Persistence
{
    /// <summary>
    /// Provides CRUD Functionality for Joke DB
    /// </summary>
    public interface IJokeStore
    {
        /// <summary>
        /// Gets Jokes List
        /// </summary>
        /// <returns>List of Jokes</returns>
        Task<IEnumerable<Joke>> GetJokesAsync();

        /// <summary>
        /// Gets a joke by Id
        /// </summary>
        /// <param name="id">Joke Id</param>
        /// <returns>The Joke</returns>
        Task<Joke> GetJokeAsync(int id);

        /// <summary>
        /// Adds a Joke
        /// </summary>
        /// <param name="joke">Joke</param>
        /// <returns>Id of the inserted Joke</returns>
        Task<int> AddJokeAsync(Joke joke);

        /// <summary>
        /// Updates a Joke
        /// </summary>
        /// <param name="joke">Joke</param>
        /// <returns></returns>
        Task UpdateJokeAsync(Joke joke);

        /// <summary>
        /// Deletes a Joke
        /// </summary>
        /// <param name="joke">Joke</param>
        /// <returns></returns>
        Task DeleteJokeAsync(Joke joke);
    }
}
