using Jokes.Models;
using Jokes.Persistence;
using Jokes.Services;
using Jokes.Views;
using Jokes.Webservices;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Jokes.ViewModels
{
    /// <summary>
    /// Joke of the Day ViewModel
    /// </summary>
    public class JokeOfTheDayViewModel : BaseViewModel
    {
        private IPageService _pageService;
        private IJokeStore _jokeStore;

        /// <summary>
        /// Navigate Command
        /// </summary>
        public ICommand NavigateCommand { get; private set; }

        /// <summary>
        /// LoadData Command
        /// </summary>
        public ICommand LoadDataCommand { get; private set; }

        /// <summary>
        /// Add Command
        /// </summary>
        public ICommand AddCommand { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jokeStore">The Joke Store</param>
        /// <param name="pageService">The Page Service</param>
        public JokeOfTheDayViewModel(IJokeStore jokeStore, IPageService pageService)
        {
            _pageService = pageService;
            _jokeStore = jokeStore;

            DisplayedJoke = new JokeViewModel();

            NavigateCommand = new Command(async () => await _pageService.PushAsync(new JokesPage()));
            LoadDataCommand = new Command(async () => await LoadData());
            AddCommand = new Command(async () => await Add());
        }

        private JokeViewModel _displayedJoke;

        /// <summary>
        /// Displayed Joke
        /// </summary>
        public JokeViewModel DisplayedJoke
        {
            get { return _displayedJoke; }
            set { SetValue(ref _displayedJoke, value); }
        }

        /// <summary>
        /// Loads Joke from Webservice
        /// </summary>
        /// <returns></returns>
        private async Task LoadData()
        {
            var wsJoke = await GetWSJokeOfTheDay();

            DisplayedJoke = new JokeViewModel
            {
                Id = 0,
                Text = wsJoke.joke,
                Date = DateTime.Now
            };
        }

        private async Task<JokeOfTheDayWSResult> GetWSJokeOfTheDay()
        {
            WSClient client = new WSClient();
            try
            {
               return await client.Get<JokeOfTheDayWSResult>("https://icanhazdadjoke.com/");
            }
            catch (Exception)
            {
                await _pageService.DisplayAlert("Error", "Couldn't reach joke API - Is your internet up?", "OK");
                return new JokeOfTheDayWSResult();
            }
        }

        private async Task Add()
        {
            var joke = DisplayedJoke.Joke;
            // Add joke to db
            await _jokeStore.AddJokeAsync(joke);
            // Send JokeAdded message
            MessagingCenter.Send(this, Events.JokeAdded, joke);

            // Go to List page
            await _pageService.PushAsync(new JokesPage());
        }

    }
}
