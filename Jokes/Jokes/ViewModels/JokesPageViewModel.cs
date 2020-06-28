using Jokes.Models;
using Jokes.Persistence;
using Jokes.Services;
using Jokes.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Jokes.ViewModels
{
    /// <summary>
    /// Jokes Page ViewModel
    /// </summary>
    public class JokesPageViewModel : BaseViewModel
    {
        private IJokeStore _jokeStore;
        private IPageService _pageService;

        private bool _isDataLoaded;

        /// <summary>
        /// LoadData Command
        /// </summary>
        public ICommand LoadDataCommand { get; private set; }

        /// <summary>
        /// AddJoke Command
        /// </summary>
        public ICommand AddJokeCommand { get; private set; }

        /// <summary>
        /// SelectJoke Command
        /// </summary>
        public ICommand SelectJokeCommand { get; private set; }

        /// <summary>
        /// DeleteJoke Command
        /// </summary>
        public ICommand DeleteJokeCommand { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jokeStore">The JokeStore</param>
        /// <param name="pageService">The PageService</param>
        public JokesPageViewModel(IJokeStore jokeStore, IPageService pageService)
        {
            _jokeStore = jokeStore;
            _pageService = pageService;

            // Set up commands
            LoadDataCommand = new Command(async () => await LoadData());
            AddJokeCommand = new Command(async () => await AddJoke());
            SelectJokeCommand = new Command<JokeViewModel>(async c => await SelectJoke(c));
            DeleteJokeCommand = new Command<JokeViewModel>(async c => await DeleteJoke(c));

            // Subscribe to events
            MessagingCenter.Subscribe<JokeDetailViewModel, Joke>(this, Events.JokeAdded, OnJokeAdded);
            MessagingCenter.Subscribe<JokeDetailViewModel, Joke>(this, Events.JokeUpdated, OnJokeUpdated);
            MessagingCenter.Subscribe<JokeOfTheDayViewModel, Joke>(this, Events.JokeAdded, OnJOTDJokeAdded);
        }

        /// <summary>
        /// Jokes List
        /// </summary>
        public ObservableCollection<JokeViewModel> Jokes { get; private set; }
            = new ObservableCollection<JokeViewModel>();

        private JokeViewModel _selectedJoke;

        /// <summary>
        /// Selected Joke
        /// </summary>
        public JokeViewModel SelectedJoke
        {
            get { return _selectedJoke; }
            set { SetValue(ref _selectedJoke, value); }
        }

        // Load jokes from db
        private async Task LoadData()
        {
            if (_isDataLoaded)
                return;

            _isDataLoaded = true;
            var jokes = await _jokeStore.GetJokesAsync();
            foreach (var joke in jokes)
                Jokes.Add(new JokeViewModel(joke));
        }

        // Navigate to detail page with empty joke object
        private async Task AddJoke()
        {
            await _pageService.PushAsync(new JokeDetailPage(new JokeViewModel()));
        }

        // Navigate to detail page with existing joke object
        private async Task SelectJoke(JokeViewModel joke)
        {
            if (joke == null)
                return;

            SelectedJoke = null;
            await _pageService.PushAsync(new JokeDetailPage(joke));
        }

        // Delete selected joke
        private async Task DeleteJoke(JokeViewModel jokeViewModel)
        {
            if (await _pageService.DisplayConfirm("Warning", $"Are you sure?", "Yes", "No"))
            {
                Jokes.Remove(jokeViewModel);

                var joke = await _jokeStore.GetJokeAsync(jokeViewModel.Id);
                await _jokeStore.DeleteJokeAsync(joke);
            }
        }

        // Add new joke to list
        private void OnJokeAdded(JokeDetailViewModel viewModel, Joke joke)
        {
            Jokes.Add(new JokeViewModel(joke));
            SortJokesDesc();
        }

        // Update joke in list
        private void OnJokeUpdated(JokeDetailViewModel viewModel, Joke joke)
        {
            var updatedJoke = Jokes.Single(j => j.Id == joke.Id);

            updatedJoke.Text = joke.Text;
        }

        // Add new JokeOfTheDay to list
        private void OnJOTDJokeAdded(JokeOfTheDayViewModel viewModel, Joke joke)
        {
            Jokes.Add(new JokeViewModel(joke));
            SortJokesDesc();
        }

        // Re-sort the Collection
        private void SortJokesDesc()
        {
            Jokes = new ObservableCollection<JokeViewModel>(Jokes.OrderByDescending(i => i.Date));
            RaisePropertyChanged("Jokes");
        }
    }
}
