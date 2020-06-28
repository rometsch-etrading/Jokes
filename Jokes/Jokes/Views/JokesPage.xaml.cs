using Jokes.Persistence;
using Jokes.Services;
using Jokes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jokes.Views
{
    /// <summary>
    /// Code-Behind of JokesPage
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JokesPage : ContentPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public JokesPage()
        {
            ViewModel = new JokesPageViewModel(new SQLiteJokeStore(new SQLiteDb()), new PageService());
            InitializeComponent();
        }

        // Loads Data on Appearing Event
        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            base.OnAppearing();
        }

        void OnJokeSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectJokeCommand.Execute(e.SelectedItem);
        }

        /// <summary>
        /// The Jokes Page ViewModel
        /// </summary>
        public JokesPageViewModel ViewModel
        {
            get { return BindingContext as JokesPageViewModel; }
            set { BindingContext = value; }
        }
    }
}