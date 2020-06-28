using Jokes.Persistence;
using Jokes.Services;
using Jokes.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jokes.Views
{
    /// <summary>
    /// Code-Behind of Joke Detail Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JokeDetailPage : ContentPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="viewModel">The JokeViewModel</param>
        public JokeDetailPage(JokeViewModel viewModel)
        {
            InitializeComponent();

            var jokeStore = new SQLiteJokeStore(new SQLiteDb());
            var pageService = new PageService();

            // Set Title based on Text Content
            Title = (viewModel.Text == null) ? "New Joke" : "Edit Joke";

            BindingContext = new JokeDetailViewModel(viewModel ?? new JokeViewModel(), jokeStore, pageService);
        }
    }
}