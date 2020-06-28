using Xamarin.Forms;
using Jokes.Views;

namespace Jokes
{
    /// <summary>
    /// App
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new JokeOfTheDayPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
