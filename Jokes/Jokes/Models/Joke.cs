using SQLite;
using System;

namespace Jokes.Models
{
    /// <summary>
    /// Joke Model
    /// </summary>
    public class Joke
    {
        /// <summary>
        /// Id of the Joke
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Text of the Joke
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Creation Date of the Joke
        /// </summary>
        public DateTime Date { get; set; }
    }
}
