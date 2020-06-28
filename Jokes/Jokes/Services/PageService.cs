using System.Threading.Tasks;
using Xamarin.Forms;

namespace Jokes.Services
{
    /// <summary>
    /// Provides Pop-Ups and Navigation
    /// </summary>
    public class PageService : IPageService
    {
        /// <summary>
        /// Displays Alert Popup
        /// </summary>
        /// <param name="title">Alert Title</param>
        /// <param name="message">Alert Message</param>
        /// <param name="ok">OK-Button Text</param>
        /// <returns></returns>
        public async Task DisplayAlert(string title, string message, string ok)
        {
            await MainPage.DisplayAlert(title, message, ok);
        }

        /// <summary>
        /// Displays Confirmation Popup
        /// </summary>
        /// <param name="title">Confirmation Title</param>
        /// <param name="message">Confirmation Message</param>
        /// <param name="ok">OK-Button Text</param>
        /// <param name="cancel">Cancel-Button Text</param>
        /// <returns></returns>
        public async Task<bool> DisplayConfirm(string title, string message, string ok, string cancel)
        {
            return await MainPage.DisplayAlert(title, message, ok, cancel);
        }

        /// <summary>
        /// Pops the current Page from Navigation Stack
        /// </summary>
        /// <returns>Page</returns>
        public async Task<Page> PopAsync()
        {
            return await MainPage.Navigation.PopAsync();
        }
        
        /// <summary>
        /// Pushes a new Page to the Navigation Stack
        /// </summary>
        /// <param name="page">New Page</param>
        /// <returns>Page</returns>
        public async Task PushAsync(Page page)
        {
            await MainPage.Navigation.PushAsync(page);
        }

        private Page MainPage
        {
            get { return Application.Current.MainPage; }
        }
    }
}
