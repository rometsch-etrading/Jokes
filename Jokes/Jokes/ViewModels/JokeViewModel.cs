using Jokes.Models;
using System;

namespace Jokes.ViewModels
{
    /// <summary>
    /// Joke ViewModel
    /// </summary>
    public class JokeViewModel :BaseViewModel
    {
        /// <summary>
        /// Joke Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public JokeViewModel() { }

        /// <summary>
        /// Joke ViewModel
        /// </summary>
        /// <param name="joke">Joke Model</param>
        public JokeViewModel(Joke joke)
        {
            Id = joke.Id;
            _text = joke.Text;
            _date = joke.Date;
        }

        private string _text;

        /// <summary>
        /// Joke Text
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                SetValue(ref _text, value);
            }
        }

        private DateTime _date;

        /// <summary>
        /// Joke Creation Date
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set
            {
                SetValue(ref _date, value);
            }
        }

        /// <summary>
        /// Joke Model
        /// </summary>
        public Joke Joke
        {
            get
            {
                return new Joke
                {
                    Id = this.Id,
                    Text = _text,
                    Date = _date
                };
            }
        }
    }
}
