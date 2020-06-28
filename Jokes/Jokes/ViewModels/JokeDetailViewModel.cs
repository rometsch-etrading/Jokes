using Jokes.Models;
using Jokes.Persistence;
using Jokes.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Jokes.ViewModels
{
    /// <summary>
    /// Joke Detail ViewModel
    /// </summary>
    public class JokeDetailViewModel
    {
        private readonly IJokeStore _jokeStore;
        private readonly IPageService _pageService;
        public Joke Joke { get; private set; }
        public ICommand SaveCommand { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="viewModel">The JokeViewModel</param>
        /// <param name="jokeStore">The JokeStore</param>
        /// <param name="pageService">The Page Service</param>
        public JokeDetailViewModel(JokeViewModel viewModel, IJokeStore jokeStore, IPageService pageService)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            _pageService = pageService;
            _jokeStore = jokeStore;

            // Define commands
            SaveCommand = new Command(async () => await Save());

            Joke = new Joke
            {
                Id = viewModel.Id,
                Text = viewModel.Text,
                Date = viewModel.Date
            };
        }

        /// <summary>
        /// Save A Joke
        /// </summary>
        /// <returns></returns>
        async Task Save()
        {
            // Display error message if joke is empty
            if(String.IsNullOrWhiteSpace(Joke.Text))
            {
                await _pageService.DisplayAlert("Error", "Please enter the Joke", "OK");
                return;
            }

            // New joke
            if(Joke.Id == 0)
            {
                // Set date
                Joke.Date = DateTime.Now;
                // Add joke to db
                await _jokeStore.AddJokeAsync(Joke);
                // Send JokeAdded message
                MessagingCenter.Send(this, Events.JokeAdded, Joke);
            }
            else
            {
                // Update joke
                await _jokeStore.UpdateJokeAsync(Joke);
                // Send JokeUpdated message
                MessagingCenter.Send(this, Events.JokeUpdated, Joke);
            }

            // Go back to previous page
            await _pageService.PopAsync();
        }
    }
}
