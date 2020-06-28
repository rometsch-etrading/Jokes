namespace Jokes.Models
{
    /// <summary>
    /// Joke of the Day Webservice Result
    /// </summary>
    public class JokeOfTheDayWSResult
    {
        /// <summary>
        /// Id of the Joke
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Joke Text 
        /// </summary>
        public string joke { get; set; }

        /// <summary>
        /// Response Status 
        /// </summary>
        public int status { get; set; }
    }
}
