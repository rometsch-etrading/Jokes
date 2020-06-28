using Jokes.Persistence;
using Jokes.Services;
using Jokes.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jokes.Views
{
    /// <summary>
    /// Code-Behind of JokeOfTheDayPage
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JokeOfTheDayPage : ContentPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public JokeOfTheDayPage()
        {
            InitializeComponent();

            ViewModel = new JokeOfTheDayViewModel(new SQLiteJokeStore(new SQLiteDb()), new PageService());
        }

        // Loads Data on Appearing Event
        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            base.OnAppearing();
        }

        /// <summary>
        /// The JOTD ViewModel
        /// </summary>
        public JokeOfTheDayViewModel ViewModel
        {
            get { return BindingContext as JokeOfTheDayViewModel; }
            set { BindingContext = value; }
        }
    }
}